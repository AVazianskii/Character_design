using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_design
{
    internal class Player_menu_ViewModel : BaseViewModel
    {
        private static Player_menu_ViewModel _instance;



        public Command Open_Common_Menu { get; private set; }
        public Command Open_Profi_Character_creation { get; private set; }
        public Command Open_Character_editor { get; private set; }
        public Command Open_Character_story_helper { get; private set; }
        public Command Open_Game_rules { get; private set; }



        public static Player_menu_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Player_menu_ViewModel();
            }
            return _instance;
        }



        private Player_menu_ViewModel()
        {
            Open_Common_Menu = new Command(o => Main_Menu_ViewModel.GetInstance()._Open_Common_Menu());
            Open_Profi_Character_creation = new Command(o => Main_Menu_ViewModel.GetInstance()._Open_main_window_creation_user_control());
            Open_Character_editor = new Command(o => _Test());
            Open_Character_story_helper = new Command(o => _Test());
            Open_Game_rules = new Command(o => _Test());
        }



        private void _Test()
        {

        }
    }
}
