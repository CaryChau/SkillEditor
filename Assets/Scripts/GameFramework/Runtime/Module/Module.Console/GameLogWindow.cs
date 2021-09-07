using System.Collections;
using System.Collections.Generic;
using System;
using GameFramework.Reference;
using UnityEngine;

namespace GameFramework.Console
{
    [ConsoleAttribute(("Game Log"), 101)]
    internal class GameLogWindow
    {

        static MonoBehaviour _behaviour;

        private class LogWrapper : IReference
        {
            public LogType Type;
            public string Log;
            public void OnRelease()
            {
                Log = string.Empty;
            }
        }

        //Log Max amount
        private const int LOG_MAX_COUNT = 500;

        //Log collection
        private List<LogWrapper> _logs = new List<LogWrapper>();

        #region 自定义协程
        //public static void Initialize(MonoBehaviour behaviour, Action<int, string> action)
        //{
        //    _behaviour = behaviour;
        //}
        //public static Coroutine StartCoroutine(IEnumerator coroutine)
        //{
        //    return _behaviour.StartCoroutine(coroutine);
        //}
        #endregion


    }

}

