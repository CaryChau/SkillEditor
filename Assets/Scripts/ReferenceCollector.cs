using System.Collections;
using System.Collections.Generic;
using System;

namespace Assets.Scripts
{

    /// <summary>
    /// 这是一个回收器
    /// </summary>
    internal class ReferenceCollector
    {
        private readonly Queue<IReference> _Collector;

        /// <summary> 
        /// 引用类型
        /// </summary>

        public Type ClassType { private set; get; }

        /// <summary>
        /// 内存缓存总数
        /// </summary>

        public int Count
        {
            get { return _Collector.Count; }
        }

        ///<summary>
        ///外部使用总数
        /// </summary>

        public int SpawnCount { private set; get; }

        public ReferenceCollector(Type type, int capacity)
        {
            ClassType = type;

            //创建缓存池
            _Collector = new Queue<IReference>(capacity);

            //检测是否继承了专属接口
            Type temp = type.GetInterface(nameof(IReference));
            if (temp == null)
                throw new Exception($"{type.Name} must inherit from {nameof(IReference)}");

        }

        ///<summary>
        ///申请引用对象
        /// </summary>
        public IReference Spawn()
        {
            IReference item;
            if(_Collector.Count > 0)
            {
                item = _Collector.Dequeue();
            }
            else
            {
                item = Activator.CreateInstance(ClassType) as IReference;
            }
            SpawnCount++;
            return item;
        }

        public void Release(IReference item)
        {
            if (item == null)
                return;
            if (item.GetType() != ClassType)
                throw new Exception($"Invalid type {item.GetType()}");
            if (_Collector.Contains(item))
                throw new Exception($"The item {item.GetType()} already exists.");

            SpawnCount--;
            item.OnRelease();
            _Collector.Enqueue(item);

        }


    }
}