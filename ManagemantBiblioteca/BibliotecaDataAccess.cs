using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ManagemantBiblioteca
{
    public class BibliotecaDataAccess
    {
        private readonly string _connectionString;

        public BibliotecaDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Autor> GetAutori()
        {
            var autori = new List<Autor>();
            using var connection = new SqlConnection(_connectionString);
            const string query = "SELECT AutorID, Nume, Prenume, TaraOrigine FROM Autori";
            using var command = new SqlCommand(query, connection);
            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                autori.Add(new Autor
                {
                    AutorID = (int)reader["AutorID"],
                    Nume = reader["Nume"].ToString(),
                    Prenume = reader["Prenume"].ToString(),
                    TaraOrigine = reader["TaraOrigine"].ToString()
                });
            }
            return autori;
        }

        public List<Carte> GetCarti()
        {
            var carti = new List<Carte>();
            using var connection = new SqlConnection(_connectionString);
            const string query = "SELECT CarteID, Titlu, AnPublicare, Gen, AutorID FROM Carti";
            using var command = new SqlCommand(query, connection);
            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                carti.Add(new Carte
                {
                    CarteID = (int)reader["CarteID"],
                    Titlu = reader["Titlu"].ToString(),
                    AnPublicare = (int)reader["AnPublicare"],
                    Gen = reader["Gen"].ToString(),
                    AutorID = (int)reader["AutorID"]
                });
            }
            return carti;
        }

        public List<Membru> GetMembri()
        {
            var membri = new List<Membru>();
            using var connection = new SqlConnection(_connectionString);
            const string query = "SELECT MembruID, Nume, Prenume, DataInscriere, Email, Telefon FROM Membri";
            using var command = new SqlCommand(query, connection);
            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                membri.Add(new Membru
                {
                    MembruID = (int)reader["MembruID"],
                    Nume = reader["Nume"].ToString(),
                    Prenume = reader["Prenume"].ToString(),
                    DataInscriere = (DateTime)reader["DataInscriere"],
                    Email = reader["Email"].ToString(),
                    Telefon = reader["Telefon"].ToString()
                });
            }
            return membri;
        }

        public List<Imprumut> GetImprumuturi()
        {
            var imprumuturi = new List<Imprumut>();
            using var connection = new SqlConnection(_connectionString);
            const string query = "SELECT ImprumutID, MembruID, CarteID, DataImprumut, DataScadenta FROM Imprumuturi";
            using var command = new SqlCommand(query, connection);
            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                imprumuturi.Add(new Imprumut
                {
                    ImprumutID = (int)reader["ImprumutID"],
                    MembruID = (int)reader["MembruID"],
                    CarteID = (int)reader["CarteID"],
                    DataImprumut = (DateTime)reader["DataImprumut"],
                    DataScadenta = reader["DataScadenta"] as DateTime?
                });
            }
            return imprumuturi;
        }
        
        public List<Returnare> GetReturnari()
        {
            var returnari = new List<Returnare>();
            using var connection = new SqlConnection(_connectionString);
            const string query = "SELECT ReturnareID, ImprumutID, DataReturnare, Observatii FROM Returnari";
            using var command = new SqlCommand(query, connection);
            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                returnari.Add(new Returnare
                {
                    ReturnareID = (int)reader["ReturnareID"],
                    ImprumutID = (int)reader["ImprumutID"],
                    DataReturnare = (DateTime)reader["DataReturnare"],
                    Observatii = reader["Observatii"].ToString()
                });
            }
            return returnari;
        }
        
        public void AddMember(Membru membru)
        {
            using var connection = new SqlConnection(_connectionString);
            const string query = "INSERT INTO Membri (Nume, Prenume, DataInscriere, Email, Telefon) VALUES (@Nume, @Prenume, @DataInscriere, @Email, @Telefon)";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Nume", membru.Nume);
            command.Parameters.AddWithValue("@Prenume", membru.Prenume);
            command.Parameters.AddWithValue("@DataInscriere", membru.DataInscriere);
            command.Parameters.AddWithValue("@Email", membru.Email);
            command.Parameters.AddWithValue("@Telefon", membru.Telefon);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public void RemoveMember(Membru membru)
        {
            using var connection = new SqlConnection(_connectionString);
            const string query = "DELETE FROM Membri WHERE MembruID = @MembruID";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MembruID", membru.MembruID);
            connection.Open();
            command.ExecuteNonQuery();
        }
        public void AddImprumut(Imprumut imprumut)
        {
            using var connection = new SqlConnection(_connectionString);
            const string query = "INSERT INTO Imprumuturi (MembruID, CarteID, DataImprumut, DataScadenta) VALUES (@MembruID, @CarteID, @DataImprumut, @DataScadenta)";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MembruID", imprumut.MembruID);
            command.Parameters.AddWithValue("@CarteID", imprumut.CarteID);
            command.Parameters.AddWithValue("@DataImprumut", imprumut.DataImprumut);
            command.Parameters.AddWithValue("@DataScadenta", imprumut.DataScadenta ?? (object)DBNull.Value);
            connection.Open();
            command.ExecuteNonQuery();
        }
        public void RemoveImprumut(Imprumut imprumut)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            // 1. Șterge întâi din Returnari
            const string deleteReturnari = "DELETE FROM Returnari WHERE ImprumutID = @ImprumutID";
            using (var cmdReturnari = new SqlCommand(deleteReturnari, connection))
            {
                cmdReturnari.Parameters.AddWithValue("@ImprumutID", imprumut.ImprumutID);
                cmdReturnari.ExecuteNonQuery();
            }

            // 2. Apoi din Imprumuturi
            const string deleteImprumut = "DELETE FROM Imprumuturi WHERE ImprumutID = @ImprumutID";
            using (var cmdImprumut = new SqlCommand(deleteImprumut, connection))
            {
                cmdImprumut.Parameters.AddWithValue("@ImprumutID", imprumut.ImprumutID);
                cmdImprumut.ExecuteNonQuery();
            }
        }

        public void AddReturnare(Returnare returnare)
        {
            using var connection = new SqlConnection(_connectionString);
            const string query = "INSERT INTO Returnari (ImprumutID, DataReturnare, Observatii) VALUES (@ImprumutID, @DataReturnare, @Observatii)";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ImprumutID", returnare.ImprumutID);
            command.Parameters.AddWithValue("@DataReturnare", returnare.DataReturnare);
            command.Parameters.AddWithValue("@Observatii", returnare.Observatii);
            connection.Open();
            command.ExecuteNonQuery();
        }
        public List<SituatieImprumut> GetSituatieImprumuturi()
        {
            var situatie = new List<SituatieImprumut>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("GetSituatieImprumuturi", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                situatie.Add(new SituatieImprumut
                {
                    MembruID = (int)reader["MembruID"],
                    Nume = reader["Nume"].ToString(),
                    Prenume = reader["Prenume"].ToString(),
                    Carte = reader["Carte"] == DBNull.Value ? "—" : reader["Carte"].ToString(),
                    DataImprumut = reader["DataImprumut"] as DateTime?,
                    DataScadenta = reader["DataScadenta"] as DateTime?,
                    DataReturnare = reader["DataReturnare"] as DateTime?,
                    Observatii = reader["Observatii"] == DBNull.Value ? "—" : reader["Observatii"].ToString()
                });
            }

            return situatie;
        }
    }

}
