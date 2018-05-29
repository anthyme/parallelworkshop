using System;

namespace Parallel
{
    [Flags]
    enum Target
    {
        None = 0,
        Sequential = 1,
        Task = 2,
        PLinq = 4,
        Agent = 8,
        CpuSequential = 16,
        CpuTask = 32,
        CpuPLinq = 64,
        CpuAgent = 128
    }
}