using System;
using System.Collections.Generic;
using System.Linq;

namespace sm_application.Service
{
    public class Services
    {
        private static readonly Dictionary<Type, IService> _registeredServices = new Dictionary<Type, IService>();

        public static void Register<T>() where T : IService
        {
            if (_registeredServices.ContainsKey(typeof(T)))
            {
                throw new Exception($"Service type of {typeof(T).Name} registered already");
            }

            var newService = Instantiate<T>();

            if (newService is IConstruct)
            {
                newService.Construct();
            }
            
            if (newService is IConstructInstaller)
            {
                throw new Exception($"Service {typeof(T).Name} has Construct. Use Services.Register(IServiceInstaller installer) instead");
            }
            
            _registeredServices.Add(typeof(T), newService);
        }

        public static void Register<T>(IServiceInstaller installer) where T : IServiceWithInstaller
        {
            if (_registeredServices.ContainsKey(typeof(T)))
            {
                throw new Exception($"Service type of {typeof(T).Name} registered already");
            }
            
            var newService = Instantiate<T>();

            newService.Construct(installer); 
            _registeredServices.Add(typeof(T), newService);
        }
        
        public static T Instantiate<T>() where T : IService
        {
            return Activator.CreateInstance<T>();
        }
        
        public static T Get<T>() where T : IService
        {
            if (_registeredServices.ContainsKey(typeof(T)) == false)
            {
                throw new Exception($"Service type of {typeof(T).Name} not found.");
            }
            
            return (T)_registeredServices[typeof(T)];
        }

        public static void Dispose()
        {
            foreach (var type in _registeredServices.Keys.ToList())
            {
                if (_registeredServices[type] is IDisposable disposable)
                {
                    disposable.Dispose();
                }
                _registeredServices[type] = null;
            }
            
            _registeredServices.Clear();
        }
    }
}