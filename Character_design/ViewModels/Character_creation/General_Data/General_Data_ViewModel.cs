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
        private SolidColorBrush character_button_border;
        private SolidColorBrush Wheat_brush;
        private SolidColorBrush Black_brush;
        
        public Command Open_character_info { get; private set; }

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
        public SolidColorBrush Character_button_border
        {
            get { return character_button_border;  }
            set { character_button_border = value; OnPropertyChanged("Character_button_border"); }
        }

        private General_Data_ViewModel()
        {
            currentViewModel = null;
            Character_info = Character_info_ViewModel.GetInstance(); 

            Open_character_info = new Command(o => _Open_character_info());

            Black_brush = new SolidColorBrush(Colors.Black);
            Wheat_brush = new SolidColorBrush(Colors.Wheat);
            Character_button_border = Wheat_brush;
        }

        private void _Open_character_info()
        {
            Character_button_border = Wheat_brush;
            CurrentViewModel = Character_info;
        }
    }
}
