using GenerateDataWeb.Models;
using GenerateDataWeb.Models.GenerateDataWeb.Models;
using GenerateDataWeb.Repositories;
using System.Data;

namespace GenerateDataWeb.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repo;

        public PersonService(IPersonRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<string>> SubmitPersonDataAsync(PersonWrapper input)
        {
            var errorList = new List<string>();

            // VALIDASI: Setiap baris ke-100, jika hobinya "Tidur" => error
            for (int i = 99; i < input.Payload.Count; i += 100)
            {
                var item = input.Payload[i];
                if (item.HobiName?.Trim().ToLower() == "tidur")
                {
                    errorList.Add($"Terdapat error pada baris {i + 1} tidak menyukai hobi tidur");
                }
            }

            if (errorList.Any()) return errorList;

            // Insert ke DB
            var dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("nama", typeof(string));
            dt.Columns.Add("genderId", typeof(int));
            dt.Columns.Add("genderName", typeof(string));
            dt.Columns.Add("HobiName", typeof(string));
            dt.Columns.Add("age", typeof(int));

            foreach (var item in input.Payload)
            {
                dt.Rows.Add(item.Id, item.Nama, item.GenderId, item.GenderName, item.HobiName, item.Age);
            }

            await _repo.InsertPersonBulkAsync(dt);
            return new List<string>(); // no error
        }
    }

}
