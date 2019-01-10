using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml.Serialization;



namespace oop_lab_14
{
 
    [Serializable]
  
    abstract  public partial class Goods: IGoods,IOwner
    {
        
        private int price;
        private string producingCountry;
        private string name;

        public int Price { get => price; set => price = value; }
        public string ProducingCountry { get => producingCountry; set => producingCountry = value; }
        public string Name { get => name; set => name = value; }

        public Goods(string name, string producingCountry,  int price):this()
        {
            this.Name = name;
            this.ProducingCountry = producingCountry;
            this.Price = price;
        }

        //реализация интерфейсов
        public virtual float Discount()
        {
            //переопределил в технике
            float resolt;
            if(price>1000000)
            {
                resolt = (float)(0.85 * price);
            }
            else
            {
                resolt = (float)(0.9 * price);
            }
            return resolt;
        }

        public string Manufacturer { get { return producingCountry ; } }

        //public string Manufacturer => throw new NotImplementedException();

        abstract public void Print();

        public override int GetHashCode()
        {
            return Price;
        }

        public override bool Equals(object obj)
        {
            if (Price > obj.GetHashCode())
            {
                return true;
            }
            else
            { 
            return false;
            }

        }
        public abstract override string ToString();
        public virtual bool allowed()
        {
            return true;
        }
    }
 

    [Serializable]


    public class Table : Goods
    {
        private string material;
        public Table(string name, string producingCountry, int prise, string material) : base (name,producingCountry,prise)
        {
            this.Material = material;    
        }

        public override void Print()
        {
            Console.WriteLine("Имя товара: {0}\tСтрона-производитель:{1}\tЦена:{2}\t", Name, ProducingCountry, Price);
        }

        public string Material { get => material; set => material = value; }

        public override string ToString()
        {
            return "Type: " + GetType() + "Имя товара: " + Name + "\t Строна-производитель:" + ProducingCountry + "\t Цена: " + Price;
        }
    }
 
    [Serializable]
    
    sealed class Screan:Goods
    {
        private int height;
        private int width;

        public int Height { get => height; set => height = value; }
        public int Width { get => width; set => width = value; }

        public Screan(string name, string producingCountry, int prise, int height,int width) : base(name, producingCountry, prise)
        {
            this.Height = height;
            this.Width = width;
        }

        public override void Print()
        {
            Console.WriteLine("Имя товара: {0}\tСтрона-производитель:{1}\tЦена:{2}\tРазрешение:{3}x{4}", Name, ProducingCountry, Price,Height,Width);
        }

        public override string ToString()
        {
            return "Type: " + GetType() + "Имя товара: " + Name + "\t Строна-производитель:" + ProducingCountry + "\t Цена: " + Price;
        }
    }

 

    [Serializable]

    public class Technique : Goods
    {
        private string operationPeriod;
        private string owner;

        public Technique() : base() { }
        public Technique(string name, string producingCountry, int prise, string operationPeriod) : base(name, producingCountry, prise)
        {
            this.OperationPeriod = operationPeriod;
            //Console.WriteLine("Введите название компании-производителя:");
            //this.Owner = Console.ReadLine();
        }

        public override string ToString()
        {
            return "Type: " +GetType() + "Имя товара: " + Name + "\t Компания-производитель:" + Owner + "\t Цена: " + Price;
        }


        public override void Print()
        {
            Console.WriteLine("Имя товара: {0}\tСтрона-производитель:{1}\tЦена:{2}\t", Name, ProducingCountry, Price);
        }

        //sealed - запрет переопределения метода(и свойсв_
        //base- дайт доступ к членам базового класса
        public override sealed float Discount()
        {
            float resolt;
            if (base.Price > 1000000 && ProducingCountry!="China")
            {
                resolt = (float)(0.8 * base.Price);
            }
            else
            {
                resolt = (float)(0.9 * Price);
            }
            return resolt;
        }
        
        //скрывает наследуемый член от Goods
        public new string Manufacturer { get { return owner; } }

