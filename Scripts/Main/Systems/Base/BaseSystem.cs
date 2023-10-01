using System;
using System.Collections.Generic;
using sm_application.Events;

namespace sm_application.Systems
{
    public abstract class BaseSystem : ISystem
    {
        private Dictionary<Type, object> _eventCallbacks = new Dictionary<Type, object>();

        public Dictionary<Type, object> EventCallbacks => _eventCallbacks;

        public virtual void Init()
        {
        }

        public virtual void Dispose()
        {
        }

        public void AddListener<T>(Action<T> callback)
        {
            var type = typeof(T);

            if (callback == null) throw new Exception("Callback is null");
            
            if (_eventCallbacks.ContainsKey(type)) throw new Exception("Listener is registered already.");
            
            _eventCallbacks.Add(type, callback);
        }

        public void RemoveListener<T>()
        {
            var type = typeof(T);

            if (!_eventCallbacks.ContainsKey(type)) throw new Exception("Listener not found.");

            _eventCallbacks[type] = null;
            _eventCallbacks.Remove(type);
        }

        public virtual void AddEventHandlers()
        {
        }

        public virtual void RemoveEventHandlers()
        {
        }
    }
}