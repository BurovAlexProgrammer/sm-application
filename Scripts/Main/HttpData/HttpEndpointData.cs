using sm_application.Events;

namespace sm_application.HttpData
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