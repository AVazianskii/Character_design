using System;
using System.Collections.Generic;
using System.IO;

namespace Character_design
{
    internal class Master_menu_ViewModel : BaseViewModel
    {
        private static Master_menu_ViewModel _instance;



        public Command Open_Common_Menu { get; private set; }
        public Command Open_Quest_creation { get; private set; }
        public Command Open_Game_helper { get; private set; }
        public Command Open_Generators { get; private set; }
        


        public static Master_menu_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Master_menu_ViewModel();
            }
            return _instance;
        }
        public void DeleteInstance()
        {
            if (_instance != null)
            {
                _instance = null;
            }
        }



        private Master_menu_ViewModel()
        {
            Open_Common_Menu = new Command(o => Main_Menu_ViewModel.GetInstance()._Open_Common_Menu());
            Open_Quest_creation = new Command(o => _Test());
            Open_Game_helper = new Command(o => _Test());
            Open_Generators = new Command(o => _Test());
        }



        private void _Test()
        {

        }
    }
}
