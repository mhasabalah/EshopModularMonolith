var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddCatalogModule(configuration)
                .AddOrderingModule(configuration)
                .AddBasketModule(configuration);

var app = builder.Build();


app.UseCatalogModule()
   .UseOrderingModule()
   .UseBasketModule();

app.Run();
