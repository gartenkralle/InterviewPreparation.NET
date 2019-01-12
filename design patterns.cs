/* Singleton */

// Eager loading
// Prefer if the singleton object is needed in any case
sealed class Singleton
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
sealed class Singleton
{
    private Singleton() { }
    private static volatile Singleton instance;

    public static Singleton Instance
    {
        get
        {
            if (instance == null) // this line is only for performance improvement
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

sealed class Foo
{
    public int ID { get; set; }

    public Foo()
    {
        ID = 1;
    }
}


/* Singleton vs. Static class */

// A singleton can inherit from interfaces or an base class, a static class not
