using System;
using System.Collections.Generic;
using Skills_libs;
using SW_Character_creation;

namespace Character_design
{
    internal class Character_skills_ViewModel : BaseViewModel
    {
        private static Character_skills_ViewModel _instance;

        private List<Skill_Class> skill_group;


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
