namespace EightQueens;

public record Parameters
{
    public int QueenCount { get; init; }

    // Mutation
    public int EliteSelection { get; init; }
    public int InsertMutation { get; init; }
    public int InversionMutation { get; init; }

    // Recombination
    public int CycleCrossover { get; init; }
    public int PartiallyMappedCrossover { get; init; }
    public int CutAndCrossfillCrossover { get; init; }
}
