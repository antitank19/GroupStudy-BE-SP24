using DataLayer.DBObject;
using RepositoryLayer.Interface;
using ServiceLayer.Interface.Db;

namespace ServiceLayer.ClassImplement.Db;

public class DocumentFileService : IDocumentFileService
{
    private IRepoWrapper repos;

    public DocumentFileService(IRepoWrapper repos)
    {
        this.repos = repos;
    }

    
    public IQueryable<DocumentFile> GetList()
    {
        return repos.DocumentFiles.GetList();
    }

    public Task CreateDocumentFile(DocumentFile documentFile)
    {
        return repos.DocumentFiles.CreateAsync(documentFile);
    }

    public Task UpdateDocumentFile(DocumentFile documentFile)
    {
        return repos.DocumentFiles.UpdateAsync(documentFile);
    }

    public Task<DocumentFile> GetById(int id)
    {
        return repos.DocumentFiles.GetByIdAsync(id);
    }

    public Task DeleteDocumentFile(int id)
    {
        return repos.DocumentFiles.RemoveAsync(id);
    }

    public IQueryable<DocumentFile> GetListByGroupId(int groupId)
    {
        var result = repos.DocumentFiles.GetList().Where(file => file.GroupId == groupId);
        //if (null != approved && approved == true)
        //{
        //    result = result.Where(file => file.Approved == true);
        //}
        
        return result;
    }

    public IQueryable<DocumentFile> GetListByAccountId(int accountId)
    {
        var result = repos.DocumentFiles.GetList().Where(file => file.AccountId == accountId);
        return result;
    }

    public IQueryable<DocumentFile> GetListFileByDate(int groupId, DateTime month)
    {
        var result = repos.DocumentFiles.GetList().Where(file => file.GroupId == groupId && file.CreatedDate == month);

        return result;
    }
}