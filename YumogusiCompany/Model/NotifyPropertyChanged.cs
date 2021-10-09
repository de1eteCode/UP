using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace YumogusiCompany.Model
{
    internal class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string nameProperty = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameProperty));
        }
    }
}
