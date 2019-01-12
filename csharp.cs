/* Useful tools */

// ILSpy (http://ilspy.net/)

// Search for type
// Right click, analyse
// Used by


/* XML Documentation comments */

/// <summary>
/// This is a test class.
/// </summary>
class Test
{

}

/* Regions */

public class Customer
{
    #region Private Fields //toggle group of fields
    private int _id;
    private string _firstName;
    private string _lastName;
    #endregion
}

/* Access modifiers for members */

private             // Only within the containing type (default)
public              // Anywhere
protected           // Only within the containing type and all derived types
internal            // Only within the containing assembly
protected internal  // Only within the containing assembly and all derived types within other assemblies

/* Access modifiers for types */

public              // Anywhere
internal            // Only within the containing assembly (default)

/* Usefull Methods for Strings */

string text = "1 2 3";
string[] numbers = text.Split(' ');         // string to array
text = String.Join(" ", numbers);           // array to string
int.Parse(numbers[0]);                      // string to int
string.IsNullOrEmpty("string");             // returns true if string is null or empty
Console.WriteLine("Text is: {0}", text);    // formatted output (to prevent code injection)

/* Usefull Methods for Arrays */

string[] numbers2 = new string[] { "1", "2", "3" };
int[] numberArr = Array.ConvertAll(numbers2, int.Parse); // string array to int array

/* Nullable data type */   // to map database values

int? count = null;	        // initialize count with null
int count2 = count ?? 0;    // if count is null then count2 is set to 0 otherwise count2 is set to count

/* Cast vs. Convert class vs. Parse method */

long number = 32853454354;

int res = (int)number;           // doesn't throw an exception (result could be wrongly converted, data type overflow)
Convert.ToInt32(32853454354);    // throws an exception (recommended) -> exception should be catched
                                 // accepts every value type as argument
                                 // implicit null check if string type
int.Parse(null);                 // throws exception
                                 // accepts only string type
                                 // no null check if string type

/* Cast vs. as */

int res = (int)number;           // doesn't throw an exception, only works for value types
Vehicle v = car as Vehicle       // doesn't throw an exception, only works for reference types

/* Convert.ToString() vs object.ToString() */

Convert.ToString(null);         // throws no exception
null.ToString();                // throws exception

/* Parse vs. TryParse */       // only accepts string as argument

int.Parse("32853454354");	    // throws an exception, use it if the values must be valid -> exception should be catched
                                // performance: good for rarely invalid values

int result;			            // doesn't throw an exception, use it if the values could be invalid -> return type should be evaluated
                                // performance: good for frequently invalid values (faster)
bool isConversionSuccessful = int.TryParse("32853454354", out result);

/* Array vs. List */

int[] arr = new int[3];             // frequently access, rarely size change -> use arrays
List<int> list = new List<int>();   // rarely access, frequently size change -> use lists

/* String vs. Stringbuilder */
string s                            // rarely content change, frequently use -> use string
StringBuilder sb                    // frequently content change, rarly use -> use StringBuilder

/* Optional parameters */

private static void PrintSum(params int[] a)
{
    Console.WriteLine(a.Sum());
}

PrintSum();         // valid
PrintSum(1);        // valid
PrintSum(1, 2);     // valid

public void SomeMethod(int a, int b = 0)
{
   //some code
}

/* Ref vs. Out vs. None */

none // 1-way, only passing value to funtion
out  // 1-way, only getting value from function
ref  // 2-ways, passing value to function and getting value from function

/* Constructor concatenation */

public Customer(int salary) { }
public Customer() : this(2000) { }

/* Calling base class constructor */

public BaseClass(int i) { }
public DerivedCalss() : base (20) { }

/* Method hiding */

public void PrintSalary() { }       // base class method
public new void PrintSalary() { }   // derived class method

/* Calling hidden method */

((BaseClass)derivedObject).PrintSalary();

/* Overriding methods */

virtual public void PrintSalary() { }        // base class method with implementation
override public void PrintSalary() { }       // derived class method

or

abstract public void PrintSalary();          // base class method without implementation
override public void PrintSalary() { }       // derived class method

/* Method hidding vs overriding */

BaseClass c1 = new DerivedCalss();          // method hidding
c1.PrintSalary();                           // calls base class method

BaseClass c1 = new DerivedCalss();          // method overriding
c1.PrintSalary();                           // calls derived class method

