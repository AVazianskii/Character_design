using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_design
{
    internal class Main_Window_Creation_ViewModel : Notify
    {
        private static Main_Window_Creation_ViewModel _instance;
        private Notify current_VM;
        private General_Data_ViewModel general_data;
        private Race_ViewModel race;
        private Skill_ViewModel skill;
        public static Main_Window_Creation_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Main_Window_Creation_ViewModel();
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
        private Main_Window_Creation_ViewModel()
        {
            Open_general_data_user_control = new Command(o => _Open_general_fata_user_control());
            Open_race_user_control = new Command(o => _Open_Race_user_control());
            Open_range_user_control = new Command(o => _Open_skill_user_control());
            general_data = General_Data_ViewModel.GetInstance();
            race = Race_ViewModel.GetInstance();
            skill = Skill_ViewModel.GetInstance();
            current_VM = null;
        }
        public Command Open_general_data_user_control { get; private set; }
        public Command Open_race_user_control { get; private set; }
        public Command Open_range_user_control { get; private set; }
        private void _Open_general_fata_user_control ()
        {
            CurrentViewModel = general_data;
        }
        private void _Open_Race_user_control()
        {
            CurrentViewModel = race;
        }
        private void _Open_skill_user_control()
        {
            CurrentViewModel = skill;
        }
    }
}
