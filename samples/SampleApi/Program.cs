using System.Collections;
using Bolt.IocScanner;
using Bolt.ServerDrivenUI.Core.Elements;
using Bolt.ServerDrivenUI.Extensions.Web;
using Microsoft.FeatureManagement;
using SampleApi.Features.Home;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFeatureManagement();

var currentAssembly = typeof(HomePageController).Assembly;
var typesToSupportSerialization = new[]
{
    typeof(IElement),
    typeof(IMetaData),
    typeof(IUiAction),
};
builder.Services.AddServerDrivenUiForWeb( builder.Configuration, new EnsembleOptions
{
    EnableFeatureFlag = true,
    TypesToScan = new []
    {
        (currentAssembly, typesToSupportSerialization),
        (typeof(Sample.Elements.Stack).Assembly, typesToSupportSerialization)
    }
});
builder.Services.AddControllers();
builder.Services.Scan(new []{currentAssembly}, new IocScannerOptions
{
    SkipWhenAutoBindMissing = true
});

builder.Services.AddHttpClient("api-sample2", client =>
{
    client.BaseAddress = new Uri("http://localhost:5133");
    client.Timeout = TimeSpan.FromMilliseconds(500);
});

builder.Services.AddHttpClient("api-sample2", client =>
{
    client.BaseAddress = new Uri("http://localhost:5134");
    client.Timeout = TimeSpan.FromMilliseconds(1000);
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

app.Run();