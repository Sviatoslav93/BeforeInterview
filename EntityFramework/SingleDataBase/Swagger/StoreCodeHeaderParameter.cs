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

            if (operation.Parameters.Any(p => p.Name == "X-StoreCode"))
            {
                return;
            }

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "X-StoreCode",
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema
                {
                    Type = "String"
                }
            });
        }
    }
}
