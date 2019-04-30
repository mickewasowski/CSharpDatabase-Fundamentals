namespace _07PrintAllMinionNames
{
    using System;
    using _01InitialSetup;
    using System.Data.SqlClient;
    using System.Collections.Generic;

    class StartUp
    {
        static void Main(string[] args)
        {
            List<string> minionNames = new List<string>();
            minionNames = GetMinionNames();

            if (minionNames.Count % 2 == 0)
            {
                minionNames = OrderNamesEvens(minionNames);
            }
            else
            {
                minionNames = OrderNamesOdd(minionNames);
            }
            Console.WriteLine($"{string.Join("\r\n", minionNames)}");
        }

        private static List<string> OrderNamesOdd(List<string> minionNames)
        {
            List<string> ordered = new List<string>();

            for (int i = 0; i < minionNames.Count / 2; i++)
            {
                ordered.Add(minionNames[i]);
                ordered.Add(minionNames[minionNames.Count - 1 - i]);
                if (i + 1 == minionNames.Count / 2)
                {
                    ordered.Add(minionNames[i + 1]);
                    break;
                }
            }
            return ordered;
        }

        private static List<string> OrderNamesEvens(List<string> minionNames)
        {
            List<string> ordered = new List<string>();
            for (int i = 0; i < minionNames.Count / 2; i++)
            {
                ordered.Add(minionNames[i]);
                ordered.Add(minionNames[minionNames.Count - 1 - i]);
            }
            return ordered;
        }

        private static List<string> GetMinionNames()
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();
                List<string> minionNames = new List<string>();

                string getMinionNamesQuery = "SELECT Name FROM Minions";
                SqlCommand command = new SqlCommand(getMinionNamesQuery, connection);

                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        minionNames.Add((string)reader[0]);
                    }
                }
                return minionNames;
            }
        }
    }
}
