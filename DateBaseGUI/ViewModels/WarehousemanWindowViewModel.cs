using DateBaseGUI.Infrastructure.Commands;
using DateBaseGUI.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DateBaseGUI.Data;
using DateBaseGUI.ViewModels.Base;

namespace DateBaseGUI.ViewModels
{
  public class WarehousemanWindowViewModel : ViewModel
  {
    #region DisplayRootRegistry
    public DisplayRootRegistry displayRootRegistry;
    #endregion

    #region Properties
    private DBInteraction _dBInteraction;
    private ObservableCollection<RequestForTransition> _requestForTransition;
    private ObservableCollection<TransitQuantity> _transitQuantity;
    private ObservableCollection<OutcomeOnTransRequest> _outcomeOnTransRequest;
    private ObservableCollection<IncomeOnTransRequest> _incomeOnTransRequest;
    private string _countOfIncomeFromVendors;
    private string _productToRealise;
    private string _countProductToRealise;
    private string _warFromWhichRealise;
    private string _vendorToWhoRealise;

    public string WarFromWhichRealise { get => _warFromWhichRealise; set => Set(ref _warFromWhichRealise, value); }
    public string VendorToWhoRealise { get => _vendorToWhoRealise; set => Set(ref _vendorToWhoRealise, value); }
    public string CountOfIncomeFromVendors { get => _countOfIncomeFromVendors; set => Set(ref _countOfIncomeFromVendors, value); }
    public string ProductToRealise { get => _productToRealise; set => Set(ref _productToRealise, value); }
    public string CountProductToRealise { get => _countProductToRealise; set => Set(ref _countProductToRealise, value); }
    public ObservableCollection<OutcomeOnTransRequest> OutcomeOnTransRequests { get => _outcomeOnTransRequest; set => Set(ref _outcomeOnTransRequest, value); }
    public ObservableCollection<IncomeOnTransRequest> IncomeOnTransRequests { get => _incomeOnTransRequest; set => Set(ref _incomeOnTransRequest, value); }
    public ObservableCollection<RequestForTransition> RequestForTransition { get => _requestForTransition; set => Set(ref _requestForTransition, value); }
    public ObservableCollection<TransitQuantity> TransitQuantity { get => _transitQuantity; set => Set(ref _transitQuantity, value); }

    private string _idOfTransRequest;
    public string IdOfTransRequest { get => _idOfTransRequest; set => Set(ref _idOfTransRequest, value); }

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

    #region WriteOffOnTransitionDocumentCommand
    public ICommand WriteOffOnTransitionDocumentCommand { get; }
    private bool CanWriteOffOnTransitionDocumentCommandExecute(object p) => true;
    private void OnWriteOffOnTransitionDocumentCommandExecited(object p)
    {
      _dBInteraction.WriteOffForTransition(System.Int32.Parse(IdOfTransRequest));
      RequestForTransition = _dBInteraction.GetRequestsForTransition();
      TransitQuantity = _dBInteraction.GetTransitQuantity();
      IncomeOnTransRequests = _dBInteraction.GetIncomeOnTransRequest();
      OutcomeOnTransRequests = _dBInteraction.GetOutcomeOnTransRequest();
    }
    #endregion

    #region RecieveIncomingOnTransitRequestCommand
    public ICommand RecieveIncomingOnTransitRequestCommand { get; }
    private bool CanRecieveIncomingOnTransitRequestCommandExecute(object p) => true;
    private void OnRecieveIncomingOnTransitRequestCommandExevuted(object p)
    {
      _dBInteraction.RecieveTransRequest(System.Int32.Parse(IdOfTransRequest));
      RequestForTransition = _dBInteraction.GetRequestsForTransition();
      TransitQuantity = _dBInteraction.GetTransitQuantity();
      IncomeOnTransRequests = _dBInteraction.GetIncomeOnTransRequest();
      OutcomeOnTransRequests = _dBInteraction.GetOutcomeOnTransRequest();
    }
    #endregion

    #region ProcessIncomeFromVendorsCommand
    public ICommand ProcessIncomeFromVendorsCommand { get; }
    private bool CanProcessIncomeFromVendorsCommandExecute(object p) => true;
    private void OnProcessIncomeFromVendorsCommandExecuted(object p)
    {
      _dBInteraction.Income(System.Int32.Parse(CountProductToRealise));
      RequestForTransition = _dBInteraction.GetRequestsForTransition();
      TransitQuantity = _dBInteraction.GetTransitQuantity();
      IncomeOnTransRequests = _dBInteraction.GetIncomeOnTransRequest();
      OutcomeOnTransRequests = _dBInteraction.GetOutcomeOnTransRequest();
    }
    #endregion

    #region RealizeProductsCommand
    public ICommand RealizeProductsCommand { get; }
    private bool CanRealizeProductsCommandExecute(object p) => true;
    private void OnRealizeProductsCommandExecuted(object p)
    {
      _dBInteraction.RealizationProcedure(System.Int32.Parse(ProductToRealise), System.Int32.Parse(VendorToWhoRealise), System.Int32.Parse(CountProductToRealise),
        System.Int32.Parse(WarFromWhichRealise));
      RequestForTransition = _dBInteraction.GetRequestsForTransition();
      TransitQuantity = _dBInteraction.GetTransitQuantity();
      IncomeOnTransRequests = _dBInteraction.GetIncomeOnTransRequest();
      OutcomeOnTransRequests = _dBInteraction.GetOutcomeOnTransRequest();
    }
    #endregion
    #endregion

    public WarehousemanWindowViewModel()
    {
      _dBInteraction = new DBInteraction();
      RequestForTransition = _dBInteraction.GetRequestsForTransition();
      TransitQuantity = _dBInteraction.GetTransitQuantity();
      IncomeOnTransRequests = _dBInteraction.GetIncomeOnTransRequest();
      OutcomeOnTransRequests = _dBInteraction.GetOutcomeOnTransRequest();

      #region Commands
      CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
      WriteOffOnTransitionDocumentCommand = new LambdaCommand(OnWriteOffOnTransitionDocumentCommandExecited, CanWriteOffOnTransitionDocumentCommandExecute);
      RecieveIncomingOnTransitRequestCommand = new LambdaCommand(OnRecieveIncomingOnTransitRequestCommandExevuted, CanRecieveIncomingOnTransitRequestCommandExecute);
      ProcessIncomeFromVendorsCommand = new LambdaCommand(OnProcessIncomeFromVendorsCommandExecuted, CanProcessIncomeFromVendorsCommandExecute);
      RealizeProductsCommand = new LambdaCommand(OnRealizeProductsCommandExecuted, CanRealizeProductsCommandExecute);
      #endregion
    }
  }
}
