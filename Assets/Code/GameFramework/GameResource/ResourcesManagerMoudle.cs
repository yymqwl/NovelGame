using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UObject = UnityEngine.Object;

namespace GameFramework
{
    public class LoadedAssetBundle
    {
        public AssetBundle m_AssetBundle;
        public int m_ReferencedCount;

        public LoadedAssetBundle(AssetBundle assetBundle)
        {
            m_AssetBundle = assetBundle;
            m_ReferencedCount = 1;
        }
    }




    public class ResourcesManagerMoudle : GameFrameworkModule
    {


        bool m_init = false;
        UpdateTime<object,object> m_utw;
        string m_BaseDownloadingURL = "";
        AssetBundleManifest m_AssetBundleManifest = null;

        private ResLoadLocation m_ResLoadLocation= ResLoadLocation.Resource;

        /// <summary>
        /// Resourcesload 
        /// </summary>
        Dictionary<string, UObject> m_LoadedResourcesAsset = new Dictionary<string, UObject>();


        /// <summary>
        /// assetbundle 
        /// </summary>

        Dictionary<string, LoadedAssetBundle> m_LoadedAssetBundles = new Dictionary<string, LoadedAssetBundle> ();//Asset bund        
		Dictionary<string, string[]> m_Dependencies = new Dictionary<string, string[]> ();



        Asset_Mode m_asset_mode = Asset_Mode.Resources;//载入模式

        public Asset_Mode Asset_Mode
        {
            get
            {
                return m_asset_mode;
            }
            set
            {
                m_asset_mode = value;
            }

        }
        /// <sumry>
        ///
        /// 
        /// 
        /// </summary>
        /// 
        private AssetPackage m_AssetPackage;

        public AssetPackage AssetPackage
        {
            get
            {
                if (m_AssetPackage == null)
                {
                    m_AssetPackage = new AssetPackage();

                    
                }
                return m_AssetPackage;
            }
        }



        public override int Priority
        {
            get
            {
                return 10;
            }
        }

        public ResLoadLocation ResLoadLocation
        {
            get { return m_ResLoadLocation; }
            set
            {
                m_ResLoadLocation = value;
            }
        }


        public  string BaseDownloadingURL
		{
			get { return m_BaseDownloadingURL; }
			set { m_BaseDownloadingURL = value; }
		}
	
	
	    AssetBundleManifest AssetBundleManifestObject
		{
			set {m_AssetBundleManifest = value; }
            get { return m_AssetBundleManifest; }
		}
	

	
		private  string GetStreamingAssetsPath()
		{
            if(Application.platform ==RuntimePlatform.Android)
            {
                return Application.streamingAssetsPath;
            }
            return  Application.streamingAssetsPath;
		}
	
		public  void SetSourceAssetBundleDirectory(string relativePath)
		{
			BaseDownloadingURL = GetStreamingAssetsPath() + relativePath;
		}
		
		public  void SetSourceAssetBundleURL(string absolutePath)
		{
			BaseDownloadingURL = absolutePath;
		}

        
        protected UObject  GetLoadedResourcesAsset(string assetname )
        {
            UObject objasset = null;
            m_LoadedResourcesAsset.TryGetValue(assetname, out objasset);
            return objasset;
        }


