using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_design
{
    internal class Range_ViewModel : Notify
    {
        private static Range_ViewModel _instance;
        private Notify current_VM;
        public static Range_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Range_ViewModel();
            }
            return _instance;
        }
        public Notify CurrentViewModel
        {
            get
            {
                return current_VM;
            }
            set
            {
                current_VM = value;
                OnPropertyChanged("CurrentViewModel");
            }
        }
        private Range_ViewModel()
        {

        }
    }
}
