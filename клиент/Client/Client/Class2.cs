using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab_14
{


    abstract partial class Goods : IGoods, IOwner
    {
        private Catcategory catcategory;
        private Age age;
        private Catcategory CatcategoryOfGood { get => catcategory; set => catcategory = value; }
        private Age AgeOfGood { get => age; set => age = value; }

        [Serializable]
        struct Age
        {
            internal int age;

            public Age(DateTime date)
            {
                if (DateTime.Now.Year > date.Year)
                {
                    this.age = DateTime.Now.Year - date.Year;
                }
                else
                {
                    this.age = 0;
                }
            }
        }

        //конструктор,в котором инициализируется Age
        public Goods()
        {
            //Console.WriteLine("Введите дату выпуска в формате dd.mm.yyyy");
            //DateTime dateFromConsole;
            //String input = Console.ReadLine();

            //if (!DateTime.TryParseExact(input, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out dateFromConsole))
            //{
            //    do
            //    {
            //        Console.WriteLine("Некорректный ввод, попробуйте ещё раз");
            //        input = Console.ReadLine();

            //    } while (!DateTime.TryParseExact(input, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out dateFromConsole));
            //}
            //this.AgeOfGood = new Age(dateFromConsole);
        }

        enum Catcategory
        {
            newObject = 1,
            averageObject,
            oldObject
        }

        public int ShowCatcategory()
        {
            if (AgeOfGood.age < 7)
            {
                if (AgeOfGood.age < 2)
                {
                    CatcategoryOfGood = Catcategory.newObject;
                }
                else
                {
                    CatcategoryOfGood = Catcategory.averageObject;
                }
            }
            else
            {
                CatcategoryOfGood = Catcategory.oldObject;
                Console.ForegroundColor = ConsoleColor.Red;
            }

            Console.WriteLine("Каегория:{0} ({1})", (int)this.CatcategoryOfGood, this.CatcategoryOfGood);
            Console.ForegroundColor = ConsoleColor.Gray;

            return (int)CatcategoryOfGood;
        }
    }
}

    

