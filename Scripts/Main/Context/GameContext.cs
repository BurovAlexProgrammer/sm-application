using System;
using sm_application.Events;
using sm_application.Service;
using UnityEngine;

namespace sm_application.Context
{
    public abstract class GameContext : MonoBehaviour, IConstruct
    {
        protected virtual void Initialize()
        {
            throw new NotImplementedException();
        }
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void Construct()
        {
            Initialize();
            new GameContextInitializedEvent().Fire();
        }
    }
}