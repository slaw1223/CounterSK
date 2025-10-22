using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CounterSK.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CounterSK.Models
{
    public partial class MainCounterPageModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<CounterViewModel> counters = new();

        [ObservableProperty]
        private string newCounterName;

        [RelayCommand]
        private async Task AddCounterAsync()
        {
            if (newCounterName == null || newCounterName == " ") {
                newCounterName = "Licznik";
            }
            Counters.Add(new CounterViewModel(newCounterName));
            newCounterName = string.Empty;
        }
        private void RemoveCounter(CounterViewModel counter)
        {
            if (Counters.Contains(counter))
                Counters.Remove(counter);
        }

        public MainCounterPageModel()
        {

            Counters.Add(new CounterViewModel("licznik",RemoveCounter));
        }
    }
}
