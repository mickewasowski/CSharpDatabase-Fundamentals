using System;
using System.Data.SqlClient;
using _01InitialSetup;

namespace _02VillainNames
{
   public class StartUp
    {
        public static void Main(string[] args)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string villansAndMinions = @"SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount  
                                                FROM Villains AS v
                                                JOIN MinionsVillains AS mv ON v.Id = mv.VillainId
                                            GROUP BY v.Id, v.Name
                                              HAVING COUNT(mv.VillainId) > 3
                                            ORDER BY COUNT(mv.VillainId)";

                SqlCommand command = new SqlCommand(villansAndMinions, connection);
                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        string villainName = (string)reader[0];
                        int minionsCount = (int)reader[1];

                        Console.WriteLine($"{villainName} - {minionsCount}");
                    }
                }

                connection.Close();
            }
        }
    }
}
