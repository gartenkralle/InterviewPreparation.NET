// Advantages:

// Linq can query any type of data store in a uniform way
// (data bases, xml documents, in memory objects)

// Compile time error checking
// Intellisense

// Extendable
// linq to twitter, linq to amazon, linq to google


// Disadvantages:



// Create own extension method:

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

// Predefined extension methods in System.Linq namespace

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

// Where function (restrictions):

IEnumerable<int> evenNumbers = intCollection.Where(number => number % 2 == 0);