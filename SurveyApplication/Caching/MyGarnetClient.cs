using Garnet.client;
using SurveyApplication.Interfaces;

namespace SurveyApplication.Caching;


public class MyGarnetClient : IGarnetClient
{
    private readonly string Address = "127.0.0.1";
    private readonly int Port = 3278;

    public async Task<string> GetValue(string key)
    {
        using var db = new GarnetClient(Address, Port, null);
        await db.ConnectAsync();
        return await db.StringGetAsync(key);
    }

    public async Task SetValue(string key, string value)
    {
        using var db = new GarnetClient(Address, Port, null);
        await db.ConnectAsync();
        await db.StringSetAsync(key, value);
    }

    public async Task DeleteValue(string key)
    {
        using var db = new GarnetClient(Address, Port, null);
        await db.ConnectAsync();
        await db.KeyDeleteAsync(key);
    }
}
