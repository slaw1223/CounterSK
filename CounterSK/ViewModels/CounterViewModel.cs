using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CounterSK.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounterSK.ViewModels
{
   public partial class CounterViewModel : ObservableObject
    {
        private CounterModel _counterModel;
        public CounterViewModel()
        {
            _counterModel = new CounterModel();
            _value = _counterModel.Value;
        }
        [ObservableProperty]
        private int _value;

        [RelayCommand]
        private async Task IncrementAsync()
        {
            _counterModel.Increment();
            _value = _counterModel.Value;
        }
        [RelayCommand]
        private async Task DecrementAsync()
        {
            _counterModel.Decrement();
            _value = _counterModel.Value;
        }
    }
}
