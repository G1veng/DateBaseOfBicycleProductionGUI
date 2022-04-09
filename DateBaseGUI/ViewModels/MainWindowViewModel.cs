using DateBaseGUI.Infrastructure.Commands;
using DateBaseGUI.ViewModels.Base;
using System.Windows.Input;
using DateBaseGUI.Data;

namespace DateBaseGUI.ViewModels
{
  public class MainWindowViewModel : ViewModel
  {
    #region DisplayRootRegistry
    private DisplayRootRegistry displayRootRegistry;
    #endregion

    #region InnerWindows
    private LogisticianWindowViewModel _logisticianWindowViewModel;
    private WarehousemanWindowViewModel _warehousemanWindowViewModel;
    private TechnologistWindowViewModel _technologistWindowViewModel;
    private ManagerWindowViewModel _managerWindowViewModel;
    private ShopManagerWindowViewModel _shopManagerWindowViewModel;
    #endregion

    #region Commands

    #region OpenLogisticianWIndowCommand
    public ICommand OpenLogisticianWIndowCommand { get; }
    private bool CanOpenLogisticianWIndowCommandExecute(object p) => !displayRootRegistry.CheckForExistingWindows(_logisticianWindowViewModel);
    private void OnOpenLogisticianWIndowCommandExecuted(object p)
    {
      if(_logisticianWindowViewModel == null)
        _logisticianWindowViewModel = new LogisticianWindowViewModel();
      else
      {
        DBInteraction _dBInteraction = new DBInteraction();
        _logisticianWindowViewModel.Paths = _dBInteraction.GetPaths();
        _logisticianWindowViewModel.RegularQuantity = _dBInteraction.GetRegularQuantity();
        _logisticianWindowViewModel.RequestForTransition = _dBInteraction.GetRequestsForTransition();
        _logisticianWindowViewModel.TransitOrderComposition = _dBInteraction.GetTransitOrderComposition();
      }
      _logisticianWindowViewModel.displayRootRegistry = displayRootRegistry;
      displayRootRegistry.ShowPresentation(_logisticianWindowViewModel);
    }
    #endregion

    #region OpenWarehousemanWIndowCommand
    public ICommand OpenWarehousemanWIndowCommand { get; }
    private bool CanOpenWarehousemanWIndowCommandExecute(object p) => !displayRootRegistry.CheckForExistingWindows(_warehousemanWindowViewModel);
    private void OnOpenWarehousemanWIndowCommandExecuted(object p)
    {
      if (_warehousemanWindowViewModel == null)
        _warehousemanWindowViewModel = new WarehousemanWindowViewModel();
      else
      {
        DBInteraction _dBInteraction = new DBInteraction();
        _warehousemanWindowViewModel.RequestForTransition = _dBInteraction.GetRequestsForTransition();
        _warehousemanWindowViewModel.TransitQuantity = _dBInteraction.GetTransitQuantity();
        _warehousemanWindowViewModel.IncomeOnTransRequests = _dBInteraction.GetIncomeOnTransRequest();
        _warehousemanWindowViewModel.OutcomeOnTransRequests = _dBInteraction.GetOutcomeOnTransRequest();
      }
      _warehousemanWindowViewModel.displayRootRegistry = displayRootRegistry;
      displayRootRegistry.ShowPresentation(_warehousemanWindowViewModel);
    }
    #endregion

    #region OpenTechnologistWindowCommand
    public ICommand OpenTechnologistWindowCommand { get; }
    private bool CanOpenTechnologistWindowCommandExecute(object p) => !displayRootRegistry.CheckForExistingWindows(_technologistWindowViewModel);
    private void OnOpenTechnologistWindowCommandExecuted(object p)
    {
      if (_technologistWindowViewModel == null)
        _technologistWindowViewModel = new TechnologistWindowViewModel();
      _technologistWindowViewModel.displayRootRegistry = displayRootRegistry;
      displayRootRegistry.ShowPresentation(_technologistWindowViewModel);
    }
    #endregion

    #region OpenManagerWindowCommand
    public ICommand OpenManagerWindowCommand { get; }
    private bool CanOpenManagerWindowCommandExecute(object p) => !displayRootRegistry.CheckForExistingWindows(_managerWindowViewModel);
    private void OnOpenManagerWindowCommandExecuted(object p)
    {
      if(_managerWindowViewModel == null)
        _managerWindowViewModel = new ManagerWindowViewModel();
      else
      {
        DBInteraction _dBInteraction = new DBInteraction();
        _managerWindowViewModel.Operations = _dBInteraction.GetOperations();
        _managerWindowViewModel.RegularQuantitiy = _dBInteraction.GetRegularQuantity();
        _managerWindowViewModel.RequestForProduction = _dBInteraction.GetRequsetsForProduction();
      }
      _managerWindowViewModel.displayRootRegistry = displayRootRegistry;
      displayRootRegistry.ShowPresentation(_managerWindowViewModel);
    }

    #region OpenShopManagerWindowCommand
    public ICommand OpenShopManagerWindowCommand { get; }
    private bool CanOpenShopManagerWindowCommandExecute(object p) => !displayRootRegistry.CheckForExistingWindows(_shopManagerWindowViewModel);
    private void OnOpenShopManagerWindowCommandExecuted(object p)
    {
      if (_shopManagerWindowViewModel == null)
        _shopManagerWindowViewModel = new ShopManagerWindowViewModel();
      else
      {
        DBInteraction _dBInteraction = new DBInteraction();
        _shopManagerWindowViewModel.UnfinishedProduction = _dBInteraction.GetUnfinishedProduction();
        _shopManagerWindowViewModel.RegularQuantity = _dBInteraction.GetRegularQuantity();
        _shopManagerWindowViewModel.RequestForProduction = _dBInteraction.GetRequsetsForProduction();
        _shopManagerWindowViewModel.OutcomeFromProduction = _dBInteraction.GetOutcomeFromProduction();
      }
      _shopManagerWindowViewModel.displayRootRegistry = displayRootRegistry;
      displayRootRegistry.ShowPresentation(_shopManagerWindowViewModel);
    }
    #endregion
    #endregion

    #region CloseApplicationCommand
    public ICommand CloseApplicationCommand { get; }
    private void OnCloseApplicationCommandExecuted(object p)
    {
      System.Windows.Application.Current.Shutdown();
    }
    private bool CanCloseApplicationCommandExecute(object p) => !displayRootRegistry.CheckForExistingWindows(_logisticianWindowViewModel) &&
      !displayRootRegistry.CheckForExistingWindows(_warehousemanWindowViewModel);
    #endregion

    #endregion
    public MainWindowViewModel()
    {
      displayRootRegistry = (System.Windows.Application.Current as App).displayRootRegistry;

      #region Commands
      OpenLogisticianWIndowCommand = new LambdaCommand(OnOpenLogisticianWIndowCommandExecuted, CanOpenLogisticianWIndowCommandExecute);
      CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
      OpenWarehousemanWIndowCommand = new LambdaCommand(OnOpenWarehousemanWIndowCommandExecuted, CanOpenWarehousemanWIndowCommandExecute);
      OpenTechnologistWindowCommand = new LambdaCommand(OnOpenTechnologistWindowCommandExecuted, CanOpenTechnologistWindowCommandExecute);
      OpenManagerWindowCommand = new LambdaCommand(OnOpenManagerWindowCommandExecuted, CanOpenManagerWindowCommandExecute);
      OpenShopManagerWindowCommand = new LambdaCommand(OnOpenShopManagerWindowCommandExecuted, CanOpenShopManagerWindowCommandExecute);
      #endregion
    }
  }
}
