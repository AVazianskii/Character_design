using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Character_design
{
    internal class General_Data_ViewModel : Notify
    {
        private static General_Data_ViewModel _instance;
        private Notify currentViewModel;
        private Notify Character_info;
        private Notify Character_skills;
        private Notify Character_forms;
        private SolidColorBrush character_info_button_border;
        private SolidColorBrush character_skills_button_border;
        private SolidColorBrush Wheat_brush;
        private SolidColorBrush Black_brush;
        
        public Command Open_character_info { get; private set; }
        public Command Open_character_skills { get; private set; }
        public Command Open_character_forms { get; private set; }

        public static General_Data_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new General_Data_ViewModel();
            }
            return _instance;
        }


        public Notify CurrentViewModel
        {
            get { return currentViewModel; }
            set { currentViewModel = value; OnPropertyChanged("CurrentViewModel"); }
        }
        public SolidColorBrush Character_info_button_border
        {
            get { return character_info_button_border;  }
            set { character_info_button_border = value; OnPropertyChanged("Character_button_border"); }
        }
        public SolidColorBrush Character_skills_button_border
        {
            get { return character_skills_button_border; }
            set { character_skills_button_border = value; OnPropertyChanged("Character_skills_button_border"); }
        }

        private General_Data_ViewModel()
        {
            Character_info = Character_info_ViewModel.GetInstance();
            Character_skills = Character_skills_ViewModel.GetInstance();
            Character_forms = Character_forms_ViewModel.GetInstance();
            currentViewModel = null;

            Open_character_info = new Command(o => _Open_character_info());
            Open_character_skills = new Command(o => _Open_character_skills());
            Open_character_forms = new Command(o => _Open_character_forms());

            Black_brush = new SolidColorBrush(Colors.Black);
            Wheat_brush = new SolidColorBrush(Colors.Wheat);
            Character_info_button_border = Wheat_brush;
        }

        private void _Open_character_info()
        {
            Character_info_button_border = Wheat_brush;
            CurrentViewModel = Character_info;
        }
        private void _Open_character_skills()
        {
            CurrentViewModel = Character_skills;
        }
        private void _Open_character_forms()
        {
            CurrentViewModel = Character_forms;
        }
    }
}
