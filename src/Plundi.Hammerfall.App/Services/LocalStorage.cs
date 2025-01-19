using System.Text.Json;
using Microsoft.JSInterop;

namespace Plundi.Hammerfall.App.Services;

public class LocalStorage
{
    private readonly IJSRuntime _jsRuntime;

    public LocalStorage(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task SetItemAsync<T>(string key, T value, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentNullException(nameof(key));
        }

        var serializedValue = JsonSerializer.Serialize(value);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", cancellationToken, key, serializedValue).ConfigureAwait(false);
    }

    public async Task<T?> GetItemAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentNullException(nameof(key));
        }

        var result = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", cancellationToken, key).ConfigureAwait(false);
        return result is not null ? JsonSerializer.Deserialize<T>(result) : default;
    }

    public async Task RemoveItemAsync(string key, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentNullException(nameof(key));
        }

        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", cancellationToken, key).ConfigureAwait(false);
    }

    public async Task ClearAsync(CancellationToken cancellationToken = default)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.clear", cancellationToken).ConfigureAwait(false);
    }
}