using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorAdmin.Helpers;
using BlazorShared.Interfaces;
using BlazorShared.Models;

namespace BlazorAdmin.Pages.OrderPage;

public partial class List : BlazorComponent
{
    [Microsoft.AspNetCore.Components.Inject]
    public IOrderService OrderService { get; set; }
    private List<Orders> Orders { get; set; } = new();
    private int PageCount { get; set; } = 1;
    private OrderListRequest RequestFilter { get; set; } = new();
    private int PageSize { get; set; } = int.MaxValue;
    private int PageIndex { get; set; } = 0;
    private Details DetailsComponent { get; set; }
    private ChangeState ChangeStateComponent { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await ReloadOrders();

            CallRequestRefresh();
        }

        await base.OnAfterRenderAsync(firstRender);
    }
    private async void DetailsClick(int id)
    {
        await DetailsComponent.Open(id);
    }
    private async void ChangeStateClick(int id,OrderStateType state)
    {
        await ChangeStateComponent.Open(id,state);
    }
    private async Task ReloadOrders()
    {
        var response = await OrderService.ListAsync(PageSize,PageIndex,RequestFilter);
        Orders = response.Item1;
        PageCount = response.Item2;
        StateHasChanged();
    }
}
