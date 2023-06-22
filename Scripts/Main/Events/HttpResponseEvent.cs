using sm_application.Scripts.Main.Events;
using UnityEngine.Networking;

namespace Events
{
    public class HttpResponseEvent : BaseEvent
    {
        public UnityWebRequest UnityWebRequest;
    }
}