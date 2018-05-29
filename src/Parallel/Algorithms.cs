using System.Collections.Generic;
using System.Linq;

namespace Parallel
{
    class Algorithms
    {
        public static Dictionary<string, int> SequencialMatch(string[] files)
        {
            var result = Helpers.WordsToSearch.ToDictionary(x => x, _ => 0);
            foreach (var file in files)
            {
                foreach (var lines in Helpers.ReadAllLines(file))
                {
                    foreach (var word in Helpers.SplitPunctuation(lines))
                    {
                        if (!Helpers.IsIgnoredWord(word))
                        {
                            var (matched, wordToSearch) = Helpers.Match(word);
                            if (matched) result[wordToSearch] += 1;
                        }
                    }
                }
            }
            return result;
        }

        public static Dictionary<string, int> CpuSequentialMatch(string[] files)
        {
            var result = Helpers.WordsToSearch.ToDictionary(x => x, _ => 0);
            foreach (var file in files)
            {
                foreach (var lines in Helpers.ReadAllLines(file))
                {
                    foreach (var word in Helpers.SplitPunctuation(lines))
                    {
                        if (!Helpers.IsIgnoredWord(word))
                        {
                            var (matched, wordToSearch) = Helpers.CpuMatch(word);
                            if (matched) result[wordToSearch] += 1;
                        }
                    }
                }
            }
            return result;
        }

        //todo
        public static Dictionary<string, int> TaskMatch(string[] files) => SequencialMatch(files);
        //todo
        public static Dictionary<string, int> PLinqlMatch(string[] files) => SequencialMatch(files);
        //todo
        public static Dictionary<string, int> AgentMatch(string[] files) => SequencialMatch(files);
        //todo
        public static Dictionary<string, int> CpuTaskMatch(string[] files) => SequencialMatch(files);
        //todo
        public static Dictionary<string, int> CpuPLinqlMatch(string[] files) => SequencialMatch(files);
        //todo
        public static Dictionary<string, int> CpuAgentMatch(string[] files) => SequencialMatch(files);
    }
}