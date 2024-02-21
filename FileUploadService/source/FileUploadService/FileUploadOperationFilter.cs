using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FileUploadService
{
    public class FileOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.RequestBody = new OpenApiRequestBody
            {
                Description = "upload file ",
                Content = new Dictionary<String, OpenApiMediaType>
                {
                    {
                        "multipart/form-data", new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema
                            {
                                Type = "object",
                                Required = new HashSet<String>{ "file" },
                                Properties = new Dictionary<String, OpenApiSchema>
                                {
                                    {
                                        "file", new OpenApiSchema()
                                        {
                                            Type = "string",
                                            Format = "binary"
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

        }
    }
}
