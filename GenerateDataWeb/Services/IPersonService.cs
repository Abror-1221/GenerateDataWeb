using GenerateDataWeb.Models.GenerateDataWeb.Models;

namespace GenerateDataWeb.Services
{
    public interface IPersonService
    {
        Task<List<string>> SubmitPersonDataAsync(PersonWrapper input);
    }
}
