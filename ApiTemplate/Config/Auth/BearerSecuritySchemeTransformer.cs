using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace ApiTemplate.Config.Auth;

/// <summary>
/// Transformer to add Bearer Validation to Scalar UI
/// </summary>
/// <see cref="https://stackoverflow.com/questions/79265776/how-to-add-jwt-token-support-globally-in-scalar-for-a-net-9-application"/>
/// <param name="authenticationSchemeProvider"></param>
internal sealed class BearerSecuritySchemeTransformer(Microsoft.AspNetCore.Authentication.IAuthenticationSchemeProvider authenticationSchemeProvider) : IOpenApiDocumentTransformer
{
    public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        var authSchemes = await authenticationSchemeProvider.GetAllSchemesAsync();
        if (authSchemes.Any(x => x.Name == "Bearer"))
        {
            document.Components ??= new OpenApiComponents();
            document.Components.SecuritySchemes = new Dictionary<string, OpenApiSecurityScheme>
            {
                ["Bearer"] = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    In = ParameterLocation.Header,
                    BearerFormat = "Json Web Token"
                }
            };

            foreach (var operation in document.Paths.Values.SelectMany(path => path.Operations))
            {
                operation.Value.Security.Add(new OpenApiSecurityRequirement
                {
                    [
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        }
                    ] = Array.Empty<string>()
                });
            }
        }
    }
}
