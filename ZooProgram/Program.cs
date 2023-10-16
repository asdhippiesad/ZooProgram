using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;

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
        private Zoo _zoo = new Zoo();

        public void DisplayInfo()
        {
            bool isWoking = true;

            while (isWoking)
            {
                const string ChooseAviaryCommand = "1";
                const string ExitCommand = "2";

                Console.WriteLine("Добро Пожоваловать в зоопарк: ");
                Console.WriteLine($"{ChooseAviaryCommand} -- выбрать вольер.");
                Console.WriteLine($"{ExitCommand} -- выход.");

                switch (Console.ReadLine())
                {
                    case ChooseAviaryCommand:
                        InputText();
                        break;

                    case ExitCommand:
                        isWoking = false;
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
                Aviary aviary = _zoo.CreateAviary();
                aviary.ShowInfo();
            }
            else
            {
                Console.WriteLine("Некорректный ввод, ведите номер вольера цифрами.");
            }
        }
    }

    class Zoo
    {
        private List<Aviary> _aviaries = new List<Aviary>();
        private Random _random = new Random();

        public Zoo()
        {
            _aviaries.Add(CreateAviary());
        }

        public Aviary CreateAviary()
        {
            int minCount = 1;
            int maxCount = 10;

            Aviary aviary = new Aviary();
            int animalCount = _random.Next(minCount, maxCount);

            for (int i = 0; i < animalCount; i++)
            {
                Animal randomAnimal = GetRandomAnimal();
                aviary.AddAnimal(randomAnimal);
            }

            return aviary;
        }

        private Animal GetRandomAnimal()
        {
            const int OneMenu = 1;
            const int TwoMenu = 2;
            const int ThreeMenu = 3;
            const int EvenMenu = 4;

            int minCount = 1;
            int maxCount = 5;

            int randomIndex = _random.Next(minCount, maxCount);

            switch (randomIndex)
            {
                case OneMenu:
                    return new Lemur(GetRandomGender());
                case TwoMenu:
                    return new Caracal(GetRandomGender());
                case ThreeMenu:
                    return new Panda(GetRandomGender());
                case EvenMenu:
                    return new Raccoon(GetRandomGender());
                default:
                    return new Lemur(GetRandomGender());
            }
        }

        private string GetRandomGender()
        {
            string female = "Женский";
            string male = "Мужской";

            string[] genders = new string[] { female, male };

            int randomNumber = _random.Next(2);

            return genders[randomNumber];
        }
    }

    class Aviary
    {
        private List<Animal> _animalList = new List<Animal>();

        public void AddAnimal(Animal animal)
        {
            _animalList.Add(animal);
        }

        public void ShowInfo()
        {
            foreach (Animal animal in _animalList)
            {
                Console.Write("Животные в вольере: ");
                animal.ShowInfo();
            }
        }
    }

    class Animal
    {
        public Animal(string gender)
        {
            Gender = gender;
        }

        public string Name { get; protected set; }
        public string Sound { get; protected set; }
        public string Gender { get; protected set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя - {Name}\n" +
                              $"{Sound}\n" +
                              $" {Gender}\n");
        }
    }

    class Lemur : Animal
    {
        public Lemur(string gender) : base(gender)
        {
            Name = "Лемур";
            Sound = "*звуки Лемура.";
        }
    }

    class Caracal : Animal
    {
        public Caracal(string gender) : base(gender)
        {
            Name = "Каракал";
            Sound = "*мяукает*";
        }
    }

    class Panda : Animal
    {
        public Panda(string gender) : base(gender)
        {
            Name = "Панда";
            Sound = "*звуки панды*";
        }
    }

    class Raccoon : Animal
    {
        public Raccoon(string gender) : base(gender)
        {
            Name = "Енот";
            Sound = "*звуки енота*";
        }
    }
}
