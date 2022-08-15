using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Character_design
{
    internal class General_Data_ViewModel : BaseViewModel
    {
        private static General_Data_ViewModel _instance;
        private BaseViewModel currentViewModel;
        private BaseViewModel Character_info;
        private BaseViewModel Character_skills;
        private BaseViewModel Character_forms;
        private BaseViewModel Character_features;
        private BaseViewModel Character_equipment;
        private BaseViewModel Character_companion;
        private SolidColorBrush character_info_button_border;
        private SolidColorBrush character_skills_button_border;
        private SolidColorBrush character_forms_button_border;
        private SolidColorBrush character_features_button_border;
        private SolidColorBrush character_equipment_button_border;
        private SolidColorBrush character_companion_button_border;
        private Color Chosen_border_color;
        private Color Unchoosen_border_color;

        private List<SolidColorBrush> SolidBrushes;
        
        public Command Open_character_info { get; private set; }
        public Command Open_character_skills { get; private set; }
        public Command Open_character_forms { get; private set; }
        public Command Open_character_features { get; private set; }
        public Command Open_character_equipment { get; private set; }
        public Command Open_character_companion { get; private set; }

        public static General_Data_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new General_Data_ViewModel();
            }
            return _instance;
        }


        public BaseViewModel CurrentViewModel
        {
            get { return currentViewModel; }
            set { currentViewModel = value; OnPropertyChanged("CurrentViewModel"); }
        }
        public SolidColorBrush Character_info_button_border
        {
            get { return character_info_button_border;  }
            set { character_info_button_border = value; OnPropertyChanged("Character_info_button_border"); }
        }
        public SolidColorBrush Character_skills_button_border
        {
            get { return character_skills_button_border; }
            set { character_skills_button_border = value; OnPropertyChanged("Character_skills_button_border"); }
        }
        public SolidColorBrush Character_forms_button_border
        {
            get { return character_forms_button_border; }
            set { character_forms_button_border = value; OnPropertyChanged("Character_forms_button_border"); }
        }
        public SolidColorBrush Character_features_button_border
        {
            get { return character_features_button_border; }
            set { character_features_button_border = value; OnPropertyChanged("Character_features_button_border"); }
        }
        public SolidColorBrush Character_equipment_button_border
        {
            get { return character_equipment_button_border; }
            set { character_equipment_button_border = value; OnPropertyChanged("Character_equipment_button_border"); }
        }
        public SolidColorBrush Character_companion_button_border
        {
            get { return character_companion_button_border; }
            set { character_companion_button_border = value; OnPropertyChanged("Character_companion_button_border"); }
        }



        private General_Data_ViewModel()
        {
            Character_info      = Character_info_ViewModel.GetInstance();
            Character_skills    = Character_skills_ViewModel.GetInstance();
            Character_forms     = Character_forms_ViewModel.GetInstance();
            Character_features  = Character_features_ViewModel.GetInstance();
            Character_equipment = Character_equipment_ViewModel.GetInstance();
            Character_companion = Character_companion_ViewModel.GetInstance();

            currentViewModel    = Character_info;

            Open_character_info         = new Command(o => _Open_character_info());
            Open_character_skills       = new Command(o => _Open_character_skills());
            Open_character_forms        = new Command(o => _Open_character_forms());
            Open_character_features     = new Command(o => _Open_character_features());
            Open_character_equipment    = new Command(o => _Open_character_equipment());
            Open_character_companion    = new Command(o => _Open_character_companion());

            Unchoosen_border_color  = Colors.Black;
            Chosen_border_color     = Colors.Wheat;

            Character_info_button_border        = new SolidColorBrush();
            Character_skills_button_border      = new SolidColorBrush();
            Character_forms_button_border       = new SolidColorBrush();
            Character_features_button_border    = new SolidColorBrush();
            Character_equipment_button_border   = new SolidColorBrush();
            Character_companion_button_border   = new SolidColorBrush();

            SolidBrushes = new List<SolidColorBrush>();
            SolidBrushes.Add(Character_info_button_border);
            SolidBrushes.Add(Character_skills_button_border);
            SolidBrushes.Add(Character_forms_button_border);
            SolidBrushes.Add(Character_features_button_border);
            SolidBrushes.Add(Character_equipment_button_border);
            SolidBrushes.Add(Character_companion_button_border);

            Set_colors_for_chosen_item(SolidBrushes, Character_info_button_border, Chosen_border_color, Unchoosen_border_color);
        }

        private void _Open_character_info()
        {
            Set_colors_for_chosen_item(SolidBrushes, Character_info_button_border, Chosen_border_color, Unchoosen_border_color);

            CurrentViewModel = Character_info;
        }
        private void _Open_character_skills()
        {
            Set_colors_for_chosen_item(SolidBrushes, Character_skills_button_border, Chosen_border_color, Unchoosen_border_color);

            CurrentViewModel = Character_skills;
        }
        private void _Open_character_forms()
        {
            Set_colors_for_chosen_item(SolidBrushes, Character_forms_button_border, Chosen_border_color, Unchoosen_border_color);

            CurrentViewModel = Character_forms;
        }
        private void _Open_character_features()
        {
            Set_colors_for_chosen_item(SolidBrushes, Character_features_button_border, Chosen_border_color, Unchoosen_border_color);

            CurrentViewModel = Character_features;
        }
        private void _Open_character_equipment()
        {
            Set_colors_for_chosen_item(SolidBrushes, Character_equipment_button_border, Chosen_border_color, Unchoosen_border_color);

            CurrentViewModel = Character_equipment;
        }
        private void _Open_character_companion()
        {
            Set_colors_for_chosen_item(SolidBrushes, Character_companion_button_border, Chosen_border_color, Unchoosen_border_color);

            CurrentViewModel = Character_companion;
        }
    }
}
