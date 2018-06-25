//------------------------------------------------------------
// Game Framework v3.x
// Copyright © 2013-2017 Jiang Yin. All rights reserved.
// Homepage: http://gameframework.cn/
// Feedback: mailto:jiangyin@gameframework.cn
//------------------------------------------------------------

using UnityEngine;
using GameFramework.Setting;
using System;
using GameFramework;

namespace GameMain
{
    /// <summary>
    /// 默认配置管理器辅助器。
    /// </summary>
    public class DefaultSettingHelper : ISettingHelper
    {
        /// <summary>
        /// 保存配置。
        /// </summary>
        public  bool Save()
        {
            PlayerPrefs.Save();
            return true;
                
        }
        
        /// <summary>
        /// 检查是否存在指定配置项。
        /// </summary>
        /// <param name="key">要检查配置项的名称。</param>
        /// <returns>指定的配置项是否存在。</returns>
        public  bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        /// <summary>
        /// 移除指定配置项。
        /// </summary>
        /// <param name="key">要移除配置项的名称。</param>
        public  void RemoveKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        /// <summary>
        /// 清空所有配置项。
        /// </summary>
        public  void RemoveAllKeys()
        {
            PlayerPrefs.DeleteAll();
        }

        /// <summary>
        /// 从指定配置项中读取布尔值。
        /// </summary>
        /// <param name="key">要获取配置项的名称。</param>
        /// <returns>读取的布尔值。</returns>
        public  bool GetBool(string key)
        {
            return PlayerPrefs.GetInt(key) != 0;
        }

        /// <summary>
        /// 从指定配置项中读取布尔值。
        /// </summary>
        /// <param name="key">要获取配置项的名称。</param>
        /// <param name="defaultValue">当指定的配置项不存在时，返回此默认值。</param>
        /// <returns>读取的布尔值。</returns>
        public  bool GetBool(string key, bool defaultValue)
        {
            return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) != 0;
        }

        /// <summary>
        /// 向指定配置项写入布尔值。
        /// </summary>
        /// <param name="key">要写入配置项的名称。</param>
        /// <param name="value">要写入的布尔值。</param>
        public  void SetBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        /// <summary>
        /// 从指定配置项中读取整数值。
        /// </summary>
        /// <param name="key">要获取配置项的名称。</param>
        /// <returns>读取的整数值。</returns>
        public  int GetInt(string key)
        {
            return PlayerPrefs.GetInt(key);
        }

        /// <summary>
        /// 从指定配置项中读取整数值。
        /// </summary>
        /// <param name="key">要获取配置项的名称。</param>
        /// <param name="defaultValue">当指定的配置项不存在时，返回此默认值。</param>
        /// <returns>读取的整数值。</returns>
        public  int GetInt(string key, int defaultValue)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }

        /// <summary>
        /// 向指定配置项写入整数值。
        /// </summary>
        /// <param name="key">要写入配置项的名称。</param>
        /// <param name="value">要写入的整数值。</param>
        public  void SetInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        /// <summary>
        /// 从指定配置项中读取浮点数值。
        /// </summary>
        /// <param name="key">要获取配置项的名称。</param>
        /// <returns>读取的浮点数值。</returns>
        public  float GetFloat(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }

        /// <summary>
        /// 从指定配置项中读取浮点数值。
        /// </summary>
        /// <param name="key">要获取配置项的名称。</param>
        /// <param name="defaultValue">当指定的配置项不存在时，返回此默认值。</param>
        /// <returns>读取的浮点数值。</returns>
        public  float GetFloat(string key, float defaultValue)
        {
            return PlayerPrefs.GetFloat(key, defaultValue);
        }

        /// <summary>
        /// 向指定配置项写入浮点数值。
        /// </summary>
        /// <param name="key">要写入配置项的名称。</param>
        /// <param name="value">要写入的浮点数值。</param>
        public  void SetFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }

        /// <summary>
        /// 从指定配置项中读取字符串值。
        /// </summary>
        /// <param name="key">要获取配置项的名称。</param>
        /// <returns>读取的字符串值。</returns>
        public  string GetString(string key)
        {
            return PlayerPrefs.GetString(key);
        }

        /// <summary>
        /// 从指定配置项中读取字符串值。
        /// </summary>
        /// <param name="key">要获取配置项的名称。</param>
        /// <param name="defaultValue">当指定的配置项不存在时，返回此默认值。</param>
        /// <returns>读取的字符串值。</returns>
        public  string GetString(string key, string defaultValue)
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }

        /// <summary>
        /// 向指定配置项写入字符串值。
        /// </summary>
        /// <param name="key">要写入配置项的名称。</param>
        /// <param name="value">要写入的字符串值。</param>
        public  void SetString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        /// <summary>
        /// 从指定配置项中读取对象。
        /// </summary>
        /// <typeparam name="T">要读取对象的类型。</typeparam>
        /// <param name="key">要获取配置项的名称。</param>
        /// <returns>读取的对象。</returns>
        public  T GetObject<T>(string key)
        {
            return JsonUtils.JsonDeserialize_String<T>(PlayerPrefs.GetString(key)); //JsonUtility.FromJson<T>(PlayerPrefs.GetString(key));
            //return Utility.Json.ToObject<T>(PlayerPrefs.GetString(key));
        }

        /// <summary>
        /// 从指定配置项中读取对象。
        /// </summary>
        /// <typeparam name="T">要读取对象的类型。</typeparam>
        /// <param name="key">要获取配置项的名称。</param>
        /// <param name="defaultObj">当指定的配置项不存在时，返回此默认对象。</param>
        /// <returns>读取的对象。</returns>
        public  T GetObject<T>(string key, T defaultObj)
        {
            string json = PlayerPrefs.GetString(key, null);
            if (string.IsNullOrEmpty(json))
            {
                return defaultObj;
            }

            return JsonUtils.JsonDeserialize_String<T>(json); //.FromJson<T>(json);

            //return Utility.Json.ToObject<T>(json);
            //return JsonUtility.FromJson<T>(json);
        }

        /// <summary>
        /// 向指定配置项写入对象。
        /// </summary>
        /// <typeparam name="T">要写入对象的类型。</typeparam>
        /// <param name="key">要写入配置项的名称。</param>
        /// <param name="obj">要写入的对象。</param>
        public  void SetObject<T>(string key, T obj)
        {
            PlayerPrefs.SetString(key, JsonUtils.JsonSerialize_String(obj));
        }

        public bool Load()
        {
            return true;
            //throw new NotImplementedException();
        }

        public object GetObject(Type objectType, string key)
        {

            throw new NotImplementedException();
        }

        public object GetObject(Type objectType, string key, object defaultObj)
        {

            throw new NotImplementedException();
        }

        public void SetObject(string key, object obj)
        {
            //throw new NotImplementedException();
            throw new NotImplementedException();
        }
    }
}
