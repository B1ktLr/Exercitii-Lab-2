using ManagemantBiblioteca;
public class Program
{
    public static async Task Main()
    {
        while (true)
        {
            Console.WriteLine("\nAlege o optiune:");
            Console.WriteLine("1 - Afiseaza autori si carti");
            Console.WriteLine("2 - Adauga membru");
            Console.WriteLine("3 - Sterge membru");
            Console.WriteLine("4 - Adauga imprumut");
            Console.WriteLine("5 - Sterge imprumut");
            Console.WriteLine("6 - Afiseaza situatie imprumuturi");
            Console.WriteLine("7 - Testeaza Dapper CRUD");
            Console.WriteLine("0 - Iesire");

            Console.Write("Introdu optiunea: ");
            string optiune = Console.ReadLine();

            switch (optiune)
            {
                case "1": BibliotecaData(); break;
                case "2": AddMemberExample(); break;
                case "3": RemoveMemberExample(); break;
                case "4": AddImprumutExample(); break;
                case "5": RemoveImprumutExample(); break;
                case "6": AfisareSituatieImprumuturi(); break;
                case "7": await BibliotecaDapperExample(); break;
                case "0":
                    Console.WriteLine("Program incheiat.");
                    return;
                default:
                    Console.WriteLine("Optiune invalida.");
                    break;
            }
        }

    }

    public static void BibliotecaData()
    {
        string connectionString = "Data Source=localhost\\SQLEXPRESS; Integrated Security=SSPI; Initial Catalog=biblioteca; TrustServerCertificate=True";

        
        BibliotecaDataAccess dataAccess = new BibliotecaDataAccess(connectionString);

        // Afisam autorii
        List<Autor> autori = dataAccess.GetAutori();
        Console.WriteLine("Autori:");
        foreach (Autor autor in autori)
        {
            Console.WriteLine($"ID: {autor.AutorID}, Nume: {autor.Prenume} {autor.Nume}, Tara: {autor.TaraOrigine}");
        }

        // Afisam cartile
        List<Carte> carti = dataAccess.GetCarti();
        Console.WriteLine("\nCarti:");
        foreach (Carte carte in carti)
        {
            Console.WriteLine($"ID: {carte.CarteID}, Titlu: {carte.Titlu}, An: {carte.AnPublicare}, Gen: {carte.Gen}, AutorID: {carte.AutorID}");
        }
    }
    static void AddMemberExample()
    {
        string connectionString = "Data Source=localhost\\SQLEXPRESS; Integrated Security=SSPI; Initial Catalog=biblioteca; TrustServerCertificate=True";
        BibliotecaDataAccess dataAccess = new BibliotecaDataAccess(connectionString);

        Membru newMember = new Membru
        {
            Nume = "Klein",
            Prenume = "David",
            DataInscriere = DateTime.Now,
            Email = "davidklein@email.com",
            Telefon = "07721189652"
        };

        dataAccess.AddMember(newMember);
        Console.WriteLine("Membru adaugat.");
    }

    static void RemoveMemberExample()
    {
        string connectionString = "Data Source=localhost\\SQLEXPRESS; Integrated Security=SSPI; Initial Catalog=biblioteca; TrustServerCertificate=True";
        var dataAccess = new BibliotecaDataAccess(connectionString);

        // Obține ultimul membru
        var membri = dataAccess.GetMembri();
        var ultim = membri.OrderByDescending(m => m.MembruID).FirstOrDefault();

        if (ultim != null)
        {
            dataAccess.RemoveMember(ultim);
            Console.WriteLine($"Membru șters: {ultim.Prenume} {ultim.Nume} (ID: {ultim.MembruID})");
        }
        else
        {
            Console.WriteLine("Nu există membri de șters.");
        }
    }


    static void AddImprumutExample()
    {
        string connectionString = "Data Source=localhost\\SQLEXPRESS; Integrated Security=SSPI; Initial Catalog=biblioteca; TrustServerCertificate=True";
        var dataAccess = new BibliotecaDataAccess(connectionString);

        var membri = dataAccess.GetMembri();
        var imprumuturi = dataAccess.GetImprumuturi();

        var membruFaraImprumut = membri.FirstOrDefault(m => !imprumuturi.Any(i => i.MembruID == m.MembruID));
        if (membruFaraImprumut != null)
        {
            var carti = dataAccess.GetCarti();
            var carte = carti.FirstOrDefault();
            if (carte != null)
            {
                var imprumutNou = new Imprumut
                {
                    MembruID = membruFaraImprumut.MembruID,
                    CarteID = carte.CarteID,
                    DataImprumut = DateTime.Today,
                    DataScadenta = DateTime.Today.AddDays(14)
                };
                dataAccess.AddImprumut(imprumutNou);
                Console.WriteLine($"Imprumut adaugat pentru {membruFaraImprumut.Prenume} {membruFaraImprumut.Nume} cu cartea {carte.Titlu}.");
            }
            else
            {
                Console.WriteLine("Nu exista carți disponibile.");
            }
        }
        else
        {
            Console.WriteLine("Toti membrii au deja un imprumut.");
        }
    }