/* Class vs. Struct */

class Test { }          // reference type
                        // stored on heap
                        // can inherit from classes and interfaces

struct Test { }         // value type
                        // stored on stack
                        // can only inherit from interfaces (structs are implicitly sealed)

/* Abstract class vs. Interface */

abstract class Test { } // can have implementation
interface ITest { }     // cannot have implementation


/* Abstract class vs. Sealed class */

abstract class Test { } // only for inheritance
sealed class Test { }   // only for instanciation


/* Static class */

static class Test { }   // can only have static members


/* Volatile keyword */

// Indicates a filed can be changed by multiple threads which are on execution

/* Multiple class inheritance */

class Program
{
    static void Main(string[] args)
    {
        AB ab = new AB();
        ab.MethodA();
        ab.MethodB();

        IA a = new AB();
        a.MethodA();
        IB b = new AB();
        b.MethodB();
    }
}

interface IA
{
    void MethodA();
}
class A : IA
{
    public void MethodA()
    {
        Console.WriteLine("A");
    }
}

interface IB
{
    void MethodB();
}
class B : IB
{
    public void MethodB()
    {
        Console.WriteLine("B");
    }
}

class AB : IA, IB
{
    private IA a = new A();
    private IB b = new B();

    public void MethodA()
    {
        a.MethodA();
    }

    public void MethodB()
    {
        b.MethodB();
    }
}


/* Delegates */

// function as a parameter for another function
// a delegate is a data type for a function
// advantage: to make implemenations interchangeable

delegate bool Condition(int x);

class Program
{
    static void Main(string[] args)
    {
        Calculation(15, isConditionTrue);            
    }

    static void Calculation(int x, Condition condition)
    {
        if (condition(x))
            Console.WriteLine("x is greater than 10");
    }

    static bool isConditionTrue(int x)
    {
        return x > 10;
    }
}


/* Mutlicast Delegates */

// no asynchronous calls
// one after another

delegate void Print();

class Program
{
    static void Main(string[] args)
    {
        Print printer = HelloOne;
        printer += HelloTwo;

        printer();
    }
    
    static void HelloOne()
    {
        Console.WriteLine("One");
    }

    static void HelloTwo()
    {
        Console.WriteLine("Two");
    }
}

// Output:
// One
// Two


/* Prevent exceptions to be thrown */

// Prevent exceptions to be thrown because the program will terminate after try/catch/finally block
// Try to solve the problem before the exception is thrown through checking result variables
// and repeatedly user interaction


/* Exception handling */

class Program
{
    static void Main(string[] args)
    {
        StreamReader sr = null;

        try
        {
            sr = new StreamReader(@"C:\Sample\Data1.txt");
            Console.WriteLine(sr.ReadToEnd());
        }
        catch(FileNotFoundException ex)
        {
            Console.WriteLine("Please check if the file {0} exists", ex.FileName);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            if (sr != null)
                sr.Close();
        }
    }
}


/* Inner Exception */

public class Program
{
    public static void Main()
    {
        try
        {
            try
            {
                int num = int.Parse("notConvertible");
            }
            catch (Exception ex)
            {
                try
                {
                    FileStream openLog = File.Open(@"c:\pathNotExists", FileMode.Open);
                }
                catch(FileNotFoundException)
                {
                    throw new FileNotFoundException("Datei nicht gefunden.", ex);
                }
            }
        }
        catch (Exception e)
        {
            printExceptionPath(e);
        }
    }

    private static void printExceptionPath(Exception e)
    {
        if(e.InnerException != null)
            printExceptionPath(e.InnerException);

        Console.WriteLine(e.Message);
    }
}

/* Exception: loop is interrupted */

try
{
    for (int i = 0; i < 10; i++)
    {
        Console.WriteLine(i);
        throw new Exception();
    }
}
catch (Exception)
{
    Console.WriteLine("catch");
}

/* Exception: loop is not interrupted */

for (int i = 0; i < 10; i++)
{
    try
    {
        Console.WriteLine(i);
        throw new Exception();
    }
    catch (Exception)
    {
        Console.WriteLine("catch");
    }
}

// 1. When exception occur code will continue with next catch block
// 2. Then code will continue after the catch block

/* Custom Exception */

