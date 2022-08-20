using System.Globalization;
using http_to_curl.Api;
using Microsoft.AspNetCore.Mvc;

namespace sniffing.Controllers;

[ApiController]
[Route("[controller]")]
public class CompositeApiController : ControllerBase
{
    private readonly INationalizeApi _nationalizeApi;
    private readonly IYodaTranslationApi _yodaTranslationApi;

    public CompositeApiController(INationalizeApi nationalizeApi, IYodaTranslationApi yodaTranslationApi)
    {
        _nationalizeApi = nationalizeApi;
        _yodaTranslationApi = yodaTranslationApi;
    }

    [HttpGet("/{name}")]
    public async Task<String> TranslateEnglishToYoda(string name)
    {
        //Invoke nationalize api to get country code
        var nationalize = await _nationalizeApi.Nationalize(name);
        var mostProblableCountry = nationalize.Country.OrderByDescending(c => c.Probability).FirstOrDefault();
        var country = mostProblableCountry == null ? "Earth" : new RegionInfo(mostProblableCountry.Country_Id).DisplayName;

        //Yoda translation
        var request = new YodaTranslationRequest { Text = $"Master {name} has lost {country}." };
        var yodaTranslation = await _yodaTranslationApi.Translate(request);

        return yodaTranslation.Contents.Translated;
    }
}