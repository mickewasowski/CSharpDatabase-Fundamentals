namespace _03MinionsNames
{
    using System;
    using System.Data.SqlClient;
    using _01InitialSetup;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                int id = int.Parse(Console.ReadLine());

                string villain = "SELECT Name FROM Villains WHERE Id = @Id";

                SqlCommand command = new SqlCommand(villain, connection);
                command.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = command.ExecuteReader();

                string villainName = "";

                if (reader.HasRows == false)
                {
                    Console.WriteLine($"No villain with ID {id} exists in the database.");
                    return;
                }
                else
                {
                    using (reader)
                    {
                        while (reader.Read())
                        {
                            villainName = (string)reader[0];
                        }
                    }

                    Console.WriteLine($"Villain: {villainName}");
                }

                string minionsInfo = @"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum,
                                         m.Name, 
                                         m.Age
                                    FROM MinionsVillains AS mv
                                    JOIN Minions As m ON mv.MinionId = m.Id
                                   WHERE mv.VillainId = @Id
                                ORDER BY m.Name";

                SqlCommand minions = new SqlCommand(minionsInfo, connection);
                minions.Parameters.AddWithValue("@Id", id);

                SqlDataReader mReader = minions.ExecuteReader();

                if (mReader.HasRows == false)
                {
                    Console.WriteLine("(no minions)");
                    return;
                }
                else
                {
                    using (mReader)
                    {
                        while (mReader.Read())
                        {
                            long rowNumber = (long)mReader[0];
                            string minionName = (string)mReader[1];
                            int age = (int)mReader[2];

                            Console.WriteLine($"{rowNumber}. {minionName} {age}");
                        }
                    }
                }
            }
        }
    }
}
