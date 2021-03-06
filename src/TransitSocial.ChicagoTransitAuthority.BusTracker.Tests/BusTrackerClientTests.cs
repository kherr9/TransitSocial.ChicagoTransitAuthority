﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Owin.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Owin;

using TransitSocial.ChicagoTransitAuthority.BusTracker.Tests.Resources;

namespace TransitSocial.ChicagoTransitAuthority.BusTracker.Tests
{
    [TestClass]
    public partial class BusTrackerClientTests
    {
        private ResourceRepository repository;

        private const string UrlBase = "http://localhost:9000";

        [TestInitialize]
        public void TestInitialize()
        {
            this.repository = new ResourceRepository();
        }

        #region "Configurations"

        private const string BaseAddress = "http://localhost:9000";

        internal static void StartOwinTest(Func<Task> testsFunc)
        {
            // HttpSelfHostConfiguration. So info: http://www.asp.net/web-api/overview/hosting-aspnet-web-api/use-owin-to-self-host-web-api

            // Start webservice
            using (WebApp.Start<WebAppConfig>(url: BaseAddress))
            {
                testsFunc().Wait(1000);

                //////wait for all recieved message, or timeout. There is no exception on timeout, so we have to check carefully in the unit test.
                ////if (LogMessageBatchController.CountdownEvent != null)
                ////{
                ////    if (LogMessageBatchController.CountdownEvent.Wait(WebserviceCheckTimeoutMs))
                ////    {
                ////        // pause for a moment so we don't shut down the response
                ////        Thread.Sleep(1000);
                ////    }
                ////}
            }
        }

        public class WebAppConfig
        {
            public const string ApiKey = "SECRET_API_KEY";

            // This code configures Web API. The Startup class is specified as a type
            // parameter in the WebApp.Start method.
            public void Configuration(IAppBuilder appBuilder)
            {
                var repository = new ResourceRepository();

                // authenticate request
                appBuilder.Use(
                    async (ctx, next) =>
                    {
                        var url = ctx.Request.Uri;

                        var queryStrings = System.Web.HttpUtility.ParseQueryString(url.Query);

                        var value = queryStrings["key"];

                        if (value == null)
                        {
                            // missing key
                            var response = ctx.Response;
                            response.StatusCode = 200;
                            ctx.Response.Headers.Add("Content-Type", new[] { "text/xml;charset=utf-8" });
                            await ctx.Response.WriteAsync(@"<?xml version=""1.0""?>
<bustime-response><error><msg>No API access key supplied</msg></error></bustime-response>");
                        }
                        else if (value != ApiKey)
                        {
                            // incorrect key
                            var response = ctx.Response;
                            response.StatusCode = 200;
                            ctx.Response.Headers.Add("Content-Type", new[] { "text/xml;charset=utf-8" });
                            await ctx.Response.WriteAsync(@"<?xml version=""1.0""?>
<bustime-response><error><msg>Invalid API access key supplied</msg></error></bustime-response>");
                        }
                        else
                        {
                            await next();
                        }
                    });

                appBuilder.Map("/bustime/api/v1/gettime",
                    map =>
                    {
                        map.Run(
                            async ctx =>
                            {
                                var xml = repository.GetString(ResourceFiles.GetTimeResponse);

                                // status code
                                ctx.Response.StatusCode = 200;

                                // headers
                                ctx.Response.Headers.Add("Content-Type", new[] { "text/xml;charset=utf-8" });

                                // content
                                await ctx.Response.WriteAsync(xml);
                            });
                    });

                appBuilder.Map("/bustime/api/v1/getvehicles",
                    map =>
                    {
                        map.Run(
                            async ctx =>
                            {
                                var xml = repository.GetString(ResourceFiles.GetVehiclesResponse);

                                // status code
                                ctx.Response.StatusCode = 200;

                                // headers
                                ctx.Response.Headers.Add("Content-Type", new[] { "text/xml;charset=utf-8" });

                                // content
                                await ctx.Response.WriteAsync(xml);
                            });
                    });

                appBuilder.Map("/bustime/api/v1/getroutes",
                    map =>
                    {
                        map.Run(
                            async ctx =>
                            {
                                var xml = repository.GetString(ResourceFiles.GetRoutesResponse);

                                // status code
                                ctx.Response.StatusCode = 200;

                                // headers
                                ctx.Response.Headers.Add("Content-Type", new[] { "text/xml;charset=utf-8" });

                                // content
                                await ctx.Response.WriteAsync(xml);
                            });
                    });

                appBuilder.Map("/bustime/api/v1/getdirections",
                    map =>
                    {
                        map.Run(
                            async ctx =>
                            {
                                var xml = repository.GetString(ResourceFiles.GetRouteDirectionsResponse);

                                // status code
                                ctx.Response.StatusCode = 200;

                                // headers
                                ctx.Response.Headers.Add("Content-Type", new[] { "text/xml;charset=utf-8" });

                                // content
                                await ctx.Response.WriteAsync(xml);
                            });
                    });

                appBuilder.Map("/bustime/api/v1/getstops",
                    map =>
                    {
                        map.Run(
                            async ctx =>
                            {
                                var xml = repository.GetString(ResourceFiles.GetStopsResponse);

                                // status code
                                ctx.Response.StatusCode = 200;

                                // headers
                                ctx.Response.Headers.Add("Content-Type", new[] { "text/xml;charset=utf-8" });

                                // content
                                await ctx.Response.WriteAsync(xml);
                            });
                    });

                appBuilder.Map("/bustime/api/v1/getpredictions",
                    map =>
                    {
                        map.Run(
                            async ctx =>
                            {
                                var xml = repository.GetString(ResourceFiles.GetPredictionsResponse);

                                // status code
                                ctx.Response.StatusCode = 200;

                                // headers
                                ctx.Response.Headers.Add("Content-Type", new[] { "text/xml;charset=utf-8" });

                                // content
                                await ctx.Response.WriteAsync(xml);
                            });
                    });

                appBuilder.Map("/bustime/api/v1/getservicebulletins",
                    map =>
                    {
                        map.Run(
                            async ctx =>
                            {
                                var xml = repository.GetString(ResourceFiles.GetServiceBulletinsResponse);

                                // status code
                                ctx.Response.StatusCode = 200;

                                // headers
                                ctx.Response.Headers.Add("Content-Type", new[] { "text/xml;charset=utf-8" });

                                // content
                                await ctx.Response.WriteAsync(xml);
                            });
                    });
            }
        }

        #endregion
    }
}
