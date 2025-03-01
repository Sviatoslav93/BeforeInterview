using System.ComponentModel.DataAnnotations;
using MediatR;
using Result;
using Store.Entities;

namespace Store.Features.Stores.Create;

public class CreateStoreRequest : IRequest<Result<Guid>>
{
    [Required]
    [Length(1, Entities.Store.NameMaxLength)]
    public string Name { get; set; } = null!;

    [Length(1, Entities.Store.WebsiteUriMaxLength)]
    public string? WebsiteUri { get; set; }
}
