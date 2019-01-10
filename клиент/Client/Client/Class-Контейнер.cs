using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace oop_lab_14
{
  
    [Serializable]
    
    public class computerLab
    {
       
        
        public Goods[] ArrayOfGoods = new Goods[0];

 
        //public Goods[] ArrayOfGoods
        //{
        //    get
        //    {
        //        return ArrayOfGoods;
        //    }
        //    set
        //    {
        //        if (value.GetType() == typeof(Goods[]))
        //        {
        //            Console.WriteLine("Хотите приобрести все элементы(0) или только 1(1)");
        //            string input;

        //            do
        //            {
        //                Console.WriteLine("Некорректный ввод, попробуйте ещё раз");
        //                input = Console.ReadLine();

        //            } while (input != "1" || input != "0");

        //            bool anser = bool.Parse(input);

        //            if (anser)
        //            {
        //                if (!Int32.TryParse(input, out int choice))
        //                {
        //                    do
        //                    {
        //                        Console.WriteLine("Некорректный ввод, попробуйте ещё раз");
        //                        input = Console.ReadLine();

        //                    } while (!Int32.TryParse(input, out choice));
        //                }
        //                this.Add(value[choice]);
        //            }
        //            else
        //            {
        //                ArrayOfGoods = value;
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("Несоответствие типов");
        //        }
        //    }
        //}

        [XmlAttribute]
    
        bool counterOfProjector = true;


        
        public void Add(Goods element)
        {
            

            if (!(element is Smartphone) || (/*element.allowed()*/element is Projector && counterOfProjector))
            {
                if (/*!element.allowed()*/ element is Projector && counterOfProjector)
                {
                    counterOfProjector = false;
                }

                Array.Resize(ref ArrayOfGoods, ArrayOfGoods.Length + 1);
                ArrayOfGoods[ArrayOfGoods.Length - 1] = element;
            }
            else
            {
                if (!counterOfProjector)
                {
                    Console.WriteLine("В классе не может быть больше одного проэктора.");
                }
                else
                {
                    Console.WriteLine("Детям не нужны телефоны.");
                }
            }

        }

        public void Delete(Goods allElements = null, int numberOfElement = -1)
        {
            if (allElements != null)
            {
                int counter = 0;
                if (/*allElements.allowed()*/ allElements is Projector && !counterOfProjector)
                {
                    counterOfProjector = true;
                }
                for (int i = 0; i < ArrayOfGoods.Length; i++)
                {
                    if (allElements == ArrayOfGoods[i])
                    {
                        counter++;
                        for (int j = i; j < ArrayOfGoods.Length - 1; j++)
                        {
                            ArrayOfGoods[j] = ArrayOfGoods[j + 1];
                        }
                    }
                }
                Array.Resize(ref ArrayOfGoods, ArrayOfGoods.Length - counter);

            }
            if (numberOfElement >= 0 && numberOfElement < ArrayOfGoods.Length)
            {
                for (int i = numberOfElement; i < ArrayOfGoods.Length - 1; i++)
                {
                    ArrayOfGoods[i] = ArrayOfGoods[i + 1];
                }
                Array.Resize(ref ArrayOfGoods, ArrayOfGoods.Length - 1);
            }
        }

        public void Print()
        {
            Console.WriteLine();
            foreach (Goods i in ArrayOfGoods)
            {
                Console.Write(i.GetType() + "   ");
            }
            Console.WriteLine();

        }

    }
}

