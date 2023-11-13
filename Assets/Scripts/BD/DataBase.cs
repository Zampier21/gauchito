using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using Player;

namespace DataBase
{
    public class Player : MonoBehaviour
    {
        public static Player instance = null;
        private Movement player1Movement;

        private void Start()
        {
            player1Movement = Players.instance.getMovementScript(Players.instance.getPlayer1());

        }

        public void createTable()
        {
            //Create table for player informations
            CreateDatabase.instance.dbcommand.CommandText = "CREATE TABLE IF NOT EXISTS Player (" +
                "id INT PRIMARY KEY UNIQUE," +
                "maxHP INT NOT NULL," +
                "currentHP INT NOT NULL," +
                "defense INT NOT NULL," +
                "positionX VARCHAR(36) NOT NULL," +
                "positionY VARCHAR(36) NOT NULL," +
                "positionZ VARCHAR(36) NOT NULL)";
            CreateDatabase.instance.dbcommand.ExecuteNonQuery();
        }

        public void fillTable()
        {
            //Insert player informations
            //Check if already inserted
            CreateDatabase.instance.dbcommand.CommandText = "Select id from Player";
            IDataReader reader = CreateDatabase.instance.dbcommand.ExecuteReader();

            if (!reader.Read())
            {
                reader.Close();

                createPlayer();
            }
            else
            {
                reader.Close();
            }
        }

        private void createPlayer()
        {
            //Create Player 1
            Vector3 player1Position = PlayerWarpPositions.instance.getPlayer1CreatePosition().position;
            CreateDatabase.instance.dbcommand.CommandText = "INSERT INTO Player(id, maxHP, currentHP, defense, positionX, positionY, positionZ) " +
                "values (1, 100, 100, 0, '"+ player1Position.x + "', '" + player1Position.y + "', '" + player1Position.z + "')";
            CreateDatabase.instance.dbcommand.ExecuteNonQuery();
        }

        public void updatePlayerPosition(Vector3 localPosition, int playerId)
        {
            if (CreateDatabase.instance.dbconnection != null)
            {
                //Update position
                CreateDatabase.instance.dbcommand.CommandText = "Update Player set " +
                            "positionX = '" + localPosition.x + "'," +
                            "positionY = '" + localPosition.y + "'," +
                            "positionZ = '" + localPosition.z + "' where Id = " + playerId + "";
                CreateDatabase.instance.dbcommand.ExecuteNonQuery();
            }
        }

        public Vector3 getPlayerPosition(int playerId)
        {
            Vector3 playerPosition = new Vector3(0f,0f,0f);

            if (CreateDatabase.instance.dbconnection != null)
            {
                //Select position
                CreateDatabase.instance.dbcommand.CommandText = "Select positionX, positionY, positionZ from Player where id = " + playerId + "";
                IDataReader reader = CreateDatabase.instance.dbcommand.ExecuteReader();
                if (reader.Read())
                {
                    string valueX = reader.GetString(0);
                    string valueY = reader.GetString(1);
                    string valueZ = reader.GetString(2);
                    playerPosition = new Vector3(
                        float.Parse(valueX),
                        float.Parse(valueY),
                        float.Parse(valueZ));

                    reader.Close();
                }
                else
                {
                    reader.Close();
                }
            }

            return playerPosition;
        }

        public int damageToPlayer(int damage, int playerId)
        {
            int currentHP = 0;

            if (CreateDatabase.instance.dbconnection != null)
            {
                //Select current HP
                CreateDatabase.instance.dbcommand.CommandText = "Select currentHP from Player where id = " + playerId + "";
                IDataReader reader = CreateDatabase.instance.dbcommand.ExecuteReader();
                if (reader.Read())
                {
                    currentHP = reader.GetInt32(0);
                    reader.Close();
                }
                else
                {
                    reader.Close();
                }

                //Set new current HP
                if (currentHP - damage < 0)
                    currentHP = 0;
                else
                    currentHP -= damage;

                //Update current HP
                CreateDatabase.instance.dbcommand.CommandText = "Update Player set " +
                            "currentHP = " + currentHP + " where Id = " + playerId + "";
                CreateDatabase.instance.dbcommand.ExecuteNonQuery();
            }

            return currentHP;
        }

        public int getMaxHP(int playerId)
        {
            CreateDatabase.instance.dbcommand.CommandText = "Select maxHP from Player where id = " + playerId + "";
            IDataReader reader = CreateDatabase.instance.dbcommand.ExecuteReader();
            if (reader.Read())
            {
                int value = reader.GetInt32(0);
                reader.Close();

                return value;
            }
            {
                reader.Close();
                return -1;
            }
        }

        public int getCurrentHP(int playerId)
        {
            CreateDatabase.instance.dbcommand.CommandText = "Select currentHP from Player where id = " + playerId + "";
            IDataReader reader = CreateDatabase.instance.dbcommand.ExecuteReader();
            if (reader.Read())
            {
                int value = reader.GetInt32(0);
                reader.Close();

                return value;
            }
            {
                reader.Close();
                return -1;
            }
        }

        public void revivePlayer(int playerId)
        {
            int maxHP = 0;

            if (CreateDatabase.instance.dbconnection != null)
            {
                //Select current HP
                CreateDatabase.instance.dbcommand.CommandText = "Select maxHP from Player where id = " + playerId + "";
                IDataReader reader = CreateDatabase.instance.dbcommand.ExecuteReader();
                if (reader.Read())
                {
                    maxHP = reader.GetInt32(0);
                    reader.Close();
                }
                else
                {
                    reader.Close();
                }

                //Update current HP
                CreateDatabase.instance.dbcommand.CommandText = "Update Player set " +
                            "currentHP = " + maxHP + " where id = " + playerId + "";
                CreateDatabase.instance.dbcommand.ExecuteNonQuery();
            }
        }

        public int getDefense(int playerId)
        {
            CreateDatabase.instance.dbcommand.CommandText = "Select defense from Player where id = " + playerId + "";
            IDataReader reader = CreateDatabase.instance.dbcommand.ExecuteReader();
            if (reader.Read())
            {
                int value = reader.GetInt32(0);
                reader.Close();

                return value;
            }
            else
            {
                reader.Close();
                return -1;
            }
        }

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(this.gameObject);
        }
    }
}