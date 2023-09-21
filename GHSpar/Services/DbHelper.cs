using Npgsql;
using System.Data;
using System.Diagnostics;

namespace GHSpar.Services
{
    public class DbHelper 
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DbHelper> _logger;

        public DbHelper(IConfiguration configuration, ILogger<DbHelper> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public NpgsqlCommand CreateCommand(string commandName, string connectionStr = "")
        {
            connectionStr = _configuration.GetConnectionString(string.IsNullOrWhiteSpace(connectionStr) ? "Database" : connectionStr);
            var connection = new NpgsqlConnection(connectionStr);
            var command = new NpgsqlCommand(commandName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            return command;
        }

        [DebuggerHidden]
        public IDbConnection CreateConnection(string connectionStr = "")
        {
            connectionStr = _configuration.GetConnectionString(string.IsNullOrWhiteSpace(connectionStr) ? "Database" : connectionStr);
            var connection = new NpgsqlConnection(connectionStr);
            _logger.LogTrace("Connection to {ConnStr}  was {Status}", connection.ConnectionString, connection.State);
            return connection;
        }
    }
}