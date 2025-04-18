using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Core
{
    public class RequestBuilder
    {
        private RestRequest _request;
        private Method _method;
        private string _resource;
        private Dictionary<string, string> _headers = new();
        private object _body;

        public RequestBuilder SetMethod(Method method)
        {
            _method = method;
            return this;
        }

        public RequestBuilder SetResource(string resource)
        {
            _resource = resource;
            return this;
        }

        public RequestBuilder AddHeader(string name, string value)
        {
            _headers[name] = value;
            return this;
        }

        public RequestBuilder AddJsonBody(object body)
        {
            _body = body;
            return this;
        }

        public RestRequest Build()
        {
            _request = new RestRequest(_resource, _method);

            foreach (var header in _headers)
            {
                _request.AddHeader(header.Key, header.Value);
            }

            if (_body != null)
            {
                _request.AddJsonBody(_body);
            }

            return _request;
        }
    }
}
