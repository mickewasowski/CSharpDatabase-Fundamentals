namespace _09IncreaseAgeStoredProcedure
{
    using System;
    using _01InitialSetup;
    using System.Data.SqlClient;

    class StartUp
    {
        static void Main(string[] args)
        {
            int id = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string uspGetOlder = "EXEC usp_GetOlder @id";

                using (SqlCommand command = new SqlCommand(uspGetOlder,connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        string name = (string)reader[0];
                        int age = (int)reader[1];

                        Console.WriteLine($"{name} – {age} years old");
                    }
                }
            }
        }
    }
}
