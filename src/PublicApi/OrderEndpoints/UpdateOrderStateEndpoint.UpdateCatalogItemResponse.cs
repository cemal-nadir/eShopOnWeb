using System;
using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class UpdateCatalogItemResponse : BaseResponse
{
    public UpdateCatalogItemResponse(Guid correlationId) : base(correlationId)
    {
    }

    public UpdateCatalogItemResponse()
    {
    }

    public CatalogItemDto CatalogItem { get; set; }
}
