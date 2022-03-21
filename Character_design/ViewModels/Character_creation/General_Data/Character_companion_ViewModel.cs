using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_design
{
    internal class Character_companion_ViewModel : Notify
    {
        private static Character_companion_ViewModel _instance;
        public static Character_companion_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Character_companion_ViewModel();
            }
            return _instance;
        }


        private Character_companion_ViewModel()
        {

        }
    }
}
