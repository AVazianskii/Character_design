using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_design
{
    internal class Nothing_chosen_ViewModel : BaseViewModel
    {
        private static Nothing_chosen_ViewModel _instance;


        public static Nothing_chosen_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Nothing_chosen_ViewModel();
            }
            return _instance;
        }


        private Nothing_chosen_ViewModel()
        {

        }
    }
}
