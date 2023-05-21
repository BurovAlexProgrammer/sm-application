using smApplication.Scripts.Extension;
using sm_application.Scripts.Main.Game;
using sm_application.Scripts.Main.Service;
using UnityEngine;

namespace sm_application.Scripts.Main.Service
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