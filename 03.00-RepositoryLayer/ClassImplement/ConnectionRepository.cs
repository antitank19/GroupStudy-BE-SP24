using DataLayer.DBContext;
using DataLayer.DBObject;
using RepositoryLayer.Interface;

namespace RepositoryLayer.ClassImplement
{
    internal class ConnectionRepository : BaseRepo<Connection, string>, IConnectionRepository
    {
        public ConnectionRepository(GroupStudyContext dbContext) : base(dbContext)
        {
        }

        public async Task CreateConnectionSignalrAsync(Connection connection)
        {
            dbContext.Connections.Add(connection);
            await dbContext.SaveChangesAsync();
        }
    }
}