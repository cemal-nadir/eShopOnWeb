using System.Collections.Generic;

namespace BlazorShared.Models;
public class OrdersResponse
{
    public List<Orders> Orders { get; set; } = new();
    public int PageCount { get; set; }
}
