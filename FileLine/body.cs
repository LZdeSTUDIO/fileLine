using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FileLine
{
    class body
    {
        public Socket socket { set; get; }
        public Form1 from { set; get; }
        public body(Socket socket, Form1 form)
        {
            this.socket = socket;
            this.from = from;
        }
    }
}
