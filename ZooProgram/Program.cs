using System;
using System.Collections.Generic;

namespace ZooProgram
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();

            menu.DisplayInfo();
        }
    }

    class Menu
    {
        private static Zoo _zoo = new Zoo();

        public void DisplayInfo()
        {
            bool isWoking = true;

            while (isWoking)
            {
                const string ShowInfoCommand = "1";
                const string ChooseAviaryCommand = "2";

                Console.WriteLine("Выберите действие: ");
                Console.WriteLine($"{ShowInfoCommand} -- показать инфорацию.");
                Console.WriteLine($"{ChooseAviaryCommand} -- выбрать вольер.");

                switch (Console.ReadLine())
                {
                    case ShowInfoCommand:
                        _zoo.ShowInfo();
                        break;

                    case ChooseAviaryCommand:
                        InputText();
                        break;
                        
                    default:
                        Console.WriteLine();
                        break;
                }

                Console.ReadLine();
                Console.Clear();
            }
        }

        public void InputText()
        {
            Console.WriteLine($"Введите номер вольера: ");

            if (int.TryParse(Console.ReadLine(), out int aviaryNumber))
            {
                _zoo.ShowAvirayInfo(aviaryNumber);
            }
            else
            {
                Console.WriteLine("Некорректный ввод, ведите номер вольера цифрами.");
            }
        }
    }

    class Zoo
    {
        private List<Animal> _animalList = new List<Animal>();
        private Random _random = new Random();

        public Zoo()
        {
            _animalList.Add(new Lemur(_random));
            _animalList.Add(new Chameleon(_random));
            _animalList.Add(new Caracal(_random));
            _animalList.Add(new Panda(_random));
            _animalList.Add(new Raccoon(_random));
        }

        public void ShowInfo()
        {
            foreach (Animal animal in _animalList)
            {
                Console.Write("Животные в вольере: ");
                animal.ShowInfo();
            }
        }

        public void ShowAvirayInfo(int aviaryNumber)
        {
            if (aviaryNumber >= 1 && aviaryNumber <= _animalList.Count)
            {
                for (int i = 0; i < _animalList.Count; i++)
                {
                    _animalList[aviaryNumber - 1].ShowInfo();
                }
            }
            else  
            {
                Console.WriteLine("Вольера с таким номером нет.");
            }
        }
    }

    class Animal
    {
        private Random _random = new Random();

        public Animal(Random random)
        {
            _random = random;
        }

        public string Name { get; protected set; }
        public string AnimalSound { get; protected set; }
        public int Count { get; protected set; }
        public string Gender { get; protected set; }

        public virtual Animal Create() => new Animal(_random) { };

        public void  ShowInfo()
        {
            Console.WriteLine($"Имя - {Name}" +
                              $"{AnimalSound}\n" +
                              $" {Gender}");
        }

        public virtual string GetRandomGender(Random random)
        {
            string female = "Женский";
            string male = "Мужской";

            int randomNumber = random.Next(2);

            if (randomNumber == 0)
            {
                return female;
            }
            else if(randomNumber == 1)
            {
                return male;
            }
            else
            {
                return null;
            }
        }
    }

    class Lemur : Animal
    {
        public Lemur(Random random) : base(random) 
        {
            Name = "Лемур";
            AnimalSound = "мяу";
            Gender = GetRandomGender(random);
        }

        public override string GetRandomGender(Random random)
        {
            return base.GetRandomGender(random);
        }
    }

    class Chameleon : Animal
    {
        public Chameleon(Random random) : base(random)
        {
            Name = "Хамелеон";
            AnimalSound = " ";
            Gender = GetRandomGender(random);
        }

        public override string GetRandomGender(Random random)
        {
            return base.GetRandomGender(random);   
        }
    }

    class Caracal : Animal
    {
        public Caracal(Random random) : base(random) 
        {
            Name = "Каракал";
            AnimalSound = "Мяу";
            Gender= GetRandomGender(random);
        }

        public override string GetRandomGender(Random random)
        {
            return base.GetRandomGender(random);   
        }
    }

    class Panda : Animal
    {
        public Panda(Random random) : base(random) 
        {
            Name = "Панда";
            AnimalSound = " ";
            Gender = GetRandomGender(random);
        }

        public override string GetRandomGender(Random random)
        {
            return base.GetRandomGender(random);
        }
    }

    class Raccoon : Animal
    {
        public Raccoon(Random random) : base(random)
        {
            Name = "Енот";
            AnimalSound = " ";
            Gender = GetRandomGender(random);
        }

        public override string GetRandomGender(Random random)
        {
            return base.GetRandomGender(random);
        }
    }
}
