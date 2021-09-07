using System.Collections;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using System.Collections.Generic;
using System;
using System.Collections.Concurrent;
using GameFramework.Console;

/// <summary>
/// 拆解该系统
/// </summary>
namespace Assets.Scripts
{

    [ConsoleAttribute("gameLog", 101)]
    internal class MyWindow : IConsoleWindow
    {
        public class LogWrapper : IReference
        {
            public UnityEngine.LogType type;
            public string logContent;

            public void OnRelease()
            {
                logContent = string.Empty;
            }
        }
        private bool _ShowLog = true;
        private bool _ShowWarning = true;
        private bool _ShowError = true;
        private Vector2 _ScrollPos = Vector2.zero;

        //[MenuItem("Window/My Window")]
        //public static void ShowWindow()
        //{
        //    EditorWindow.GetWindow(typeof(MyWindow));
        //}

        /// <summary>
        /// 日志最大显示数量
        /// </summary>
        private const int LOG_MAX_COUNT = 500;

        /// <summary>
        /// 日志集合
        /// </summary>
        public List<LogWrapper> _Logs = new List<LogWrapper>();

        public MyWindow()
        {
            //注册UnityEngine日志系统
            Application.logMessageReceived += HandleUnityEngineLog;
            //UnityEngine.Debug.Log();

        }

        void IConsoleWindow.OnGUI()
        {


            GUILayout.BeginHorizontal();
            _ShowLog = ConsoleGUI.Toggle("Log", _ShowLog);
            _ShowWarning = ConsoleGUI.Toggle("Warning", _ShowWarning);
            _ShowError = ConsoleGUI.Toggle("Error", _ShowError);
            GUILayout.EndHorizontal();

            float offset = ConsoleGUI.ToolbarStyle.fixedHeight;
            _ScrollPos = ConsoleGUI.BeginScrollView(_ScrollPos, offset);

            for (int i = 0; i < _Logs.Count; i++)
            {
                LogWrapper wrapper = _Logs[i];
                if (wrapper.type == UnityEngine.LogType.Log)
                {
                    if (_ShowLog)
                        ConsoleGUI.Label(wrapper.logContent);

                }
                else if (wrapper.type == UnityEngine.LogType.Warning)
                {
                    if (_ShowWarning)
                        ConsoleGUI.YellowLabel(wrapper.logContent);

                }
                else
                {
                    if (_ShowError)
                        ConsoleGUI.RedLabel(wrapper.logContent);

                }
            }
            ConsoleGUI.EndScrollView();
        }

        private void HandleUnityEngineLog(string logString, string stackTrace, UnityEngine.LogType type)
        {
            UnityEngine.Debug.Log("handle enter...");

            LogWrapper wrapper = ReferencePool.Spawn<LogWrapper>();
            wrapper.type = type;

            if (type == UnityEngine.LogType.Assert || type == UnityEngine.LogType.Error || type == UnityEngine.LogType.Exception)
                wrapper.logContent = logString + "\n" + stackTrace;
            else
                wrapper.logContent = logString;

            _Logs.Add(wrapper);
            if (_Logs.Count > LOG_MAX_COUNT)
            {
                ReferencePool.Release(_Logs[0]);
                _Logs.RemoveAt(0);
            }
        }
    }
}