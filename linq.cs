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
    new Employee(1, "Sabine", new string[]{"Football", "TV" }),
    new Employee(2, "Heinz", new string[]{"Gardening", "Traveling" })
};

var employeesHobbies1 = employees.Select(employee => new { employee.Name, employee.Hobbies }); 
// 2 rows
// Hobbies = IEnumerable<string[]>
// 2 nested for loops are needed to iterate over all hobbies

var employeesHobbies2 = employees.SelectMany(employee => employee.Hobbies, (employee, hobby)  => new { employee.Name, hobby });
// 4 rows
// Hobies = IEnumerable<string>
// 1 for loop is needed to iterate over all hobbies (less complex code)


//Ordering functions:

//Primary sort
IEnumerable<Employee> orderedEmpoyees1 = employees.OrderBy(employee => employee.Name);
IEnumerable<Employee> orderedEmpoyees2 = employees.OrderByDescending(employee => employee.Name);

//Secondary sort
IEnumerable<Employee> orderedEmpoyees1 = employees.OrderBy(employee => employee.Name).ThenBy(employee => employee.ID);
IEnumerable<Employee> orderedEmpoyees2 = employees.OrderBy(employee => employee.Name).ThenByDescending(employee => employee.ID);

//ThenBy can be chained, OrderBy not
//ThenBy(...).ThenBy(...).etc

//Reverse
string[] alphabet = new string[] { "A", "B", "C" };

IEnumerable<string> arr = alphabet.Reverse(); //C B A


//Partitioning functions:
int[] intCollection = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

IEnumerable<int> ints1 = intCollection.Take(3); // 1, 2, 3
IEnumerable<int> ints2 = intCollection.Skip(3); // 4, 5, 6, 7, 8, 9, 10
IEnumerable<int> ints3 = intCollection.Skip(3).Take(3); // 4, 5, 6

IEnumerable<int> ints4 = intCollection.TakeWhile(x => x < 6); // 1, 2, 3, 4, 5
IEnumerable<int> ints5 = intCollection.SkipWhile(x => x < 6); // 6, 7, 8, 9, 10

