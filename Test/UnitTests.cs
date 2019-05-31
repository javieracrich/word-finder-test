using ConsoleApp;
using System.Collections.Generic;
using Xunit;

namespace test
{
    public class UnitTests
    {
        [Fact]
        public void FindWords()
        {
            //arrange
            var matrix = new List<string>() { "abcdc", "fgwio", "chill", "pqnsd", "uvdxy" };
            var finder = new WordFinder(matrix);
            var expected = new List<string>() { "chill", "cold", "wind" };
            var wordStream = new List<string>() { "cold", "wind", "snow", "chill" };

            //act
            var actual = finder.Find(wordStream);

            //assert
            Assert.Equal(expected, actual);

        }
    }
}
