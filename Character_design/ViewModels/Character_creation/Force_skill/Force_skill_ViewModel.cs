using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_design
{ 
    internal class Force_skill_ViewModel : BaseViewModel
    {
        private static Force_skill_ViewModel _instance;



        public static Force_skill_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Force_skill_ViewModel();
            }
            return _instance;
        }



        private Force_skill_ViewModel()
        {

        }
    }
}
