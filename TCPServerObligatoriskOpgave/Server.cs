using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ModelLib;
using Newtonsoft.Json;

namespace TCPServerObligatoriskOpgave
{
    class Server
    {
        private static List<Book> books;
        public Server()
        {
            books = new List<Book>
            {
                new Book("Harry Potter", "Gertrud", 560, "1234567890123"),
                new Book("Putta Horri", "Felix", 350, "3210987654321"),
                new Book("Game Of Thrones", "Samuel", 849, "2345678901234"),
                new Book("Scary Books", "Scarecrow", 234, "3456789012345"),
                new Book("SukaB", "Putin", 987, "4567890123456")
            };
        }

        public void Start()
        {
            try
            {
                TcpListener server = new TcpListener(IPAddress.Loopback, 4646);
                server.Start();

                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    TcpClient socket = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");
                    Task.Run(() =>
                    {
                        TcpClient tempSocket = socket;
                        DoClient(tempSocket);

                    });

                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {1}", e);

            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }

        public void DoClient(TcpClient socket)
        {
            using (StreamReader sr = new StreamReader(socket.GetStream()))
            using (StreamWriter sw = new StreamWriter(socket.GetStream()))
            {
                bool runSocket = true;
                while (runSocket)
                {
                    string str = sr.ReadLine();
                    string strInfo;
                    string returnString = null;
                    switch (str.ToLower())
                    {
                        case "hent":
                            strInfo = sr.ReadLine();
                            Book returnBook = books.Find(book => book.Isbn13 == strInfo);
                            returnString = JsonConvert.SerializeObject(returnBook);
                            break;
                        case "hentalle":
                            returnString = JsonConvert.SerializeObject(books);
                            break;
                        case "gem":
                            strInfo = sr.ReadLine();
                            Book savedBook = JsonConvert.DeserializeObject<Book>(strInfo);
                            books.Add(savedBook);
                            break;
                        case "stop":
                            runSocket = false;
                            break;
                    }
                    sw.WriteLine(returnString);
                    sw.Flush();
                }
            }
        }
        

    }
}

