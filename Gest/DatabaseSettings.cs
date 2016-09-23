using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Bel3
{
   

    public partial class DatabaseSettings : Form
    {
        public string mServer = "127.0.0.1";

        public string mDatabase = "Bel3";

        public string mUser = "root";

        public string mPassword = "paramecio";

        public DatabaseSettings()
        {
            InitializeComponent();
            // The files used in this example are created in the topic 
            // How to: Write to a Text File. You can change the path and 
            // file name to substitute text files of your own. 

            // Example #1 
            // Read the file as one string. 
            string[] s;
            if (!File.Exists(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/dbconf.ini"))
            {
                string lines = "127.0.0.1\r\nbel3\r\nroot\r\nparamecio\r\nDriver={MySQL ODBC 3.51 Driver};";

                // Write the string to a file.
                System.IO.StreamWriter file = new System.IO.StreamWriter(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/dbconf.ini");
                file.WriteLine(lines);

                file.Close();
                s = System.IO.File.ReadAllLines(@"dbconf.ini");
            }
            else
            {
                s = System.IO.File.ReadAllLines(@"dbconf.ini");
            }
            mServer = s[0];

       mDatabase =  s[1];

       mUser = s[2];

       mPassword = s[3];
            ServerAddress.Text = s[0];
            Database.Text = s[1];
            User.Text = s[2];
            Password.Text = s[3];
        }

        private void DatabaseSettings_Load(object sender, EventArgs e)
        {

        }

        private void Accept_Click(object sender, EventArgs e)
        {
            mServer = ServerAddress.Text;
            mDatabase = Database.Text;
            mUser = User.Text;
            mPassword = Password.Text;
            string lines = mServer + "\r\n" + mDatabase + "\r\n" + mUser + "\r\n" + mPassword + "\r\nDriver={MySQL ODBC 3.51 Driver};";

            // Write the string to a file.
            System.IO.StreamWriter file = new System.IO.StreamWriter(System.IO.Path.GetDirectoryName(Application.ExecutablePath) +"/dbconf.ini");
            file.WriteLine(lines);

            file.Close();
            this.Hide();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


 

      
    }
}
