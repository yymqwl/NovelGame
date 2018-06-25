using System.Collections;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace GameFramework
{
    /* 暂时禁止异步加载操作
    public abstract class AssetBundleLoadOperation : IEnumerator
    {
        public object Current
        {
            get
            {
                return null;
            }
        }
        public bool MoveNext()
        {
            return !IsDone();
        }

        public void Reset()
        {
        }

        abstract public bool Update();

        abstract public bool IsDone();
    }


    public abstract class AssetBundleLoadAssetOperation : AssetBundleLoadOperation
    {
        public abstract T GetAsset<T>() where T : UnityEngine.Object;
    }
    public delegate void AssetBundleFinished(AssetBundleLoadAssetOperationFull arg);



   

    public class AssetBundleLoadAssetOperationFull : AssetBundleLoadAssetOperation
    {
        protected string m_AssetBundleName;
        protected string m_AssetName;
        protected string m_DownloadingError;
        protected System.Type m_Type;
        protected AssetBundleRequest m_Request = null;
        protected AssetBundleFinished m_funcfinish = null;

        public AssetBundleLoadAssetOperationFull(string bundleName, string assetName, System.Type type ,AssetBundleFinished  func=null)
        {
            m_AssetBundleName = bundleName;
            m_AssetName = assetName;
            m_Type = type;
            m_funcfinish = func;
        }

        public override T GetAsset<T>()
        {
            if (m_Request != null && m_Request.isDone)
                return m_Request.asset as T;
            else
                return null;
        }

        // Returns true if more Update calls are required.
        public override bool Update()
        {

            if (m_Request != null && m_Request.isDone)
            {
                if (m_funcfinish != null)
                {
                    m_funcfinish(this);
                }
                return false;
            }
            LoadedAssetBundle bundle = ResourcesManager.Instance.GetLoadedAssetBundle(m_AssetBundleName);
            if (bundle != null)
            {
                m_Request = bundle.m_AssetBundle.LoadAssetAsync(m_AssetName, m_Type);
            }



            return true;
        }

        public override bool IsDone()
        {

            if (m_Request == null && m_DownloadingError != null)
            {
                Debug.LogError(m_DownloadingError);
                return true;
            }

            return m_Request != null && m_Request.isDone;
        }
    }



    public class AssetBundleLoadManifestOperation : AssetBundleLoadAssetOperationFull
    {
        public AssetBundleLoadManifestOperation(string bundleName, string assetName, System.Type type)
            : base(bundleName, assetName, type)
        {
        }

        public override bool Update()
        {

            LoadedAssetBundle bundle = ResourcesManager.Instance.GetLoadedAssetBundle(m_AssetBundleName);
            if (bundle != null)
            {
                m_Request = bundle.m_AssetBundle.LoadAssetAsync(m_AssetName, m_Type);
            }

            if (m_Request != null && m_Request.isDone)
            {
                ResourcesManager.Instance.AssetBundleManifestObject = GetAsset<AssetBundleManifest>();
                return false;
            }
            else
                return true;
        }
    }




    public delegate void ResourcesAssetFinished(ResourcesLoadAssetOperationFull arg);
    public class ResourcesLoadAssetOperationFull : AssetBundleLoadAssetOperation
    {

        protected string m_AssetName;
        protected string m_DownloadingError;
        protected System.Type m_Type;
        protected ResourcesAssetFinished m_funcfinish = null;
        protected UObject m_objasset=null;
        public ResourcesLoadAssetOperationFull( string assetName, System.Type type, ResourcesAssetFinished func = null)
        {

            m_AssetName = assetName;
            m_Type = type;
            m_funcfinish = func;
            m_objasset = null;
        }

        public override T GetAsset<T>()
        {
            return (m_objasset as T);
        }
        public override bool Update()
        {

            m_objasset = ResourcesManager.Instance.GetLoadedResourcesAsset(m_AssetName);
            if (m_objasset != null)
            {
                if (m_funcfinish != null)
                {
                    m_funcfinish(this);
                }
                
                return false;
            }


            return true;
            
        }

        public override bool IsDone()
        {
            if (m_objasset == null)
            {
                return false;
            }
            return true;
        }
    }
    */

}
