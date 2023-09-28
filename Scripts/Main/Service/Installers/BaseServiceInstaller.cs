using sm_application.Context;
using sm_application.Extension;
using UnityEngine;

namespace sm_application.Service
{
    public abstract class BaseServiceInstaller: MonoBehaviour, IServiceInstaller
    {
        public virtual IServiceInstaller Install()
        {
            var installer = Instantiate(this, AppContext.ServicesHierarchy);
            installer.gameObject.CleanName();
            return installer;
        }
    }
}