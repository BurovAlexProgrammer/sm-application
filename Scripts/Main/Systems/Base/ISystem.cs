using System;
using System.Collections.Generic;
using sm_application.Events;

namespace sm_application.Systems
{
    public interface ISystem
    {
        Dictionary<Type, object> EventCallbacks { get; }
        void Init();
        void AddListener<T>(Action<T> callback);
        void RemoveListener<T>();
        void AddEventHandlers();
        void RemoveEventHandlers();
        void Dispose();
    }
}