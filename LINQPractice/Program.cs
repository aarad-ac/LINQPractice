using NUnitLite;

namespace YourProjectName
{
    class Program
    {
        static void Main(string[] args)
        {
            // Run all the NUnit tests in the current assembly
            new AutoRun().Execute(args);
        }
    }
}
