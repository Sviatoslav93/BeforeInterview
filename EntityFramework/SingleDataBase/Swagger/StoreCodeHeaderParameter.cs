using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SingleDataBase.Swagger
{
    public class StoreCodeHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var path = context.ApiDescription.RelativePath;

            if (path == null)
                return;

            if (path.Contains("/auth", StringComparison.OrdinalIgnoreCase) ||
                path.Contains("/store", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "X-StoreCode",
                In = ParameterLocation.Header,
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "String"
                }
            });
        }
    }
}
