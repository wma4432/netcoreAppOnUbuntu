using System;
using MySQL.Data.EntityFrameworkCore;
using MySQL.Data;
using MySql.Data.MySqlClient;
using System.IO;

namespace ConsoleApp2
{
    class Program
    {
        public static string wtsDBConnStr = "server=wtsdb.watchtower-security.net;user=hqadmin;database=watchtowerShared;port=3306;password=WatchHQ2";
        public static string hqAdminConnStr = "server=sysdb.watchtower-security.net;user=hqadmin;database=hqadmin;port=3306;password=WatchHQ2";


        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");

            MySqlConnection hqConn = new MySqlConnection
            {
                //ConnectionString = "server=<ServerAddress>;user id=<User>;password=<Password>;persistsecurityinfo=True;port=<Port>;database=sakila"
                ConnectionString = hqAdminConnStr
            };
            hqConn.Open();

            MySqlCommand hqCommand = new MySqlCommand("SELECT * FROM hqadmin.sites;", hqConn);

            using (MySqlDataReader reader = hqCommand.ExecuteReader())
            {
                System.Console.WriteLine("Category Id\t\tName\t\tLast Update");
                while (reader.Read())
                {
                    string row = $"{reader["site_id"]}\t\t{reader["site_name"]}\t\t{reader["site_status"]}";                  

                    Console.WriteLine(String.Format("{0,-10}  {1,-30}  {2,5}", reader["site_id"], reader["site_name"], reader["site_status"]));
                  
                    //System.Console.WriteLine(row);
                }
            }
            Console.ReadLine();

            MySqlConnection wtsDBConn = new MySqlConnection
            {
                //ConnectionString = "server=<ServerAddress>;user id=<User>;password=<Password>;persistsecurityinfo=True;port=<Port>;database=sakila"
                ConnectionString = wtsDBConnStr
            };
            wtsDBConn.Open();

            MySqlCommand command = new MySqlCommand("SELECT * FROM watchtowerShared.sites;", wtsDBConn);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                System.Console.WriteLine("Category Id\t\tName\t\tLast Update");
                while (reader.Read())
                {
                    string row = $"{reader["id"]}\t\t{reader["name"]}\t\t{reader["siteTypeID"]}";
                    System.Console.WriteLine(row);
                }
            }

            string appended = string.Format("{0,5}", "MM");
            appended += string.Format("{0,20}", "45444");
            appended += string.Format("{0,30}", "");

            Console.WriteLine(appended);

            Console.WriteLine(hqConn.Database.ToString() + " Opened ");
            Console.WriteLine(wtsDBConn.Database.ToString()+ " Opened" );
            var term = Environment.GetEnvironmentVariable("term");
            if (term == null)
            {
                Console.WriteLine("No Terminal Set");
            }
            else
            {
                Console.WriteLine("Terminal: {0}\n", term);
            }
            Console.ReadLine();
            hqConn.Close();
            wtsDBConn.Close();
        }
    }
}