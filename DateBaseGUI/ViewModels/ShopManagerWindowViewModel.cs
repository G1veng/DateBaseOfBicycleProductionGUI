using DateBaseGUI.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DateBaseGUI.Data;
using DateBaseGUI.ViewModels.Base;
using System.Collections.Generic;
using DateBaseGUI.Infrastructure.Commands;

namespace DateBaseGUI.ViewModels
{
  public class ShopManagerWindowViewModel : ViewModel
  {
    #region DisplayRootRegistry
    public DisplayRootRegistry displayRootRegistry;
    #endregion

    #region Properties
    private DBInteraction _dBInteraction;
    private ObservableCollection<UnfinishedProduction> _unfinishedProduction;
    private ObservableCollection<RegularQuantity> _regularQuantity;
    private ObservableCollection<RequestForProduction> _requestForProduction;
    private ObservableCollection<OutcomeFromProduction> _outcomeFromProduction;
    private string _productionRequestId;
    private string _producedProduct;
    private string _countOfPRoducedProduct;

    public string ProducedProduct { get => _producedProduct; set => Set(ref _producedProduct, value); }
    public string CountOfPRoducedProduct { get => _countOfPRoducedProduct; set => Set(ref _countOfPRoducedProduct, value); }
    public string ProductionRequestId { get => _productionRequestId; set => Set(ref _productionRequestId, value); }
    public ObservableCollection<UnfinishedProduction> UnfinishedProduction { get => _unfinishedProduction; set => Set(ref _unfinishedProduction, value); }
    public ObservableCollection<RegularQuantity> RegularQuantity { get => _regularQuantity; set => Set(ref _regularQuantity, value); }
    public ObservableCollection<RequestForProduction> RequestForProduction { get => _requestForProduction; set => Set(ref _requestForProduction, value);}
    public ObservableCollection<OutcomeFromProduction> OutcomeFromProduction { get => _outcomeFromProduction; set => Set(ref _outcomeFromProduction, value); }
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

    #region OutComeFromProductionCommand
    public ICommand OutComeFromProductionCommand { get; }
    private bool CanOutComeFromProductionCommandExecute(object p) => System.Int32.TryParse(ProductionRequestId, out int result) &&
      System.Int32.TryParse(ProducedProduct, out result) && System.Int32.TryParse(CountOfPRoducedProduct, out result);
    private void OnOutComeFromProductionCommandExecuted(object p)
    {
      _dBInteraction.OutcomeFromProduction(System.Int32.Parse(ProductionRequestId), System.Int32.Parse(CountOfPRoducedProduct), System.Int32.Parse(ProducedProduct));
      UnfinishedProduction = _dBInteraction.GetUnfinishedProduction();
      RegularQuantity = _dBInteraction.GetRegularQuantity();
      RequestForProduction = _dBInteraction.GetRequsetsForProduction();
      OutcomeFromProduction = _dBInteraction.GetOutcomeFromProduction();
    }
    #endregion
    #endregion
    public ShopManagerWindowViewModel()
    {
      _dBInteraction = new DBInteraction();
      UnfinishedProduction = _dBInteraction.GetUnfinishedProduction();
      RegularQuantity = _dBInteraction.GetRegularQuantity();
      RequestForProduction = _dBInteraction.GetRequsetsForProduction();
      OutcomeFromProduction = _dBInteraction.GetOutcomeFromProduction();

      #region Commands
      CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
      OutComeFromProductionCommand = new LambdaCommand(OnOutComeFromProductionCommandExecuted, CanOutComeFromProductionCommandExecute);
      #endregion
    }
  }
}
