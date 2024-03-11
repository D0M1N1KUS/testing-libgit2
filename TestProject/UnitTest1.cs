namespace TestProject;

public class Tests
{
    private int _iterations = 0;

    private int[][] _tables =
    [
        [10, 20, 30, 40, 50],
        [1, 11, 12, 21, 22, 31],
        [5, 15, 25, 35, 45, 55],
        [3, 6, 9, 17, 18, 24, 27]
    ];

    [SetUp]
    public void Setup()
    {
        _iterations = 0;
    }

    [Test]
    [TestCase(1, 1)]
    [TestCase(1, 0, 2)]
    [TestCase(0, 1, 2)]
    [TestCase(2, 0, 1)]
    [TestCase(0, 1, 2, 3)]
    [TestCase(1, 0, 2, 3)]
    [TestCase(2, 0, 1, 3)]
    [TestCase(3, 0, 1, 2)]
    [TestCase(0, 1, 2, 3, 4)]
    [TestCase(1, 0, 2, 3, 4)]
    [TestCase(2, 0, 1, 3, 4)]
    [TestCase(3, 0, 1, 2, 4)]
    [TestCase(4, 0, 1, 2, 3)]
    public void BisectInsert_InsertingNonExisting_ShouldYieldSortedList(int numberToInsert, params int[] initialArray)
    {
        var actualList = initialArray.ToList();

        BisectInsert(actualList, numberToInsert);

        Assert.That(actualList, Is.Ordered.Ascending);
    }

    [Test]
    [TestCase(0, 0)]
    [TestCase(0, 0, 1)]
    [TestCase(1, 0, 1)]
    [TestCase(0, 0, 1, 2)]
    [TestCase(1, 0, 1, 2)]
    [TestCase(2, 0, 1, 2)]
    [TestCase(0, 0, 1, 2, 3)]
    [TestCase(1, 0, 1, 2, 3)]
    [TestCase(2, 0, 1, 2, 3)]
    [TestCase(3, 0, 1, 2, 3)]
    public void BisectInsert_InsertingDuplicate_ShouldYieldSortedList(int numberToInsert, params int[] initialArray)
    {
        var actualList = initialArray.ToList();

        BisectInsert(actualList, numberToInsert);

        Assert.That(actualList, Is.Ordered.Ascending);
    }

    [Test]
    public void BisectInsert_ListIsEmpty_ShouldGetInserted()
    {
        const int numberToInsert = 0;
        var expectedList = new List<int> { numberToInsert };
        var actualList = new List<int>();

        BisectInsert(actualList, numberToInsert);

        Assert.That(actualList, Is.EquivalentTo(expectedList));
    }

    [Test]
    public void Exists()
    {
        int[][] table = [[1, 2]];
        int[] expected = [1, 2];

        var acutal = GetSmallestN(2, table);

        Assert.That(acutal, Is.EqualTo(expected));
    }

    [Test]
    public void TestCase1()
    {
        int[][] table = [[2, 3], [1]];
        int[] expected = [1, 2];

        var acutal = GetSmallestN(2, table);

        Assert.That(acutal, Is.EqualTo(expected));
    }

    private T[] GetSmallestN<T>(int n, T[][] table) where T : IComparable<T>
    {
        List<T> sortedList = new();
        bool[] needsChecking = Enumerable.Repeat(true, table.Length).ToArray();

        for (int i = 0; i < table.Length; i++)
        {
            for (int j = 0; j < table[i].Length; j++)
            {
                T currentValue = table[i][j];

                BisectInsert(sortedList, currentValue);

                if (sortedList.Count >= n && sortedList[^1].Equals(currentValue))
                    sortedList.RemoveAt(sortedList.Count - 1);

                _iterations++; // For testing purposes only
            }
        }

        return sortedList.ToArray();
    }

    private static void BisectInsert<T>(List<T> targetList, T itemToInsert) where T : IComparable<T>
    {
        int step = targetList.Count + targetList.Count % 2;
        int i = 0;

        while (step != 0 && i >= 0 && i < targetList.Count)
        {
            step /= 2;

            int comparisonResult = itemToInsert.CompareTo(targetList[i]);

            if (comparisonResult == 0)
                break;

            i += step * comparisonResult + (comparisonResult > 0 && step == 0 ? 1 : 0);
        }

        if (i < 0) i = 0;
        else if (i > targetList.Count) i = targetList.Count;

        targetList.Insert(i, itemToInsert);
    }
}