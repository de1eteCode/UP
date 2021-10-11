using System.Windows;
using YumogusiCompany.Core;
using YumogusiCompany.View;
using YumogusiCompany.ViewModel;

namespace YumogusiCompany
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ListOfProductVM? _mainVM;
        public DisplayRootRegistrary DisplayRootRegistrary = new DisplayRootRegistrary();

        public App()
        {
            DisplayRootRegistrary.RegisterWindowType<ListOfProductVM, ListOfProducts>();
            DisplayRootRegistrary.RegisterWindowType<AddProductVM, AddProduct>();
            DisplayRootRegistrary.RegisterWindowType<EditProductVM, EditProduct>();
        }

        protected async override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _mainVM = new ListOfProductVM();
            await DisplayRootRegistrary.ShowModalPresentation(_mainVM);

            Shutdown();
        }
    }
}
