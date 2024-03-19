using Bolt.Polymorphic.Serializer;
using Bolt.ServerDrivenUI.Core.Elements;
using SampleBlazorWeb.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("api-home-page", (sp, client) =>
{
    client.BaseAddress = new Uri("http://localhost:5163");
});

builder.Services.AddPolymorphicSerializer(opt =>
{
    opt.AddSupportedType(typeof(IElement).Assembly, 
        typeof(IElement), 
        typeof(IUiAction), 
        typeof(IMetaData));

    opt.AddSupportedType(typeof(Sample.Elements.Direction).Assembly, typeof(IElement),
        typeof(IUiAction),
        typeof(IMetaData));
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();