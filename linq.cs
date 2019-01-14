// Advantages:

// Linq can query any type of data store in a uniform way
// (data bases, xml documents, in memory objects)

// Compile time error checking
// Intellisense

// Extendable
// linq to twitter, linq to amazon, linq to google


// Disadvantages:



// Linq to SQL Connection:

DataClassesDataContext data = new DataClassesDataContext();
System.Data.Linq.Table<Person> persons = data.Person;

foreach(Person person in persons)
{
    Console.WriteLine(person.Name);
}

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

// Lazy/Deferred execution functions:
// Select, Where, Take, Skip, ToLookup, Cast, OfType
// Each function which returns an interface collection

// Eager execution functions:
// Count, Average, Min, Max, ToList, ToArray, ToDictionary
// Each function which NOT returns an interface collection


//ToDictionary
Dictionary<int, Employee> dictionary1 = employees.ToDictionary(employee => employee.ID);
Dictionary<int, string> dictionary2 = employees.ToDictionary(employee => employee.ID, employee => employee.Name);


//ToLookup (data structure which can contain duplicate keys)
ILookup<int, Employee> lookup1 = employees.ToLookup(employee => employee.ID);

foreach (IGrouping<int, Employee> group in lookup1)
{
    Console.WriteLine(group.Key);

    foreach (Employee employee in group)
    {
        Console.WriteLine(" " + employee.Name);
    }
}


ILookup<int, string> lookup2 = employees.ToLookup(employee => employee.ID, employee => employee.Name);

foreach (IGrouping<int, string> group in lookup2)
{
    Console.WriteLine(group.Key);

    foreach (string name in group)
    {
        Console.WriteLine(" " + name);
    }
}


// Cast (if element cannot be converted an exception is thrown)
List<object> intCollection = new List<object> { 1, 2, 3, "exception" };

IEnumerable<int> ints = intCollection.Cast<int>();

foreach (long number in ints)
    Console.WriteLine(number);

// OfType (if element cannot be converted the element is skipped, works as a filter)
List<object> intCollection = new List<object> { 1, 2, 3, "no exception" };

IEnumerable<int> ints = intCollection.OfType<int>();

foreach (long number in ints)
    Console.WriteLine(number);


//AsEnumerable (tranfers all data to client side, all subsequent operations will be executed at client side)
IEnumerable<Employee> employeeCollection = employees.AsEnumerable();
//Time advantage: if table is small AND there are many requests for this table
//Time disadvantag: if table is big AND there are only few requests which operate on a small subset of the table


//GroupBy
public class Employee
{
    public Employee(int ID, string Name, string Department)
    {
        this.ID = ID;
        this.Name = Name;
        this.Department = Department;
    }

    public int ID { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
}

List<Employee> employees = new List<Employee>
{
    new Employee(1, "Sabine", "HR"),
    new Employee(2, "Heinz", "HR"),
    new Employee(3, "John", "IT"),
    new Employee(4, "Peter", "IT")
};

IEnumerable<IGrouping<string, Employee>> groups = employees.GroupBy(employee => employee.Department); // 2 Groups

foreach (IGrouping<string, Employee> group in groups)
{
    Console.WriteLine(group.Key);

    foreach (Employee employee in group)
    {
        Console.WriteLine(" " + employee.ID + " " + employee.Name);
    }
}


//GroupBy (multiple keys)
public class Employee
{
    public Employee(int ID, string Name, string Department, string Gender)
    {
        this.ID = ID;
        this.Name = Name;
        this.Department = Department;
        this.Gender = Gender;
    }

