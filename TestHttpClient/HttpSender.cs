using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace TestHttpClient;

public static class HttpSender
{
    public static async Task RequestAsync()
    {
        using var httpClient = new HttpClient();
        httpClient.Timeout = TimeSpan.FromMinutes(2);
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var content = new
        {
            message = "ping"
        };

       var res=  await httpClient.PostAsJsonAsync("http://localhost:8001/ping", content);
       Console.WriteLine(res.Content.ToString());
    }
}