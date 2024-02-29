namespace TestingLibgit2;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var sampleRepo = new SampleRepo(new DirectoryInfo("/home/ionescic/repos/other/local-test-repo"));
        
        Checkout(sampleRepo, "development");
        Checkout(sampleRepo, "701b310");
        Checkout(sampleRepo, "master");
        
        Console.WriteLine("Done!");
    }

    private static void Checkout(SampleRepo sampleRepo, string pointer)
    {
        Console.WriteLine($"Checking out {pointer}...");
        sampleRepo.Checkout(pointer);
        Console.WriteLine("Done");
    }
}