using DateBaseGUI.Infrastructure.Commands;
using DateBaseGUI.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DateBaseGUI.Data;
using DateBaseGUI.ViewModels.Base;
using System.Collections.Generic;
using System.Windows;

namespace DateBaseGUI.ViewModels
{
  public class LogisticianWindowViewModel : ViewModel
  {
    #region Properties
    private DBInteraction _dBInteraction;
    private ObservableCollection<Paths> _paths;
    private ObservableCollection<RegularQuantity> _regularQuantity;
    private ObservableCollection<RequestForTransition> _requestForTransition;
    private ObservableCollection<TransitOrderComposition> _transitOrderComposition;
    private string _startWarId;
    private string _endWarId;
    private string _transitWarId;
    private string _products;
    private string _count;

    public ObservableCollection<Paths> Paths { get => _paths; set => Set(ref _paths, value); }
    public ObservableCollection<RegularQuantity> RegularQuantity { get => _regularQuantity; set => Set(ref _regularQuantity, value); }
    public ObservableCollection<RequestForTransition> RequestForTransition { get => _requestForTransition; set => Set(ref _requestForTransition, value); }
    public ObservableCollection<TransitOrderComposition> TransitOrderComposition { get => _transitOrderComposition; set => Set(ref _transitOrderComposition, value); }
    public string StartWarId { get => _startWarId; set => Set(ref _startWarId, value); }
    public string EndWarId { get => _endWarId; set => Set(ref _endWarId, value);}
    public string TransWarId { get => _transitWarId; set => Set(ref _transitWarId, value); }
    public string Products { get => _products; set => Set(ref _products, value); }
    public string Count { get => _count; set => Set(ref _count, value); }
    #endregion

    #region DisplayRootRegistry
    public DisplayRootRegistry displayRootRegistry;
    #endregion

    private MessageBoxResult Alarm(string message, string caption, MessageBoxButton button, MessageBoxImage icon) =>
      System.Windows.MessageBox.Show(message, caption, button, icon);
    private string ResultToString(List<int> result)
    {
      string toReturn = "";
      foreach(var number in result)
        toReturn += number.ToString() + ", ";
      toReturn.Trim(' ', ',');
      return toReturn;
    }

    #region Commands

    #region CloseApplicationCommand
    public ICommand CloseApplicationCommand { get; }
    private void OnCloseApplicationCommandExecuted(object p)
    {
      displayRootRegistry.HidePresentation(this);
    }
    private bool CanCloseApplicationCommandExecute(object p) => true;
    #endregion

    #region CreateTransitionDocumentCommand
    public ICommand CreateTransitionDocumentCommand { get; }
    private bool CanCreateTransitionDocumentCommandExecute(object p) => System.Int32.TryParse(StartWarId, out int result) && System.Int32.TryParse(TransWarId, out result) &&
      System.Int32.TryParse(EndWarId, out result) && System.Int32.TryParse(Products, out result) && System.Int32.TryParse(Count, out result);
    private void OnCreateTransitionDocumentCommandExevuted(object p)
    {
      string[] products = Products.Split(',');
      List<int> listProducts = new List<int>();
      foreach (string product in products)
      {
        listProducts.Add(System.Int32.Parse(product.Trim(' ')));
      }
      List<int> listCount = new List<int>();
      string[] counts = Count.Split(',');
      foreach(string count in counts)
      {
        listCount.Add(System.Int32.Parse(count.Trim(' ')));
      }
      int currentRequestId = _dBInteraction.MaxIndexTransReq() + 1;
      for(int i = 0; i < listProducts.Count; i++)
      {
        var someList = _dBInteraction.CreateRequestForTransitionForOneProduct(currentRequestId, System.Int32.Parse(StartWarId), System.Int32.Parse(TransWarId),
          System.Int32.Parse(EndWarId), listProducts[i], listCount[i], i+1);
        if (someList != null)
        {
          Alarm($"All transit warehouses are bisy, please choose from this: {ResultToString(someList)}", "Word Processor", MessageBoxButton.OK, MessageBoxImage.Information);
          return;
        }
      }
      RequestForTransition = _dBInteraction.GetRequestsForTransition();
      TransitOrderComposition = _dBInteraction.GetTransitOrderComposition();
    }

    #endregion

    #region AddNewPathCommand
    public ICommand AddNewPathCommand { get; }
    private bool CanAddNewPathCommandExecute(object p) => System.Int32.TryParse(StartWarId, out int result) && System.Int32.TryParse(TransWarId, out result) &&
      System.Int32.TryParse(EndWarId, out result);
    private void OnAddNewPathCommandExecuted(object p)
    {
      _dBInteraction.SetPaths(System.Int32.Parse(StartWarId), System.Int32.Parse(EndWarId), System.Int32.Parse(TransWarId));
    }

    #endregion
    #endregion
    public LogisticianWindowViewModel()
    {
      _dBInteraction = new DBInteraction();
      Paths = _dBInteraction.GetPaths();
      RegularQuantity = _dBInteraction.GetRegularQuantity();
      RequestForTransition = _dBInteraction.GetRequestsForTransition();
      TransitOrderComposition = _dBInteraction.GetTransitOrderComposition();

      #region Commands
      AddNewPathCommand = new LambdaCommand(OnAddNewPathCommandExecuted, CanAddNewPathCommandExecute);
      CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
      CreateTransitionDocumentCommand = new LambdaCommand(OnCreateTransitionDocumentCommandExevuted, CanCreateTransitionDocumentCommandExecute);
      #endregion
    }
  }
}
