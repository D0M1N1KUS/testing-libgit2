using LibGit2Sharp;

namespace TestingLibgit2;

public class SampleRepo : ISampleRepo
{
    private Branch? _currentBranch = default;
    
    public SampleRepo(DirectoryInfo directoryInfo)
    {
        directoryInfo.Refresh();

        if (!directoryInfo.Exists)
            throw new DirectoryNotFoundException($"The directory {directoryInfo.FullName} does not exist");

        RepositoryPath = directoryInfo.FullName;
    }
    
    public string RepositoryPath { get; }
    
    public void Checkout(string pointer)
    {
        using var repo = new Repository(RepositoryPath);

        Branch branch = repo.Branches[pointer];

        if (branch == null)
        {
            Commit commit = repo.Lookup<Commit>(pointer);
            
            if (commit == null)
                throw new InvalidOperationException(
                $"The Git commit/branch {pointer} does not exist in this repository {RepositoryPath}");

            Commands.Checkout(repo, commit);

            return;
        }
        
        _currentBranch = Commands.Checkout(repo, branch);
    }

    public bool TryCheckout(string pointer)
    {
        throw new NotImplementedException();
    }
}