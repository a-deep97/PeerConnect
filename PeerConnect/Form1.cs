using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PeerConnect
{
    
    public partial class Form1 : Form
    {
        public string machineType = "client";
        connection connect;
        private string server="127.0.0.1";
        private int port = 7;
        public string sentMessage="initial";
        public static System.Timers.Timer aTimer,bTimer;
       

        public Form1()
        {
            InitializeComponent();
            aTimer = new System.Timers.Timer(100);
            bTimer = new System.Timers.Timer(100);
            bTimer.Elapsed += new System.Timers.ElapsedEventHandler(print);
            aTimer.Elapsed += new System.Timers.ElapsedEventHandler(Recieve);
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            bTimer.AutoReset = true;
            bTimer.Enabled = true;

        }
        void Recieve(object source ,System.Timers.ElapsedEventArgs e)
        {
            if (connect != null)
                {
                    if (machineType == "client") connect.receiveMessageFromServer();
                    else if (machineType == "server") connect.receiveMessageFromClient();
                }
         
        }
        void print(object source, System.Timers.ElapsedEventArgs e)
        {
            if(connect!=null)
            {
                MethodInvoker invoker = new MethodInvoker(delegate
                 {
                    if (connect.receiveStatus == true) { messageList.Items.Add("Sender:  "+connect.receiveMessage.text);connect.receiveStatus = false;connect.receiveMessage.clearData(); }
                 });
                messageList.Invoke(invoker);
            }
           
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            server = textBox1.Text;
            Console.WriteLine(server);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            machineType = "client";
            textBox1.Invoke(new MethodInvoker(delegate { textBox1.Text = server; }));
            textBox3.Invoke(new MethodInvoker(delegate { textBox3.Text = Convert.ToString(port); }));
            connect = new connection(server, port);
            connect.connectServer();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            port = Convert.ToInt32(textBox3.Text);
            Console.WriteLine(port);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            connect.stop_server();
          
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            machineType = "server";
            //  button1.Enabled = false;
            textBox1.Invoke(new MethodInvoker(delegate { textBox1.Text = server; }));
            textBox3.Invoke(new MethodInvoker(delegate { textBox3.Text = Convert.ToString(port); }));
            connect = new connection(server,port);
            connect.start_server();
           

        }

        private void button3_Click(object sender, EventArgs e)
        {   if (connect != null)
            {
                if (machineType == "client") connect.sendMessageToServer();
                else if (machineType == "server") connect.sendMessageToClient();
                messageList.Items.Add("You:  "+textBox2.Text);
                textBox2.Text = string.Empty;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            connect.sendMessage.text = textBox2.Text;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

      

        
    }
}
