using Microsoft.Data.Sqlite;
using System.Data;

namespace MyCourse.Models.Services.Infrastructure
{
    public class SqliteDatabaseAccessor : IDatabaseAccessor
    {
        public DataSet Query(FormattableString formattableQuery)
        {
            // CREAZIONE STRINGA QUERY (con parametri)
            var queryArgs = formattableQuery.GetArguments();    // Prendiamo i valori inseriti nella stringa tramite interpolazione
            var sqliteParams = new List<SqliteParameter>(); // Creaiamo una lista dove ospitare i parametri

            for(var i = 0; i < queryArgs.Length; i++)   // Per ogni parametro trovato nella stringa
            {
                var parameter = new SqliteParameter(i.ToString(), queryArgs[i]);    // Creiamo un nuovo oggetto parametro (come nome gli diamo il contatore del ciclo)
                sqliteParams.Add(parameter);    // Aggiungiamo il parametro alla lista
                queryArgs[i] = "@"+i;   // Nella stringa sostituiamo il valore con il riferimento (@<nome parametro>)
            }

            string query = formattableQuery.ToString(); // Trasformiamo la FormattableString in una stringa

            // CONNESSIONE ED ESECUZIONE
            using (var conn = new SqliteConnection("Data Source=Data/MyCourse.db")) // Il blocco using esegue in automatico (alla fine delle istruzioni) il metodo .Dispose() di ogni oggetto di una classe che implementa IDisposable
            {
                conn.Open();

                using (var cmd = new SqliteCommand(query, conn))   // Passiamo la query e la connessione già 
                {
                    cmd.Parameters.AddRange(sqliteParams);  // Inseriamo i parametri all'interno della stringa query

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