    public int ID { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public string Gender { get; set; }
}

List<Employee> employees = new List<Employee>
{
    new Employee(1, "Sabine", "HR", "Female"),
    new Employee(2, "Julia", "IT", "Female"),
    new Employee(3, "John", "HR", "Male"),
    new Employee(4, "Peter", "IT", "Male")
};

IEnumerable<IGrouping<string, Employee>> departmentGroups = employees.GroupBy(employee => employee.Department); 

foreach (IGrouping<string, Employee> departmentGroup in departmentGroups)
{
    IEnumerable<IGrouping<string, Employee>> genderGroups = departmentGroup.GroupBy(employee => employee.Gender); // 2x2 Groups
    Console.WriteLine(departmentGroup.Key);

    foreach (IGrouping<string, Employee> genderGroup in genderGroups)
    {
        Console.WriteLine(" " + genderGroup.Key);

        foreach (Employee employee in genderGroup)
        {
            Console.WriteLine("  " + employee.ID + " " + employee.Name);
        }
    }
}


//Element operators
int first = numbers.First(); //if collection is empty exception is thrown
int first = numbers.First(n => n % 2 == 0);

int first = numbers.FirstOrDefault(); //if collection is empty NO exception is thrown
int first = numbers.FirstOrDefault(n => n % 2 == 0);

int last = numbers.Last(); //if collection is empty exception is thrown
int last = numbers.Last(n => n % 2 == 0);

int last = numbers.LastOrDefault(); //if collection is empty NO exception is thrown
int last = numbers.LastOrDefault(n => n % 2 == 0);

int result = numbers.ElementAt(index); //if collection is empty exception is thrown
int result = numbers.ElementAtOrDefault(index); //if collection is empty NO exception is thrown

int result = numbers.Single(); //if collection is empty OR has more than one element exception is thrown
int result = numbers.Single(n => n % 2 == 0);

int result = numbers.SingleOrDefault(); //if collection is empty OR has more than one element NO exception is thrown
int result = numbers.SingleOrDefault(n => n % 2 == 0);

IEnumerable<int> result = intCollection.DefaultIfEmpty(); //if collection is empty NO exception is thrown
IEnumerable<int> result = intCollection.DefaultIfEmpty(1); //if collection is empty NO exception is thrown


//Data structure + data for following joins
public class Employee
{
    public Employee(int ID, string Name, string Gender, int DepartmentID)
    {
        this.ID = ID;
        this.Name = Name;
        this.DepartmentID = DepartmentID;
        this.Gender = Gender;
    }

    public int ID { get; set; }
    public string Name { get; set; }
    public int DepartmentID { get; set; }
    public string Gender { get; set; }
}

public class Department
{
    public Department(int ID, string Name)
    {
        this.ID = ID;
        this.Name = Name;
    }

    public int ID { get; set; }
    public string Name { get; set; }
}

List<Employee> employeeCollection = new List<Employee>
{
    new Employee(1, "Sabine", "Female", 1),
    new Employee(2, "Julia", "Female", 1),
    new Employee(3, "John", "Male", 2),
    new Employee(4, "Peter", "Male", 99)
};

List<Department> departmentCollection = new List<Department>
{
    new Department(1, "HR"),
    new Department(2, "IT")
};

//Join = Inner Join
var employeeDepartmentCollection = departmentCollection.Join(
    employeeCollection,
    department => department.ID,
    employee => employee.DepartmentID,
    (department, employee) => new { department, employee });

foreach (var employeeDepartment in employeeDepartmentCollection)
{
    Console.WriteLine(employeeDepartment.department.Name);
    Console.WriteLine(employeeDepartment.employee.Name);
}

//Group Join = Left Join || Right Join
var employeesByDepartmentCollection = departmentCollection.GroupJoin(
    employeeCollection, 
    department => department.ID, 
    employee => employee.DepartmentID, 
    (department, employees) => new { department, employees });

foreach(var employeesByDepartment in employeesByDepartmentCollection)
{
    Console.WriteLine(employeesByDepartment.department.Name);

    foreach(Employee employee in employeesByDepartment.employees)
    {
        Console.WriteLine(" " + employee.Name);
    }
}

//Full Join
//Left Join UNION Right Join - Inner Join

//Cross Join with SelectMany
var employeesDepartmentsCollection = departmentCollection.SelectMany(
    department => employeeCollection,
    (department, employee) => new { department, employee });

foreach (var employeeDepartment in employeesDepartmentsCollection)
{
    Console.WriteLine(employeeDepartment.department.Name);
    Console.WriteLine(employeeDepartment.employee.Name);
}

//Cross Join with Join
var employeesDepartmentsCollection = departmentCollection.Join(
    employeeCollection,
    department => true,
    employee => true,
    (department, employee) => new { department, employee });

foreach (var employeeDepartment in employeesDepartmentsCollection)
{
    Console.WriteLine(employeeDepartment.department.Name);
    Console.WriteLine(employeeDepartment.employee.Name);
}

//Distinct
string[] continents = { "USA", "usa" };

IEnumerable<string> distinctContinents = continents.Distinct(StringComparer.OrdinalIgnoreCase); //"USA"

//Data
List<Employee> employeeCollection = new List<Employee>
{
    new Employee(1, "John"),
    new Employee(1, "John"),
};

//Distinct with reference types (1)
public class Employee
{
    public Employee(int ID, string Name)
    {
        this.ID = ID;
        this.Name = Name;
    }

