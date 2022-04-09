using DateBaseGUI.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DateBaseGUI.Data;
using DateBaseGUI.ViewModels.Base;
using System.Collections.Generic;
using DateBaseGUI.Infrastructure.Commands;

namespace DateBaseGUI.ViewModels
{
  public class ManagerWindowViewModel : ViewModel
  {
    #region DisplayRootRegistry
    public DisplayRootRegistry displayRootRegistry;
    #endregion

    #region Properties
    private DBInteraction _dBInteraction;
    private ObservableCollection<Operations> _operations;
    private ObservableCollection<RegularQuantity> _regularQuantity;
    private ObservableCollection<RequestForProduction> _requestForProduction;
    private string _operationId;
    private string _warWhereProductId;
    private string _productId;
    private string _countOfProduct;
    public string OperationId { get => _operationId; set => Set(ref _operationId, value); }
    public string WarWhereProductId { get => _warWhereProductId; set => Set(ref _warWhereProductId, value); }
    public string ProductId { get => _productId; set => Set(ref _productId, value); }
    public string CountOfProduct { get => _countOfProduct; set => Set(ref _countOfProduct, value); }
    public ObservableCollection<RequestForProduction> RequestForProduction { get => _requestForProduction; set => Set(ref _requestForProduction, value); }
    public ObservableCollection<Operations> Operations { get => _operations; set => Set(ref _operations, value); }
    public ObservableCollection<RegularQuantity> RegularQuantitiy { get => _regularQuantity; set => Set(ref _regularQuantity, value); }
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

    #region CreateRequestForProduction
    public ICommand CreateRequestForProduction { get; }
    private bool CanCreateRequestForProductionExecute(object p) => System.Int32.TryParse(ProductId, out int result)
      && System.Int32.TryParse(WarWhereProductId, out result) &&
      System.Int32.TryParse(ProductId, out result) && System.Int32.TryParse(CountOfProduct, out result);
    private void OnCreateRequestForProductionExecuted(object p)
    {
      _dBInteraction.CreateRequestForProduction(System.Int32.Parse(OperationId), System.Int32.Parse(WarWhereProductId), System.Int32.Parse(ProductId), System.Int32.Parse(CountOfProduct));
      Operations = _dBInteraction.GetOperations();
      RegularQuantitiy = _dBInteraction.GetRegularQuantity();
      RequestForProduction = _dBInteraction.GetRequsetsForProduction();
    }
    #endregion
    #endregion
    public ManagerWindowViewModel()
    {
      _dBInteraction = new DBInteraction();
      Operations = _dBInteraction.GetOperations();
      RegularQuantitiy = _dBInteraction.GetRegularQuantity();
      RequestForProduction = _dBInteraction.GetRequsetsForProduction();

      #region Commands
      CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
      CreateRequestForProduction = new LambdaCommand(OnCreateRequestForProductionExecuted, CanCreateRequestForProductionExecute);
      #endregion
    }
  }
}
