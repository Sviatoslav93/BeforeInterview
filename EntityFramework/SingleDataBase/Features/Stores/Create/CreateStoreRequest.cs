using System.ComponentModel.DataAnnotations;
using MediatR;
using Result;
using SingleDataBase.Entities;

namespace SingleDataBase.Features.Stores.Create;

public class CreateStoreRequest : IRequest<Result<Guid>>
{
    [Required]
    [Length(1, Store.NameMaxLength)]
    public string Name { get; set; } = null!;

    [Length(1, Store.WebsiteUriMaxLength)]
    public string? WebsiteUri { get; set; }
}
