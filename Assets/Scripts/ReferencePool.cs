using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts
{
    /// <summary>
    /// 引用池
    /// </summary>
    public static class ReferencePool
    {
        private static readonly Dictionary<Type, ReferenceCollector> _Collectors = new Dictionary<Type, ReferenceCollector>();

        public static int InitCapacity { get; set; } = 100;

        public static int Count
        {
            get
            {
                return _Collectors.Count;
            }
        }
        
        /// <summary>
        /// 申请对象引用
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IReference Spawn(Type type)
        {
            if(_Collectors.ContainsKey(type) == false)
            {
                _Collectors.Add(type, new ReferenceCollector(type, InitCapacity));

            }
            return _Collectors[type].Spawn();
        }

        public static T Spawn<T>() where T : class, IReference, new()
        {
            Type type = typeof(T);
            return Spawn(type) as T;
        }


        public static void Release(IReference item)
        {
            Type type = item.GetType();
            if(_Collectors.ContainsKey(type) == false)
            {
                _Collectors.Add(type, new ReferenceCollector(type, InitCapacity));
            }
            _Collectors[type].Release(item);
        }
    }
}