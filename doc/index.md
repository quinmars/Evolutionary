# Evolutionary

A small toolbox library for evolutionary algorithms. It does not implement one or more evolutionary algorithms for you, but gives you the data types and operators at hand to write it yourself.

The main logic uses only two immutable types that are transformed by operators. All operators are implemented as extension methods, hence it is easy to add custom operators.

- [`Population<TIndividual>`](xref:Evolutionary.Population%601) – the collection of all individuals, the fitness function and the random number generator.
- [`Offspring<TIndividual>`](xref:Evolutionary.Offspring%601) – besides a reference to the parent population, it offers a random enumeration of the parent individuals and an enumeration of the children generated so far.

For the individuals, the library ships three ready-made types — [`Permutation`](xref:Evolutionary.Permutation) for permutation-encoded, [`BitString`](xref:Evolutionary.BitString) for binary-encoded and [`RealVector<T>`](xref:Evolutionary.RealVector%601) for real-encoded problems. But you can add with little effort your own implementation for the individuals.

## A complete example

In this example the square root of 2 is calculated. Of course, there are better methods to do this, but it gives a good impression how the `Evolutionary` library is used.

[!code-csharp[Square root example](../examples/SquareRoot/SquareRoot.cs)]

## More examples

- The [OneMax](https://github.com/quinmars/evolutionary/tree/master/examples/OneMax) example is the "hello world" of evolutionary algorithms: it maximizes the number of set bits in a bit string with the [`BitString`](xref:Evolutionary.BitString) individual. It also shows how a maximization problem is expressed with the minimize-oriented fitness of the library.
- The [Sphere](https://github.com/quinmars/evolutionary/tree/master/examples/Sphere) example minimizes the Sphere function f(x) = sum(x_i^2) with the real-encoded [`RealVector<T>`](xref:Evolutionary.RealVector%601) individual and shows how a decreasing mutation strength avoids the convergence plateau of a fixed sigma.
- The [EightQueens](https://github.com/quinmars/evolutionary/tree/master/examples/EightQueens) example is a WPF application that visualizes how a population of queen permutations evolves towards a solution of the eight queens puzzle.
