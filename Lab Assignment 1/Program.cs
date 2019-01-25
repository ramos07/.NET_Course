using System;

namespace HelloWorld1
{
    class Program
    {

        public class IntegerSet
        {
            private bool[] integers;

            public IntegerSet(bool [] integers) 
            {
                integers = new bool[100]; //bool condition set to false by default
            }

            public static IntegerSet InputSet(bool [] integers)
            {
                string userInput;
                int number;
                do
                {
                    Console.WriteLine("Enter a number to enter in the set (enter -1 to quit)");
                    userInput = Console.ReadLine();
                    number = Convert.ToInt32(userInput);
                    
                    if(number > 0 && number < 100)
                    {
                        for(int i=0; i < 100; i++)
                        {
                            integers[number] = true;
                        }
                        return integers;
                    }
                    else
                    {
                        Console.WriteLine("Enter a number between 0 and 100");

                        return null;
                    }

                } while (number != -1);

            }

        }

        static void Main(string[] args)
        {
            Console.WriteLine("Input Set A");
            IntegerSet set1 = InputSet();
        }

    }
 
}
