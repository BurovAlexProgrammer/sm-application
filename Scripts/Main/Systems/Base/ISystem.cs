using System;
using System.Collections.Generic;
using smApplication.Scripts.Main.Events;

namespace smApplication.Scripts.Main.Systems
{
    public interface ISystem
    {
        Dictionary<Type, Action<BaseEvent>> EventCallbacks { get; }
        void Init();
        void AddListener<T>(Action<BaseEvent> callback) where T : BaseEvent;
        void RemoveListener<T>();
        void AddEventHandlers();
        void RemoveEventHandlers();
    }
}