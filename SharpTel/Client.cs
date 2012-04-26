using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace SharpTel{
    class Client{

        StreamReader input = null;

        public Client(StreamReader input){
            this.input = input;
        }

        public void Run(){
            String line;
            while ((line=input.ReadLine())!=null){
                Console.Write(line+"\r\n");
            }
        }

        static void Main(string[] args){
                Console.Write("Host: ");
                string host=Console.ReadLine();
                Console.Write("Port: ");
                int port;
                try{
                    port=int.Parse(Console.ReadLine());
                }
                catch(Exception){
                    Console.WriteLine("Wrong parameter. Using 23 instead");
                    port=23;
                }
                TcpClient socket=null;
                try{
                    socket = new TcpClient(host, port);
                }
                catch(SocketException){
                    Console.WriteLine("Unknown host - " + host + ". Quitting");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                NetworkStream stream = socket.GetStream();
                StreamWriter output = new StreamWriter(stream);
                StreamReader input = new StreamReader(stream);

                Client cliobj = new Client(input);
                Thread t = new Thread(new ThreadStart(cliobj.Run));
                t.Start();

                while(true){
                    output.Write(Console.ReadLine()+"\r\n");
                    output.Flush();
                }
        }
    }
}
