namespace TestingLibgit2;

public interface ISampleRepo
{
    /// <summary>
    /// Checks out the given branch/commit.
    /// </summary>
    /// <param name="pointerr">Name of the branch or commit sha to checkout.</param>
    public void Checkout(string pointer);

    /// <summary>
    /// Checks out the given branch/commit.
    /// </summary>
    /// <param name="pointer">Name of the branch or commit sha to checkout.</param>
    public bool TryCheckout(string pointer);
}