namespace _05ChangeTownNamesCasing
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using _01InitialSetup;

    class StartUp
    {
        static void Main(string[] args)
        {
            string countryName = Console.ReadLine();

            int checker = CheckIfCountryHasTowns(countryName);

            if (checker == -1)
            {
                Console.WriteLine("No town names were affected.");
            }
            else
            {
                int affectedRows = UpdateTownsCasing(countryName);
                List<string> townNames = GetTownNames(countryName);

                Console.WriteLine($"{affectedRows} town names were affected. ");
                Console.WriteLine($"[{string.Join(", ",townNames)}]");
            }
        }

        private static int UpdateTownsCasing(string countryName)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string updateTownCasingQuery = @"UPDATE Towns
                                   SET Name = UPPER(Name)
                                 WHERE CountryCode = (SELECT c.Id FROM Countries AS c WHERE c.Name = @countryName)";

                SqlCommand command = new SqlCommand(updateTownCasingQuery, connection);
                command.Parameters.AddWithValue("@countryName", countryName);

                return (int)command.ExecuteNonQuery();
            }
        }

        private static List<string> GetTownNames(string countryName)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string townsForCountryQuery = @"SELECT t.Name 
                                             FROM Towns as t
                                             JOIN Countries AS c ON c.Id = t.CountryCode
                                            WHERE c.Name = @countryName";
                SqlCommand command = new SqlCommand(townsForCountryQuery, connection);
                command.Parameters.AddWithValue("@countryName", countryName);

                SqlDataReader reader = command.ExecuteReader();
                List<string> townNames = new List<string>();
                using (reader)
                {
                    while (reader.Read())
                    {
                        string townName = (string)reader[0];
                        townNames.Add(townName);
                    }
                }
                return townNames;
            }
        }

        private static int CheckIfCountryHasTowns(string countryName)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string townsForCountryQuery = @"SELECT t.Name 
                                             FROM Towns as t
                                             JOIN Countries AS c ON c.Id = t.CountryCode
                                            WHERE c.Name = @countryName";
                SqlCommand command = new SqlCommand(townsForCountryQuery, connection);
                command.Parameters.AddWithValue("@countryName", countryName);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows == false)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
