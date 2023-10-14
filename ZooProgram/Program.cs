using System;
using System.Collections.Generic;

namespace ZooProgram
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Zoo menu = new Zoo();

            menu.DisplayInfo();
        }
    }

    class Zoo
    {
        private Aviary _zoo = new Aviary();

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

    class Aviary
    {
        private List<Animal> _animalList = new List<Animal>();

        private Random _random = new Random();

        public Aviary()
        {
            _animalList.Add(new Lemur(GetRandomGender()));
            _animalList.Add(new Caracal(GetRandomGender()));
            _animalList.Add(new Panda(GetRandomGender()));
            _animalList.Add(new Raccoon(GetRandomGender()));
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

        public virtual string GetRandomGender()
        {
            string female = "Женский";
            string male = "Мужской";

            string[] genders = new string[] { female, male };

            int randomNumber = _random.Next(2);

            return genders[randomNumber];
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
        public int Count { get; private set; }

        public virtual Animal Create() => new Animal(Gender) { };

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
