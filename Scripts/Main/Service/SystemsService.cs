using System;
using System.Collections.Generic;
using System.Linq;
using sm_application.Events;
using sm_application.Extension;
using sm_application.Systems;
using sm_application.Wrappers;

namespace sm_application.Service
{
    public static class SystemsService
    {
        private static Dictionary<Type, ISystem> _systems = new Dictionary<Type, ISystem>();

        public static T Bind<T>() where T : ISystem
        {
            var type = typeof(T);

            if (_systems.ContainsKey(type)) throw new Exception("System has bound already.");

            var newSystem = Activator.CreateInstance<T>(); 
            _systems.Add(type, newSystem);
            newSystem.Init();
            newSystem.AddEventHandlers();
            return newSystem;
        }
        
        public static BaseSystem Bind(Type type) 
        {
            if (type.BaseType != typeof(BaseSystem))
            {
                throw new Exception($"SystemsService cannot bind. Type '{type}' is not BaseSystem type.");
            }
            
            if (_systems.ContainsKey(type)) throw new Exception("System has bound already.");

            var newSystem = Activator.CreateInstance(type) as BaseSystem; 
            _systems.Add(type, newSystem);
            newSystem.Init();
            newSystem.AddEventHandlers();
            return newSystem;
        }

        public static void FireEvent<T>(T firedEvent) where T : BaseEvent
        {
            var color = Common.ThemeColorHex("#00952A", "#017020");
            Log.Info($"Fired event <color={color}>{firedEvent.GetType().Name}</color>. {DateTime.Now.ToString("hh:mm:ss")}");

            foreach (var (key, system) in _systems)
            {
                if (system.EventCallbacks.ContainsKey(firedEvent.GetType()) == false) continue;
                
                system.EventCallbacks[firedEvent.GetType()]?.Invoke(firedEvent);
            }
        }


        public static void DisposeSystem(Type systemType)
        {
            _systems[systemType].Dispose();
            _systems.Remove(systemType);
        }
        
        public static void DisposeSystem(BaseSystem system)
        {
            DisposeSystem(system.GetType());
        }
        
        public static void DisposeSystem<T>() where T : BaseSystem
        {
            DisposeSystem(typeof(T));
        }

        public static void DisposeAllSystems()
        {
            foreach (var key in _systems.Keys.ToList())
            {
                _systems[key].Dispose();
                _systems[key] = null;
            }
            
            _systems.Clear();
        }
    }
}