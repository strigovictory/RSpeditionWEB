//global using fuelTranzGRPC;
//using Grpc.Net.Client;
//using RSpeditionWEB.Services.GRPCServices;
using Microsoft.Extensions.DependencyInjection;
using RSpeditionWEB.Configs;
using RSpeditionWEB.Services.ID;
using RSpeditionWEB.Services.ID.Interfaces;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

// Зарегистрировать сервисы
builder.Services.AddServices(builder.Configuration);
//builder.Services.AddGRPC(builder.Configuration);

builder.Services.AddRequestDecompression();

builder.Host.UseSerilog((ctx, lc) => lc
       .WriteTo.Console()
       .ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();

app.UseRequestDecompression();

// Конвейер обработки запроса
ProgramsMidleware.AddLogger(app, builder.Environment);
ProgramsMidleware.AddCulture();
ProgramsMidleware.Configure(app, builder.Environment);

app.Run();

// DI - класс расширения для регистрации зависимостей
#region  
public static class ServicesRegister
{
    #region // gRPC
    //public static void AddGRPC(this IServiceCollection services, IConfiguration config)
    //{
    //    services.AddScoped<FuelTranzGRPC>();

    //    services
    //            .AddGrpcClient<FuelTranzGRPCService.FuelTranzGRPCServiceClient>((services, options) =>
    //            {
    //                var gRPCApi = config.GetSection("HostedUri:ODATA").Value;
    //                options.Address = new Uri(gRPCApi);
    //                options.ChannelOptionsActions.Add((opt) =>
    //                {
    //                    opt.MaxReceiveMessageSize = 4 * 1024 * 1024 * 10; //  Если задано значение null, то размер сообщения не ограничен.
    //                    opt.MaxSendMessageSize = 4 * 1024 * 1024 * 10; //  Если задано значение null, то размер сообщения не ограничен.
    //                });
    //            });

    //}
    #endregion

    public static void AddServices(this IServiceCollection services, IConfiguration config)
    {
        // Connect mapper
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // Cache
        services.AddDistributedMemoryCache();

        // Sessions
        services.AddSession();

        services.AddRazorPages();
        services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

        // Регистрация HttpClient 
        services.AddResponseCompression(opts =>
        {
            opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                new[] { "application/octet-stream" });
        });

        // Adding HttpClient
        services.AddHttpClient("fuelapiclient").ConfigureHttpClient(x =>
        {
            x.Timeout = TimeSpan.FromMinutes(20);
        });

        // DI
        // Добавлен сервис для доступа к контексту запроса
        services.AddHttpContextAccessor();

        // Storage
        services.AddTransient<ProtectedSessionStorage>();

        // Authenticate 
        services.AddAuthentication();
        services.AddAuthorization();
        services.AddTransient<IIdentityHelperService, IdentityHelperService>();
        services.AddTransient<AuthenticationStateProvider, AppAuthenticationStateProvider>();
        services.AddTransient<AppAuthenticationStateProvider>();
        services.Configure<GateWayConfigurations>(config.GetSection("ApiPoints:GateWay"));
        services.Configure<IdentityServerConfigurations>(config.GetSection("ApiPoints:IdentityServer"));

        // Project's API
        services.AddTransient<IFuelApies, FuelApies>();
        services.AddTransient<IApies, Apies>();
        services.AddTransient<MobileApi>();
        services.AddTransient<PersonApi>();
        services.AddTransient<FuelCardsApi>();
        services.AddTransient<FuelCardsEventsApi>();
        services.AddTransient<FuelParserApi>();
        services.AddTransient<FuelProviderApi>();
        services.AddTransient<FuelTransactionApi>();
        services.AddTransient<FuelTypeApi>();
        services.AddTransient<DivisionApi>();
        services.AddTransient<CurrencyApi>();
        services.AddTransient<CountryApi>();
        services.AddTransient<TruckApi>();
        services.AddTransient<TrailerApi>();
        services.AddTransient<FinanceApi>();
        services.AddTransient<GenericApi>();
        services.AddTransient<RideApi>();
        services.AddTransient<RideDriverReportApi>();
        services.AddTransient<KitEventApi>();
        services.AddTransient<AdminApi>();
        services.AddTransient<FuelUploadLogApi>();

        // HTTP
        services.AddTransient<IHttpService, HttpService>();

        // Other services
        services.AddTransient<IDownloadFileService, DownloadFileService>();

        // Добавить сжатие ответов по умолчанию (Brotli и gzip)
        services.AddResponseCompression();
    }
}
#endregion

public static class ProgramsMidleware
{
    // LOGGING
    #region
    public static void AddLogger(WebApplication app, IWebHostEnvironment env)
    {
        var seqHost = "http://seqevent.rgroup-cargo.com";
#if DEBUG
        seqHost = "http://localhost:5341";
#endif
        // Логгирование Serilog + Seq
        if (env.IsProduction())
        {
            Log.Logger = new LoggerConfiguration()
                                                  .MinimumLevel.Information()
                                                  .Enrich.FromLogContext()
                                                  .Enrich.WithProperty("Application", env.ApplicationName)
                                                  .Enrich.WithProperty("Environment", env.EnvironmentName)
                                                  .WriteTo.File(Path.Combine(env.WebRootPath, "logs", $"log-{DateTime.Now.ToShortDateString() ?? string.Empty}.txt"))
                                                  .WriteTo.Seq(seqHost)
                                                  .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code).CreateLogger()
                                                  ;
        }
        else
        {
            Log.Logger = new LoggerConfiguration()
                                                  .MinimumLevel.Information()
                                                  .Enrich.FromLogContext()
                                                  .Enrich.WithProperty("Application", env.ApplicationName)
                                                  .Enrich.WithProperty("Environment", env.EnvironmentName)
                                                  .WriteTo.File(Path.Combine(env.WebRootPath, "logs", $"log-{DateTime.Now.ToShortDateString() ?? string.Empty}.txt"))
                                                  .WriteTo.Seq(seqHost)
                                                  .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code).CreateLogger()
                                                  ;
            app.UseSerilogRequestLogging();
        }
    }
    #endregion

    // CULTURE
    #region
    public static void AddCulture()
    {
        Serilog.Log.Information($"Установлена по умолчанию культура «ru-RU»");

        CultureInfo cultureInfo = new("ru-RU");
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
    }
    #endregion

    // CONFIGURE
    #region
    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        //app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSession();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
        });

        // Подключить сжатие ответов по умолчанию (Brotli и gzip)
        app.UseResponseCompression();
    }
    #endregion
}