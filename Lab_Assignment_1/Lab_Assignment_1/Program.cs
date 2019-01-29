//Ricardo Ramos
//CECS 475
//Lab Assignment #1
//29 January 2019

using System;
using System.Linq;

namespace LabAssignment1
{
    class Program
    {

        public class IntegerSet
        {
            private bool[] integers;

            //default contstructor
            public IntegerSet()
            {
                integers = new bool[101]; 
            }

            //paratmeterized constructor
            public IntegerSet(int[] intArray)
            {
                integers = new bool[101];

                for (int i = 0; i < intArray.Length; i++)
                {
                    int number = intArray[i];
                    if(number >= 0 && number <= 100)
                    {
                        integers[number] = true;
                    }
                }          
            }

            /// <summary>
            /// This function will allow the user to enter integers
            /// into the IntegerSet array. For each number entered, that 
            /// will be the index of the IntegerSet array and that index
            /// will then be set to true.
            /// </summary>
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

            /// <summary>
            /// This function will return a union set. It will
            /// compare both sets and their indexes. If they are 
            /// both true, then it will combine both indexes and 
            /// only show duplicates once.
            /// </summary>
            /// <param name="otherSet"></param>
            /// <returns>A union set wich contains both numbers in set and duplicates only once.</returns>
            public IntegerSet Union(IntegerSet otherSet)
            {
                IntegerSet unionSet = new IntegerSet(); //declaring the Union set

                for (int i = 0; i <= 100; i++)
                {
                    if (integers[i] == true || otherSet.integers[i] == true)
                    {
                        unionSet.integers[i] = true;
                    }
                }
                return unionSet;
            }

            /// <summary>
            /// This function will compare both IntegerSet objects
            /// and only pick out the IntegerSet indexes that are
            /// the same.
            /// </summary>
            /// <param name="otherSet"></param>
            /// <returns>A intersection set that has the values in both sets only.</returns>
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

            /// <summary>
            /// This function will take in one set and compare it to
            /// another one.
            /// </summary>
            /// <param name="otherSet"></param>
            /// <returns>If both have equal index values
            /// then it will return true else it returns false.</returns>
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

            /// <summary>
            /// This function will take in a value and 
            /// aggregate it to an IntegerSet. It will set
            /// the index value to true.
            /// </summary>
            /// <param name="number"></param>
            public void InsertElement(int number)
            {
                integers[number] = true;
            }

            /// <summary>
            /// This function will take in a value and 
            /// remove it from an IntegerSet object. It 
            /// will set the index value of the number 
            /// to false.
            /// </summary>
            /// <param name="number"></param>
            public void DeleteElement(int number)
            {
                integers[number] = false;
            }

            /// <summary>
            /// This function will return the values in the set.
            /// It will only print out the index values that are true in the 
            /// IntegerSet array index.
            /// </summary>
            /// <returns></returns>
            override
            public string ToString()
            {

                string numbers="";
                for (int i = 0; i <= 100; i++)
                {
                    if (checkNumber(i))
                    {
                        numbers = numbers + " " + i;
                        
                    }
                }
                return numbers;

            }

            /// <summary>
            /// This function will take in a number
            /// and check if the number is between 0 and 100
            /// </summary>
            /// <param name="i"></param>
            /// <returns>if the number is valid it will return the number
            /// else it will return false</returns>
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
            Console.WriteLine("\nNew Set contains elements: ");
            Console.WriteLine(set3.ToString());

            Console.ReadKey(true);

        }

    }

}
