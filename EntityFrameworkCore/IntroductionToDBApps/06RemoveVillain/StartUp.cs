namespace _06RemoveVillain
{
    using System;
    using _01InitialSetup;
    using System.Data.SqlClient;

    class StartUp
    {
        static void Main(string[] args)
        {
            int id = int.Parse(Console.ReadLine());

            string villainName = GetVillainName(id);

            if (villainName == null)
            {
                Console.WriteLine("No such villain was found.");
                return;
            }

            int releasedMinions = ReleaseMinions(id);

            RemoveVillainFromDB(id);

            Console.WriteLine($"{villainName} was deleted.");
            Console.WriteLine($"{releasedMinions} minions were released.");
        }

        private static int ReleaseMinions(int id)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string releaseMinionsQuery = @"DELETE FROM MinionsVillains 
                                        WHERE VillainId = @villainId";

                SqlCommand command = new SqlCommand(releaseMinionsQuery, connection);
                command.Parameters.AddWithValue("@villainId",id);

                return (int)command.ExecuteNonQuery();
            }
        }

        private static string GetVillainName(int id)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string villainNameQuery = "SELECT Name FROM Villains WHERE Id = @villainId";
                SqlCommand command = new SqlCommand(villainNameQuery, connection);
                command.Parameters.AddWithValue("@villainId", id);

                return (string)command.ExecuteScalar();
            }
        }

        private static void RemoveVillainFromDB(int id)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string removeVillain = @"DELETE FROM Villains
                                     WHERE Id = @villainId";

                SqlCommand command = new SqlCommand(removeVillain, connection);
                command.Parameters.AddWithValue("@villainId", id);

                command.ExecuteNonQuery();
            }
        }
    }
}
