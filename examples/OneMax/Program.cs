// The OneMax problem: maximize the number of set bits in a bit string.
//
using Evolutionary;

const int Length = 100;
const double MutationProbability = 1.0 / Length;

// The library ranks individuals by fitness increasingly, i.e. it minimizes
// the fitness (like the SquareRoot example minimizes the distance to the
// target). OneMax is a maximization problem, hence the fitness inverts the
// number of ones: an individual with all bits set has fitness 0.
Func<BitString, double> fitness = bits => Length - bits.PopCount();

var population = Population
    .Create(fitness, new Random(12345))
    .AddRandomIndividuals(30, rnd => BitString.CreateRandomly(Length, rnd));

int i;
for (i = 0; i < 200; i++)
{
    population = population
        .SelectParentsByRank()
        .Recombine(20, BitString.UniformCrossover)
        .Mutate(20, (bits, rnd) => BitString.FlipMutation(bits, rnd, MutationProbability))
        .SelectElite(2)
        .SelectSurvivors(30)
        .ToPopulation();

    var ones = population.Individuals[0].PopCount();

    if (i % 10 == 0 || ones == Length)
    {
        Console.WriteLine($"({i}) Best: {ones}/{Length}");
    }

    if (ones == Length)
    {
        break;
    }
}

Console.WriteLine();
Console.WriteLine($"Best: {population.Individuals[0].PopCount()}/{Length} after {i} generations");
Console.WriteLine($"Value: {population.Individuals[0]}");


