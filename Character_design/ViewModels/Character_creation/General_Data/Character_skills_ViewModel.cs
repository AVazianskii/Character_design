using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_design
{
    internal class Character_skills_ViewModel : Notify
    {
        private static Character_skills_ViewModel _instance;
        public static Character_skills_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Character_skills_ViewModel();
            }
            return _instance;
        }


        private Character_skills_ViewModel()
        {

        }
    }
}
