using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;//Adding the Mysql Library

namespace DesktopCRUDNetFramework
{
    class DbClass
    {
        private static MySqlConnection connection;
        private static string server;
        private static string database;
        private static string uid;
        private static string password;
        public DbClass(){
            Initialize();
        }
        public static void Initialize()
        {
         //This is not an optimal solution, the database credentials should be secured in and XML file and parsed from there
         //not exposed in source code due to time limitation imposed by Finals and testing new waters in C# I have chosen to go this route
         server = "localhost";
         database = "partmanager";
         uid ="root";
         password = "ke1923ge";
         string connectionString;
         connectionString = "SERVER=" + server + ";" + "DATABASE=" +
         database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
         connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        public static bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, the 
                //application response is based on
                //the numbers
                //on the error number.
                //The two most common error numbers 
                //when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        System.Windows.MessageBox.Show("Cannot connect to server. Contact administrator");
                        break;

                    case 1045:
                        System.Windows.MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        public static bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return false;
            }
        }
        public void Insert(int id, string part_name, string customer_name, string retailer_name, string price)
        {
            //Working!
            String query = string.Format("INSERT INTO parts(id, part_name,customer_name,retailer_name,item_price) VALUES ('{0}', '{1}','{2}','{3}','{4}')", id, part_name, customer_name, retailer_name, price);
            MySqlCommand cmd = new MySqlCommand(query, DbClass.connection);
            DbClass.OpenConnection();
            cmd.ExecuteNonQuery();
            DbClass.CloseConnection();
        }
        public void Update(int id, string part_name, string customer_name, string retailer_name, string price)
        {
            //Working!
            String query = string.Format("UPDATE parts SET part_name='{1}',customer_name='{2}', retailer_name='{3}', item_price='{4}' WHERE id={0}", id, part_name, customer_name, retailer_name, price);
            MySqlCommand cmd = new MySqlCommand(query, DbClass.connection);
            DbClass.OpenConnection();
            cmd.ExecuteNonQuery();
            DbClass.CloseConnection();
        }

        public void Delete(int id)
        {
            //Working!
            String query = string.Format("DELETE FROM parts WHERE id={0}", id);
            MySqlCommand cmd = new MySqlCommand(query, DbClass.connection);
            DbClass.OpenConnection();
            cmd.ExecuteNonQuery();
            DbClass.CloseConnection();
        }
        public static List<part> Fetch_2(int id_p)
        {
            List<part> parts = new List<part>();
            String query = string.Format("SELECT * FROM parts WHERE id={0}", id_p);
            MySqlCommand cmd = new MySqlCommand(query, DbClass.connection);
            DbClass.OpenConnection();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = (int)reader["id"];
                String part_name = reader["part_name"].ToString();
                String customer = reader["customer_name"].ToString();
                String retailer = reader["retailer_name"].ToString();
                String price = reader["item_price"].ToString();
                part p = new part(id, part_name, customer, retailer, price);
                parts.Add(p);
            }
            reader.Close();
            DbClass.CloseConnection();
            return parts;
        }


        public static List<part> Fetch()
        {
            List<part> parts = new List<part>();
            String query = "SELECT * FROM parts";
            MySqlCommand cmd = new MySqlCommand(query, DbClass.connection);
            DbClass.OpenConnection();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = (int)reader["id"];
                String part_name = reader["part_name"].ToString();
                String customer = reader["customer_name"].ToString();
                String retailer = reader["retailer_name"].ToString();
                String price = reader["item_price"].ToString();
                part p = new part(id, part_name, customer,retailer,price);
                parts.Add(p);
            }
            reader.Close();
            DbClass.CloseConnection();
            return parts;
        }

    }

    public class part {
        public int id {get; private set;}
        public string part_name { get; private set; }
        public string customer_name { get; private set; }
        public string retailer_name { get; private set; }
        public string price { get; private set; }

        public part(int id_c, string part_name_c, string customer_name_c, string retailer_name_c, string price_c)
        {
            id = id_c;
            part_name = part_name_c;
            customer_name = customer_name_c;
            retailer_name = retailer_name_c;
            price = price_c;

        }

    }
}
