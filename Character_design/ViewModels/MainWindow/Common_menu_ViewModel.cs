using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_design
{
    internal class Common_menu_ViewModel : BaseViewModel
    {
        private static Common_menu_ViewModel _instance;


        public Command Open_Player_Menu { get; private set; }
        public Command Open_Master_Menu { get; private set; }



        public static Common_menu_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Common_menu_ViewModel();
            }
            return _instance;
        }



        private Common_menu_ViewModel()
        {
            Open_Master_Menu = new Command(o => Main_Menu_ViewModel.GetInstance()._Open_Master_Menu());
            Open_Player_Menu = new Command(o => Main_Menu_ViewModel.GetInstance()._Open_Player_Menu());
        }
    }
}
