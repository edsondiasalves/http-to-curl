using http_to_curl.Api;
using http_to_curl.Handlers;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddRefitClient<INationalizeApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.nationalize.io"))
    .AddHttpMessageHandler<HttpToCurlHandler>();

builder.Services
    .AddRefitClient<IYodaTranslationApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.funtranslations.com"))
    .AddHttpMessageHandler<HttpToCurlHandler>();

builder.Services.AddTransient<HttpToCurlHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