public class Program
{
    public static void Main()
    {
        try
        {
            Login();
        }
        catch (UserAlreadyLoggedInException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static void Login()
    {
        bool alreadyLoggedIn = true;

        if(alreadyLoggedIn)
            throw new UserAlreadyLoggedInException("User Already logged in");
    }
}


[Serializable]
public class UserAlreadyLoggedInException : Exception
{
    public UserAlreadyLoggedInException(string message) : base(message)
    {
    }


    public UserAlreadyLoggedInException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public UserAlreadyLoggedInException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}


/* Enums */

public class Program
{
    public static void Main()
    {
        int[] values = (int[])Enum.GetValues(typeof(Gender));
        Console.WriteLine("Gender Enum Values:");

        foreach (int value in values)
            Console.WriteLine(value);

        Console.WriteLine();

        string[] names = Enum.GetNames(typeof(Gender));
        Console.WriteLine("Gender Enum Names:");

        foreach (string name in names)
            Console.WriteLine(name);
    }
}
public enum Gender : int
{
    Unknown = 0,
    Male,
    Female
}

/* Attributes */

public class Calculator
{
    [Obsolete("Use Add(List<int> Numbers) instead")]
    public static int Add(int FirstNumber, int SecondNumber)
    {
        return FirstNumber + SecondNumber;
    }

    public static int Add(List<int> Numbers)
    {
        int Sum = 0;

        foreach (int Number in Numbers)
            Sum = Sum + Number;

        return Sum;
    }
}

/* Reflection (type & object information) */

namespace ConsoleApplication1
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Type type = Type.GetType("ConsoleApplication1.Customer");     // Get type information at runtime
            //Type type = typeof(Customer);                                 // Get type information at compile-time
            Type type = new Customer().GetType();                           // Get object information at compile-time

            Console.WriteLine(type.FullName);

            PropertyInfo[] propertyInfos = type.GetProperties();

            foreach (PropertyInfo propertyInfo in propertyInfos)
                Console.WriteLine(propertyInfo.PropertyType.Name + " " + propertyInfo.Name);
        }
    }

    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}

// Output: 
// ConsoleApplication1.Customer
// Int32 ID
// String Name


/* Reflection (type & object loading) */

namespace ConsoleApplication1
{
    public class Program
    {
        private static void Main()
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            Type customerType = executingAssembly.GetType("ConsoleApplication1.Customer");

            object customerInstance = Activator.CreateInstance(customerType);

            MethodInfo getFullName = customerType.GetMethod("GetFullName");

            string[] methodParameters = new string[2];
            methodParameters[0] = "Pragim";
            methodParameters[1] = "Tech";

            string fullName = (string)getFullName.Invoke(customerInstance, methodParameters);

            Console.WriteLine(fullName);
        }
    }
    public class Customer
    {
        public string GetFullName(string FirstName, string LastName)
        {
            return FirstName + " " + LastName;
        }
    }
}


/* Generics */

// Generic class
public class Calculator<T>
{
    public static bool AreEqual(T value1, T value2)
    {
        return value1.Equals(value2);
    }
}

// Generic method
public class Calculator
{
    public static bool AreEqual<T>(T value1, T value2)
    {
        return value1.Equals(value2);
    }
}

public class Program
{
    private static void Main()
    {
        Calculator<int>.AreEqual(123, 123); // Use of generic class (explicit type)
        Calculator.AreEqual(123, 123);      // Use of generic method (implicit type)
    }
}

/* Partial classes */

public partial class WebForm { }    // split a class to separate user inferface from business logic

/* var vs. Special class */

// only use var if you have to assign an anonymous type


/* 2-dimensional array vs. jagged array */


/* Singleton */

// Eager loading
// Prefer if the singleton object is needed in any case
class Singleton
{
    private Singleton() { }
    private static Singleton instance = new Singleton();

    public static Singleton Instance
    {
        get
        {
            return instance;
        }
    }
}

// Lazy loading (classic way)
// Prefer if the singleton object is not needed in any cae
class Singleton
{
    private Singleton() { }
    private static volatile Singleton instance;

    public static Singleton Instance
    {
        get
        {
            if (instance == null)
            {
                lock(_lock)
                {
                    if (instance == null)
                    {
                        instance = new Singleton();
                    }
                }
            }
            return instance;
        }
    }

    private static object _lock = new object();
}

