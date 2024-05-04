using Bolt.IocScanner;
using Bolt.ServerDrivenUI;
using Bolt.ServerDrivenUI.Core.Elements;
using Bolt.ServerDrivenUI.Extensions.ExternalSource;
using Bolt.ServerDrivenUI.Extensions.Web;
using Bolt.ServerDrivenUI.Extensions.Web.Endpoints;
using Bookshop.ServerDrivenUI.Api.Features.NotFound;
using Bookshop.ServerDrivenUI.Api.Features.Shared.AppShell;
using Bookshop.ServerDriveUI.Elements;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddServerDrivenUiForWeb(builder.Configuration, new ServerDrivenWebOptions
    {
        TypesToScanForPolymorphicSerialization = new []
        {
            (typeof(Stack).Assembly, Array.Empty<Type>())
        }
    }).AddServerDrivenUiExternalSource(builder.Configuration);

builder.Services.Scan<Program>(new IocScannerOptions
{
    SkipWhenAutoBindMissing = true
});

builder.Services.TryAdd(ServiceDescriptor.Transient(typeof(IScreenSectionProvider<>), typeof(AppShellProvider<>)));
builder.Services.TryAdd(ServiceDescriptor.Transient(typeof(IFallbackScreenComposer<>), typeof(FallbackScreenComposer<>)));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy  =>
        {
            policy.WithOrigins("http://localhost:3000","*")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.MapEndpoints();

app.Run();