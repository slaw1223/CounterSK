using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CounterSK.Models;
using System.Threading.Tasks;

namespace CounterSK.ViewModels
{
    public partial class CounterViewModel : ObservableObject
    {
        private readonly CounterModel _counter = new();
        private readonly Action<CounterViewModel>? _onDelete;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private int value;

        public CounterViewModel(string name = "Licznik", Action<CounterViewModel>? onDelete = null)
        {
            this.name = name;
            _onDelete = onDelete;
            Value = _counter.Value;
        }

        [RelayCommand]
        private async Task IncrementAsync()
        {
            await Task.Delay(20);
            _counter.Increment();
            Value = _counter.Value;
        }

        [RelayCommand]
        private async Task DecrementAsync()
        {
            await Task.Delay(20);
            _counter.Decrement();
            Value = _counter.Value;
        }
        [RelayCommand]
        private async Task DeleteAsync()
        {
            await Task.Delay(20);
            _onDelete?.Invoke(this);
        }
    }
}
