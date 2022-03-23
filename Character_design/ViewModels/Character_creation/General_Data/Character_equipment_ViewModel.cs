using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_design
{
    internal class Character_equipment_ViewModel : BaseViewModel
    {
        private static Character_equipment_ViewModel _instance;
        public static Character_equipment_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Character_equipment_ViewModel();
            }
            return _instance;
        }


        private Character_equipment_ViewModel()
        {

        }
    }
}
