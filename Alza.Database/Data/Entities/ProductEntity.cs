using Alza.Core.Models;

namespace Alza.Database.Data.Entities;

public class ProductEntity : BaseEntity<int>
{
    public string Name { get; set; }

    public string? Description { get; set; }

    public string ImgUri { get; set; }

    public decimal Price { get; set; }
}
