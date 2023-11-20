using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;

public class DataBase : MonoBehaviour
{
    public string databaseName = "AudioData.db";

    public void Start()
    {
        string connection = "URI=file:" + Application.dataPath + "/" + databaseName;
        using (IDbConnection dbConnection = new SqliteConnection(connection))
        {
            dbConnection.Open();
            string sql = "CREATE TABLE IF NOT EXISTS Audio (" +
                   "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                   "name TEXT NOT NULL," +
                   "file TEXT NOT NULL);";

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                dbCmd.CommandText = sql;
                dbCmd.ExecuteNonQuery();
            }

            sql = "SELECT * FROM Audio";

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                dbCmd.CommandText = sql;
                IDataReader reader = dbCmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader.GetString(reader.GetOrdinal("NAME"));
                    float volume = reader.GetFloat(reader.GetOrdinal("VOLUME"));

                    AudioManager.instance.SetVolume(name, volume);
                }
                reader.Close();
            }
        }
        InsertAudioData("Music", @"C:\Users\Alemao\Documents\GitHub\gauchito\Assets\Scripts\Audio\FundoMenu.mp3", 0.5f);
    }
public void InsertAudioData(string name, string file, float volume)
{
    string connection = "URI=file:" + Application.dataPath + "/" + databaseName;
    using (IDbConnection dbConnection = new SqliteConnection(connection))
    {
        dbConnection.Open();

        string sql = "INSERT INTO Audio (name, file, volume) VALUES (?, ?, ?)";

        using (IDbCommand dbCmd = dbConnection.CreateCommand())
        {
            dbCmd.CommandText = sql;

            dbCmd.Parameters.Add(new SqliteParameter(DbType.String, name));
            dbCmd.Parameters.Add(new SqliteParameter(DbType.String, file));
            dbCmd.Parameters.Add(new SqliteParameter(DbType.Single, volume));

            dbCmd.ExecuteNonQuery();
        }
    }
}

}