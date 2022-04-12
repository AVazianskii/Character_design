using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_design
{
    internal class Master_menu_ViewModel : BaseViewModel
    {
        private static Master_menu_ViewModel _instance;



        public Command Open_Common_Menu { get; private set; }



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
