using GHSpar.Abstractions;
using GHSpar.Models;
using GHSpar.Services;

namespace GHSpar
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            _logger = logger;
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AppContext.SetSwitch("Npgsql.EnableStoredProcedureCompatMode", true);
            services.AddTransient<DbHelper>();
            services.AddTransient<IDbServiceHelper, DbServiceHelper>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IMomoHelper, MomoHelper>();
            services.AddTransient<ISmsHelper, SmsHelper>();
            services.Configure<AppSettings>(Configuration.GetSection("App"));
            services.Configure<AppStrings>(Configuration.GetSection("Outputs"));
            services.AddControllers();
            _logger.LogInformation("Services Loaded");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseRouting();
            app.UseEndpoints(endpoint => { endpoint.MapControllers(); });
            _logger.LogInformation("Application Middleware Loaded");
        }
    }
}
