using System;
using System.Collections.Generic;
using System.IO;

namespace Character_design
{
    internal class Master_menu_ViewModel : BaseViewModel
    {
        private static Master_menu_ViewModel _instance;



        public Command Open_Common_Menu { get; private set; }
        public string Img_path
        {
            get { return $@"{Directory.GetCurrentDirectory()}\Pictures\Common\Content_soon.jpg"; }
        }



        public static Master_menu_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Master_menu_ViewModel();
            }
            return _instance;
        }



        private Master_menu_ViewModel()
        {
            Open_Common_Menu = new Command(o => Main_Menu_ViewModel.GetInstance()._Open_Common_Menu());
        }
    }
}
