using DataLayer.DBObject;

namespace RepositoryLayer.Interface
{
    public interface IConnectionRepository : IBaseRepo<Connection, string>
    {
        public Task CreateConnectionSignalrAsync(Connection connection);
    }
}