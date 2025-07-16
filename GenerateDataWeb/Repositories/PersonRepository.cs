using GenerateDataWeb.Repositories;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace GenerateDataWeb.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly string _config;

        public PersonRepository(IConfiguration config)
        {
            _config = config.GetConnectionString("DefaultConnection");
        }

        public async Task InsertPersonBulkAsync(DataTable dt)
        {
            using (var conn = new SqlConnection(_config))
            using (var cmd = new SqlCommand("sp_Insert_PersonData", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                var param = cmd.Parameters.AddWithValue("@PersonList", dt);
                param.SqlDbType = SqlDbType.Structured;
                param.TypeName = "dbo.UDT_PersonList";

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

    }
}
