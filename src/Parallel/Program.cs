using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Parallel
{
    class Program
    {
        static Target target = Target.Sequential | Target.Task;

        static void Main(string[] args)
        {
            var files = Directory.GetFiles("Data\\Text");
            foreach (var algorithm in algorithms.OrderByDescending(x => x.Key).Where(x => (target & x.Key) == x.Key).Select(x => x.Value))
            {
                Helpers.MeasureAndPrint(files, algorithm);
            }
            Console.WriteLine("program ended press any key to stop");
            Console.Read();
        }

        private static Dictionary<Target, Func<string[], Dictionary<string, int>>> algorithms =
            new Dictionary<Target, Func<string[], Dictionary<string, int>>>
            {
                [Target.Sequential] = Algorithms.SequencialMatch,
                [Target.Task] = Algorithms.TaskMatch,
                [Target.PLinq] = Algorithms.PLinqlMatch,
                [Target.Agent] = Algorithms.AgentMatch,
                [Target.CpuSequential] = Algorithms.CpuSequentialMatch,
                [Target.CpuTask] = Algorithms.CpuTaskMatch,
                [Target.CpuPLinq] = Algorithms.CpuPLinqlMatch,
                [Target.CpuAgent] = Algorithms.CpuAgentMatch
            };
    }
}