    public int ID { get; set; }
    public string Name { get; set; }
}

public class EmployeeComparer : IEqualityComparer<Employee>
{
    public bool Equals(Employee x, Employee y)
    {
        return (x.ID == y.ID) && (x.Name == y.Name);
    }

    public int GetHashCode(Employee obj)
    {
        return obj.ID.GetHashCode() ^ obj.Name.GetHashCode();
    }
}

IEnumerable<Employee> distinctEmployees = employeeCollection.Distinct(new EmployeeComparer());

//Distinct with reference types (2)
public class Employee
{
    public Employee(int ID, string Name)
    {
        this.ID = ID;
        this.Name = Name;
    }

    public int ID { get; set; }
    public string Name { get; set; }

    public override bool Equals(object obj)
    {
        return (((Employee)obj).ID == ID) && (((Employee)obj).Name == Name);
    }

    public override int GetHashCode()
    {
        return ID.GetHashCode() ^ Name.GetHashCode();
    }
}

IEnumerable<Employee> distinctEmployees = employeeCollection.Distinct();

//Distinct with reference types (3)
public class Employee
{
    public Employee(int ID, string Name)
    {
        this.ID = ID;
        this.Name = Name;
    }

    public int ID { get; set; }
    public string Name { get; set; }
}

//Anonymous Types override Equals and GetHashCode internally
var distinctEmployees = employeeCollection.Select(employee => new { employee.ID, employee.Name }).Distinct();

//Union
int[] intCollection1 = { 1, 2, 3 };
int[] intCollection2 = { 2, 3, 4 };

IEnumerable<int> distinctUniontInts = intCollection1.Union(intCollection2); //1, 2, 3, 4

//Data
List<Employee> employeeCollection1 = new List<Employee>
{
    new Employee(1, "John"),
    new Employee(1, "Sarah"),
};

List<Employee> employeeCollection2 = new List<Employee>
{
    new Employee(1, "John"),
    new Employee(1, "Peter"),
};

//Union
var distinctUnion = // 3 Employees
    employeeCollection1.Select(employee => new { employee.ID, employee.Name }).
    Union
    (employeeCollection2.Select(employee => new { employee.ID, employee.Name }));

//Intersect
var intersect = // 1 Employee (John)
    employeeCollection1.Select(employee => new { employee.ID, employee.Name }).
    Intersect
    (employeeCollection2.Select(employee => new { employee.ID, employee.Name }));

//Except
var except = // 1 Employee (Sarah)
    employeeCollection1.Select(employee => new { employee.ID, employee.Name }).
    Except
    (employeeCollection2.Select(employee => new { employee.ID, employee.Name }));


//Range
IEnumerable<int> intCollection = Enumerable.Range(1, 10).Where(x => x % 2 == 0); //2, 4, 6, 8, 10

//Repeat
IEnumerable<string> stringCollection = Enumerable.Repeat("Hello", 3); //Hello, Hello, Hello

//Empty (use empty function instead of null to prevent NullReference Exceptions while accessing)
IEnumerable<string> ints = Enumerable.Empty<string>(); //Empty collection

