using System;
using System.Windows;
using YumogusiCompany.ViewModel.Abstract;

namespace YumogusiCompany.Model.Command
{
    class ModalWindowOpen : WindowCommand
    {
        public ModalWindowOpen(IVM viewModel) : base(viewModel)
        {
        }

        protected async override void Command()
        {
            var displayRootRegistry = (Application.Current as App).DisplayRootRegistrary;
            await displayRootRegistry.ShowModalPresentation(_viewModel);
        }
    }
}
