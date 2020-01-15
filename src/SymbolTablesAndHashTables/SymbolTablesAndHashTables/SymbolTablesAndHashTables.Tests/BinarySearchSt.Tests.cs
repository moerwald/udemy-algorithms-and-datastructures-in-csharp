using NUnit.Framework;
using System.Linq;

namespace SymbolTablesAndHashTables.Tests
{
    [TestFixture]
    public class BinarySearchStTests
    {
        [Test]
        public void Add_Multiple_Entries()
        {
            const int NrOfItems = 5;
            var bsc = new BinarySearchSt<int, string>();
            for (int i = 0; i < NrOfItems; i++)
            {
                bsc.Add(i, i.ToString());
            }

            Assert.That(bsc.Count, Is.EqualTo(NrOfItems));
        }

        [Test]
        public void Get_Index_Of_Entry()
        {
            var bsc = new BinarySearchSt<string, int>();
            bsc.Add("b", 1);
            bsc.Add("c", 2);
            bsc.Add("a", 3);
            bsc.Add("f", 4);

            Assert.That(bsc.Rank("a"), Is.EqualTo(0));
            Assert.That(bsc.Rank("b"), Is.EqualTo(1));
            Assert.That(bsc.Rank("c"), Is.EqualTo(2));
            Assert.That(bsc.Rank("f"), Is.EqualTo(3));
        }

        [Test]
        public void Range()
        {
            var bsc = new BinarySearchSt<string, int>();
            bsc.Add("b", 1);
            bsc.Add("c", 2);
            bsc.Add("a", 3);
            bsc.Add("f", 4);

            Assert.That(
                bsc.Range("b", "g"), 
                Is.EquivalentTo(new[] { "b", "c", "f" })
                );
        }
    }
}
