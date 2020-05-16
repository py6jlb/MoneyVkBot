using StoreService.DataAccess.Abstractions;

namespace StoreService.DataAccess
{
    public class DataReaderService : IDataReaderService
    {
        private readonly IDataReaderService _reader;

        public DataReaderService(IDataReaderService reader)
        {
            _reader = reader;
        }

    }
}