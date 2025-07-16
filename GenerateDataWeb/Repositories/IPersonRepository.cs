using System.Data;

namespace GenerateDataWeb.Repositories
{
    public interface IPersonRepository
    {
        Task InsertPersonBulkAsync(DataTable dt);
    }
}
