namespace Alza.Contracts.DataObjects.Products;

/// <summary>
/// Represents a response item containinf detailed information about product in a list
/// </summary>
public class ProductListItemResponse
{
    /// <summary>
    /// Gets or sets the ID of the product
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the product
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the image URI of the product
    /// </summary>
    public string ImgUri { get; set; }

    /// <summary>
    /// Gets or sets the price of the product
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the description of the product
    /// </summary>
    public string? Description { get; set; }
}
