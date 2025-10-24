using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CounterSK.Models;
using System;
using System.Threading.Tasks;

namespace CounterSK.ViewModels
{
    public partial class CounterViewModel : ObservableObject
    {
        private readonly CounterModel _counter = new();
        private readonly Action<CounterViewModel>? _onDelete;
        private readonly Func<Task>? _onSave;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private int value;

        [ObservableProperty]
        private int initialValue;

        public CounterViewModel(string name = "Licznik",
                                Action<CounterViewModel>? onDelete = null,
                                int initialValue = 0,
                                Func<Task>? onSave = null,
                                int? loadedValue = null)
        {
            Name = name;
            _onDelete = onDelete;
            _onSave = onSave;

            InitialValue = initialValue;
            Value = loadedValue ?? initialValue;
            _counter.Value = Value;
        }

        [RelayCommand]
        private async Task Increment()
        {
            _counter.Increment();
            Value = _counter.Value;
            if (_onSave != null) await _onSave();
        }

        [RelayCommand]
        private async Task Decrement()
        {
            _counter.Decrement();
            Value = _counter.Value;
            if (_onSave != null) await _onSave();
        }

        [RelayCommand]
        private void DeleteCounter() => _onDelete?.Invoke(this);

        [RelayCommand]
        private async Task Reset()
        {
            _counter.Value = InitialValue;
            Value = InitialValue;
            if (_onSave != null) await _onSave();
        }
    }
}
