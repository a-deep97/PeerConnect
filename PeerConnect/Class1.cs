using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PeerConnect
{
    public class message
    {
        private static int MAX_BUFF_SIZE=512;
        public string text = "";
        public byte[] encodedtext = new byte[MAX_BUFF_SIZE] ;
        public message()
        {
            //encodedtext = new byte[MAX_BUFF_SIZE];
         //   Console.WriteLine(Encoding.ASCII.GetString(encodedtext));
          //  Array.Clear(encodedtext, 0, encodedtext.Length);
        }
        
        public void encode()
        {
            encodedtext = Encoding.UTF8.GetBytes(text);
        }

        public void decode()
        {
            if(encodedtext!=null)
            text = Encoding.UTF8.GetString(encodedtext);
        }
        public void clearData()
        {
            text = "";
            Array.Clear(encodedtext,0,encodedtext.Length);
        }
    }
}
