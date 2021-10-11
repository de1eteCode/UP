using System;
using System.Windows;
using YumogusiCompany.ViewModel.Abstract;

namespace YumogusiCompany.Model.Command
{
    class WindowOpen : WindowCommand
    {
        public WindowOpen(IVM viewModel) : base(viewModel)
        {
        }

        protected override void Command()
        {
            var displayRootRegistry = (Application.Current as App).DisplayRootRegistrary;
            displayRootRegistry.ShowPresentation(_viewModel);
        }
    }
}
