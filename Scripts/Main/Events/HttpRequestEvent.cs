using System;
using System.Collections.Generic;
using Events;
using sm_application.Scripts.Main.HttpData;
using UnityEngine.Networking;

namespace sm_application.Scripts.Main.Events
{
    public class HttpRequestEvent : BaseEvent
    {
        public string Endpoint;
        public HttpRequestMethod HttpMethod;
        public Dictionary<string, string> Fields;
        public HttpResponseEvent HttpResponseEvent;
        public Action<HttpRequestEvent> Timeout;
        public Action<DownloadHandler> Success;
        public Action<HttpRequestEvent, UnityWebRequest> Error; //Изучить что приходит в аргументах экшена, может текстом ошибку прокидывать
        public Action<DownloadHandler> Response;
        public Action<float> ProgressChanged;

        private const float RequestTimeout = 0.7f;
        private float timeoutTimer = RequestTimeout;
        
        public HttpRequestEvent(HttpEndpointData endpointData, Dictionary<string, string> fields) : this(endpointData)
        {
            Fields = fields;
        }
        
        public HttpRequestEvent(HttpEndpointData endpointData)
        {
            Endpoint = endpointData.Endpoint;
            HttpMethod = endpointData.HttpMethod;
        }

        public HttpRequestEvent OnSuccess(Action<DownloadHandler> action)
        {
            Success += action;
            return this;
        }
        
        public HttpRequestEvent OnError(Action<HttpRequestEvent, UnityWebRequest> action)
        {
            Error += action;
            return this;
        }
        
        public HttpRequestEvent OnResponse(Action<DownloadHandler> action)
        {
            Response += action;
            HttpResponseEvent.Fire();
            return this;
        }
        
        public HttpRequestEvent OnProgressChanged(Action<float> action)
        {
            ProgressChanged += action;
            return this;
        }

        public HttpRequestEvent OnTimeOut(Action<HttpRequestEvent> action)
        {
            Timeout += action;
            return this;
        }
    }
}