WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Host.UseSerilog((context, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(context.Configuration));

Assembly basketAssembly = typeof(BasketModule).Assembly;
Assembly catalogAssembly = typeof(CatalogModule).Assembly;

builder.Services.AddCarterWithAssemblies(basketAssembly, catalogAssembly);
builder.Services.AddMediatR(basketAssembly, catalogAssembly);

// add redis cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = configuration.GetConnectionString("Redis");
});

builder.Services.AddMassTransitWithAssemblies(basketAssembly, catalogAssembly);

builder.Services.AddCatalogModule(configuration)
    .AddOrderingModule(configuration)
    .AddBasketModule(configuration);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

WebApplication app = builder.Build();

app.MapCarter();
app.UseSerilogRequestLogging();

app.UseCatalogModule()
    .UseOrderingModule()
    .UseBasketModule();

app.UseExceptionHandler(options => { });

app.Run();