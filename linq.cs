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

IEnumerable<int> evenNumber = intCollection.Where((number, index) => index > 4 && number % 2 == 0); // considering index

// Projection functions:

// Select
public class Employee
{
    public Employee(int ID, string Name, int Salary)
    {
        this.ID = ID;
        this.Name = Name;
        this.Salary = Salary;
    }

    public int ID { get; set; }
    public string Name { get; set; }
    public int Salary { get; set; }
}

List<Employee> employees = new List<Employee>
{
    new Employee(1, "Heinz", 1000),
    new Employee(2, "Sabine", 2000)
};

IEnumerable<string> employeeNames = employees.Select(employee => employee.Name); // Select only name column

var employeeNames = employees.Select(employee => new { employee.ID, employee.Name }); // Select multiple columns with anonymous type

//SelectMany vs Select
public class Employee
{
    public Employee(int ID, string Name, string[] Hobbies)
    {
        this.ID = ID;
        this.Name = Name;
        this.Hobbies = Hobbies;
    }

    public int ID { get; set; }
    public string Name { get; set; }
    public string[] Hobbies { get; set; }
}

List<Employee> employees = new List<Employee>
{
    new Employee(1, "Heinz", new string[]{"Football", "TV" }),
    new Employee(2, "Sabine", new string[]{"Gardening", "Traveling" })
};

var employeesHobbies1 = employees.Select(employee => new { employee.Name, employee.Hobbies }); // 2 rows
var employeesHobbies2 = employees.SelectMany(employee => employee.Hobbies, (employee, hobby)  => new { employee.Name, hobby }); // 4 rows
