using Refit;

namespace http_to_curl.Api;

public interface IYodaTranslationApi
{
    [Headers("ApiKey: 123456789", "Accept-Language: en")]
    [Post("/translate/yoda.json")]
    Task<YodaTranslationResponse> Translate(YodaTranslationRequest request);
}

public class YodaTranslationRequest
{
    public string Text { get; set; }
}

public class YodaTranslationResponse
{
    public Success Success { get; set; }
    public Contents Contents { get; set; }
}

public partial class Success
{
    public long Total { get; set; }
}

public class Contents
{
    public string Translation { get; set; }
    public string Text { get; set; }
    public string Translated { get; set; }
}