		protected LoadedAssetBundle GetLoadedAssetBundle (string assetBundleName)
		{		
			LoadedAssetBundle bundle = null;
			m_LoadedAssetBundles.TryGetValue(assetBundleName, out bundle);
			if (bundle == null)
				return null;
			
			// No dependencies are recorded, only the bundle itself is required.
			string[] dependencies = null;
			if (!m_Dependencies.TryGetValue(assetBundleName, out dependencies) )
				return bundle;
			
			// Make sure all dependencies are loaded
			foreach(var dependency in dependencies)
			{
				LoadedAssetBundle dependentBundle;
				m_LoadedAssetBundles.TryGetValue(dependency, out dependentBundle);
				if (dependentBundle == null)
					return null;
			}
	
			return bundle;
		}

	
        public void InitAssetTable()
        {
            string strbundle="json";
            string strtable="";
            if (m_ResLoadLocation ==  ResLoadLocation.Resource)
            {
                strtable = "Json/AssetPackage";
                AssetPackage.LoadAssetFromJson(ReadTextFile(strtable));
            }
            else if (m_ResLoadLocation == ResLoadLocation.Streaming)
            {
                strtable = "AssetPackage";
                var txtasset=  LoadAssetBundleDirectly<TextAsset>(strbundle, strtable);
                AssetPackage.LoadAssetFromJson(txtasset.text);
            }

        }
		 public void Initialize (string manifestAssetBundleName)
		{
            if(m_init)
            {

                return;
            }
            LoadManifestDirectly(manifestAssetBundleName);
            m_utw = new  UpdateTime<object, object>();
            m_utw.init( GameConstant.Instance.TResourcesRecovery);
            m_utw.Evt_Act += UpdateClearUnusedResources;
            m_init = true;
        }
         void UpdateClearUnusedResources(object psender,object param)
        {
            UnloadUnusedAssets();
        }
        
        protected void LoadManifestDirectly(string assetBundleName)
        {


            if (m_LoadedAssetBundles.ContainsKey(assetBundleName))//已经加载
            {
                return;
            }
            

            string url = m_BaseDownloadingURL + assetBundleName;
            var  ab = AssetBundle.LoadFromFile(url);
            if(ab ==null)
            {
                DebugHandler.LogError("ab Null"+ assetBundleName);
            }
            m_LoadedAssetBundles.Add(assetBundleName,new LoadedAssetBundle(ab));
            AssetBundleManifestObject = ab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            if(AssetBundleManifestObject == null)
            {
                throw new GameFrameworkException("AssetBundleManifest");
            }
       
        } 
      
		 public void UnloadAssetBundle(string assetBundleName)
		{
			UnloadAssetBundleInternal(assetBundleName);
			UnloadDependencies(assetBundleName);
            UnloadUnusedAssets();
		}
         public void UnloadResourcesAsset(string assetName)
         {
             
             UObject objasset=null;
             m_LoadedResourcesAsset.TryGetValue(assetName, out objasset);
             if (objasset != null)
             {
                 m_LoadedResourcesAsset.Remove(assetName);
             }
             UnloadUnusedAssets();
         }

         protected void UnloadUnusedAssets()
         {
             Resources.UnloadUnusedAssets();
             GC.Collect();
         }
	
		 protected void UnloadDependencies(string assetBundleName)
		{
			string[] dependencies = null;
			if (!m_Dependencies.TryGetValue(assetBundleName, out dependencies) )
				return;
	
			foreach(var dependency in dependencies)
			{
				UnloadAssetBundleInternal(dependency);
			}
	
			m_Dependencies.Remove(assetBundleName);
		}
	
		 protected void UnloadAssetBundleInternal(string assetBundleName)
		{
			LoadedAssetBundle bundle = GetLoadedAssetBundle(assetBundleName);
			if (bundle == null)
				return;
	
			if (--bundle.m_ReferencedCount == 0)
			{
				
				m_LoadedAssetBundles.Remove(assetBundleName);
                bundle.m_AssetBundle.Unload(true);
                DebugHandler.Log( assetBundleName + " has been unloaded successfully");
			}
		}
        protected void LoadDependenciesDirectly(string assetBundleName)
        {
            if (m_AssetBundleManifest == null)
            {
                Debug.LogError("Please initialize AssetBundleManifest by calling AssetBundleManager.Initialize()");
                return;
            }
            string[] dependencies = m_AssetBundleManifest.GetAllDependencies(assetBundleName);
            if (dependencies.Length == 0)
                return;



            // Record and load all dependencies.
            m_Dependencies.Add(assetBundleName, dependencies);
            for (int i = 0; i < dependencies.Length; i++)
                LoadAssetBundleDirectlyInternal(dependencies[i]); 
        }
        protected void LoadAssetBundleDirectlyInternal(string assetBundleName) //依赖加载
        {
            LoadedAssetBundle bundle = null;
            m_LoadedAssetBundles.TryGetValue(assetBundleName, out bundle);
            if (bundle != null)
            {
                bundle.m_ReferencedCount++;
                return ;
            }
            string url = m_BaseDownloadingURL + assetBundleName;
            AssetBundle ab = AssetBundle.LoadFromFile(url); 

            if(ab== null)
            {
                throw new GameFrameworkException("ab Null");
            }
            m_LoadedAssetBundles.Add(assetBundleName,new LoadedAssetBundle( ab) );
        }
        public AssetBundle LoadAssetBundleDirectly(string assetBundleName  )
        {
            LoadedAssetBundle lab = GetLoadedAssetBundle(assetBundleName);

            if(lab !=null)
            {
                return lab.m_AssetBundle;
            }
            LoadDependenciesDirectly(assetBundleName);//加载依赖
            string url = m_BaseDownloadingURL + assetBundleName;
            AssetBundle ab = AssetBundle.LoadFromFile(url); //加载
            if(ab == null)
            {
                throw new GameFrameworkException("ab Null"+ url);
            }
            m_LoadedAssetBundles.Add(assetBundleName,new LoadedAssetBundle(ab) );
            return ab;
        }
        public T LoadAssetBundleDirectly<T>(string assetBundleName, string assetname) where T :UObject
        {
            AssetBundle ab = LoadAssetBundleDirectly(assetBundleName);
            return  ab.LoadAsset<T>(assetname);
        }


