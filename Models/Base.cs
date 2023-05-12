using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF_EF_MSSQL_eCatalog.Models
{
    public class Base : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    }
}