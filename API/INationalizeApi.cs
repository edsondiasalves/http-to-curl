using Refit;

namespace http_to_curl.Api;

public interface INationalizeApi
{
    [Headers("User-Agent: SnifferApp")]
    [Get("/")]
    Task<NationalizeResult> Nationalize(string name);
}

public class NationalizeResult
{
    public string Name { get; set; }
    public List<Country> Country { get; set; }
}

public class Country
{
    public string Country_Id { get; set; }
    public double Probability { get; set; }
}