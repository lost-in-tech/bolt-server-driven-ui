using Bolt.IocScanner;
using Bolt.ServerDrivenUI.Core.Elements;
using Bolt.ServerDrivenUI.Extensions.Web;
using Microsoft.FeatureManagement;
using SampleApi2.Features.AppShell;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFeatureManagement();

var currentAssembly = typeof(AppShellController).Assembly;
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