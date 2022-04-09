using System.Windows.Input;
using DateBaseGUI.ViewModels.Base;
using DateBaseGUI.Infrastructure.Commands;
using DateBaseGUI.Data;
using System.Collections.ObjectModel;
using DateBaseGUI.Models;

namespace DateBaseGUI.ViewModels
{
  public class TechnologistWindowViewModel : ViewModel
  {
    #region DisplayRootRegistry
    public DisplayRootRegistry displayRootRegistry;
    #endregion
    #region Properties
    private DBInteraction _dBInteraction;
    private string _name;
    private ObservableCollection<Operations> _operations;
    public ObservableCollection<Operations> Operations { get => _operations; set => Set(ref _operations, value); }
    public string Name { get => _name; set => Set(ref _name, value); }

    #endregion

    #region Commands

    #region CloseApplicationCommand
    public ICommand CloseApplicationCommand { get; }
    private void OnCloseApplicationCommandExecuted(object p)
    {
      displayRootRegistry.HidePresentation(this);
    }
    private bool CanCloseApplicationCommandExecute(object p) => true;
    #endregion

    #region AddOpearationCommand
    public ICommand AddOpearationCommand { get; }
    private bool CanAddOpearationCommandExecute(object p) => true;
    private void OnAddOpearationCommandExecuted(object p)
    {
      _dBInteraction.AddOperation(Name);
      Operations = _dBInteraction.GetOperations();
    }
    #endregion
    #endregion
    public TechnologistWindowViewModel()
    {
      _dBInteraction = new DBInteraction();
      Operations = _dBInteraction.GetOperations();

      #region Commands
      CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
      AddOpearationCommand = new LambdaCommand(OnAddOpearationCommandExecuted, CanAddOpearationCommandExecute);
      #endregion
    }
  }
}
