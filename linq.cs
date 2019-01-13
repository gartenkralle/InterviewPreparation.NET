// Advantages:

// Linq can query any type of data store in a uniform way
// (data bases, xml documents, in memory objects)

// Compile time error checking
// Intellisense

// Extendable
// linq to twitter, linq to amazon, linq to google


// Disadvantages:



// Syntax:

int[] numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

IEnumerable<int> evenNumbers = numbers.Where(number => number % 2 == 0);


// Create extension method:

class Program
{
    static void Main(string[] strings)
    {
        Console.WriteLine("hello".FirstUpperCase()); // Hello
    }
}

public static class StringHelper
{
    public static string FirstUpperCase(this string s)
    {
        return s[0].ToString().ToUpper() + s.Substring(1);
    }
}


// Aggregate functions:

int min = numbers.Min();
int max = numbers.Max();
int sum = numbers.Sum();
int count = numbers.Count();
double average = numbers.Average();

int shortestStringCount = stringCollection.Min(x => x.Length);
int longestStringCount = stringCollection.Max(x => x.Length);

string singleString = stringCollection.Aggregate((a, b) => a + " " + b); // string join

int product = intCollection.Aggregate((a, b) => a * b); // build product of entire collection entries

