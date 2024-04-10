using System;
using System.Linq;
using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications;
public sealed class OrderFilterPaginatedSpecification:Specification<Order>
{
    public OrderFilterPaginatedSpecification(int skip,int take,string?buyerId,DateTimeOffset?orderDateStart,DateTimeOffset?orderDateEnd,decimal?orderPriceTotalStart,decimal?orderPriceTotalEnd,int?orderItemCountMin,int?orderItemCountMax,int?catalogItemId,string?catalogItemName):base()
    {
        if (take is 0)
            take = int.MaxValue;

        Query
            .Include(x => x.OrderItems).ThenInclude(x => x.ItemOrdered)
            .Where(x => (buyerId == null || string.IsNullOrEmpty(buyerId) || x.BuyerId.Contains(buyerId)) &&
                        (orderDateStart == null || x.OrderDate >= orderDateStart) &&
                        (orderDateEnd == null || x.OrderDate <= orderDateEnd) &&
                        (orderPriceTotalStart == null ||
                         x.OrderItems.Sum(z => z.Units * z.UnitPrice) >= orderPriceTotalStart) &&
                        (orderPriceTotalEnd == null ||
                         x.OrderItems.Sum(z => z.Units * z.UnitPrice) <= orderPriceTotalEnd) &&
                        (orderItemCountMin == null || x.OrderItems.Count > orderItemCountMin) &&
                        (orderItemCountMax == null || x.OrderItems.Count < orderItemCountMax) &&
                        (catalogItemId == null || x.OrderItems.Any(z => z.ItemOrdered.CatalogItemId == catalogItemId) &&
                            (catalogItemName == null || string.IsNullOrEmpty(catalogItemName) ||
                             x.OrderItems.Any(y => y.ItemOrdered.ProductName.Contains(catalogItemName)))
                        )

            )
            .Skip(skip).Take(take);

    }
    public OrderFilterPaginatedSpecification(string? buyerId, DateTimeOffset? orderDateStart, DateTimeOffset? orderDateEnd, decimal? orderPriceTotalStart, decimal? orderPriceTotalEnd, int? orderItemCountMin, int? orderItemCountMax, int? catalogItemId, string? catalogItemName) : base()
    {

        Query
            .Include(x => x.OrderItems).ThenInclude(x => x.ItemOrdered)
            .Where(x => (buyerId == null || string.IsNullOrEmpty(buyerId) || x.BuyerId.Contains(buyerId)) &&
                        (orderDateStart == null || x.OrderDate >= orderDateStart) &&
                        (orderDateEnd == null || x.OrderDate <= orderDateEnd) &&
                        (orderPriceTotalStart == null ||
                         x.OrderItems.Sum(z => z.Units * z.UnitPrice) >= orderPriceTotalStart) &&
                        (orderPriceTotalEnd == null ||
                         x.OrderItems.Sum(z => z.Units * z.UnitPrice) <= orderPriceTotalEnd) &&
                        (orderItemCountMin == null || x.OrderItems.Count > orderItemCountMin) &&
                        (orderItemCountMax == null || x.OrderItems.Count < orderItemCountMax) &&
                        (catalogItemId == null || x.OrderItems.Any(z => z.ItemOrdered.CatalogItemId == catalogItemId) &&
                            (catalogItemName == null || string.IsNullOrEmpty(catalogItemName) ||
                             x.OrderItems.Any(y => y.ItemOrdered.ProductName.Contains(catalogItemName)))
                        )

            );

    }

}
