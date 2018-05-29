using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using FuzzyMatch;

namespace Parallel
{
    class Helpers
    {
        public static string[] WordsToSearch = {"ENGLISH", "RICHARD", "STEALLING", "MAGIC", "STARS", "MOON", "CASTLE"};
        public static HashSet<string> WordsToSearchSet = new HashSet<string>(WordsToSearch);

        public static string[] IgnoreWords = { "a", "an", "the", "and", "of", "to" };
        public static HashSet<string> IgnoreWordsSet = new HashSet<string>(IgnoreWords);
        public static bool IsIgnoredWord(string input) => IgnoreWordsSet.Contains(input.ToLower());

        public static char[] Punctuation = Enumerable.Range(0, 256).Select(c => (char)c)
                .Where(c => char.IsWhiteSpace(c) || char.IsPunctuation(c)).ToArray();
        public static IEnumerable<string> SplitPunctuation(string input) => input.Split(Punctuation);

        public static (bool Matched, string Word) Match(string word)
        {
            var upper = word.ToUpper();
            return WordsToSearchSet.Contains(upper) ? (true, upper) : (false, null);
        }

        public static (bool Matched, string Word) CpuMatch(string word)
        {
            var match = JaroWinklerModule.bestMatch(WordsToSearch, word, 0.9);
            return match.Any() ? (true, match.First().Match) : (false, null);
        }

        public static string[] ReadAllLines(string path)
        {
            Enumerable.Range(0, 8).Select(_ => File.ReadAllLines(path)).Iter();
            return File.ReadAllLines(path);
        }

        public static void MeasureAndPrint(string[] files, Func<string[], Dictionary<string, int>> match)
        {
            var watch = new Stopwatch();
            watch.Start();
            var result = match(files);
            watch.Stop();

            Console.WriteLine($"Score of {watch.ElapsedMilliseconds}ms {match.Method.Name}");

            foreach (var r in result.OrderBy(x => x.Key))
            {
                Console.Write($"{r.Key} found {r.Value} times;");
            }

            Console.WriteLine("");
        }
    }
}