        public UObject LoadResourcesAssetDirectly(string assetName)
        {
            UObject obj = GetLoadedResourcesAsset(assetName);
            if(obj == null)
            {
                obj = Resources.Load(assetName);
                m_LoadedResourcesAsset.Add(assetName, obj);

            }
            return obj;
        }


        public T LoadAssetById<T>(int id) where T : UObject
        {
            return LoadAssetById<T>(id, Asset_Mode);
        }
        public T LoadAssetById<T>(int id, Asset_Mode mode= Asset_Mode.Resources) where T :UObject
        {
            AssetItem assetItem=null;
            UObject uobj = null;
            if (!AssetPackage.Dict_Assets.TryGetValue(id, out assetItem))
            {
                throw new GameFrameworkException("Asset id"+id.ToString());
            }
            
            if (mode == Asset_Mode.Resources)
            {
                uobj = LoadResourcesAssetDirectly(assetItem.ResourceName);

            }
            else
            {
                uobj = LoadAssetBundleDirectly<T>(assetItem.AssetBundleName, assetItem.AssetName);
            }
            return uobj as T;
        }




        public  void Dispose()
        {
            m_LoadedResourcesAsset.Clear();
            m_Dependencies.Clear();
            m_LoadedResourcesAsset.Clear();
            AssetBundle.UnloadAllAssetBundles(true);
            UnloadUnusedAssets();

        }

        public override bool BeforeInit()
        {
            return true;
        }

        public override bool Init()
        {

#if UNITY_STANDALONE_WIN
            SetSourceAssetBundleDirectory("/");
            Initialize("StandaloneWindows");
#endif
            InitAssetTable();

            return true;
        }




        public override bool AfterInit()
        {
            return true;
            //throw new NotImplementedException();
        }

        public override bool BeforeShutdown()
        {
            return true;
            //throw new NotImplementedException();
        }

        public override bool Shutdown()
        {
            Dispose();
            return base.Shutdown();
        }

        public override bool AfterShutdown()
        {
            return true;
            //throw new NotImplementedException();
        }

        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            if (m_utw != null)
                m_utw.OnUpdate(elapseSeconds, null, null);
        }
        public string ReadTextFile(string textName)
        {
            //if(ResLoadLocation == ResLoadLocation.Resource)
            {
                TextAsset text = LoadResourcesAssetDirectly(textName) as TextAsset;
                if (text == null)
                {
                    throw new Exception(string.Format("ReadTextFile not find " + textName));
                }
                else
                {
                    return text.text;
                }
            }
            //TextAsset text=
        }

        /*public T Load<T>(string name) where T: UnityEngine.Object
        {
            if(m_)
        }*/

        /*
        public override void Execute(float elapseSeconds)
        {
            if(m_utw!=null)
            m_utw.OnUpdate(elapseSeconds,null,null);
        }*/




    }

}