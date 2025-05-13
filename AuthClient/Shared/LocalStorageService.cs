using Microsoft.JSInterop;

public class LocalStorageService
{
    private readonly IJSRuntime _jsRuntime;

    public LocalStorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task SetAsync(string key, string value)
    {
        await _jsRuntime.InvokeVoidAsync("localStorageWrapper.setItem", key, value);
    }

    public async Task<string> GetAsync(string key)
    {
        return await _jsRuntime.InvokeAsync<string>("localStorageWrapper.getItem", key);
    }

    public async Task RemoveAsync(string key)
    {
        await _jsRuntime.InvokeVoidAsync("localStorageWrapper.removeItem", key);
    }
}