// Copyright (c) .NET Foundation. All rights reserved. 
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Server.Kestrel;
using Microsoft.AspNet.Hosting;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.DependencyInjection;

namespace Benchmarks
{
    public class Startup
    {
    /*
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
		Console.WriteLine ("startup from appEnv");
	}
	*/
	
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            var kestrel = app.ServerFeatures[typeof(IKestrelServerInformation)] as IKestrelServerInformation;

            if (kestrel != null)
            {
                // BUG: Multi-loop Kestrel doesn't work on Windows right now, see https://github.com/aspnet/KestrelHttpServer/issues/232
                // kestrel.ThreadCount = 2;
            }

            app.UsePlainText();
            app.UseJson();
            app.UseMvc();
        }
    }
}
