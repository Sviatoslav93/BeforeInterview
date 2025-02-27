using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace SingleDataBase.Extensions;

public static class EndpointConventionBuilderExtensions
{
    private const string StoreCodeHeader = "X-StoreCode";

    public static TBuilder WithStoreCode<TBuilder>(this TBuilder builder) where TBuilder : IEndpointConventionBuilder
    {
        builder.Add(endpointBuilder =>
        {
            endpointBuilder.Metadata.Add(new StoreCodeMetadata());
        });
        return builder;
    }

}
public class StoreCodeMetadata
{
}
