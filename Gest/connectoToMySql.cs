using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Bel3
{
    public class connectoToMySql
    {
        public MySqlConnection cnMySQL;
        public MySqlCommand cmdMySQL;
        public MySqlDataReader reader;
        public string server, database, user, password;
        public connectoToMySql(string server, string database, string user, string password)
        {
            //set your connection string. 
            //NOTE: I am a big supporter of having the connection
            //stored in the web.config, not inline like this
            this.server = server;
            this.database = database;
            this.user = user;
            this.password = password;
            string fuuuck = "driver=MySQL ODBC 3.51 Driver;Host=host;Database=nome_db;uid=xuser;Password= password;Port=3306;";
            string connString = "Provider=sqloledb;Data Source=" + server + ";" +
                    "Database=" + database + ";" +
                    "Uid=" + user + ";" +
                    "Pwd=" + password + ";";
            //create your mySQL connection
            string myconn = "User ID=" + user + ";Password=" + password + ";Host=" + server + ";Port=3306;Database=" + database + ";Protocol=TCP;Compress=false;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;";
            cnMySQL = new MySqlConnection(fuuuck);



        }
    }
}
