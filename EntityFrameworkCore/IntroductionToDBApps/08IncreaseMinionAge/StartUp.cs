namespace _08IncreaseMinionAge
{
    using System;
    using _01InitialSetup;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Collections.Generic;

    class StartUp
    {
        static void Main(string[] args)
        {
            int[] minionsIds = Console.ReadLine()
                .Split(" ",StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x))
                .ToArray();

            UpdateMinionsNameAndAge(minionsIds);

            Dictionary<string,int> minionTokens = GetMinionsNames();

            foreach (var m in minionTokens)
            {
                Console.WriteLine($"{m.Key} {m.Value}");
            }
        }

        private static Dictionary<string, int> GetMinionsNames()
        {
            Dictionary<string, int> minionTokens = new Dictionary<string, int>();

            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string getMinionTokens = "SELECT Name, Age FROM Minions";
                SqlCommand command = new SqlCommand(getMinionTokens, connection);
                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        minionTokens.Add((string)reader[0], (int)reader[1]);
                    }
                }
            }
            return minionTokens;
        }

        private static void UpdateMinionsNameAndAge(int[] minionsIds)
        {
            for (int i = 0; i < minionsIds.Length; i++)
            {
                using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
                {
                    connection.Open();

                    string updateMinionsQuery = @" UPDATE Minions
                                       SET Name = UPPER(LEFT(Name, 1)) + SUBSTRING(Name, 2, LEN(Name)), Age += 1
                                     WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(updateMinionsQuery, connection);
                    command.Parameters.AddWithValue("@Id", minionsIds[i]);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
