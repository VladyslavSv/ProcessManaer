using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            BootServer bootServer = new BootServer();
            bootServer.Accept();
        }
    }
}
