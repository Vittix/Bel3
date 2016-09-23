using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace Bel3

{
    public partial class Telefonate : Form
    {
        SerialPort mySerialPort;
        connectoToMySql mConnection;
        public Telefonate(connectoToMySql mcon)
        {
            mConnection = mcon;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mySerialPort = new SerialPort("COM1");

            mySerialPort.BaudRate = 9600;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.RequestToSendXOnXOff;
            
            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            this.FormClosed += new FormClosedEventHandler(close);
            mySerialPort.Open();
              
        }
        void close(object sender, EventArgs e)
        {
            mySerialPort.Close();
        }

        private void DataReceivedHandler(
                          object sender,
                          SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            TelefonateTex.Text +="\n"+ indata;
            //Console.WriteLine("Data Received:");
            //Console.Write(indata);
        }
    }
}
