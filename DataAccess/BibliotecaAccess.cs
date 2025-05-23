using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BibliotecaDapper
    {
        private readonly string _connectionString;

        public BibliotecaDapper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> AddMembruAsync(Membru membru)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = @"INSERT INTO Membri (Nume, Prenume, DataInscriere, Email, Telefon)
                        VALUES (@Nume, @Prenume, @DataInscriere, @Email, @Telefon);
                        SELECT CAST(SCOPE_IDENTITY() as int);";
            return await connection.ExecuteScalarAsync<int>(sql, membru);
        }

        public async Task<List<Membru>> GetAllMembriAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "SELECT * FROM Membri";
            var result = await connection.QueryAsync<Membru>(sql);
            return result.ToList();
        }

        public async Task<Membru?> GetMembruByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "SELECT * FROM Membri WHERE MembruID = @Id";
            return await connection.QueryFirstOrDefaultAsync<Membru>(sql, new { Id = id });
        }

        public async Task<bool> UpdateMembruAsync(Membru membru)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = @"UPDATE Membri
                        SET Nume = @Nume, Prenume = @Prenume, DataInscriere = @DataInscriere, Email = @Email, Telefon = @Telefon
                        WHERE MembruID = @MembruID";
            var affected = await connection.ExecuteAsync(sql, membru);
            return affected > 0;
        }

        public async Task<bool> DeleteMembruAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "DELETE FROM Membri WHERE MembruID = @Id";
            var affected = await connection.ExecuteAsync(sql, new { Id = id });
            return affected > 0;
        }
    }
}
