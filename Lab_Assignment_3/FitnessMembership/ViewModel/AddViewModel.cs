using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using FitnessMembership.Model;

//SenderViewModel.cs

namespace FitnessMembership.ViewModel
{
    class AddViewModel : ViewModelBase
    {
        private string _firstNameText;
        public RelayCommand OnClickCommand { get; set; }

        public string FirstNameText
        {
            get
            {
                return _firstNameText;
            }
            set
            {
                _firstNameText = value;
                RaisePropertyChanged("FirstNameText");
            }
        }
        public AddViewModel()
        {
            OnClickCommand = new RelayCommand(OnClickCommandAction, null);
        }
        
        private void OnClickCommandAction()
        {
            var viewFirstName = new Member()
            {
                FirstName = FirstNameText
            };
            Messenger.Default.Send(viewFirstName);
        }
    }
   
}