    static void RemoveImprumutExample()
    {
        string connectionString = "Data Source=localhost\\SQLEXPRESS; Integrated Security=SSPI; Initial Catalog=biblioteca; TrustServerCertificate=True";
        var dataAccess = new BibliotecaDataAccess(connectionString);

        var imprumuturi = dataAccess.GetImprumuturi();
        var ultim = imprumuturi.OrderByDescending(i => i.ImprumutID).FirstOrDefault();

        if (ultim != null)
        {
            dataAccess.RemoveImprumut(ultim);
            Console.WriteLine($"Imprumut sters (ID: {ultim.ImprumutID})");
        }
        else
        {
            Console.WriteLine("Nu exista iprumuturi de sters.");
        }
    }


    static void AfisareSituatieImprumuturi()
    {
        string connectionString = "Data Source=localhost\\SQLEXPRESS; Integrated Security=SSPI; Initial Catalog=biblioteca; TrustServerCertificate=True";
        var dataAccess = new BibliotecaDataAccess(connectionString);
        var situatie = dataAccess.GetSituatieImprumuturi();

        // Grupăm dupa membru
        var grupuri = situatie
            .GroupBy(s => new { s.MembruID, s.Nume, s.Prenume });

        Console.WriteLine("\n Situatie Imprumuturi \n");

        foreach (var grup in grupuri)
        {
            Console.WriteLine($"Membru: {grup.Key.Prenume} {grup.Key.Nume} (ID: {grup.Key.MembruID})");

            // filtram doar imprumuturile cu carți
            var imprumuturi = grup
                .Where(g => !string.IsNullOrWhiteSpace(g.Carte))
                .DistinctBy(g => new { g.Carte, g.DataImprumut, g.DataScadenta, g.DataReturnare, g.Observatii })
                .ToList();

            if (imprumuturi.Any())
            {
                foreach (var i in imprumuturi)
                {
                    string dataImprumut = i.DataImprumut?.ToShortDateString() ?? "-";
                    string dataScadenta = i.DataScadenta?.ToShortDateString() ?? "-";
                    string dataReturnare = i.DataReturnare?.ToShortDateString() ?? "-";
                    string observatii = i.Observatii ?? "-";

                    Console.WriteLine($" {i.Carte}  Imprumut: {dataImprumut}  Scadent: {dataScadenta}  Returnare: {dataReturnare}  Obs: {observatii}");
                }
            }
            else
            {
                Console.WriteLine(" Fara Imprumuturi.");
            }

            Console.WriteLine();
        }
    }
    public static async Task BibliotecaDapperExample()
    {
        string connectionString = "Data Source=localhost\\SQLEXPRESS; Integrated Security=SSPI; Initial Catalog=biblioteca; TrustServerCertificate=True";
        var dapperAccess = new DataAccess.BibliotecaDapper(connectionString);

        // insert
        var membruNou = new DataAccess.Membru
        {
            Nume = "Moromete",
            Prenume = "Liviu",
            DataInscriere = DateTime.Today,
            Email = "test@dapper.ro",
            Telefon = "0700123456"
        };

        int id = await dapperAccess.AddMembruAsync(membruNou);
        Console.WriteLine($"[INSERT] Membru adaugat cu ID: {id}");

        // get data
        var membri = await dapperAccess.GetAllMembriAsync();
        Console.WriteLine("\n[SELECT] Membri existenti:");
        foreach (var m in membri)
            Console.WriteLine($"{m.MembruID}: {m.Prenume} {m.Nume}");

        // update
        var membruUpdate = membri.FirstOrDefault(m => m.MembruID == id);
        if (membruUpdate != null)
        {
            membruUpdate.Nume = "Actualizat";
            var updated = await dapperAccess.UpdateMembruAsync(membruUpdate);
            Console.WriteLine(updated ? $"[UPDATE] Membru ID {id} actualizat." : "Eroare la actualizare.");
        }

        // delete
        var deleted = await dapperAccess.DeleteMembruAsync(id);
        Console.WriteLine(deleted ? $"[DELETE] Membru ID {id} sters." : "Stergere esuata.");
    }

}