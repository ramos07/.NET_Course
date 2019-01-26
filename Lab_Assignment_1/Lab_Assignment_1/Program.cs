using System;
using System.Linq;

namespace HelloWorld1
{
    class Program
    {

        public class IntegerSet
        {
            private bool[] integers;

            public IntegerSet()
            {
                integers = new bool[101];
            }

            public IntegerSet(int[] intArray)
            {

                for (int i = 0; i <= 100; i++)
                {
                    for (int j = 0; j <= 100; j++)
                    {
                        integers[intArray[j]] = true;
                    }
                }

            }

            public void InputSet()
            {
                string userInput;
                int number;
                Console.WriteLine("Enter a number to insert in the set");
                do
                {
                    userInput = Console.ReadLine();
                    number = Convert.ToInt32(userInput);
                    if (number >= 0 && number <= 100)
                    {
                        integers[number] = true;
                    }
                } while (number != -1);

            }

            public IntegerSet Union(IntegerSet otherSet)
            {
                IntegerSet unionSet = new IntegerSet();

                for (int i = 0; i <= 100; i++)
                {
                    if (integers[i] == true || otherSet.integers[i] == true)
                    {
                        unionSet.integers[i] = true;
                    }
                }
                return unionSet;
            }

            public IntegerSet Intersection(IntegerSet otherSet)
            {
                IntegerSet intersectionSet = new IntegerSet();

                for (int i = 0; i <= 100; i++)
                {
                    if (integers[i] == true && otherSet.integers[i] == true)
                    {
                        intersectionSet.integers[i] = true;
                    }
                }
                return intersectionSet;
            }

            public bool IsEqualTo(IntegerSet otherSet)
            {
                for (int i = 0; i <= 100; i++)
                {
                    if (integers[i] != otherSet.integers[i])
                    {
                        return false;
                    }
                }
                return true;
            }

            public void InsertElement(int number)
            {
                integers[number] = true;
            }

            public void DeleteElement(int number)
            {
                integers[number] = false;
            }

            override
            public string ToString()
            {
                string numbers = "{ ";

                for (int i = 0; i <= 100; i++)
                {
                    if (checkNumber(i))
                    {
                        numbers = numbers + " " + i;
                    }
                }
                numbers = numbers + " }";
                return numbers;

            }

            public bool checkNumber(int i)
            {
                if (i >= 0 && i <= 100)
                {
                    return integers[i];
                }
                return false;
            }


        }

        static void Main(string[] args)
        {
            //Initialize two sets
            Console.WriteLine("Input Set A");
            IntegerSet set1 = new IntegerSet();
            set1.InputSet();
            Console.WriteLine("Input Set B");
            IntegerSet set2 = new IntegerSet();
            set2.InputSet();

            IntegerSet union = set1.Union(set2);
            IntegerSet intersection = set1.Intersection(set2);

            //prepare output
            Console.WriteLine("\nSet A contains elements:");
            Console.WriteLine(set1.ToString());
            Console.WriteLine("\nSet B contains elements:");
            Console.WriteLine(set2.ToString());
            Console.WriteLine("\nUnion of Set A and Set B contains elements: ");
            Console.WriteLine(union.ToString());
            Console.WriteLine("\nIntersection of Set A and Set B contains elements: ");
            Console.WriteLine(intersection.ToString());

            //test whether two sets are equal
            if (set1.IsEqualTo(set2))
            {
                Console.WriteLine("\nSet A is equal to Set B");
            }
            else
            {
                Console.WriteLine("\nSet A is not equal to Set B");
            }

            //test insert and delete
            Console.WriteLine("\nInserting 77 into Set A...");
            set1.InsertElement(77);
            Console.WriteLine("\nSet A now contains elements: ");
            Console.WriteLine(set1.ToString());

            Console.WriteLine("\nDeleteing 77 from Set A...");
            set1.DeleteElement(77);
            Console.WriteLine("\nSet A now contains elements: ");
            Console.WriteLine(set1.ToString());

            //test constructor
            int[] intArray = { 25, 67, 2, 9, 99, 105, 45, -5, 100, 1 };
            IntegerSet set3 = new IntegerSet(intArray);
            Console.WriteLine("/nNew Set contains elements: ");
            Console.WriteLine(set3.ToString());

            Console.ReadKey(true);

        }

    }

}
