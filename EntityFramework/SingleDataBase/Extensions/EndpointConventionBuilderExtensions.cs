namespace SingleDataBase.Extensions;

public static class EndpointConventionBuilderExtensions
{
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
