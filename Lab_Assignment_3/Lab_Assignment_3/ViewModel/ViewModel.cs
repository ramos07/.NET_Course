using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Lab_Assignment_3.Command;


namespace Lab_Assignment_3.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ICommand MyCommand { get; set; }
        public ICommand MultiplyCommand { get; set; }
        public ICommand SubtractCommand { get; set; }
        public ICommand DivideCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;


        private void OnPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        private float _number1;
        public float Number1
        {
            get { return _number1; }
            set { _number1 = value; OnPropertyChanged("Number1"); }
        }


        private float _number2;
        public float Number2
        {
            get { return _number2; }
            set { _number2 = value; OnPropertyChanged("Number2"); }
        }


        private float nubersum;

        public float NumberSum
        {
            get { return nubersum; }
            set { nubersum = value; OnPropertyChanged("NumberSum"); }
        }


        public ViewModel()
        {
            MyCommand = new RelayCommand(execute, canexecute);
            MultiplyCommand = new RelayCommand(executeMultiplication, canexecute);
            SubtractCommand = new RelayCommand(executeSubtraction, canexecute);
            DivideCommand = new RelayCommand(executeDivision, canexecute);
        }


        private bool canexecute(object parameter)
        {
            return true;
        }

        private void execute(object parameter)
        {
            NumberSum = Number1 + Number2;
        }

        private void executeMultiplication(object parameter)
        {
            NumberSum = Number1 * Number2;
        }

        private void executeSubtraction(object parameter)
        {
            NumberSum = Number1 - Number2;
        }

        private void executeDivision(object paramter)
        {
            NumberSum = Number1 / Number2;
        }

    }
}
