﻿namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class CatalogItemOrderedDto
{
    public int CatalogItemId { get; set; }
    public string ProductName { get; set; }
    public string PictureUri { get; set; }
}
