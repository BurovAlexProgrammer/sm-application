using sm_application.Service;

namespace sm_application.Events
{
    public abstract class BaseEvent
    {
        public void Fire()
        {
            SystemsService.FireEvent(this);
        }
    }
}