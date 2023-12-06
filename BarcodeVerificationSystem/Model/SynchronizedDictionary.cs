using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeVerificationSystem.Model
{
    public class SynchronizedDictonary<T, Y>
    {
        object obj = new object();
        Dictionary<T, Y> data = new Dictionary<T, Y>();

        public Y GetValue(T t)
        {
            lock (obj)
            {
                if(data.TryGetValue(t, out Y y))
                {
                    return y;
                }
                else
                {
                    return default(Y);
                }
            }
        }

        public void SetValue(T t, Y y)
        {
            lock (obj)
            {
                data[t] = y;
            }
        }

        public void AddValue(T t, Y y)
        {
            lock (obj)
            {
                data.Add(t, y);
            }
        }
    }
}
