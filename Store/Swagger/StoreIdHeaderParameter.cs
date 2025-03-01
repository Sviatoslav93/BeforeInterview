using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Store.Swagger
{
    public class StoreIdHeaderParameter : IOperationFilter
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

            if (operation.Parameters.Any(p => p.Name == "X-StoreId"))
            {
                return;
            }

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "X-StoreId",
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema
                {
                    Type = "String"
                }
            });
        }
    }
}
