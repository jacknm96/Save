using System;

namespace Greetings
{
    public class Person
    {
        public string name;
        public string phoneNumber;
        public string email;

        public void SayGreeting() // default greeting, person does not know their name
        {
            Console.WriteLine("Hello! I'm... well, I can't quite remember my name.");
        }

        virtual public void SayGreeting(string s) // person recalls their name as it is given to them
        {
            Console.WriteLine("Hello, I'm " + s);
        }
    }

    public class Doctor : Person
    {
        public int salary;

        override public void SayGreeting(string s) // overrides base SayGreeting method, introducing themselves as Dr.
        {
            Console.WriteLine("Hello, I'm Dr. " + s);
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Person a = new Person();
            a.SayGreeting();
            Console.WriteLine("What's my name?");
            a.name = Console.ReadLine(); //user gives person their name
            Console.WriteLine("Ah, that's right. Am I a doctor?");
            Console.WriteLine("(y/n)");
            while (true)
            {
                string answer = Console.ReadLine();
                if (answer == "y") //if user tells person they're a doctor, will use the doctor's greeting
                {
                    Console.WriteLine("Ah, I remember now");
                    Doctor d = new Doctor();
                    d.name = a.name; // doctor object must initialize their name
                    Introduce(d);
                    break;
                }
                else if (answer == "n")
                {
                    Console.WriteLine("Ah, I remember now");
                    Introduce(a);
                    break;
                } else
                {
                    Console.WriteLine("Sorry, I didn't understand. Please answer 'y' or 'n'");
                }
            }
            Console.ReadLine();
        }

        static void Introduce(Person person) // can take either a person or a doctor object
        {
            person.SayGreeting(person.name);
        }
    }
}
