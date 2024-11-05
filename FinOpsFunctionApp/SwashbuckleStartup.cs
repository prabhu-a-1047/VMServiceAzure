﻿using AzureFunctions.Extensions.Swashbuckle;
using AzureFunctions.Extensions.Swashbuckle.Settings;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

[assembly: WebJobsStartup(typeof(AzFuncWithSwagger.SwashbuckleStartup))]

namespace AzFuncWithSwagger
{
    public class SwashbuckleStartup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.AddSwashBuckle(
                Assembly.GetExecutingAssembly(),
                opts =>
                {
                    opts.AddCodeParameter = true;
                    opts.Documents = new[]
                    {
                        new SwaggerDocument
                        {
                            Name = "v1",
                            Title = "Swagger document",
                            Description = "Integrate Swagger UI With Azure Functions",
                            Version = "v2"
                        }
                    };
                    opts.ConfigureSwaggerGen = x =>
                    {
                        x.CustomOperationIds(apiDesc =>
                        {
                            return apiDesc.TryGetMethodInfo(out MethodInfo mInfo) ? mInfo.Name : default(Guid).ToString();
                        });
                    };
                });
        }
    }
}
