using smApplication.Scripts.Main.Services;

namespace smApplication.Scripts.Main.Events
{
    public abstract class BaseEvent
    {
        public void Fire()
        {
            SystemsService.FireEvent(this);
        }
    }
}