using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Character_design
{
    internal class Main_Window_Creation_ViewModel : BaseViewModel
    {
        private static Main_Window_Creation_ViewModel _instance;
        private BaseViewModel current_VM;
        private General_Data_ViewModel general_data;
        private Race_ViewModel race;
        private Skill_ViewModel skill;
        private Force_skill_ViewModel force_skill;

        private SolidColorBrush character_button_border,
                                race_button_border,
                                skill_button_border,
                                force_skill_border;

        private List<SolidColorBrush> Button_borders;

        private Color Chosen_color,
                      Unchosen_color;



        public static Main_Window_Creation_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Main_Window_Creation_ViewModel();
            }
            return _instance;
        }



        public Command Open_general_data_user_control { get; private set; }
        public Command Open_race_user_control { get; private set; }
        public Command Open_skill_user_control { get; private set; }
        public Command Open_force_skill_user_control { get; private set; }
        public BaseViewModel CurrentViewModel
        {
            get { return current_VM; }
            set { current_VM = value; OnPropertyChanged("CurrentViewModel"); }
        }
        public SolidColorBrush Character_button_border
        {
            get { return character_button_border; }
            set { character_button_border = value; OnPropertyChanged("Character_button_border"); }
        }
        public SolidColorBrush Race_button_border
        {
            get { return race_button_border; }
            set { race_button_border = value; OnPropertyChanged("Race_button_border"); }
        }
        public SolidColorBrush Skill_button_border
        {
            get { return skill_button_border; }
            set { skill_button_border = value; OnPropertyChanged("Skill_button_border"); }
        }
        public SolidColorBrush Force_skill_border
        {
            get { return force_skill_border; }
            set { force_skill_border = value; OnPropertyChanged("Force_skill_border"); }
        }



        private Main_Window_Creation_ViewModel()
        {
            Open_general_data_user_control  = new Command(o => _Open_general_fata_user_control());
            Open_race_user_control          = new Command(o => _Open_Race_user_control());
            Open_skill_user_control         = new Command(o => _Open_skill_user_control());
            Open_force_skill_user_control   = new Command(o => _Open_force_skill_user_control(), o => Character.GetInstance().Forceuser);

            general_data    = General_Data_ViewModel.GetInstance();
            race            = Race_ViewModel.GetInstance();
            skill           = Skill_ViewModel.GetInstance();
            force_skill     = Force_skill_ViewModel.GetInstance();

            current_VM = general_data;

            Chosen_color    = Colors.Wheat;
            Unchosen_color  = Colors.Black;

            Character_button_border = new SolidColorBrush();
            Race_button_border      = new SolidColorBrush();
            Skill_button_border     = new SolidColorBrush();
            Force_skill_border      = new SolidColorBrush();

            Button_borders = new List<SolidColorBrush>();
            Button_borders.Add(Character_button_border);
            Button_borders.Add(Race_button_border);
            Button_borders.Add(Skill_button_border);
            Button_borders.Add(Force_skill_border);

            Set_colors_for_chosen_item(Button_borders, Character_button_border, Chosen_color, Unchosen_color);
        }



        private void _Open_general_fata_user_control ()
        {
            Set_colors_for_chosen_item(Button_borders, Character_button_border, Chosen_color, Unchosen_color);

            CurrentViewModel = general_data;
        }
        private void _Open_Race_user_control()
        {
            Set_colors_for_chosen_item(Button_borders, Race_button_border, Chosen_color, Unchosen_color);

            CurrentViewModel = race;
        }
        private void _Open_skill_user_control()
        {
            Set_colors_for_chosen_item(Button_borders, Skill_button_border, Chosen_color, Unchosen_color);

            CurrentViewModel = skill;
        }
        private void _Open_force_skill_user_control()
        {
            Set_colors_for_chosen_item(Button_borders, Force_skill_border, Chosen_color, Unchosen_color);

            CurrentViewModel = force_skill;
        }
    }
}
