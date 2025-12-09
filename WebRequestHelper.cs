using System.Net.Http;
using System.Threading.Tasks;

namespace CSharpInterviewMessageProcessor;

public class WebRequestHelper
{
    private readonly HttpClient _httpClient;

    public WebRequestHelper()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> GetAsync(string url)
    {
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public string Get(string url)
    {
        return GetAsync(url).GetAwaiter().GetResult();
    }
}
