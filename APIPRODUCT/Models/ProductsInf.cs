using System;
using System.Collections.Generic;

namespace APIPRODUCT.Models;

public partial class ProductsInf
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public int? IdCategory { get; set; }

    public virtual Category? oCategory { get; set; }
}
