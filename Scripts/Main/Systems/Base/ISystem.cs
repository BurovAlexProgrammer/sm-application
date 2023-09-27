using System;
using System.Collections.Generic;
using sm_application.Events;

namespace sm_application.Systems
{
    public interface ISystem
    {
        Dictionary<Type, Action<BaseEvent>> EventCallbacks { get; }
        void Init();
        void AddListener<T>(Action<BaseEvent> callback) where T : BaseEvent;
        void RemoveListener<T>();
        void AddEventHandlers();
        void RemoveEventHandlers();
        void Dispose();
    }
}