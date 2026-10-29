// Function minimization: minimize the Sphere function f(x) = sum(x_i^2)
// in `Dimensions` dimensions over the interval [-5, 5].
//
// Unlike the OneMax example, no fitness inversion is needed here: the
// library ranks individuals by fitness increasingly, i.e. it minimizes the
// fitness - and the Sphere function is naturally a minimization problem.
//
// The mutation strength is decreased over the generations. With a fixed
// sigma the population converges to a plateau: the mutation noise sets a
// lower bound for the achievable fitness (this is why evolution strategies
// self-adapt their mutation strengths).
using Evolutionary;

const int Dimensions = 10;
const int PopulationSize = 50;
const int Generations = 200;
const int EliteCount = 2;
const double Min = -5.0;
const double Max = 5.0;
const double MutationSigma = 0.25;

Func<RealVector<double>, double> fitness = v =>
{
    var sum = 0.0;
    for (var i = 0; i < v.Length; i++)
    {
        sum += v[i] * v[i];
    }

    return sum;
};

var population = Population
    .Create(fitness, new Random(12345))
    .AddRandomIndividuals(PopulationSize, rnd => RealVector.CreateRandomly(Dimensions, Min, Max, rnd));

for (var i = 0; i < Generations; i++)
{
    // Decrease the mutation strength over the generations.
    var sigma = MutationSigma / (1.0 + i / 10.0);

    population = population
        .SelectParentsByRank()
        .Recombine(PopulationSize, RealVector.ArithmeticCrossover)
        .Mutate(PopulationSize, (v, rnd) => RealVector.GaussianMutation(v, rnd, sigma))
        .SelectElite(EliteCount)
        .SelectSurvivors(PopulationSize)
        .ToPopulation();

    if (i % 20 == 0)
    {
        Console.WriteLine($"({i}) Best fitness: {fitness(population.Individuals[0]):G4}");
    }
}

Console.WriteLine($"({Generations - 1}) Best fitness: {fitness(population.Individuals[0]):G4}");
Console.WriteLine($"Best individual: {population.Individuals[0]:G4}");
