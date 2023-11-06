using System;
using System.Collections.Generic;
using System.ComponentModel;

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
            bool isWorking = true;

            while (isWorking)
            {
                const string ChooseAviaryCommand = "1";
                const string ExitCommand = "2";

                Console.WriteLine("Добро пожаловать в зоопарк:\nВ зоопарке есть 4 вольера.");
                Console.WriteLine($"{ChooseAviaryCommand} -- выбрать вольер.");
                Console.WriteLine($"{ExitCommand} -- выход.");

                switch (Console.ReadLine())
                {
                    case ChooseAviaryCommand:
                        SetText();
                        break;

                    case ExitCommand:
                        isWorking = false;
                        break;

                    default:
                        Console.WriteLine();
                        break;
                }

                Console.ReadLine();
                Console.Clear();
            }
        }

        private void SetText()
        {
            Console.WriteLine("Введите номер вольера: ");
            if (int.TryParse(Console.ReadLine(), out int aviaryNumber))
            {
                ShowAviaryInfo(aviaryNumber - 1);
            }
            else
            {
                Console.WriteLine("Некорректный ввод, введите номер вольера цифрами.");
            }
        }

        private void ShowAviaryInfo(int aviaryNumber)
        {
            if (aviaryNumber >= 0 && aviaryNumber < _zoo.GetAviaries().Count)
            {
                Aviary aviary = _zoo.GetAviaries()[aviaryNumber];
                Console.WriteLine($"Информация о вольере {aviaryNumber + 1}:");
                aviary.ShowInfo();
            }
            else
            {
                Console.WriteLine("Некорректный номер вольера.");
            }
        }
    }

    class Zoo
    {
        private List<Aviary> _aviaries = new List<Aviary>();
        private Random _random = new Random();

        public Zoo()
        {
            FillAviaries();
        }

        public List<Aviary> GetAviaries()
        {
            return _aviaries;
        }

        private string GetRandomGender()
        {
            string female = "Женский";
            string male = "Мужской";

            string[] genders = new string[] { female, male };

            int randomNumber = _random.Next(genders.Length);

            return genders[randomNumber];
        }

        private void FillAviaries()
        {
            int aviaryCount = 4;
            int maxAnimalsCount = 5;

            for (int i = 0; i < aviaryCount; i++)
            {
                Aviary aviary = new Aviary();

                List<Animal> animals = new List<Animal>();

                int numAnimals = _random.Next(1, maxAnimalsCount);

                for (int j = 0; j < numAnimals; j++)
                {
                    animals.Add(GetNextAnimal(currentIndex: i));
                }

                aviary.AddAnimal(animals);
                _aviaries.Add(aviary);
            }
        }

        private Animal GetNextAnimal(int currentIndex)
        {
            Animal[] aviabaleAnimals = new Animal[] { new Lemur(GetRandomGender()), new Caracal(GetRandomGender()), new Panda(GetRandomGender()), new Raccoon(GetRandomGender()) };

            Animal nextAnimal = aviabaleAnimals[currentIndex % aviabaleAnimals.Length];

            return nextAnimal;
        }
    }

    class Aviary
    {
        private List<Animal> _animalList = new List<Animal>();

        public void AddAnimal(List<Animal> animals)
        {
            _animalList.AddRange(animals);
        }

        public void ShowInfo()
        {
            foreach (Animal animal in _animalList)
            {
                Console.WriteLine($"Животные в вольере: ");
                animal.ShowInfo();
            }
        }
    }

    abstract class Animal
    {
        public Animal(string gender, string name, string sound)
        {
            Name = name;
            Sound = sound;
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

        public abstract Animal Clone();
    }

    class Lemur : Animal
    {
        public Lemur(string gender) : base("Лемур", "*звуки Лемура.", gender) { }

        public override Animal Clone() => new Lemur(Gender);
    }

    class Caracal : Animal
    {
        public Caracal(string gender) : base("Каракал", "*мяукает*", gender) { }

        public override Animal Clone() => new Caracal(Gender);
    }

    class Panda : Animal
    {
        public Panda(string gender) : base("Панда", "*звуки панды*", gender) { }

        public override Animal Clone() => new Panda(Gender);
    }

    class Raccoon : Animal
    {
        public Raccoon(string gender) : base("Енот", "*звуки енота*", gender) { }

        public override Animal Clone() => new Raccoon(Gender);
    }
}