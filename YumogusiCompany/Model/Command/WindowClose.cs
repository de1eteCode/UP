using System.Windows;
using YumogusiCompany.ViewModel.Abstract;

namespace YumogusiCompany.Model.Command
{
    internal class WindowClose : WindowCommand
    {
        public WindowClose(IVM viewModel) : base(viewModel)
        {
        }

        protected override void Command()
        {
            var displayRootRegistry = (Application.Current as App).DisplayRootRegistrary;
            displayRootRegistry.HidePresentation(_viewModel);
        }
    }
}
