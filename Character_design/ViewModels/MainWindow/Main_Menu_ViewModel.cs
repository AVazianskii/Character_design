using System.IO;

namespace Character_design
{
    internal class Main_Menu_ViewModel : Notify
    {
        private static Main_Menu_ViewModel _instance;
        private Notify current_VM;
        private Main_Window_Creation_ViewModel main_window_creation;

        public string left_picture_source,
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
            main_window_creation = Main_Window_Creation_ViewModel.GetInstance();
            Open_main_window_creation_user_control = new Command(o => _Open_main_window_creation_user_control());

            Left_picture_source     = Directory.GetCurrentDirectory() + "\\Pictures\\Main_menu\\Left_picture.jpg";
            Right_picture_source    = Directory.GetCurrentDirectory() + "\\Pictures\\Main_menu\\Right_picture.jpg";
            Central_picture_source  = Directory.GetCurrentDirectory() + "\\Pictures\\Main_menu\\Central_picture.jpg";
        }
        public Command Open_main_window_creation_user_control { get; private set; }
        private void _Open_main_window_creation_user_control()
        {
            CurrentViewModel = main_window_creation;
        }
    }
}