// Lazy loading (with Lazy class)
class Program
{
    static void Main(string[] args)
    {
        Lazy<Foo> foo = new Lazy<Foo>();
        int? id = null;

        if (!foo.IsValueCreated)
            id = (foo.Value as Foo).ID;

        Console.WriteLine(id);
    }
}

public class Foo
{
    public int ID { get; set; }

    public Foo()
    {
        ID = 1;
    }
}


/* Indexer */

using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Car car = new Car();

            Console.WriteLine(car[0]);
            car[0] = "wheel0";
            Console.WriteLine(car[0]);
        }
    }

    class Car
    {
        private List<string> wheels = new List<string> { "wheel1", "wheel2", "wheel3", "wheel4" };

        public string this[int index]
        {
            get
            {
                return wheels[index];
            }
            set
            {
                wheels[index] = value;
            }
        }
    }
}


/* Optional parameters */

using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(OptionalParameters(1, 2));
            Console.WriteLine(OptionalParameters(1, 2, 3));
        }

        private static int OptionalParameters(int a, int b, params int[] rest)
        {
            int result = a + b;

            if(rest != null)
            {
                foreach(int c in rest)
                {
                    result += c;
                }
            }

            return result;
        }
    }
}


/* Default parameters and named parameters */

using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Test(5, c: 100);
        }

        static void Test(int a, int b = 10, int c = 20)
        {
            Console.WriteLine("a: " + a);
            Console.WriteLine("b: " + b);
            Console.WriteLine("c: " + c);
        }
    }
}


/* Sortable complex type */

// 1. list.sort()
// recommented if implementation is known
class Customer : IComparable<Customer>
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Salary { get; set; }

    public int CompareTo(Customer other)
    {
        return Name.CompareTo(other.Name);
    }
}

// 2. customerList.Sort(new CustomerComparer())
class CustomerComparer : IComparer<Customer>
{
    public int Compare(Customer x, Customer y)
    {
        return x.Name.CompareTo(y.Name);
    }
}

class Customer
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Salary { get; set; }
}

// 3. customerList.Sort(lambdaExpression)
customerList.Sort((x, y) => x.Name.CompareTo(y.Name));

// 4. anonymous function
customerList.Sort(delegate(Customer a, Customer b) { return a.ToString().CompareTo(b.ToString()); });

// 5. comparison delegate
Comparison<Customer> comp = delegate (Customer a, Customer b) { return a.ToString().CompareTo(b.ToString()); };
customerList.Sort(comp);

// 6. customerList.Sort(Helper.CustomComparer)
//    recommented if implementation is not known
class Helper
{
    public static int CustomComparer(Customer x, Customer y)
    {
        return x.Name.CompareTo(y.Name);
    }
}

class Customer
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Salary { get; set; }
}


/* Thread class (blocking) */
// not recommented

using System;
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Number number = new Number(12);

            Thread thread = new Thread(number.CalculateResult);
            thread.Start(); // fork

            Console.WriteLine("This is the main thread.");

            thread.Join(); // join (blocking)
            Console.WriteLine(number.Result);
        }
    }

    class Number
    {
        private int parameter;
        public int Result { get; private set; }

        public Number(int parameter)
        {
            this.parameter = parameter;
        }

        private int MethodWhichTakesLongTime()
        {
            int result = 0;

            Thread.Sleep(5000);
            result = 2 * parameter;

            return result;
        }

        public void CalculateResult()
        {
            Result = MethodWhichTakesLongTime();
        }
    }
}


/* Thread class (non-blocking) */
// not recommented

using System;
using System.Threading;

namespace ConsoleApp
{
    public delegate void PrintResult(int result);

    class Program
    {
        static void Main(string[] args)
        {
            Number number = new Number(12, PrintResult);

            Thread thread = new Thread(number.MethodWhichTakesLongTime);
            thread.Start(); // fork

            Console.WriteLine("This is the main thread.");

            // implicit join at the end of the program to keep program responsive (non-blocking)
        }

        static void PrintResult(int result)
        {
            Console.WriteLine(result);
        }
    }

    class Number
    {
        private int parameter;
        private int Result { get; set; }
        private PrintResult printResult = null;

        public Number(int parameter, PrintResult printResult)
        {
            this.parameter = parameter;
            this.printResult = printResult;
        }

        public void MethodWhichTakesLongTime()
        {
            int result = 0;

            Thread.Sleep(5000);
            result = 2 * parameter;

            printResult(result);
        }
    }
}


