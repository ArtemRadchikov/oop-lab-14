using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab_14
{
    /* Найти общую стоимость всей  техники, 
         * вывести оборудование в порядке убывания  одного из параметров. 
         * Найти оборудование, которое должно быть списано ( политику списания определить самостоятельно) */
    [Serializable]
  
    class classController
    {
        computerLab obj;

        private classController() { }

        public classController(computerLab obj)
        {
            this.obj = obj;
        }

        public int TotalCost()
        {
            int sum=0;
            foreach(Goods i in obj.ArrayOfGoods)
            {
                sum += i.Price;
            }
            Console.WriteLine("Общая ценна:{0}", sum);
            return sum;
        }

        public void SortAndPrint()
        {
            Goods[] array = new Goods[obj.ArrayOfGoods.Length];
            int counter;
            for (int i=0;i< obj.ArrayOfGoods.Length;i++)
            {
                counter = 0;
                for (int j=0;j< obj.ArrayOfGoods.Length;j++)
                {
                    if(obj.ArrayOfGoods[i].Price> obj.ArrayOfGoods[j].Price || (obj.ArrayOfGoods[i].Price == obj.ArrayOfGoods[j].Price && i<j))
                    {
                        counter++;
                    }
                }
                array[counter] = obj.ArrayOfGoods[i];
            }
            
            foreach(Goods i in array)
            {
                Console.WriteLine("{0} ({1})", i.GetType(), i.Price);
            }
        }

        public void Search_to_write_off()
        {
            foreach(Goods i in obj.ArrayOfGoods)
            {
                i.ShowCatcategory();
            }
        }
    }

    
}
