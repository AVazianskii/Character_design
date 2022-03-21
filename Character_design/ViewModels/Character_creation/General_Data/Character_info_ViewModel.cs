using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_design
{
    internal class Character_info_ViewModel : Notify
    {
        private static Character_info_ViewModel _instance;
        public static Character_info_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Character_info_ViewModel();
            }
            return _instance;
        }
    }
}
