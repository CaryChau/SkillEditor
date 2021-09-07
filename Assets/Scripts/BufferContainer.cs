using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;
using System.Threading.Tasks;



namespace Assets.Scripts
{
    public class User
    {
        string _name;
        string _ID;

        public string Name
        {
            get { return _name; }
        }
        public string ID
        {
            get { return _ID; }
        }
    }
    public class BufferContainer
    {
        private Queue<User> _users = new Queue<User>();
        private Queue<User> _users_2 = new Queue<User>();
        private int _capacity = 10;

        #region 信号量
        ////signal
        //Semaphore fullCount = new Semaphore(0, 0);
        //Semaphore emptyCount = new Semaphore(0, 10);
        //Semaphore isUse = new Semaphore(1, 1);
        #endregion

        public void put(User data)
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(1000);

                }
                catch (ThreadInterruptedException e)
                {
                    Debug.Log(e.StackTrace);
                }

                //Synchronize code block
                Monitor.Enter(_users);

                //在这里换缓冲区
                while (_users.Count == _capacity)
                {
                    Debug.Log("container is full, waiting...");
                    try
                    {
                        //wait
                        Monitor.Wait(_users);
                    }
                    catch (ThreadInterruptedException e)
                    {
                        Debug.Log(e.StackTrace);
                    }
                }
                Debug.Log("producer--" + Thread.CurrentThread.Name + "--put" + data.Name);
                _users.Enqueue(data);

                //Notify all to release lock，步骤繁杂，频繁地切换线程、释放锁，性能消耗大
                Monitor.Pulse(_users_2);

                Monitor.Exit(_users);
            }
        }

        public User Take()
        {
            User mUser;
            while (true)
            {
                try
                {
                    Thread.Sleep(1000);

                }
                catch (ThreadInterruptedException e)
                {
                    Debug.Log(e.StackTrace);
                }
                Monitor.Enter(_users_2);
                
                while (_users_2.Count == 0)
                {
                    Debug.Log("container is empty, waiting...");
                    try
                    {
                        //wait for
                        Monitor.Wait(_users_2);
                    }
                    catch (ThreadInterruptedException e)
                    {
                        Debug.Log(e.StackTrace);

                    }
                }

                mUser = _users.Dequeue();
                _users_2.Enqueue(mUser);//second

                //Notify all to release
                Monitor.Pulse(_users);

                Monitor.Exit(_users_2);
            }
        }
    }
}