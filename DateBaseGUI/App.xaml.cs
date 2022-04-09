using System.Windows;
using DateBaseGUI.ViewModels;
using DateBaseGUI.Views;

namespace DateBaseGUI
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    public DisplayRootRegistry displayRootRegistry = new DisplayRootRegistry();
    public App()
    {
      displayRootRegistry.RegisterWindowType<LogisticianWindowViewModel, LogisticianWindow>();
      displayRootRegistry.RegisterWindowType<WarehousemanWindowViewModel, WarehousemanWindow>();
      displayRootRegistry.RegisterWindowType<TechnologistWindowViewModel, TechnologistWindow>();
      displayRootRegistry.RegisterWindowType<ManagerWindowViewModel, ManagerWindow>();
      displayRootRegistry.RegisterWindowType<ShopManagerWindowViewModel, ShopManagerWindow>();
    }
  }
}
