using Bolt.IocScanner;
using Bolt.ServerDrivenUI.Extensions.ExternalSource;
using Bolt.ServerDrivenUI.Extensions.Web;
using Bolt.ServerDrivenUI.Extensions.Web.Endpoints;
using Bookshop.ServerDriveUI.Elements;
using Bookshop.ServerDriveUI.Elements.Layouts;

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

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "test",
        policy  =>
        {
            policy.WithOrigins("http://localhost:3000/","*")
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
app.UseCors("test");
app.MapEndpoints();

app.Run();