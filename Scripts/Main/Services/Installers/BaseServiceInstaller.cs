using smApplication.Scripts.Extension;
using smApplication.Scripts.Main.Game;
using smApplication.Scripts.Main.Services;
using UnityEngine;

namespace smApplication.Scripts.Main.Services
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