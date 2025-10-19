using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CounterSK.Models
{
    internal class CounterModel
    {
        public int Value { get; set; }
        public void Increment() => Value++;

        public void Decrement() => Value--;
    }
}
