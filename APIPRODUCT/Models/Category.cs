using System;
using System.Collections.Generic;

namespace APIPRODUCT.Models;

public partial class Category
{
    public int IdCategory { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<ProductsInf> ProductsInfs { get; } = new List<ProductsInf>();
}
