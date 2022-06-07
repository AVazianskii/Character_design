using System;
using System.Collections.Generic;
using Skills_libs;
using SW_Character_creation;

namespace Character_design
{
    internal class Character_skills_ViewModel : BaseViewModel
    {
        private static Character_skills_ViewModel _instance;

        private object skill_group;
        private object selected_skill;
        private List<Skill_Class> usual_skills_group;
        private List<Force_skill_class> force_skills_group;

        private int button_opacity;



        public Command Show_skills { get; private set; }
        public Command Show_force_skills { get; private set; }
        public object Skill_group
        {
            get { return skill_group; }
        }
        public object Selected_skill
        {
            get { return selected_skill; }
            set { selected_skill = value; OnPropertyChanged("Selected_skill"); }
        }
        public int Button_opacity
        {
            get { return button_opacity; }
            set { button_opacity = value; OnPropertyChanged("Button_opacity"); }
        }



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
            skill_group = new object();
            usual_skills_group = Character.GetInstance().Skills_with_points;
            //force_skills_group = Character.GetInstance().Force_skills_with_points;

            Show_skills = new Command(o => _Show_skills());

            Button_opacity = 100;
        }



        private void _Show_skills()
        {
            skill_group = usual_skills_group;
        }
        private void _Show_force_skills()
        {
            skill_group = force_skills_group;
        }
    }
}
