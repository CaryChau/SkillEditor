using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using UnityEngine;
namespace Assets.Scripts
{
    public class test : MonoBehaviour
    {

        private static readonly Dictionary<string, List<Type>> _cache;
        #region 哈希算法加密
        //public void test_encry()
        //{
        //    //LogHelper.Print("hello world!", stack: "asss");
        //    //System.Diagnostics.Debug.WriteLine("debug");
        //    string _str = "I love you!";
        //    byte[] buffer = Encoding.UTF8.GetBytes(_str);

        //    HashAlgorithm hash = HashAlgorithm.Create();
        //    byte[] hashBytes = hash.ComputeHash(buffer);
        //    foreach (byte _b in hashBytes)
        //        UnityEngine.Debug.Log(_b);

        //}
        #endregion
        #region 显示日志
        //public void testShowLog()
        //{
        //    MyWindow myWindow = new MyWindow();
        //    MyWindow.LogWrapper[] wrapper = new MyWindow.LogWrapper[2];
        //    wrapper[0].logContent = "fafa";
        //    wrapper[0].type = UnityEngine.LogType.Log;
        //    wrapper[1].logContent = "aaaaa";
        //    wrapper[1].type = UnityEngine.LogType.Warning;
        //    foreach(var item in wrapper)
        //    myWindow._Logs.Add(item);
        //    //UnityEngine.Debug.Log(myWindow._Logs[0].logContent);

        //}
        #endregion

        StringBuilder logBuilder = new StringBuilder();

        private void Awake()
        {
            //VM虚拟机会自动将用到的静态成员加载到内存
            Application.logMessageReceived += HandleLog;
        }
        

        private void OnGUI()
        {
            GUILayout.Label(logBuilder.ToString());
        }

        void HandleLog(string condition, string stackTrace, UnityEngine.LogType type)
        {
            if(type == UnityEngine.LogType.Error || type == UnityEngine.LogType.Exception)
            {
                string message = string.Format("condition = {0} \n stackTrace = {1} \n type = {2}", condition, stackTrace, type);
                SendLog(message);

            }
        }

        void SendLog(string message)
        {
            logBuilder.Append(message);
        }

        //override
        //private static List<Type> GetTypes(string assemblyName)
        //{
        //    string aa;

        //    return _cache[aa];

        //}

        // Update is called once per frame
        
    }

}
