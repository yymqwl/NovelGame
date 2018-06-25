using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using GameFramework;

namespace GameMain.Async
{
    public class BackgroundTask : YieldInstruction
    {
    };

    public class ForegroundTask : YieldInstruction
    {
    };
    
	/// <summary>
	/// Magic thread allow a coroutine to go into the background and return to foreground using yield statements.
	/// </summary>
    public class MagicThread :Singleton<MagicThread>
    {
    
    
        public static void Start (IEnumerator task, bool startInBackground=true)
        {
            if (startInBackground)
                Instance.backgroundTasks.Add (task);
            else
                Instance.foregroundTasks.Add (task);
        }
    
        List<IEnumerator> foregroundTasks = new List<IEnumerator> ();
        List<IEnumerator> backgroundTasks = new List<IEnumerator> ();

        IEnumerator Start ()
        {
            while (true) {
                yield return null;
                if (foregroundTasks.Count > 0) {
                    IEnumerator[] newTasks;
                    lock (foregroundTasks) {
                        newTasks = foregroundTasks.ToArray ();
                        foregroundTasks.Clear ();
                    }
                    foreach (var i in newTasks) {
                        StartCoroutine (HandleCoroutine (i));   
                    }
                }
                if (backgroundTasks.Count > 0) {
                    foreach (var i in backgroundTasks) {
                        ThreadPool.QueueUserWorkItem ((state) => {
                            HandleThread (i); 
                        });
                    }
                    backgroundTasks.Clear ();
                }
            }
        }
    
        IEnumerator HandleCoroutine (IEnumerator task)
        {
            while (task.MoveNext()) {
                var t = task.Current;
                if ((t as BackgroundTask) == null)
                    yield return t;
                else {
                    backgroundTasks.Add (task);
                    yield break;
                }
            }
        }
    
        void HandleThread (IEnumerator task)
        {
            try {
                while (task.MoveNext()) {
                    var t = task.Current;
                    if ((t as ForegroundTask) != null) {
                        lock (foregroundTasks) {
                            foregroundTasks.Add (task);
                        }
                        break;
                    }
                }
            } catch (ThreadAbortException) {
                return;
            }
            catch (System.Exception e) {
                throw new  GameFrameworkException("Exception in MagicThread: " + e.ToString());
                //Debug.LogError ("Exception in MagicThread: " + e.ToString ());
            }
        }
    
    
    }
}
