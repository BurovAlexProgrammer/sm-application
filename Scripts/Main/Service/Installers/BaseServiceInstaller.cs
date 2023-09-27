using sm_application.Extension;
using sm_application.Game;
using sm_application.Service;
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