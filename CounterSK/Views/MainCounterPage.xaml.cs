using CounterSK.Models;

namespace CounterSK.Views
{
    public partial class MainCounterPage : ContentPage
    {
        public MainCounterPage(MainCounterPageModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
