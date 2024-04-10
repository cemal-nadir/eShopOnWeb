﻿using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BlazorShared;
using BlazorShared.Models;
using Microsoft.Extensions.Options;

namespace BlazorAdmin.Services;

public class HttpService
{
    private readonly HttpClient _httpClient;
    private readonly ToastService _toastService;
    private readonly string _apiUrl;


    public HttpService(HttpClient httpClient, IOptions<BaseUrlConfiguration> baseUrlConfiguration, ToastService toastService)
    {
        _httpClient = httpClient;
        _toastService = toastService;
        _apiUrl = baseUrlConfiguration.Value.ApiBase;
    }

    public async Task<T> HttpGet<T>(string uri,CancellationToken cancellationToken=default)
        where T : class
    {
        var result = await _httpClient.GetAsync($"{_apiUrl}{uri}", cancellationToken);
        if (!result.IsSuccessStatusCode)
        {
            return null;
        }

        return await FromHttpResponseMessage<T>(result, cancellationToken);
    }

    public async Task<T> HttpDelete<T>(string uri, int id, CancellationToken cancellationToken = default)
        where T : class
    {
        var result = await _httpClient.DeleteAsync($"{_apiUrl}{uri}/{id}", cancellationToken);
        if (!result.IsSuccessStatusCode)
        {
            return null;
        }

        return await FromHttpResponseMessage<T>(result, cancellationToken);
    }

    public async Task<T> HttpPost<T>(string uri, object dataToSend, CancellationToken cancellationToken = default)
        where T : class
    {
        var content = ToJson(dataToSend);

        var result = await _httpClient.PostAsync($"{_apiUrl}{uri}", content, cancellationToken);
        if (!result.IsSuccessStatusCode)
        {
            var exception = JsonSerializer.Deserialize<ErrorDetails>(await result.Content.ReadAsStringAsync(cancellationToken), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            _toastService.ShowToast($"Error : {exception.Message}", ToastLevel.Error);

            return null;
        }

        return await FromHttpResponseMessage<T>(result, cancellationToken);
    }

    public async Task<T> HttpPut<T>(string uri, object dataToSend, CancellationToken cancellationToken = default)
        where T : class
    {
        var content = ToJson(dataToSend);

        var result = await _httpClient.PutAsync($"{_apiUrl}{uri}", content, cancellationToken);
        if (result.IsSuccessStatusCode)
        {
            return await FromHttpResponseMessage<T>(result, cancellationToken);
        }

        _toastService.ShowToast("Error", ToastLevel.Error);
        return null;

    }

    private StringContent ToJson(object obj)
    {
        return new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");
    }

    private async Task<T> FromHttpResponseMessage<T>(HttpResponseMessage result, CancellationToken cancellationToken = default)
    {
        return JsonSerializer.Deserialize<T>(await result.Content.ReadAsStringAsync(cancellationToken), new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }
}
