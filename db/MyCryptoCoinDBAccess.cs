// MyCryptoCoinDBAccess.cs //

using MyCryptoCoin
using System.Data.SqlClient;

namespace MyCryptoCoin
{
    public class MyCryptoCoinDBAccess
    {
        private readonly string connectionString;

        public MyCryptoCoinDBAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void ConnectAndAccessDatabase()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connected to the MyCryptoCoin database.");

                    // Perform your database access operations here
                    // Example:
                    // SqlCommand command = new SqlCommand("SELECT * FROM TableName", connection);
                    // SqlDataReader reader = command.ExecuteReader();
                    // while (reader.Read())
                    // {
                    //     // Access and process the data from the reader
                    //     // Example:
                    //     // string columnName = reader.GetString(0);
                    //     // int columnValue = reader.GetInt32(1);
                    //     // ...
                    // }

                    Console.WriteLine("Finished accessing the MyCryptoCoin database.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error connecting to the database: " + ex.Message);
                }
            }
        }
    }
}