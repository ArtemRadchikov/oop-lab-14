using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace oop_lab_14
{

    class Program
    {
        // адрес и порт сервера, к которому будем подключаться
        static int port = 8006; // порт сервера
        static string address = "127.0.0.1"; // адрес сервера
        static void Main(string[] args)
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // подключаемся к удаленному хосту
                socket.Connect(ipPoint);
                Console.Write("Введите сообщение:");
                string message = Console.ReadLine();
                byte[] data = Encoding.Unicode.GetBytes(message);
                socket.Send(data);

                // получаем ответ
                data = new byte[256]; // буфер для ответа
                StringBuilder builder = new StringBuilder();
                int bytes = 0; // количество полученных байт

                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.ASCII.GetString(data, 0, bytes));
                }
                while (socket.Available > 0);
                Console.WriteLine("ответ сервера: " + builder.ToString());

                using (StreamWriter fs = new StreamWriter("Array.bat"))
                {
                    fs.Write(builder);
                }

                BinaryFormatter formatterBinary = new BinaryFormatter();
                Console.ForegroundColor = ConsoleColor.Blue;

                using (FileStream fs = new FileStream("Array.bat", FileMode.OpenOrCreate))
                {
                    List<int> newObj = (List<int>)formatterBinary.Deserialize(fs);
                    //List<Technique> newObj = (List<Technique>)formatterBinary.Deserialize(fs);

                    Console.WriteLine("Объект десериализован");
                    Console.ResetColor();

                    foreach(var i in newObj)
                    {
                        Console.WriteLine(i);
                    }
                }

                // закрываем сокет
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
    
