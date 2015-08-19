﻿using System;
using System.Diagnostics;

namespace NGitLab.Impl
{
    [DebuggerStepThrough]
    public class API
    {
        public readonly string APIToken;
        private readonly string _hostUrl;
        private const string APINamespace = "/api/v3";
        public readonly string AccessToken;

        public API(string hostUrl, string apiToken, string accessToken)
        {
            _hostUrl = hostUrl.EndsWith("/") ? hostUrl.Replace("/$", "") : hostUrl;

            if (!String.IsNullOrEmpty(apiToken))
            {
                APIToken = apiToken;
            }
            else
            {
                AccessToken = accessToken;
            }
        }
        
        public HttpRequestor Get()
        {
            return new HttpRequestor(this, MethodType.Get);
        }

        public HttpRequestor Post()
        {
            return new HttpRequestor(this, MethodType.Post);
        }

        public HttpRequestor Put()
        {
            return new HttpRequestor(this, MethodType.Put);
        }
        
        public HttpRequestor Delete()
        {
            return new HttpRequestor(this, MethodType.Delete);
        }

        public Uri GetAPIUrl(string tailAPIUrl)
        {
            if (APIToken != null)
            {
                tailAPIUrl = tailAPIUrl + (tailAPIUrl.IndexOf('?') > 0 ? '&' : '?') + "private_token=" + APIToken;
            }

            if (AccessToken != null)
            {
                tailAPIUrl = tailAPIUrl + (tailAPIUrl.IndexOf('?') > 0 ? '&' : '?') + "access_token=" + AccessToken;
            }

            if (!tailAPIUrl.StartsWith("/"))
            {
                tailAPIUrl = "/" + tailAPIUrl;
            }
            return new Uri(_hostUrl + APINamespace + tailAPIUrl);
        }

        public Uri GetUrl(string tailAPIUrl)
        {
            if (!tailAPIUrl.StartsWith("/"))
            {
                tailAPIUrl = "/" + tailAPIUrl;
            }

            return new Uri(_hostUrl + tailAPIUrl);
        }
    }
}