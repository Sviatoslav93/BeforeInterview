namespace SingleDataBase.Extensions;

public static class EndpointConventionBuilderExtensions
{
    public static TBuilder WithStoreId<TBuilder>(this TBuilder builder) where TBuilder : IEndpointConventionBuilder
    {
        builder.Add(endpointBuilder =>
        {
            endpointBuilder.Metadata.Add(new StoreIdMetadata());
        });
        return builder;
    }
}

public class StoreIdMetadata
{
}
