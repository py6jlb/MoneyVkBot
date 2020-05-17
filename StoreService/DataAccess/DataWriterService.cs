using StoreService.DataAccess.Abstractions;

namespace StoreService.DataAccess
{
    public class DataWriterService : IDataWriterService
    {
        private readonly IDataWriterService _writer;

        public DataWriterService(IDataWriterService writer)
        {
            _writer = writer;
        }
    }
}