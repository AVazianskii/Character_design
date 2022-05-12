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
        private Combat_forms_ViewModel combat_forms;
        private Features_ViewModel features;
        private Equipment_ViewModel equipment;
        private Spaceship_ViewModel spaceship;
        private Companion_ViewModel companion;

        private SolidColorBrush character_button_border,
                                race_button_border,
                                skill_button_border,
                                force_skill_border,
                                combat_form_border,
                                feature_border,
                                equipment_border,
                                spaceship_border,
                                companion_border;

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
        public Command Open_combat_forms_user_control { get; private set; }
        public Command Open_feature_user_control { get; private set; }
        public Command Open_equipment_user_control { get; private set; }
        public Command Open_spaceship_user_control { get; private set; }
        public Command Open_companion_user_control { get; private set; }
        public Command Return_back_to_Menu { get; private set; }
        public Command Save_character_card { get; private set; }
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
        public SolidColorBrush Combat_form_border
        {
            get { return combat_form_border; }
            set { combat_form_border = value; OnPropertyChanged("Combat_form_border"); }
        }
        public SolidColorBrush Feature_border
        {
            get { return feature_border; }
            set { feature_border = value; OnPropertyChanged("Feature_border"); }
        }
        public SolidColorBrush Equipment_border
        {
            get { return equipment_border; }
            set { equipment_border = value; OnPropertyChanged("Equipment_border"); }
        }
        public SolidColorBrush Spaceship_border
        {
            get { return spaceship_border; }
            set { spaceship_border = value; OnPropertyChanged("Spaceship_border"); }
        }
        public SolidColorBrush Companion_border
        {
            get { return companion_border; }
            set { companion_border = value; OnPropertyChanged("Companion_border"); }
        }



        private Main_Window_Creation_ViewModel()
        {
            Open_general_data_user_control  = new Command(o => _Open_general_fata_user_control());
            Open_race_user_control          = new Command(o => _Open_Race_user_control());
            Open_skill_user_control         = new Command(o => _Open_skill_user_control());
            Open_force_skill_user_control   = new Command(o => _Open_force_skill_user_control(), 
                                                          o => Character.GetInstance().Forceuser);
            Open_combat_forms_user_control  = new Command(o => _Open_combat_forms_user_control());
            Open_feature_user_control       = new Command(o => _Open_feature_user_control());
            Open_equipment_user_control     = new Command(o => _Open_equipment_user_control());
            Open_spaceship_user_control     = new Command(o => _Open_spaceship_user_control());
            Open_companion_user_control     = new Command(o => _Open_companion_user_control());
            Return_back_to_Menu             = new Command(o => Return_to_main_menu(Character.GetInstance()));
            Save_character_card             = new Command(o => Save_character_info(), 
                                                          o => Character.GetInstance().Saved_state == false);

            general_data    = General_Data_ViewModel.GetInstance();
            race            = Race_ViewModel.GetInstance();
            skill           = Skill_ViewModel.GetInstance();
            force_skill     = Force_skill_ViewModel.GetInstance();
            combat_forms    = Combat_forms_ViewModel.GetInstance();
            features        = Features_ViewModel.GetInstance();
            equipment       = Equipment_ViewModel.GetInstance();
            spaceship       = Spaceship_ViewModel.GetInstance();
            companion       = Companion_ViewModel.GetInstance();

            current_VM = general_data;

            Chosen_color    = Colors.Wheat;
            Unchosen_color  = Colors.Black;

            Character_button_border = new SolidColorBrush();
            Race_button_border      = new SolidColorBrush();
            Skill_button_border     = new SolidColorBrush();
            Force_skill_border      = new SolidColorBrush();
            Combat_form_border      = new SolidColorBrush();
            Feature_border          = new SolidColorBrush();
            Equipment_border        = new SolidColorBrush();
            Spaceship_border        = new SolidColorBrush();
            Companion_border        = new SolidColorBrush();

            Button_borders = new List<SolidColorBrush>();
            Button_borders.Add(Character_button_border);
            Button_borders.Add(Race_button_border);
            Button_borders.Add(Skill_button_border);
            Button_borders.Add(Force_skill_border);
            Button_borders.Add(Combat_form_border);
            Button_borders.Add(Feature_border);
            Button_borders.Add(Equipment_border);
            Button_borders.Add(Spaceship_border);
            Button_borders.Add(Companion_border);

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
        private void _Open_combat_forms_user_control()
        {
            Set_colors_for_chosen_item(Button_borders, Combat_form_border, Chosen_color, Unchosen_color);

            CurrentViewModel = combat_forms;
        }
        private void _Open_feature_user_control()
        {
            Set_colors_for_chosen_item(Button_borders, Feature_border, Chosen_color, Unchosen_color);

            CurrentViewModel = features;
        }
        private void _Open_equipment_user_control()
        {
            Set_colors_for_chosen_item(Button_borders, Equipment_border, Chosen_color, Unchosen_color);

            CurrentViewModel = equipment;
        }
        private void _Open_spaceship_user_control()
        {
            Set_colors_for_chosen_item(Button_borders, Spaceship_border, Chosen_color, Unchosen_color);

            CurrentViewModel = spaceship;
        }
        private void _Open_companion_user_control()
        {
            Set_colors_for_chosen_item(Button_borders, Companion_border, Chosen_color, Unchosen_color);

            CurrentViewModel = companion;
        }
        private void  Save_character_info ()
        {
            Character.GetInstance().Save_character_to_excel_card();

            Save_character_excel.GetInstance().Save_character_to_Excel_card(Character.GetInstance());
        }
        private void Return_to_main_menu(Character character)
        {
            if (character.Saved_state != true)
            {
                // Предупреждение, что выход без сохранения актуального состояния персонажа
            }

            // Вызов метода возврата к главному меню
            Main_Menu_ViewModel.GetInstance()._Return_from_exp_player_char_creation();
        }
    }
}
