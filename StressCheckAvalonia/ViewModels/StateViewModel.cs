using System.Collections.Frozen;
using ReactiveUI;
using StressCheckAvalonia.Models;
using StressCheckAvalonia.Models.States;

namespace StressCheckAvalonia.ViewModels;

public class StateViewModel : ReactiveObject
{
    private static readonly FrozenDictionary<State, IAppState> States = new Dictionary<State, IAppState>
    {
        [State.Input] = InputState.Instance,
        [State.SectionActive] = SectionActiveState.Instance,
        [State.Aggregated] = AggregatedState.Instance,
    }.ToFrozenDictionary();

    private readonly SectionViewModel _sectionViewModel;
    private readonly EmployeeViewModel _employeeViewModel;
    private State _currentState;
    private IAppState _appState = InputState.Instance;

    public StateViewModel(SectionViewModel sectionViewModel, EmployeeViewModel employeeViewModel)
    {
        _sectionViewModel = sectionViewModel;
        _employeeViewModel = employeeViewModel;
    }

    public SectionViewModel SectionViewModel => _sectionViewModel;
    public EmployeeViewModel EmployeeViewModel => _employeeViewModel;

    public State CurrentState
    {
        get => _currentState;
        set
        {
            this.RaiseAndSetIfChanged(ref _currentState, value);
            _appState = States[value];
            this.RaisePropertyChanged(nameof(IsInput));
            this.RaisePropertyChanged(nameof(IsSectionActive));
            this.RaisePropertyChanged(nameof(IsAggregated));
            this.RaisePropertyChanged(nameof(IsInputInverted));
            this.RaisePropertyChanged(nameof(AppTitle));
            this.RaisePropertyChanged(nameof(DescriptionText));
            this.RaisePropertyChanged(nameof(NextButtonText));
        }
    }

    public bool IsInput => CurrentState == State.Input;
    public bool IsInputInverted => !IsInput;
    public bool IsSectionActive => CurrentState == State.SectionActive;
    public bool IsAggregated => CurrentState == State.Aggregated;

    public string AppTitle => _appState.GetAppTitle(this);
    public string DescriptionText => _appState.GetDescriptionText(this);
    public string NextButtonText => _appState.NextButtonText;

    public void HandleNext() => _appState.HandleNext(this);
    public void HandleBack() => _appState.HandleBack(this);

    public void Initialize()
    {
        if (_employeeViewModel.IsInformationComplete())
        {
            CurrentState = State.SectionActive;
            _sectionViewModel.UpdateDisplayedQuestions(0);
        }
    }
}
