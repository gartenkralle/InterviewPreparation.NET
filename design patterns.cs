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
using System;

sealed class Singleton
{
    private Singleton() { }
    private static readonly Lazy<Singleton> instance = new Lazy<Singleton>(() => new Singleton());

    public static Singleton Instance
    {
        get
        {
            return instance.Value;
        }
    }
}


/* Singleton vs. Static class */

// A singleton can inherit from interfaces or an base class, a static class not
