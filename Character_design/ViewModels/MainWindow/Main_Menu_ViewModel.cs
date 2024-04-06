using System.IO;
using System.Windows;

namespace Character_design
{
    internal class Main_Menu_ViewModel : BaseViewModel
    {
        private static Main_Menu_ViewModel _instance;
        private BaseViewModel current_VM;
        private BaseViewModel current_Menu_ViewModel;
        private Main_Window_Creation_ViewModel main_window_creation;
        private Common_menu_ViewModel common_menu;
        private Player_menu_ViewModel player_menu;
        private Master_menu_ViewModel master_menu;
        
        private string left_picture_source,
                      right_picture_source,
                      central_picture_source;



        public static Main_Menu_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Main_Menu_ViewModel();
            }
            return _instance;
        }
        public void OverWriteInstance()
        {
            if (_instance != null)
            {
                _instance = new Main_Menu_ViewModel();
            }
        }
        public void _Open_Player_Menu()
        {
            Current_Menu_ViewModel = player_menu;
        }
        public void _Open_Master_Menu()
        {
            Current_Menu_ViewModel = master_menu;
        }
        public void _Open_Common_Menu()
        {
            Current_Menu_ViewModel = common_menu;
        }
        public void _Open_main_window_creation_user_control()
        {
            Run_method_with_loading("Создание персонажа", () =>
            {
                Character_creation_model.GetInstance().Create_character_creating_model();
            });
            Character_creation_model.GetInstance().Create_character(Character_creation_model.GetInstance().creation_managers);
            main_window_creation = Character_creation_model.GetInstance().Main_Window_Creation_ViewModel;
            CurrentViewModel = main_window_creation;
        }
        public void _Open_main_window_creation_card_edition()
        {
            main_window_creation = Character_creation_model.GetInstance().Main_Window_Creation_ViewModel;
            CurrentViewModel = main_window_creation;
        }

        public void _Return_from_exp_player_char_creation()
        {
            CurrentViewModel        = null;
        }



        public BaseViewModel CurrentViewModel
        {
            get { return current_VM; }
            set { current_VM = value; OnPropertyChanged("CurrentViewModel"); }
        }
        public BaseViewModel Current_Menu_ViewModel
        {
            get { return current_Menu_ViewModel; }
            set { current_Menu_ViewModel = value; OnPropertyChanged("Current_Menu_ViewModel"); }
        }
        public string Left_picture_source
        {
            get { return left_picture_source; }
            set { left_picture_source = value; OnPropertyChanged("Leftt_picture_source"); }
        }
        public string Right_picture_source
        {
            get { return right_picture_source; }
            set { right_picture_source = value; OnPropertyChanged("Right_picture_source"); }
        }
        public string Central_picture_source
        {
            get { return central_picture_source; }
            set { central_picture_source = value; OnPropertyChanged("Central_picture_source"); }
        }




        private Main_Menu_ViewModel()
        {
            current_VM = null;
            Current_Menu_ViewModel = new BaseViewModel();

            common_menu = Common_menu_ViewModel.GetInstance();
            player_menu = Player_menu_ViewModel.GetInstance();
            master_menu = Master_menu_ViewModel.GetInstance();
            
            Current_Menu_ViewModel = common_menu;
                        
            Left_picture_source     = Directory.GetCurrentDirectory() + "\\Pictures\\Main_menu\\Left_picture.jpg";
            Right_picture_source    = Directory.GetCurrentDirectory() + "\\Pictures\\Main_menu\\Right_picture.jpg";
            Central_picture_source  = Directory.GetCurrentDirectory() + "\\Pictures\\Main_menu\\Central_picture.jpg";            
        }
    }
}
