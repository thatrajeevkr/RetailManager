using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;
//Add a CS file to add operation filter, ie when every operation runs, add a parameter to 
//it like bearer token so that the user is authorized in every call

namespace RMDataManager.App_Start
{
    public class AuthorizionOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if(operation.parameters == null)
            {
                operation.parameters = new List<Parameter>();
            }
            operation.parameters.Add(new Parameter
            {
                name = "Authorization",
                @in = "header",
                description = "access token",
                required = false,
                type = "string"
            });
        }
    }
}