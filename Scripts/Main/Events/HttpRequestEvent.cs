using System.Collections.Generic;
using Events;
using sm_application.Scripts.Main.HttpData;

namespace sm_application.Scripts.Main.Events
{
    public class HttpRequestEvent : BaseEvent
    {
        public HttpRequestEvent(HttpEndpointData endpointData, Dictionary<string, string> fields) : this(endpointData)
        {
            Fields = fields;
        }
        
        public HttpRequestEvent(HttpEndpointData endpointData)
        {
            Endpoint = endpointData.Endpoint;
            HttpMethod = endpointData.HttpMethod;
        }
        
        public string Endpoint;
        public HttpRequestMethod HttpMethod;
        public Dictionary<string, string> Fields;
        public HttpResponseEvent HttpResponseEvent;
    }
}