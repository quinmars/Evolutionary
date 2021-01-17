using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Troschuetz.Random;

namespace Evolutionary
{
    public class Offspring<TIndividual, TDataSet>
    {
        public Enviroment<TIndividual, TDataSet> Enviroment { get; }
        public IEnumerable<TIndividual> Parents { get; }
        public IEnumerable<TIndividual> RandomParents { get; }
        public IEnumerable<TIndividual> Children { get; }

        public Offspring(Enviroment<TIndividual, TDataSet> enviroment, IEnumerable<TIndividual> parents, IEnumerable<TIndividual> randomParents,  IEnumerable<TIndividual> children)
        {
            Enviroment = enviroment;
            Parents = parents;
            RandomParents = randomParents;
            Children = children;
        }
    }
}
