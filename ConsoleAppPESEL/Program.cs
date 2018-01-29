using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppPESEL
{
    class Program
    {
        static void Main(string[] args)
        {
            //using function to validate pesel + getting data from other methods
            //Validate.ValidatePESEL(Person.GetDataOfBrith(), Person.GetPESEL(), Person.GetSex());
            
            //creating new object
            Person2 P1 = new Person2();

            //getting data
            Console.WriteLine("Podaj rok urodzenia:");
            P1.YearOfBrith = Console.ReadLine();

            Console.WriteLine("Podaj miesiąc urodzenia(01-12):");
            P1.MonthOfBrith = Console.ReadLine(); 

            Console.WriteLine("Podaj dzień urodzenia(01-31):");
            P1.DayOfBrith = Console.ReadLine();

            Console.WriteLine("Podaj płeć (M lub K):");
            P1.Sex = Console.ReadLine();

            Console.WriteLine("Podaj PESEL:");
            P1.PESEL = Console.ReadLine();

            //printing
            P1.Print();
            Console.WriteLine(P1.PrintDateForValidate());

            //validate
            Validate.ValidatePESEL(P1.PrintDateForValidate(), P1.PESEL, P1.Sex);


            Console.ReadKey();
        }
    }




    interface IPerson2
    {
        string PrintDateForValidate();
        void Print();
    }




    public class Person2 : IPerson2
    {   
        // Creating data fields
        private string _yearOfBirth;
        string _monthOfBirth;
        string _dayOfBirth;
        string _sex;
        string _pesel;


        #region get/set properties
        // Access fields using properties
        public string YearOfBrith
        {
            set
            {
                if (value.Length != 4) { throw new Exception("Rok musi składać się z 4 cyfr, np. 1987."); }
                else this._yearOfBirth = value;
            }
            get { return this._yearOfBirth; }
        }

        public string MonthOfBrith
        {
            set
            {
                if (value.Length != 2) { throw new Exception("Miesiąc musi składać się z 2 cyfr, np. 01, 06, 11."); }
                else this._monthOfBirth = value;
            }
            get { return this._monthOfBirth; }
        }

        public string DayOfBrith
        {
            set
            {
                if (value.Length != 2) { throw new Exception("Dzień musi składać się z 2 cyfr, np. 01, 16, 27."); }
                else this._dayOfBirth = value;
            }
            get { return this._dayOfBirth; }
        }

        public string Sex
        {
            set
            {
                if (value.Length != 1 && ( (value == "M") || (value == "K") )) { throw new Exception("Płeć musi zostać przedstawiona za pomocą litery M dla mężczyzny lub K dla kobiety."); }
                else this._sex = value;
            }
            get { return this._sex; }
        }

        public string PESEL
        {
            set
            {
                if (value.Length != 11) { throw new Exception("PESEL musi składać się z 11 cyfr, np. 87012700252"); }
                else this._pesel = value;
            }
            get { return this._pesel; }
        }
        #endregion



        /// using constructr vs get/set properties?
        //public Person2(string YearOfBirth, string MonthOfBirth, string DayOfBirth, string Sex, string Pesel)
        //{}

        
       
        public string PrintDateForValidate()
        {
            return _yearOfBirth + _monthOfBirth + _dayOfBirth;            
        }

        public void Print()
        {
            Console.WriteLine($"Received data: {_yearOfBirth}.{_monthOfBirth}.{_dayOfBirth}, sex: {_sex}, PESEL: {_pesel}");
        }
    }

    
    /// <summary>
    /// old class used in static class
    /// </summary>
    //    public class Person
    //{   //get date (without checking input)
    //    public static string GetDataOfBrith()
    //    {            

    //        Console.WriteLine("Podaj rok urodzenia:");
    //        string yearOfBirth = Console.ReadLine();  //1987

    //        Console.WriteLine("Podaj miesiąc urodzenia(01-12):");
    //        string monthOfBirth = Console.ReadLine(); //01

    //        Console.WriteLine("Podaj dzień urodzenia(01-31):");
    //        string dayOfBirth = Console.ReadLine(); //27

    //        string fullDate = yearOfBirth + monthOfBirth + dayOfBirth;
    //            Console.WriteLine("     Kontrolny kod daty :{0}", fullDate);  //control
    //        return fullDate;
    //    }

        
    //    //get PESEL + input check
    //    public static string GetPESEL()
    //    {
    //        Start:

    //        Console.WriteLine("Podaj PESEL:");
    //        string value;
    //        value = Console.ReadLine();      //87012700252
    //        if (value.Length == 11) return value;            
    //        else
    //        {
    //            Console.WriteLine("Niepoprawna długość PESEL - wpisz ponownie: ");
    //            goto Start; 
    //        }
    //    }

    //    //get sec + input check
    //    public static string GetSex()
    //    {
    //        Start:

    //        Console.WriteLine("Podaj płeć (M lub K):");
    //        string value = Console.ReadLine();

    //        //checking string value
    //        if ((value == "M")||(value == "K")) return value;  
    //        else if (value=="m")
    //        {
    //            value = "M";
    //            return value;
    //        }
    //        else if (value == "k")
    //        {
    //            value = "K";
    //            return value;
    //        }

    //        else
    //        {
    //            Console.WriteLine("Niepoprawny symbol płci - użyj M lub K: ");
    //            goto Start;
    //        }

    //    }
    //}


    

    public class Validate
    {
        public static void ValidatePESEL(string date, string pesel, string sex) ///data format: 19870127    pesel format: 87012700252
        {
            //validate 1-2 Pesel number (year)
            if (date.Substring(2, 2) == pesel.Substring(0, 2))
            {   //validate 4 Pesel number (second month number)
                if ((date.Substring(5, 1) == pesel.Substring(3, 1)))
                {
                    if (//1800-1899
                        (((pesel.Substring(2, 1) == "8") || (date.Substring(2, 1) == "9")) && (Convert.ToInt16((date.Substring(0, 4))) >= 1800) && (Convert.ToInt16((date.Substring(0, 4))) <= 1899))
                        ||//1900-1999
                        (((pesel.Substring(2, 1) == "0") || (date.Substring(2, 1) == "1")) && (Convert.ToInt16((date.Substring(0, 4))) >= 1900) && (Convert.ToInt16((date.Substring(0, 4))) <= 1999))
                        ||//2000-2099
                        (((pesel.Substring(2, 1) == "2") || (date.Substring(2, 1) == "3")) && (Convert.ToInt16((date.Substring(0, 4))) >= 2000) && (Convert.ToInt16((date.Substring(0, 4))) <= 2099))
                        ||//2100-2199
                        (((pesel.Substring(2, 1) == "4") || (date.Substring(2, 1) == "5")) && (Convert.ToInt16((date.Substring(0, 4))) >= 2100) && (Convert.ToInt16((date.Substring(0, 4))) <= 2199))
                        ||//2200-2299
                        (((pesel.Substring(2, 1) == "6") || (date.Substring(2, 1) == "7")) && (Convert.ToInt16((date.Substring(0, 4))) >= 2200) && (Convert.ToInt16((date.Substring(0, 4))) <= 2299))
                        )
                    {   //validate 10 number (Sex)  - checking if its even number: (num%2==0)
                        if (
                            ((Convert.ToInt16((pesel.Substring(9, 1))) % 2 == 0) && (sex == "K"))
                            ||
                            ((Convert.ToInt16((pesel.Substring(9, 1))) % 2 != 0) && (sex == "M"))
                            )
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
                                                                    //were eg. 169 % 10 give 9
                            if (((9*a + 7*b + 3*c + 1*d + 9*e + 7*f + 3*g +1*h +9*i+ 7*j) % 10) == Convert.ToInt16(pesel.Substring(10, 1)))
                            {
                                Console.WriteLine("PESEL zgodny z danymi osobowymi.");
                            }
                            else Console.WriteLine("PESEL niezgodny z danymi osobowymi(numer_kontrolny).");
                        }
                        else Console.WriteLine("PESEL niezgodny z danymi osobowymi(płeć).");
                    }
                    else Console.WriteLine("PESEL niezgodny z danymi osobowymi(miesiąc lub rok).");
                }
                else Console.WriteLine("PESEL niezgodny z danymi osobowymi(miesiąc).");
            }
            else Console.WriteLine("PESEL niezgodny z danymi osobowymi(rok).");


        }

    }


   


}
