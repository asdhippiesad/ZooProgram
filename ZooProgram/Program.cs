using System;
using System.Collections.Generic;
using System.Linq;

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

                Console.WriteLine("Добро Пожоваловать в зоопарк:\n " +
                                  "В зоопарке 5 вольеров.");
                Console.WriteLine($"{ChooseAviaryCommand} -- выбрать вольер.");
                Console.WriteLine($"{ExitCommand} -- выход.");

                switch (Console.ReadLine())
                {
                    case ChooseAviaryCommand:
                        SetText();
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

        public void SetText()
        {
            Console.WriteLine($"Введите номер вольера: ");

            if (int.TryParse(Console.ReadLine(), out int aviaryNumber))
            {
                Aviary aviary = _zoo.CreateAviary(aviaryNumber);
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
        private Dictionary<int, Aviary> _aviaries = new Dictionary<int, Aviary>();
        private List<Animal> _avialableAnimals = new List<Animal>();
        private Random _random = new Random();

        private int _maxAviaryCount = 5;

        public Zoo()
        {
            AddAnimals();
            Fill();
        }

        public void AddAnimals()
        {
            _avialableAnimals.Add(new Lemur(GetRandomGender()));
            _avialableAnimals.Add(new Panda(GetRandomGender()));
            _avialableAnimals.Add(new Caracal(GetRandomGender()));
            _avialableAnimals.Add(new Raccoon(GetRandomGender()));
        }

        public Aviary CreateAviary(int aviaryNumber)
        {
            if (aviaryNumber < 1 || aviaryNumber > _maxAviaryCount)
            {
                Console.WriteLine("Некорректный номер вольера.");
                return null;
            }

            if (_aviaries.ContainsKey(aviaryNumber))
            {
                Aviary newAriary = new Aviary();

                for (int i = 0; i < _avialableAnimals.Count; i++)
                {
                    Animal randomAnimal = GetRandomAnimal();
                    newAriary.AddAnimal(randomAnimal);
                }

                _aviaries[aviaryNumber] = newAriary;
            }

            return _aviaries[aviaryNumber];
        }

        private Animal GetRandomAnimal()
        {
            Animal randomAnimal = _avialableAnimals[_random.Next(_avialableAnimals.Count)];
            return randomAnimal.Clone();
        }

        public string GetRandomGender()
        {
            string female = "Женский";
            string male = "Мужской";

            string[] genders = new string[] { female, male };

            int randomNumber = _random.Next(genders.Length);

            return genders[randomNumber];
        }

        public void Fill()
        {
            for (int i = 1; i <= _maxAviaryCount; i++)
            {
                _aviaries.Add(i, new Aviary());
            }
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

    abstract class Animal
    {
        public Animal(string gender, string name, string sound)
        {
            Gender = gender;
            Name = name;
            Sound = sound;
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

        public override Animal Clone()
        {
            return new Lemur(Gender);
        }
    }

    class Caracal : Animal
    {
        public Caracal(string gender) : base("Каракал", "*мяукает*", gender) { }

        public override Animal Clone()
        {
            return new Caracal(Gender);
        }
    }

    class Panda : Animal
    {
        public Panda(string gender) : base("Панда", "*звуки панды*", gender) { }

        public override Animal Clone()
        {
            return new Panda(Gender);
        }
    }

    class Raccoon : Animal
    {
        public Raccoon(string gender) : base("Енот", "*звуки енота*", gender) { }

        public override Animal Clone()
        {
            return new Raccoon(Gender);
        }
    }
}
