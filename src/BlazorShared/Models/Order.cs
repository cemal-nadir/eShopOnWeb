using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShared.Models;
public class Order
{
    public string BuyerId { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public OrderState State { get; set; }
    public Address ShipToAddress { get; set; }
    public List<OrderItems> OrderItems { get; set; }
    public decimal Total { get; set; }
}
