using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;
using System;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Models
{
    public class Client
    {
        private string _clientName;
        private int _stylistId;
        private int _id;

        public Client(string clientName, int stylistId, int id=0)
        {
            _clientName = clientName;
            _stylistId = stylistId;
            _id = id;
        }

        public string GetClientName()
        {
            return _clientName;
        }
        public void SetClientName(string ClientName)
        {
            _clientName = ClientName;
        }

        public int GetStylistId()
        {
          return _stylistId;
        }

        public int GetClientId()
        {
            return _id;
        }

        public override bool Equals(System.Object otherClient)
        {
            if (!(otherClient is Client))
            {
                return false;
            }
            else
            {
                Client newClient = (Client) otherClient;
                bool idEquality = this.GetClientId() == newClient.GetClientId();
                bool clientNameEquality = this.GetClientName() == newClient.GetClientName();
                bool stylistEquality = this.GetStylistId() == newClient.GetStylistId();
                return (idEquality && clientNameEquality && stylistEquality);
            }
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO client (client_name, stylist_id) VALUES (@client_name, @stylist_id);";

            MySqlParameter clientName = new MySqlParameter();
            clientName.ParameterName = "@client_name";
            clientName.Value = this._clientName;
            cmd.Parameters.Add(clientName);

            MySqlParameter stylistId = new MySqlParameter();
            stylistId.ParameterName = "@stylist_id";
            stylistId.Value = this._stylistId;
            cmd.Parameters.Add(stylistId);


            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Client> GetAll()
        {
            List<Client> allClients = new List<Client> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM client;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int clientId = rdr.GetInt32(0);
              string clientName = rdr.GetString(1);
              int stylistId = rdr.GetInt32(2);
              Client newClient = new Client(clientName, stylistId, clientId);
              allClients.Add(newClient);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allClients;
        }

        public static Client Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM client WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int clientId = 0;
            string clientName = "";
            int clientStylistId = 0;

            while(rdr.Read())
            {
              clientId = rdr.GetInt32(0);
              clientName = rdr.GetString(1);
              clientStylistId = rdr.GetInt32(2);
            }
            Client newClient = new Client(clientName, clientStylistId, clientId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newClient;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM client;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}