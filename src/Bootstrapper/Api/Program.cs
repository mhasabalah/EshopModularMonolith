var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services
    .AddCarterWithAssemblies(typeof(CatalogModule).Assembly);

builder.Services.AddCatalogModule(configuration)
                .AddOrderingModule(configuration)
                .AddBasketModule(configuration);

var app = builder.Build();

app.MapCarter();

app.UseCatalogModule()
   .UseOrderingModule()
   .UseBasketModule();

app.Run();
