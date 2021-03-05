using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace EightQueens
{
    public class MainViewModel : ReactiveObject
    {
        public ParametersViewModel ParametersViewModel { get; } = new ParametersViewModel();
        public RunViewModel RunViewModel { get; } = new RunViewModel();

        //public ReactiveCommand<Unit, Parameters> Start { get; }

        public MainViewModel()
        {
            //Start = ReactiveCommand.Create(() => ParametersViewModel.Parameters);
            //Start.Subscribe(p => RunViewModel.Parameters = p);
        }
    }
}
