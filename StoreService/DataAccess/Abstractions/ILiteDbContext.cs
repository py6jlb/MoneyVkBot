using LiteDB;

namespace StoreService.DataAccess.Abstractions
{
    public interface ILiteDbContext
    {
        LiteDatabase Database { get; }
    }
}