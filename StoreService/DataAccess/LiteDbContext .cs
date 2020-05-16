using LiteDB;
using Microsoft.Extensions.Options;
using StoreService.DataAccess.Abstractions;

namespace StoreService.DataAccess
{
    public class LiteDbContext : ILiteDbContext
    {
        public LiteDatabase Database { get; }

        public LiteDbContext(IOptions<LiteDbOptions> options)
        {
            Database = new LiteDatabase(options.Value.DatabaseLocation);
        }
    }
}