using System;
using Npgsql;

namespace ChessGame.DAL;

public class UtilityDB
{
    private static string connectionString =
        "Host=70.48.140.178:47020;Username=projectuser;Password=CodeMonkeys1;Database=ChessGameDB";


    public static bool ValidatePlayer(string username, string password)
    {
        using var conn = GetConnection();
        using var cmd =
            new NpgsqlCommand("Select Count(*) from players where Username = (@pUsername) and Password = (@pPassword)", conn)
            {
                Parameters =
                {
                    new("@pUsername", username),
                    new("@pPassword", password)
                }
            };

        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                if (Convert.ToInt32(reader["count"].ToString()) == 1)
                {
                    conn.Close();
                    return true;
                } 
            }
        }
        
        conn.Close();
        return false;
    }

    private static NpgsqlConnection GetConnection()
    {
        using var dataSource = NpgsqlDataSource.Create(connectionString);
        
        return dataSource.OpenConnection();
    }
}