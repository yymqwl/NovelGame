using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using GameFramework;

namespace GameMain.Net
{
    public class KcpPeer : PackPeer
    {
        Socket m_socket;

        public Socket Socket
        {
            get
            {
                return m_socket;
            }
            set
            {
                m_socket = value;
            }
        }
        IPEndPoint m_RemoteEndPoint;
        private bool m_running;
        KCP m_kcp;

        byte[] m_receiveBuffer = new byte[NetConstants.SocketBufferSize];
        byte[] m_kcp_receiveBuffer = new byte[NetConstants.SocketBufferSize];

        SwitchQueue<byte[]> m_switch_queue = new SwitchQueue<byte[]>(NetConstants.SwitchQueueLen);

        private UInt32 m_NextUpdateTime = 0;//

        Action<byte[]> m_Act_Receive;

        public Action<byte[]> Act_Act_Receive
        {
            get
            {
                return m_Act_Receive;
            }
            set
            {
                m_Act_Receive = value;
            }
        }

        public override bool Init()
        {
            bool pret = base.Init();
            m_running = false;

            return pret;
        }

        public void Process_Recv_Queue()
        {
            m_switch_queue.Switch();
            while (!m_switch_queue.Empty())
            {
                var tmpbuf = m_switch_queue.Pop();
                KcpInput(tmpbuf);

            }
        }

        public int KcpInput(byte[] data)
        {
            return m_kcp.Input(data);
        }
        private  int SendTo(byte[] data, int offset, int size)
        {
            return SendTo(data, offset, size, m_RemoteEndPoint);
        }
        public int KcpSend(byte[] data)
        {
            return m_kcp.Send(data);
        }
        public int SendTo(byte[] data, int offset, int size, IPEndPoint remoteEndPoint)
        {
            try
            {
                int result = 0;

                if (!m_socket.Poll(NetConstants.SocketSendPollTime , SelectMode.SelectWrite))
                    return -1;
                result = m_socket.SendTo(data, offset, size, SocketFlags.None, remoteEndPoint);

                return result;
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode != SocketError.MessageSize)
                {
                    DebugHandler.LogDebug("[S]" + ex);
                }
                else
                {
                    DebugHandler.LogDebug(ex.ToString());
                }
                return -1;
            }
        }

        /// <summary>
        /// 接受消息包
        /// </summary>
        private void ReceiveLogic()
        {
            if (!m_running)//停止
            {
                return ;
            }
            if (!Socket.Poll(NetConstants.SocketReceivePollTime, SelectMode.SelectRead))
            {
                return ;
            }
            EndPoint bufferEndPoint = new IPEndPoint(IPAddress.Any, 0);
            int result;
            try
            {
                result = Socket.ReceiveFrom(m_receiveBuffer, 0, m_receiveBuffer.Length, SocketFlags.None, ref bufferEndPoint);
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.ConnectionReset ||
                    ex.SocketErrorCode == SocketError.MessageSize)
                {
                    DebugHandler.Log( string.Format("[R] Ingored error: +{0} - {1}", (int)ex.SocketErrorCode, ex.ToString()));
                }
                DebugHandler.Log(string.Format("Error code: {0} - {1}", (int)ex.SocketErrorCode, ex.ToString()));
                return ;
            }
            if (result > 0)
            {
                Push(m_receiveBuffer.CloneRange(0, result));
            }

        }
        public bool Bind(IPEndPoint ep)
        {
            bool pret = Init();


            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            Socket.Blocking = false;
            Socket.ReceiveBufferSize = NetConstants.SocketBufferSize;
            Socket.SendBufferSize = NetConstants.SocketBufferSize;
            Socket.DontFragment = true;


            if (!BindSocket(Socket, ep))
            {
                return false;
            }
            m_running = true;
            //////////////////////////////////////////////////////////////////////////
            m_kcp = new KCP(NetConstants.Kcp_Con_V,
                (byte[] buf, int size) =>
                {
                    //DebugHandler.Log("buf+"+ size);
                    SendTo(buf.CloneRange(0, size), 0, size);
                });
            m_kcp.NoDelay(1, 10, 2, 1);
            m_kcp.WndSize(NetConstants.PacketSizeLimit, NetConstants.PacketSizeLimit);
            m_kcp.SetMtu(NetConstants.Kcp_Mtu);

            return pret;
        }

        public override void Close()
        {
            base.Close();
            m_running = false;
            if (Socket!=null)
            {
                Socket.Close();
                Socket = null;
            }
        }
        public void Connect(IPEndPoint rept)
        {
            m_RemoteEndPoint = rept;
            Socket.Connect(rept);
        }
        public uint KcpCheck(uint iclock)
        {
            return m_kcp.Check(iclock);
        }
        public void KcpUpdate(uint iclock)
        {

            //////////////////////////////////////////////////////////////////////////
            ReceiveLogic();//没有起线程
            Process_Recv_Queue();
            KcpReceive();
            //////////////////////////////////////////////////////////////////////////
            if (iclock>= m_NextUpdateTime)
            {
                m_kcp.Update(iclock);
                m_NextUpdateTime = KcpCheck(iclock);

            }
        }
        public void KcpReceive()
        {
            while (true)
            {
                int rv_len = m_kcp.Recv(m_kcp_receiveBuffer);
                if (rv_len<=0)
                {
                    break;
                }
                else
                {
                    Act_Act_Receive.InvokeGracefully(m_kcp_receiveBuffer.CloneRange(0, rv_len));
                    //////////////////////////////////////////////////////////////////////////

                }
            }
        }
        private bool BindSocket(Socket socket, IPEndPoint ep)
        {
            try
            {
                socket.Bind(ep);

            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.AddressFamilyNotSupported)
                {
                    return true;
                }
                return false;
            }
            return true;
        }
        public void Update(float elapseSeconds, float realElapseSeconds)
        {
            if (!m_running)
            {
                return;
            }
            var ick = NetConstants.IClock();
            KcpUpdate(ick);

        }
        public void Push(byte[] bufs)
        {
            m_switch_queue.Push(bufs);
        }


        public override IPEndPoint Get_RemoteEndPoint()
        {
            return m_RemoteEndPoint;
        }
    }
}
