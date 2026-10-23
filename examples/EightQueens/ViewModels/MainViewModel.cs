using ReactiveUI;

namespace EightQueens;

public class MainViewModel : ReactiveObject
{
    public ParametersViewModel ParametersViewModel { get; } = new();
    public RunViewModel RunViewModel { get; } = new();

    //public ReactiveCommand<Unit, Parameters> Start { get; }

    public MainViewModel()
    {
        //Start = ReactiveCommand.Create(() => ParametersViewModel.Parameters);
        //Start.Subscribe(p => RunViewModel.Parameters = p);
    }
}
