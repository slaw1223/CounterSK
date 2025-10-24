using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CounterSK.ViewModels;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Xml.Linq;

namespace CounterSK.Models
{
    public partial class MainCounterPageModel : ObservableObject
    {
        private const string Filename = "counters.xml";

        [ObservableProperty]
        private ObservableCollection<CounterViewModel> counters = new();

        /* [observablePropery] skrócona wersja 
         * public string NewCounterName { get; set; } = string.Empty;
         * 
         */
        [ObservableProperty]
        private string newCounterName = string.Empty;

        [ObservableProperty]
        private int newCounterInitialValue;

        public MainCounterPageModel()
        {
            LoadCounters();
        }

        [RelayCommand]
        private void AddCounter()
        {
            if (string.IsNullOrWhiteSpace(NewCounterName))
                NewCounterName = "Licznik";

            Counters.Add(new CounterViewModel(NewCounterName, RemoveCounter, NewCounterInitialValue, () => { SaveCounters(); return Task.CompletedTask; }));

            NewCounterName = string.Empty;
            NewCounterInitialValue = 0;

            SaveCounters();
        }

        private void RemoveCounter(CounterViewModel counter)
        {
            Counters.Remove(counter);
            SaveCounters();
        }

        private void SaveCounters()
        {
            try
            {
                var doc = new XDocument(
                    new XElement("Counters",
                        Counters.Select(c =>
                            new XElement("Counter",
                                new XElement("Name", c.Name),
                                new XElement("Value", c.Value),
                                new XElement("InitialValue", c.InitialValue)
                            )
                        )
                    )
                );

                var path = Path.Combine(FileSystem.AppDataDirectory, Filename);
                doc.Save(path);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SaveCounters error: {ex}");
            }
        }

        private void LoadCounters()
        {
            try
            {
                var path = Path.Combine(FileSystem.AppDataDirectory, Filename);

                Debug.WriteLine(path);

                if (!File.Exists(path))
                    return;

                var doc = XDocument.Load(path);

                //Counters.Clear();

                foreach (var element in doc.Elements("Counters")!.Elements("Counter"))
                {
                    string name = (string?)element.Element("Name") ?? "Licznik";
                    int.TryParse(element.Element("Value")?.Value, out int value);
                    int.TryParse(element.Element("InitialValue")?.Value, out int initialValue);

                    var counter = new CounterViewModel(name, RemoveCounter, initialValue, () => { SaveCounters(); return Task.CompletedTask; }, value);
                    Counters.Add(counter);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadCounters error: {ex}");
            }
        }
    }
}
