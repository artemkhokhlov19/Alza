﻿namespace Alza.Contracts.DataObjects.Products;

/// <summary>
/// Represents a response containing information about product
/// </summary>
public class ProductResponse
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
