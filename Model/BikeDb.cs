using System;
using System.Collections.Generic;

namespace Crudtoso_api.Model;

public partial class BikeDb
{
    public int? ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? Category { get; set; }

    public string? Price { get; set; }

    public int? StockQuantity { get; set; }

    public string? Supplier { get; set; }

    public DateTime? DateAdded { get; set; }
}
