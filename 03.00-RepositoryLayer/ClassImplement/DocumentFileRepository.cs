using DataLayer.DBContext;
using DataLayer.DBObject;
using RepositoryLayer.Interface;

namespace RepositoryLayer.ClassImplement;

public class DocumentFileRepository : BaseRepo<DocumentFile, int>, IDocumentFileRepository
{
    public DocumentFileRepository(GroupStudyContext dbContext) : base(dbContext)
    {
    }

    public IQueryable<DocumentFile> GetList()
    {
        return base.GetList();
    }

    public Task<DocumentFile> GetByIdAsync(int id)
    {
        return base.GetByIdAsync(id);
    }

    public Task CreateAsync(DocumentFile entity)
    {
        return base.CreateAsync(entity);
    }

    public Task UpdateAsync(DocumentFile entity)
    {
        return base.UpdateAsync(entity);
    }

}