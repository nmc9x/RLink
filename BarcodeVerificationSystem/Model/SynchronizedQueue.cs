using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace BarcodeVerificationSystem.Model
{
    public class SynchronizedQueue<T>
    {
        Queue<T> _Queue = new Queue<T>();
        object _Obj = new object();
        public void Enqueue(T item)
        {
            lock (_Obj)
            {
                this._Queue.Enqueue(item);
                Monitor.PulseAll(_Obj);
            }
        }
        public T Dequeue()
        {
            lock (_Obj)
            {
                while (this._Queue.Count() == 0)
                    Monitor.Wait(_Obj);
                return this._Queue.Dequeue();
            }
        }
        public int Count()
        {
            lock (_Obj)
                return this._Queue.Count();
        }
        public void Clear()
        {
            lock (_Obj)
                this._Queue.Clear();
        }
    }
}
