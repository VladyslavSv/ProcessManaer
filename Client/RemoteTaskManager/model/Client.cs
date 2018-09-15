using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteTaskManager
{
    public class Client
    {
        Socket socket
        {
            get;set;
        }
        bool isConnected
        {
            get;set;
        }
        string ipAddress
        {
            get;set;
        }
        int port
        {
            get;set;
        }

        public Client()
        {
            port = 25065;
            ipAddress = "127.0.0.1";
        }
        public void removeRemoteProcess(string path)
        {
            sendNumber((int)MessageType.REMOVE_TASK);

            sendString(path);
        }
        public int receiveNumber()
        {
            byte[] bytes = new byte[1024];
            int bytesReceived = socket.Receive(bytes);

            sendAnswer();

            return BitConverter.ToInt32(bytes, 0);
        }
        public void sendNumber(int number)
        {
            byte[] bytes = BitConverter.GetBytes(number);
            socket.Send(bytes);

            receiveAnswer();
        }
        public string receiveString()
        {
            byte[] bytes = new byte[1024];
            int bytesReceived = socket.Receive(bytes);

            sendAnswer();

            return System.Text.Encoding.Default.GetString(bytes).TrimEnd('\0');
        }
        public void sendString(string sendedString)
        {
            byte[] bytes = Encoding.Default.GetBytes(sendedString);
            socket.Send(bytes);

            receiveAnswer();
        }
        public void sendAnswer()
        {
            byte[] bytes = BitConverter.GetBytes(1);
            socket.Send(bytes);
        }
        public void receiveAnswer()
        {
            byte[] bytes = new byte[1024];
            socket.Receive(bytes);
        }
        public void createRemoteTask(string path)
        {
            sendNumber((int)MessageType.CREATE_TASK);
            sendString(path);
        }
        public string[] getAllRemoteTasks()
        {
            sendNumber((int)MessageType.GET_ALL_TASKS);

            int processesCount = receiveNumber();
            string[] allProcesses = new string[processesCount];

            for(int i = 0; i < processesCount; i++)
            {
                allProcesses[i] = receiveString();
            }

            return allProcesses;
        }
        public void connect()
        {
            IPAddress ipAddr = IPAddress.Parse(ipAddress);

            // устанавливаем удаленную конечную точку для сокета
            // уникальный адрес для обслуживания TCP/IP определяется комбинацией IP-адреса хоста с номером порта обслуживания
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr /* IP-адрес */, port /* порт */);

            // создаем потоковый сокет
            socket = new Socket(AddressFamily.InterNetwork /*схема адресации*/, SocketType.Stream /*тип сокета*/, ProtocolType.Tcp /*протокол*/);
            /* Значение InterNetwork указывает на то, что при подключении объекта Socket к конечной точке предполагается использование IPv4-адреса.
              SocketType.Stream поддерживает надежные двусторонние байтовые потоки в режиме с установлением подключения, без дублирования данных и 
              без сохранения границ данных. Объект Socket этого типа взаимодействует с одним узлом и требует предварительного установления подключения 
              к удаленному узлу перед началом обмена данными. Тип Stream использует протокол Tcp и схему адресации AddressFamily.
            */

            // соединяем сокет с удаленной конечной точкой
            socket.Connect(ipEndPoint);
        }
    }
}
