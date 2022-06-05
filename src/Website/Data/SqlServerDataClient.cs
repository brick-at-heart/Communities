using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BrickAtHeart.Communities.Data
{
    public partial class SqlServerDataClient
    {
        public SqlServerDataClient( IOptions<SqlServerDataClientOptions> options,
                                    ILogger<SqlServerDataClient> logger)
        {
            connectionString = options.Value.ConnectionString;
            this.logger = logger;
        }

        private readonly string? connectionString;
        private readonly ILogger<SqlServerDataClient> logger;
    }
}