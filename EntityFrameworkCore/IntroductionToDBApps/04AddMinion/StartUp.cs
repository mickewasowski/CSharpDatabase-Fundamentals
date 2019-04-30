namespace _04AddMinion
{
    using System;
    using System.Data.SqlClient;
    using _01InitialSetup;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            Console.Write("Minions: ");
            string[] minionTokens = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string minionName = minionTokens[0];
            int minionAge = int.Parse(minionTokens[1]);
            string townName = minionTokens[2];

            Console.Write("Villain: ");
            string[] villainTokens = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string villainName = villainTokens[0];


            //check town
            int? townId = GetTownIdByName(townName);
            if (townId == null)
            {
                InsertTown(townName);
                townId = GetTownIdByName(townName);
            }

            //check villain
            int? villainId = GetVillainIdByName(villainName);
            if (villainId == null)
            {
                InsertVillain(villainName);
                villainId = GetVillainIdByName(villainName);
            }

            //check minion
            int? minionId = GetMinionIdByName(minionName);
            if (minionId == null)
            {
                InsertMinion(minionName, minionAge, townId);
                minionId = GetMinionIdByName(minionName);
            }

            //add to minionsVillains
            AddMinionsAsVillainsServer(minionId, villainId, minionName, villainName);

        }

        private static void AddMinionsAsVillainsServer(int? minionId, int? villainId, string minionName, string villainName)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string minionsVillainsQuery = "INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@minionId, @villainId)";
                SqlCommand command = new SqlCommand(minionsVillainsQuery, connection);
                command.Parameters.AddWithValue("@villainId", villainId);
                command.Parameters.AddWithValue("@minionId", minionId);

                command.ExecuteNonQuery();

                Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
            }
        }

        private static void InsertVillain(string villainName)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string villainInsertQuery = "INSERT INTO Villains (Name, EvilnessFactorId)  VALUES (@villainName, 4)";
                SqlCommand command = new SqlCommand(villainInsertQuery, connection);
                command.Parameters.AddWithValue("@villainName", villainName);

                command.ExecuteNonQuery();
                Console.WriteLine($"Villain {villainName} was added to the database.");
            }
        }

        private static int? GetVillainIdByName(string villainName)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string villainIdQuery = "SELECT Id FROM Villains WHERE Name = @Name";
                SqlCommand command = new SqlCommand(villainIdQuery, connection);
                command.Parameters.AddWithValue("@Name", villainName);

                return (int?)command.ExecuteScalar();
            }
        }

        private static void InsertMinion(string minionName, int minionAge, int? townId)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string minionInsertQuery = "INSERT INTO Minions (Name, Age, TownId) VALUES (@name, @age, @townId)";
                SqlCommand command = new SqlCommand(minionInsertQuery, connection);
                command.Parameters.AddWithValue("@name", minionName);
                command.Parameters.AddWithValue("@age", minionAge);
                command.Parameters.AddWithValue("@townId", townId);

                command.ExecuteNonQuery();
            }
        }

        private static int? GetMinionIdByName(string minionName)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string minionIdQuery = "SELECT Id FROM Minions WHERE Name = @Name";
                SqlCommand command = new SqlCommand(minionIdQuery, connection);
                command.Parameters.AddWithValue("@Name", minionName);

                return (int?)command.ExecuteScalar();
            }
        }

        private static void InsertTown(string townName)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string townInsertQuery = "INSERT INTO Towns (Name) VALUES (@townName)";
                SqlCommand command = new SqlCommand(townInsertQuery,connection);
                command.Parameters.AddWithValue("@townName", townName);

                command.ExecuteNonQuery();

                Console.WriteLine($"Town {townName} was added to the database.");
            }
        }

        private static int? GetTownIdByName(string townName)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string townIdSelect = "SELECT Id FROM Towns WHERE Name = @townName";

                SqlCommand command = new SqlCommand(townIdSelect, connection);
                command.Parameters.AddWithValue("@townName", townName);

                return (int?)command.ExecuteScalar();  //nullable int => int?
            }
        }
    }
}
