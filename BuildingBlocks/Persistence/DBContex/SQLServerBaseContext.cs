using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Persistence.DBContex
{
    public class SQLServerBaseContext : DbContext
    {
        public SQLServerBaseContext(DbContextOptions options) : base(options)
        {
        }

        // You can add common behavior here if needed (logging, interceptors, etc)
    }
}
