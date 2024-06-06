using System.Data.SQLite;

namespace TelegramBotSQL
{
    internal class SQL
    {
        public static string Connection
        {
            get
            {
                string databasePath = Path.Combine(Environment.CurrentDirectory, "users.db");
                return $"Data Source={databasePath}; Version=3;";
            }
        }
        public static void CreateTable()
        {
            using (var connection = new SQLiteConnection(Connection))
            {
                connection.Open();
                var command = new SQLiteCommand();
                command.Connection = connection;
                command.CommandText = @"CREATE TABLE IF NOT EXISTS UserInfo (Id INTEGER PRIMARY KEY AUTOINCREMENT, Username TEXT NOT NULL, EnterDate TEXT NOT NULL, LastDate TEXT NOT NULL)";
                command.ExecuteNonQuery();
            }
        }
        public static List<string> GetUsers()
        {
            using (var connection = new SQLiteConnection(Connection))
            {
                var users = new List<string>();
                connection.Open();
                var command = new SQLiteCommand();
                command.Connection = connection;
                command.CommandText = $"Select username from UserInfo";
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(reader.GetString(0));
                }
                return users;
            }
        }
        public static void RegisterUser(string username)
        {
            using (var connection = new SQLiteConnection(Connection))
            {
                connection.Open();
                var command = new SQLiteCommand();
                command.Connection = connection;
                if (IsUserExists(username))
                {
                    command.CommandText = $"update UserInfo set LastDate = '{DateTime.Now.ToString("yyyy-MM-dd")}' where username like '{username}'";
                }
                else
                {
                    command.CommandText = $"insert into UserInfo(Username,EnterDate,LastDate) values('{username}'" + $",'{DateTime.Now.ToString("yyyy-MM-dd")}','{DateTime.Now.ToString("yyyy-MM-dd")}') ";
                command.ExecuteNonQuery();
                }
            }
        }

        public static bool IsUserExists(string username)
        {
            using (var connection = new SQLiteConnection(Connection))
            {
                connection.Open();
                var command = new SQLiteCommand();
                command.Connection = connection;
                command.CommandText = $"Select 1 from UserInfo where username like '{username}'";
                return command.ExecuteScalar() != null;
            }
        }
    }
}