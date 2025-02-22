namespace SingleDataBase.Entities;

public class Store
{
    #region Constants
    public const int NameMaxLength = 256;
    public const int WebsiteUriMaxLength = 1024;
    #endregion

    public Guid StoreCode { get; set; }
    public required string Name { get; set; }
    public string? WebsiteUri { get; set; }
    public Guid UserId { get; set; }
}
