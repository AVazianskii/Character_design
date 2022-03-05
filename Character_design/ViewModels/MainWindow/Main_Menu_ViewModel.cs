using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_design
{
    internal class Main_Menu_ViewModel : Notify
    {
        private static Main_Menu_ViewModel _instance;
        private Notify current_VM;
        private Main_Window_Creation_ViewModel main_window_creation;
        public static Main_Menu_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Main_Menu_ViewModel();
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

        private Main_Menu_ViewModel()
        {
            current_VM = null;
            main_window_creation = Main_Window_Creation_ViewModel.GetInstance();
            Open_main_window_creation_user_control = new Command(o => _Open_main_window_creation_user_control());
        }
        public Command Open_main_window_creation_user_control { get; private set; }
        private void _Open_main_window_creation_user_control()
        {
            CurrentViewModel = main_window_creation;
        }
    }
}
