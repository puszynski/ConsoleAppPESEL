using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppPESEL
{
    public interface IPerson
    {
        void Print();
    }


    public interface IIsPeselValidate
    {
        void ValidatePESEL();
    }


    public enum Gender
    {
        Male,
        Female
    }


    class Pesel // ??
    {
        public Pesel()
        {
            // ??
        }
    }


    public class Person : IPerson
    {
        public readonly string _name;
        public readonly DateTime _birthDate;
        public readonly Gender _gender;
        public readonly string _peselNumber;

        public Person(String Name, DateTime BirthDate, Gender Gender, string PeselNumber)
        {
            this._name = Name;
            this._birthDate = BirthDate;
            this._gender = Gender;
            this._peselNumber = PeselNumber;
        }

        public void Print()
        {
            Console.WriteLine($"Data of {this._name}, birth date: {this._birthDate}, gender: {this._gender}, PESEL: {this._peselNumber}");
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            Person human01 = new Person("James", new DateTime(1987, 01, 27), Gender.Male, "87012700252");
            Person human02 = new Person("Jessie", new DateTime(1823, 4, 23), Gender.Female, "23042300123");
            Person human04 = new Person("Joseph", new DateTime(1833, 04, 23), Gender.Female, "23042300123");

            human01.Print();
            human02.Print();
            human04.Print();

            Console.WriteLine();

            IsPeselValidate validatingPesel = new IsPeselValidate();
            validatingPesel.ValidatePESEL(human01);
            validatingPesel.ValidatePESEL(human02);
            validatingPesel.ValidatePESEL(human04);

            Console.ReadKey();
        }
    }



    public class IsPeselValidate
    {
        public void ValidatePESEL(Person per)
        {
            if (ValidatePeselYear(per._birthDate, per._peselNumber)) Console.WriteLine("PESEL niezgodny z danymi osobowymi(rok).");
            else if (ValidatePeselGender(per._gender, per._peselNumber)) Console.WriteLine("PESEL niezgodny z danymi osobowymi(płeć).");
            else if (ValidatePeselControlNumber(per._peselNumber)) Console.WriteLine("PESEL niezgodny z danymi osobowymi(numer_kontrolny).");
            else Console.WriteLine("Pesel zgodny z danymi osobowymi.");
        }


        bool ValidatePeselYear(DateTime date, string pesel)
        {
            if (Convert.ToString(date.Year).Substring(2, 2) == pesel.Substring(0, 2)) return false;
            else return true;
        }

        bool ValidatePeselGender(Gender gender, string pesel)
        {
            if (
                    ((Convert.ToInt16((pesel.Substring(9, 1))) % 2 == 0) && (gender == Gender.Female))
                    ||
                    ((Convert.ToInt16((pesel.Substring(9, 1))) % 2 != 0) && (gender == Gender.Male))
                ) { return false; }
            else return true;
        }

        bool ValidatePeselControlNumber(string pesel)
        {   //validate 11 number(control)
            int a = Convert.ToInt16(pesel.Substring(0, 1));
            int b = Convert.ToInt16(pesel.Substring(1, 1));
            int c = Convert.ToInt16(pesel.Substring(2, 1));
            int d = Convert.ToInt16(pesel.Substring(3, 1));
            int e = Convert.ToInt16(pesel.Substring(4, 1));
            int f = Convert.ToInt16(pesel.Substring(5, 1));
            int g = Convert.ToInt16(pesel.Substring(6, 1));
            int h = Convert.ToInt16(pesel.Substring(7, 1));
            int i = Convert.ToInt16(pesel.Substring(8, 1));
            int j = Convert.ToInt16(pesel.Substring(9, 1));
            //were e.g. 169 % 10 give 9
            if (((9 * a + 7 * b + 3 * c + 1 * d + 9 * e + 7 * f + 3 * g + 1 * h + 9 * i + 7 * j) % 10) == Convert.ToInt16(pesel.Substring(10, 1)))
            {
                return false;
            }
            else return true;
        }
    }