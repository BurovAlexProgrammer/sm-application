using sm_application.Scripts.Main.Events;

namespace sm_application.Scripts.Main.HttpData
{
    public struct HttpEndpointData
    {
        public HttpEndpointData(string endpoint, HttpRequestMethod httpMethod)
        {
            Endpoint = endpoint;
            HttpMethod = httpMethod;
        }
        
        public string Endpoint;
        public HttpRequestMethod HttpMethod;
    }
}