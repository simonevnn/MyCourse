using Microsoft.Data.Sqlite;
using System.Data;

namespace MyCourse.Models.Services.Infrastructure
{
    public class SqliteDatabaseAccessor : IDatabaseAccessor
    {
        public DataSet Query(string query)
        {
            using (var conn = new SqliteConnection("Data Source=Data/MyCourse.db")) // Il blocco using esegue in automatico (alla fine delle istruzioni) il metodo .Dispose() di ogni oggetto di una classe che implementa IDisposable
            {
                conn.Open();

                using (var cmd = new SqliteCommand(query, conn))   // Passiamo la query e la connessione già 
                {
                    using(var reader = cmd.ExecuteReader())
                    {
                        var dataSet = new DataSet();

                        do
                        {
                            var dataTable = new DataTable();
                            dataSet.Tables.Add(dataTable);  // Un DataSet è una collezione di tabelle di un database
                            dataTable.Load(reader); // Inserisce nel DataTable tutte le righe del risultato
                        } while (!reader.IsClosed); // Ripetiamo tutto finché ci sono tabelle da leggere
                        
                        return dataSet;
                    }
                }
            }
        }
    }
}
