using System.Collections;
using Bolt.IocScanner;
using Bolt.Sdui.Core;
using Ensemble.Extensions.Web;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFeatureManagement();

var currentAssembly = typeof(SampleApi.Elements.Stack).Assembly;
builder.Services.AddEnsemble( builder.Configuration, new EnsembleOptions
{
    EnableFeatureFlag = true,
    TypesToScan = new []
    {
        (currentAssembly, new []
        {
            typeof(IElement),
            typeof(IMetaData),
            typeof(IUiAction),
        })
    }
});
builder.Services.AddControllers();
builder.Services.Scan(new []{currentAssembly}, new IocScannerOptions
{
    SkipWhenAutoBindMissing = true
});

var app = builder.Build();
app.MapControllers();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


// app.MapGet("/weatherforecast", () =>
//     {
//         var forecast = Enumerable.Range(1, 5).Select(index =>
//                 new WeatherForecast
//                 (
//                     DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//                     Random.Shared.Next(-20, 55),
//                     summaries[Random.Shared.Next(summaries.Length)]
//                 ))
//             .ToArray();
//         return forecast;
//     })
//     .WithName("GetWeatherForecast")
//     .WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}