using NUnit.Framework;

namespace SymbolTablesAndHashTables.Tests
{
    [TestFixture]
    class SequentialSearchStTests
    {
        [Test]
        public void Add_One_Entry()
        {
            var search = new SequentialSearchSt<int, string>();
            search.Add(1, "val1");
            Assert.That(search.Count, Is.EqualTo(1));
        }

        [Test]
        public void Add_Multiple_Entries()
        {
            var search = new SequentialSearchSt<int, string>();
            var entries = 5;
            for (int i = 0; i < 5; i++)
            {
                search.Add(i, $"val{i}");
            }
            Assert.That(search.Count, Is.EqualTo(entries));
        }

        [Test]
        public void Get_Added_Entry()
        {
            // Arrange
            var search = new SequentialSearchSt<int, string>();
            for (int i = 0; i < 5; i++)
            {
                search.Add(i, $"val{i}");
            }

            // Act
            Assert.That(search.TryGet(3, out var value), Is.True);

            // Assert
            Assert.That(value, Is.EqualTo("val3"));
        }

        [Test]
        public void Remove_Entry_Entry_Is_Found()
        {
            // Arrange
            var search = new SequentialSearchSt<int, string>();
            for (int i = 0; i < 5; i++)
            {
                search.Add(i, $"val{i}");
            }

            // Act
            // Assert
            Assert.That(search.Remove(3), Is.True);
        }

        [Test]
        public void Remove_Entry_Key_Is_Invalid()
        {
            // Arrange
            var search = new SequentialSearchSt<int, string>();
            for (int i = 0; i < 5; i++)
            {
                search.Add(i, $"val{i}");
            }

            // Act
            // Assert
            Assert.That(search.Remove(300), Is.False);
        }


    }
}
