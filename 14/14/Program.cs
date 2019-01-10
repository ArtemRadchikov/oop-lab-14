using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using System.Xml.Linq;




namespace oop_lab_14
{
    class Program
    {
        static int port = 8006; // порт для приема входящих запросов

        static void Main(string[] args)
        {

            Technique tecniqueExample = new Technique("Товар", "РБ", 23333, "2 year");

            computerLab Obj = new computerLab();

            Obj.Add(new Technique("Technique1", "РБ", 10000, "11"));
            Obj.Add(new Technique("Technique2", "РБ", 20000, "12"));
            Obj.Add(new Technique("Technique3", "РБ", 30000, "13"));
            Obj.Add(new Technique("Technique4", "РБ", 40000, "14"));
            Obj.Add(new Technique("Technique5", "РБ", 50000, "15"));
                       
            Console.Write("Массив:");
            Obj.Print();

            Console.Read();
            Console.Clear();

            #region Binary
            Console.WriteLine("Binary");
            Console.ForegroundColor = ConsoleColor.Green;
            BinaryFormatter formatterBinary = new BinaryFormatter();
            
            using (FileStream fs = new FileStream("Binary.txt", FileMode.Create))
            {
                formatterBinary.Serialize(fs, Obj);

                Console.WriteLine("Объект сериализован");
            }

            Console.ForegroundColor = ConsoleColor.Blue;

            using (FileStream fs = new FileStream("Binary.txt", FileMode.OpenOrCreate))
            {
                computerLab newObj = (computerLab)formatterBinary.Deserialize(fs);

                Console.WriteLine("Объект десериализован");
                Console.ResetColor();

                newObj.Print();
            }

            Console.ResetColor();
            #endregion

            #region SOAP
            Console.WriteLine("SOAP");
            Console.ForegroundColor = ConsoleColor.Green;
            SoapFormatter formatterSOAP = new SoapFormatter();
            
            using (FileStream fs = new FileStream("SOAP.txt", FileMode.Create))
            {
                formatterSOAP.Serialize(fs, Obj);

                Console.WriteLine("Объект сериализован");
            }

            Console.ForegroundColor = ConsoleColor.Blue;

            using (FileStream fs = new FileStream("SOAP.txt", FileMode.OpenOrCreate))
            {
                computerLab newObj = formatterSOAP.Deserialize(fs) as computerLab;

                Console.WriteLine("Объект десериализован");
                Console.ResetColor();

                newObj.Print();
            }

            #endregion

            #region JSON
            Console.WriteLine("JSON");
            Console.ForegroundColor = ConsoleColor.Green;
            Type[] type = new Type[] { typeof(Technique), typeof(Goods), typeof(Headphones), typeof(Table) };
            DataContractJsonSerializer formatterJSON = new DataContractJsonSerializer(typeof(computerLab),type);
           
            using (FileStream fs = new FileStream("JSON.txt", FileMode.Create))
            {
                formatterJSON.WriteObject(fs, Obj);
                Console.WriteLine("Объект сериализован");
            }

            Console.ForegroundColor = ConsoleColor.Blue;

            using (FileStream fs = new FileStream("JSON.txt", FileMode.OpenOrCreate))
            {
                computerLab newObj = (computerLab)formatterJSON.ReadObject(fs);

                Console.WriteLine("Объект десериализован");
                Console.ResetColor();

                newObj.Print();
            }

            #endregion

            #region XML
            Console.WriteLine("XML");
            Console.ForegroundColor = ConsoleColor.Green;
            
            XmlSerializer formatterXML = new XmlSerializer(typeof(computerLab), new Type[] { typeof(Technique) });

            using (FileStream fs = new FileStream("XML.xml", FileMode.OpenOrCreate))
            {
                formatterXML.Serialize(fs, Obj);

                Console.WriteLine("Объект сериализован");
            }

            Console.ForegroundColor = ConsoleColor.Blue;

            using (FileStream fs = new FileStream("XML.xml", FileMode.OpenOrCreate))
            {
                computerLab newObj = (computerLab)formatterXML.Deserialize(fs);

                Console.WriteLine("Объект десериализован");
                Console.ResetColor();

                newObj.Print();
            }

            Console.Read();
           
            #endregion

            Console.Read();
            Console.Clear();

            #region Task 3

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("XML.xml");
            XmlElement xRoot = xmlDocument.DocumentElement;

            XmlNodeList childnodes1 = xRoot.SelectNodes("ArrayOfGoods//Goods");
            foreach (XmlNode n in childnodes1)
            {
                Console.WriteLine(n.OuterXml);
            }
            Console.WriteLine();

            XmlNodeList childnodes2 = xRoot.SelectNodes("ArrayOfGoods//Goods");

            foreach (XmlNode n in childnodes2)
            {
                var p=n.SelectSingleNode("Price");
                Console.WriteLine(n.SelectSingleNode("Name").InnerText+' '+p.Name+' '+p.InnerText);

            }

            #endregion

            #region Task 4

            XDocument StudentsXML = new XDocument();

            XElement FirsStudent = new XElement("Student");
            FirsStudent.Add(new XAttribute("faculty", "ФИТ"));
            FirsStudent.Add(new XElement("course", "2"));
            FirsStudent.Add(new XElement("group", "3"));
            FirsStudent.Add(new XElement("Name", "Артем"));
            FirsStudent.Add(new XElement("Surname", "Радчиков"));

            XElement SecondStudent = new XElement("Student");
            SecondStudent.Add(new XAttribute("faculty", "ФИТ"));
            SecondStudent.Add(new XElement("course", "2"));
            SecondStudent.Add(new XElement("group", "3"));
            SecondStudent.Add(new XElement("Name", "Катя"));
            SecondStudent.Add(new XElement("Surname", "Щепина"));

            XElement ThirdStudent = new XElement("Student");
            ThirdStudent.Add(new XAttribute("faculty", "ФИТ"));
            ThirdStudent.Add(new XElement("course", "2"));
            ThirdStudent.Add(new XElement("group", "3"));
            ThirdStudent.Add(new XElement("Name", "Влад"));
            ThirdStudent.Add(new XElement("Surname", "Качан"));

            XElement root = new XElement("Students");

            root.Add(FirsStudent);
            root.AddFirst(SecondStudent);
            root.Add(ThirdStudent);

            StudentsXML.Add(root);

            StudentsXML.Save("Students.xml");

            #endregion

            #region Task 2

            Console.Read();

            List<Technique> l = new List<Technique>() {
                new Technique("1","1",1,"1"),
                new Technique("2","2",2,"2"),
                new Technique("3","3",3,"3"),
                new Technique("4","4",4,"4"),
                new Technique("5","5",5,"5"), };

            using (FileStream fs = new FileStream("List.txt", FileMode.Create))
            {
                formatterBinary.Serialize(fs, l);

                Console.WriteLine("Объект сериализован");
            }

            Console.ForegroundColor = ConsoleColor.Blue;

            using (FileStream fs = new FileStream("List.txt", FileMode.OpenOrCreate))
            {
                List<Technique> newObj = (List<Technique>)formatterBinary.Deserialize(fs);

                Console.WriteLine("Объект десериализован");
                Console.ResetColor();

                foreach(var i in newObj)
                {
                    Console.WriteLine(i);
                }
            }

            List<int> list = new List<int>() {1,2,3,4,6,7,8,9};
            using (FileStream fs = new FileStream("Array.bat", FileMode.Create))
            {
                formatterBinary.Serialize(fs,list);
            }

               
           
            // получаем адреса для запуска сокета
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);

            // создаем сокет
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                // связываем сокет с локальной точкой, по которой будем принимать данные
                listenSocket.Bind(ipPoint);

                // начинаем прослушивание
                listenSocket.Listen(10);

                Console.WriteLine("Сервер запущен. Ожидание подключений...");

                
                    Socket handler = listenSocket.Accept();
                    // получаем сообщение
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; // количество полученных байтов
                    byte[] data = new byte[256]; // буфер для получаемых данных

                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);

                    Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());

                    

                    // отправляем ответ
                    string message = "ваше сообщение доставлено";
                    using (StreamReader fs = new StreamReader("Array.bat"))
                    {
                        message = fs.ReadToEnd();
                    }
                    data = Encoding.Unicode.GetBytes(message);
                    foreach (var i in data)
                    {
                        Console.Write(i);
                    }
                    data = Encoding.ASCII.GetBytes(message);
                    handler.Send(data);
                    // закрываем сокет
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.Read();

            #endregion

            
        }
    }
}
