using sm_application.Scripts.Main.Service;

namespace sm_application.Scripts.Main.Events
{
    public abstract class BaseEvent
    {
        public void Fire()
        {
            SystemsService.FireEvent(this);
        }
    }
}