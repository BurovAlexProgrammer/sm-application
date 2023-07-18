using sm_application.Scripts.Main.Events;

namespace Events
{
    public class HttpResponseEvent : BaseEvent
    {
        public HttpRequestEvent HttpRequestEvent;
    }
}