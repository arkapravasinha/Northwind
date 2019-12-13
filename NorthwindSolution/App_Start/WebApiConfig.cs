using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;
using Microsoft.Web.Http;
using Microsoft.Web.Http.Routing;
using Microsoft.Web.Http.Versioning;
using Microsoft.Web.Http.Versioning.Conventions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NorthwindSolution.Controllers;

namespace NorthwindSolution
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            config.AddApiVersioning(cfg=> {
                cfg.DefaultApiVersion = new ApiVersion(1, 0);
                cfg.AssumeDefaultVersionWhenUnspecified = true;
                cfg.ReportApiVersions = true;

                // config for reading from Header
                //cfg.ApiVersionReader = new HeaderApiVersionReader("n-version");

                //config for reading from query String
                //cfg.ApiVersionReader = new QueryStringApiVersionReader("n-version");

                //config for adding more than one version reader
                //cfg.ApiVersionReader = ApiVersionReader.Combine(
                //                        new HeaderApiVersionReader("n-version"),
                //                        new QueryStringApiVersionReader("n-version")
                //                        );

                //config for adding url segment reader
                cfg.ApiVersionReader = new UrlSegmentApiVersionReader();

                //config conventions for assigning versions here
                //cfg.Conventions.Controller<Customerv2Controller>()
                //.HasApiVersion(1, 0)
                //.HasApiVersion(1, 1)
                //.Action(m => m.Get(default(string), default(int), default(bool)))
                //.MapToApiVersion(2, 0);

            });

            //adding Route Constratints or Parameters Type
            var constratinResolver = new DefaultInlineConstraintResolver() {
                                      ConstraintMap = 
                                        {
                                            ["apiVersion"]=typeof(ApiVersionRouteConstraint)
                                        }
                                      };


            // Web API routes
            config.MapHttpAttributeRoutes(constratinResolver);

            //formatters
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver=
                new CamelCasePropertyNamesContractResolver();

            //removed as we are using Attribute Routing

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
