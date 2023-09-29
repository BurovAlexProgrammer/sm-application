using System;
using sm_application.Service;
using UnityEngine;

namespace sm_application.Context
{
    abstract class SceneContextInstaller : MonoBehaviour, IConstruct, IDisposable
    {
        public virtual void Construct()
        {
            throw new NotImplementedException();
        }

        public virtual void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}