using YumogusiCompany.ViewModel.Abstract;

namespace YumogusiCompany.Model.Command
{
    abstract class WindowCommand : RelayCommand
    {
        protected IVM _viewModel;

        public WindowCommand(IVM viewModel) : base()
        {
            base._execute = obj =>
            {
                Command();
            };
            base._canExecute = null;
            _viewModel = viewModel;
        }

        protected abstract void Command();
    }
}
