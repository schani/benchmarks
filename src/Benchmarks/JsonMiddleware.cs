﻿// Copyright (c) .NET Foundation. All rights reserved. 
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Newtonsoft.Json;

namespace Benchmarks
{
    public class JsonMiddleware
    {
        private static readonly Task _done = Task.FromResult(0);
        private static readonly PathString _path = new PathString("/json");
        private static readonly JsonSerializer _json = new JsonSerializer();

        private readonly RequestDelegate _next;
        
        public JsonMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            // We check Ordinal explicitly first because it's faster than OrdinalIgnoreCase
            if (httpContext.Request.Path.StartsWithSegments(_path) ||
                httpContext.Request.Path.StartsWithSegments(_path))
            {
                httpContext.Response.StatusCode = 200;
                httpContext.Response.ContentType = "application/json";
                using (var sw = new StreamWriter(httpContext.Response.Body))
                {
                    _json.Serialize(sw, new { message = "Hello, World!" });
                }
                return _done;
            }

            return _next(httpContext);
        }
    }
    
    public static class JsonMiddlewareExtensions
    {
        public static IApplicationBuilder UseJson(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JsonMiddleware>();
        }
    }
}
