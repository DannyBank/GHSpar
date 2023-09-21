using GHSpar.Abstractions;
using GHSpar.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
InitializeBusinessRules(builder.Services);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void InitializeBusinessRules(IServiceCollection services)
{
    AppContext.SetSwitch("Npgsql.EnableStoredProcedureCompatMode", true);
    services.AddTransient<DbHelper>();
    services.AddTransient<IDbServiceHelper, DbServiceHelper>();
    services.AddTransient<IGameService, GameService>();
    services.AddTransient<IMomoHelper, MomoHelper>();
    services.AddTransient<ISmsHelper, SmsHelper>();
}