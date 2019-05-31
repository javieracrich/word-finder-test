using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class WordFinder
    {
        private readonly List<string> _matrix;
        private List<string> _transposedMatrix = new List<string>();
        private Dictionary<string, int> result = new Dictionary<string, int>();

        public WordFinder(IEnumerable<string> matrix)
        {
            _matrix = matrix.ToList();
        }
        /// <summary>
        /// 
        /// Returns the top 10 most repeated words from the word stream found in the matrix.
        /// If no words are found, the "Find" method should return an empty set of strings.
        /// If any word in the word stream is found more than once within the matrix, the search results should count it only once.
        /// 
        /// </summary>
        /// <param name="wordstream"></param>
        /// <returns></returns>
        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            Parallel.ForEach(wordstream, (horizontal) =>
            {
                FindWordAndAddToResults(horizontal, _matrix);
            });

            _transposedMatrix = Transpose(_matrix);

            Parallel.ForEach(wordstream, (horizontal) =>
            {
                FindWordAndAddToResults(horizontal, _transposedMatrix);
            });

            return result
                .GroupBy(x => x.Key)
                .Select(x => new { Word = x.Key, TotalCount = x.Sum(y => y.Value) })
                .OrderBy(x => x.TotalCount)
                .Take(10)
                .Select(x => x.Word);

        }

        private void FindWordAndAddToResults(string word, List<string> matrix)
        {
            var count = FindWordCount(word, matrix);
            if (count > 0)
            {
                result.Add(word, count);
            }
        }

        /// <summary>
        /// returns true if 'word' was found horizontally in matrix
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private int FindWordCount(string word, List<string> matrix)
        {
            var count = 0;
            foreach (var horizontal in matrix)
            {
                if (horizontal.IndexOf(word) > -1)
                    count++;
            }
            return count;
        }

        private List<string> Transpose(List<string> matrix)
        {
            var transposed = new List<string>();
            var count = matrix.Count;
            for (int c = 0; c < count; c++)
            {
                var word = "";
                for (int r = 0; r < count; r++)
                {
                    var horizontal = matrix[r];
                    word += horizontal.Substring(c, 1);
                }
                transposed.Add(word);
            }
            return transposed;
        }
    }
}
