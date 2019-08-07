using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

public class connection
{
    public string serverIP = "127.0.0.1";
    public int serverport = 7;
    public Socket server = null;
    public Socket client = null;
    public bool receiveStatus = false;
    public PeerConnect.message sendMessage=new PeerConnect.message();
    public PeerConnect.message receiveMessage = new PeerConnect.message();
    public connection(string server,int port)
    {
        serverIP = server;
        serverport = port;
    }
    public void start_server()
    {
        try
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(new IPEndPoint(IPAddress.Any, serverport));
            server.Listen(1);
            Console.WriteLine("server listening");
            client = server.Accept();
            Console.WriteLine("server connected to :"+client.RemoteEndPoint);
        }
        catch (Exception e) { Console.WriteLine(e.Message); }
    }
    public void connectServer()
    {
        try
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(new IPEndPoint(Dns.Resolve(serverIP).AddressList[0],serverport));
            Console.WriteLine("connected to server :" + serverIP);
        }
        catch (Exception e) { Console.WriteLine(e.Message); }
    }
    public void stop_server()
    {
        server.Close();
        Console.WriteLine("server closed");
    }
    public void sendMessageToServer()
    {
        if (client != null)
            try
            {
                sendMessage.encode();
                client.Send(sendMessage.encodedtext, 0, sendMessage.encodedtext.Length, SocketFlags.None);
                Console.WriteLine("message sent:" + sendMessage.text);
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
    }
    public void sendMessageToClient()
    {
        if (server != null)
            try
            {
                receiveMessage.encode();
                client.Send(sendMessage.encodedtext, 0, sendMessage.encodedtext.Length, SocketFlags.None);
                Console.WriteLine("message sent:" + sendMessage.text);
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
    }
    public void receiveMessageFromClient()
    {
        if(client!=null)
        try
        {
            if (client.Receive(receiveMessage.encodedtext, 0, receiveMessage.encodedtext.Length, SocketFlags.None) > 0)
            {
                receiveMessage.decode();
                receiveStatus = true;
                Console.WriteLine("message received:" + receiveMessage.text);
            }
        }
         catch (Exception e) { Console.WriteLine(e.Message); }
    }
    public void receiveMessageFromServer()
    {
        if (client != null)
            try
            {
                if (client.Receive(sendMessage.encodedtext, 0, receiveMessage.encodedtext.Length, SocketFlags.None) > 0)
                {
                    receiveMessage.decode();
                    receiveStatus = true;
                    Console.WriteLine("message received:" + receiveMessage.text);
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
    }
}