/* Await and Async (blocking) */
// not recommented

using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Task<int> task = AsyncMethod();  // fork 1  child thread
            Task<int> task2 = AsyncMethod(); // fork 2. child thread

            Console.WriteLine("kkk"); // main thread

            Console.WriteLine(task.Result + task2.Result); // join child threads (blocking)
        }

        static int MethodWhichTakesLongTime()
        {
            int result = 0;

            Thread.Sleep(5000);
            result = 12;

            return result;
        }

        static async Task<int> AsyncMethod()
        {
            Task<int> task = new Task<int>(MethodWhichTakesLongTime);

            task.Start(); // fork

            // time consumsing method is running in own thread
            // main thread is still responsive

            int result = await task; // wait for thread to complete (no join)

            // other asyncronous taks (optional)
            
            return result;
        }
    }
}


/* Await and Async (non-blocking, without parameters) */
// recommented

private async void button1_Click(object sender, EventArgs e)
{
    button1.Enabled = false;

    Task<int> task = AsyncMethod();  // fork 1  child thread
    Task<int> task2 = AsyncMethod(); // fork 2. child thread

    button1.Text = (await task + await task2).ToString(); // wait for tasks to be completed (non-blocking)

    button1.Enabled = true;

    // implicit join at the end of the program to keep program responsive (non-blocking)
}

static int MethodWhichTakesLongTime()
{
    int result = 0;

    Thread.Sleep(5000);
    result = 12;

    return result;
}

static async Task<int> AsyncMethod()
{
    Task<int> task = new Task<int>(MethodWhichTakesLongTime);

    task.Start(); // fork

    // time consumsing method is running in own thread
    // main thread is still responsive

    int result = await task; // wait for thread to complete (no join)

    // other asyncronous taks (optional)

    return result;
}


/* Await and Async (non-blocking, with parameters) */
// recommented

private async void button1_Click(object sender, EventArgs e)
{
    button1.Enabled = false;

    Task<int> task = AsyncMethod(12);  // fork 1  child thread
    Task<int> task2 = AsyncMethod(12); // fork 2. child thread

    button1.Text = (await task + await task2).ToString(); // wait for tasks to be completed (non-blocking)

    button1.Enabled = true;

    // implicit join at the end of the program to keep program responsive (non-blocking)
}

static int MethodWhichTakesLongTime(int number)
{
    int result = 0;

    Thread.Sleep(5000);
    result = 2 * number;

    return result;
}

static async Task<int> AsyncMethod(int number)
{
    Task<int> task = Task.Factory.StartNew(() => MethodWhichTakesLongTime(number));

    // time consumsing method is running in own thread
    // main thread is still responsive

    int result = await task; // wait for task to be completed (no join)

    // other asyncronous taks (optional)

    return result;
}


/* lock shared resources in multithreading environments from concurent access */

// fast, but only primitive operations
int a = 0;
Interlocked.Increment(ref a);

// slow, but also complex operations
object _lock = new object();

lock(_lock)
{
    a++;
}

// if you want more control over locking mechanism use Monitor class instead of lock keyword


/* Deadlock */
// add Thread.Sleep() between two lock statements to detect deadlocks

using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Account accountA = new Account(1000);
            Account accountB = new Account(2000);

            AccountManager accountManager1 = new AccountManager();
            AccountManager accountManager2 = new AccountManager();

            Task t1 = accountManager1.TransferAsync(accountA, accountB, 500);
            Task t2 = accountManager2.TransferAsync(accountB, accountA, 1000);

            t1.Wait();
            t2.Wait();
        }
    }

    class Account
    {
        private int balance;

        public Account(int balance)
        {
            this.balance = balance;
        }

        public void Deposit(int amount)
        {
            balance += amount;
        }

        public void Withdraw(int amount)
        {
            balance -= amount;
        }
    }

    class AccountManager
    {
        public void Transfer(Account accountA, Account accountB, int amount)
        {
            lock (accountA)
            {
                Thread.Sleep(1000);

                lock (accountB)
                {
                    accountA.Withdraw(amount);
                    accountB.Deposit(amount);
                }
            }
        }

        public async Task TransferAsync(Account accountA, Account accountB, int amount)
        {
            Task task = Task.Factory.StartNew(() => Transfer(accountA, accountB, amount));
            await task;
        }
    }
}


