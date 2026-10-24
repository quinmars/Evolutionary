using Evolutionary;

namespace EightQueens;

public record RunResult
{
    public int Run { get; init; }
    public Permutation BestIndividual { get; init; }
    public double BestFitness { get; init; }
    public double MedianFitness { get; init; }
    public double WorstFitness { get; init; }
}
