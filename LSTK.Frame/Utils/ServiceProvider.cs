using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSTK.Frame.Utils
{
    public static class ServiceProvider
    {
        private static Dictionary<string, object> _services = new Dictionary<string, object>();
        public static void AddService<T>()
        {
            string serviceTypeName = typeof(T).FullName;          
            T instance = Activator.CreateInstance<T>();
            _services.Add(serviceTypeName, instance);

        }

        public static T GetService<T>()
        {          
            string serviceTypeName = typeof(T).FullName;
            var serv = _services[serviceTypeName];
            if (serv is T)
            {
                return (T)serv;
            }
            try
            {
                return (T)Convert.ChangeType(serv, typeof(T));
            }
            catch (InvalidCastException)
            {
                return default(T);
            }
        }
    }
}
