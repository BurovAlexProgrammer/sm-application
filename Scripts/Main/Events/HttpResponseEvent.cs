using sm_application.Events;

namespace Events
{
    public class HttpResponseEvent : BaseEvent
    {
        public HttpRequestEvent HttpRequestEvent;
    }
}