        public string OperationPeriod { get => operationPeriod; set => operationPeriod = value; }
        public string Owner { get => owner; set => owner = value; }
    }
 

    [Serializable]    

    class Monitor :Technique
    {
        private int diagonal;

        public Monitor(string name, string producingCountry, int prise, string operationPeriod,int diagonal):base(name, producingCountry, prise, operationPeriod)
        {
            this.Diagonal = diagonal;
        }

        public int Diagonal { get => diagonal; set => diagonal = value; }
    }
 

    [Serializable]

    class Headphones :Technique
    {
        
        public ConsoleColor color;

        public Headphones(string name, string producingCountry, int prise, string operationPeriod, ConsoleColor color) : base(name, producingCountry, prise, operationPeriod)
        {
            this.Color = color;
        }

        public new void Print()
        {
            Console.ForegroundColor = Color;
            Console.WriteLine("Имя товара: {0}\tСтрона-производитель:{1}\tЦена:{2}\t", Name, ProducingCountry, Price);
            Console.ForegroundColor =ConsoleColor.Gray;
        }

        public ConsoleColor Color { get => color; set => color = value; }
    }
    //Композиция
 

    [Serializable]
    

    class PC : Technique
    {
        private Monitor monitorPC;
        internal Headphones HeadphonesPS {get;set;}

        public PC(string name, string producingCountry, int prise, string operationPeriod) : base(name, producingCountry, prise, operationPeriod)
        {
            this.MonitorPC=new Monitor("MonitorPC","Chine",0,"2года",20);
            this.HeadphonesPS = new Headphones("HeadphonesPC", "Chine", 0, "2года", ConsoleColor.Red);
        }

        internal Monitor MonitorPC { get => monitorPC; set => monitorPC = value; }
    }
    //Агрегация
 

    [Serializable]

    class Projector :Technique
    {
        private bool wirelessCommunication;
        private Screan screan;

        public Projector(string name, string producingCountry, int prise, string operationPeriod, bool wirelessCommunication,Screan screan) : base(name, producingCountry, prise,operationPeriod)
        {
            this.WirelessCommunication = wirelessCommunication;
            this.Screan = screan;
        }

        public override bool allowed()
        {
            return false;
        }

        public bool WirelessCommunication { get => wirelessCommunication; set => wirelessCommunication = value; }
        internal Screan Screan { get => screan; set => screan = value; }
    }

    //class TestForSealed : Screan{}//не может быть производным от запечатанного типа 
 

    [Serializable]
   

    abstract class Phone:Technique
    {
        public long Number { get; set; }
        abstract public void Call();
        public Phone(string name, string producingCountry, int prise, string operationPeriod,long number) : base(name, producingCountry, prise, operationPeriod)
        {
            this.Number = number;
        }

        
    }
 

    [Serializable]
   

    class Smartphone : Phone, ICommonFunctionsForPhones, ICommonFunctionsForSmartphones
    {
        public Smartphone(string name, string producingCountry, int prise, string operationPeriod, long number,bool wifiConnection) : base(name, producingCountry, prise, operationPeriod, number)
        {
            this.WiFiConnection = wifiConnection;
        }

        public bool WiFiConnection { get; set; }

        void ICommonFunctionsForPhones.Call()
        {
            Console.WriteLine("Звонки доступны по номеру{0}", base.Number);
        }
        public override void Call()
        {
            Console.WriteLine("Для получения подробной информации о функции Call, обратитесь по номеру *141*3*1#");
        }

        void ICommonFunctionsForSmartphones.Call()
        {
            //Console.WriteLine("Звонки доступны по номеру{0}", base.Number);

            if (WiFiConnection)
            {
                Console.WriteLine("Возможны звонки через интернет");
            }
            else
            {
                Console.WriteLine("Интернет соединение отсутствует");
            }
        }

        public override bool allowed()
        {
            return false;
        }
    }
 
   

    class Printer
    {
        public virtual void IAmPrinting(Goods goods)
        {
            Console.WriteLine(goods.GetType());
            Console.WriteLine(goods.ToString());
        }
    }
}
