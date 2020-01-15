using System;
using System.Collections.Generic;

namespace SymbolTablesAndHashTables
{
    class Program
    {
        static void Main(string[] args)
        {
            var number1 = new PhoneNumber ( "141804", "27", "9031334" );
            var number2 = new PhoneNumber ( "141804", "27", "9031334" );
            var number3 = new PhoneNumber ( "141804", "27", "9031334" );

            Console.WriteLine(number1.GetHashCode());
            Console.WriteLine(number2.GetHashCode());
            Console.WriteLine(number1 == number2);
            Console.WriteLine(number1.Equals(number2));

            var customers = new Dictionary<PhoneNumber, Person>();
            customers.Add(number1, new Person());
            //            customers.Add(number2, new Person());

            Console.WriteLine(customers.ContainsKey(number1));
            Console.WriteLine("After adding phone numbers");

//            number1.AreadCode = "141805";
            Console.WriteLine(customers.ContainsKey(number1));

            var c = customers[number2];

            Console.WriteLine();
        }
    }


    public class PhoneNumber
    {
        public string AreadCode { get; }
        public string Exchange { get;  }
        public string Number { get; }
        public string AreaCode { get; }

        public PhoneNumber(string areaCode, string exchange, string number)
        {
            AreaCode = areaCode;
            Exchange = exchange;
            Number = number;
        }

        public override bool Equals(object obj)
        {
            if (obj is PhoneNumber number){
                return string.Equals(number.AreadCode, AreadCode)
                && string.Equals(number.Exchange, Exchange)
                && string.Equals(number.Number, Number);
            }
            return false;
        }

        public static bool operator ==(PhoneNumber left, PhoneNumber right)
        {
            if (object.ReferenceEquals(left, right))
                return true;

            if (object.ReferenceEquals(null, left))
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(PhoneNumber left, PhoneNumber right)
            => !left.Equals(right);

        public override int GetHashCode()
        {
            unchecked
            {
                const int HashingBase = (int)2166136261;
                const int HashingMultiplier = 16777619;

                int hash = HashingBase;
                hash = (hash * HashingMultiplier) ^ (AreadCode is object ? AreadCode.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (Exchange  is object ? Exchange.GetHashCode()  : 0);
                hash = (hash * HashingMultiplier) ^ (Number    is object ? Number.GetHashCode()    : 0);
                return hash;
            }
        }

    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Ssn { get; set; }
    }
}
