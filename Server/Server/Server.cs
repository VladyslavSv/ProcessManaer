using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

namespace Server
{
    public class BootServer
    {
        private class Server
        {
            private Socket clientSocket
            {
                get;
                set;
            }

            public Server() { }

            public Server(Socket clientSocket)
            {
                this.clientSocket = clientSocket;
            }

            public int receiveNumber()
            {
                byte[] bytes = new byte[1024];
                int bytesReceived = clientSocket.Receive(bytes);

                sendAnswer();

                return BitConverter.ToInt32(bytes, 0);
            }
            public void sendNumber(int number)
            {
                byte[] bytes = BitConverter.GetBytes(number);
                clientSocket.Send(bytes);

                receiveAnswer();
            }
            public string receiveString()
            {
                byte[] bytes = new byte[1024];
                int bytesReceived = clientSocket.Receive(bytes);

                sendAnswer();

                return System.Text.Encoding.Default.GetString(bytes).TrimEnd('\0');
            }
            public void sendString(string sendedString)
            {
                byte[] bytes = Encoding.Default.GetBytes(sendedString);
                clientSocket.Send(bytes);

                receiveAnswer();
            }
            public void createProcess(string fileName)
            {
                Process process = new Process();
                process.StartInfo.FileName = fileName;
                process.Start();

            }
            public void sendAllProcesses()
            {
                int filesCount = Process.GetProcesses().Count();
                Process[] processes = Process.GetProcesses();

                sendNumber(filesCount);

                for (int i = 0; i < filesCount; i++)
                {
                    sendString(processes[i].ProcessName);
                }
            }
            public void removeProcess(string taskName)
            {
                Process process = Process.GetProcessesByName(taskName).First();

                process.Kill();
                process.CloseMainWindow();
            }
            public void sendAnswer()
            {
                byte[] bytes = BitConverter.GetBytes(1);
                clientSocket.Send(bytes);
            }
            public void receiveAnswer()
            {
                byte[] bytes = new byte[1024];
                clientSocket.Receive(bytes);
            }
        }
        private async void ExecuteClientRequests(Socket clientSocket)
        {
            await Task.Run(() =>
            {
                try
                {
                    Server server = new Server(clientSocket);
                    while (true)
                    {
                        MessageType messageType = (MessageType)server.receiveNumber();

                        switch(messageType)
                        {
                            case MessageType.CREATE_TASK:
                                string fileName = server.receiveString();
                                server.createProcess(fileName);
                                break;
                            case MessageType.GET_ALL_TASKS:
                                server.sendAllProcesses();
                                break;
                            case MessageType.REMOVE_TASK:
                                string processName = server.receiveString();
                                server.removeProcess(processName);
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Сервер: " + ex.Message);
                    clientSocket.Shutdown(SocketShutdown.Both); // Блокируем передачу и получение данных для объекта Socket.
                    clientSocket.Close(); // закрываем сокет
                }
            });
        }

        public void Accept()
        {
                try
                {
                    IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 25065);
                    Socket sListener = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
                    sListener.Bind(ipEndPoint);

                    sListener.Listen(10);
                    Console.WriteLine("Server started");
                    while (true)
                    {
                        Socket handler = sListener.Accept();

                        Console.WriteLine("Client connected to server");

                        ExecuteClientRequests(handler);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Сервер: " + ex.Message);
                }
        }
    }
}
