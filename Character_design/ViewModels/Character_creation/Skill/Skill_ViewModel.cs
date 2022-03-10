using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SW_Character_creation;
using Skills_libs;

namespace Character_design
{
    internal class Skill_ViewModel : Notify
    {
        private static Skill_ViewModel _instance;
        private Notify current_VM;

        private Skill_Class selected_skill;
        private List<Skill_Class> skill_group;
        private List<Skill_Class> combat_skills;
        private List<Skill_Class> surviving_skills;
        private List<Skill_Class> charming_skills;
        private List<Skill_Class> tech_skills;
        private List<Skill_Class> specific_skills;

        private string selected_skill_description;

        private int exp_points_left;
        public static Skill_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Skill_ViewModel();
            }
            return _instance;
        }
        public Notify CurrentViewModel
        {
            get
            {
                return current_VM;
            }
            set
            {
                current_VM = value;
                OnPropertyChanged("CurrentViewModel");
            }
        }
        public Command Show_combat_skills { get; private set; }
        public Command Show_surviving_skills { get; private set; }
        public Command Show_charming_skills { get; private set; }
        public Command Show_tech_skills { get; private set; }
        public Command Show_specific_skills { get; private set; }

        private Skill_ViewModel()
        {
            combat_skills = Skill_manager.GetInstance().Get_Combat_skills();
            surviving_skills = Skill_manager.GetInstance().Get_Survivng_skills();
            charming_skills = Skill_manager.GetInstance().Get_Charming_skills();
            tech_skills = Skill_manager.GetInstance().Get_Tech_skills();
            specific_skills = Skill_manager.GetInstance().Get_Specific_skills();

            skill_group = combat_skills;

            Show_combat_skills = new Command(o => _Show_combat_skills());
            Show_surviving_skills = new Command(o => _Show_surviving_skills());
            Show_charming_skills = new Command(o => _Show_charming_skills());
            Show_tech_skills = new Command(o => _Show_tech_skills());
            Show_specific_skills = new Command(o => _Show_specific_skills());
        }
        private void _Show_combat_skills()
        {
            skill_group = combat_skills;
        }
        private void _Show_surviving_skills()
        {
            skill_group = surviving_skills;
        }
        private void _Show_charming_skills()
        {
            skill_group = charming_skills;
        }
        private void _Show_tech_skills()
        {
            skill_group = tech_skills;
        }
        private void _Show_specific_skills()
        {
            skill_group = specific_skills;
        }

        public int Exp_points_left
        {
            get
            {
                return exp_points_left;
            }
            set
            {
                exp_points_left = value;
                OnPropertyChanged("Exp_points_left");
            }
        }
        public Skill_Class Selected_skill
        {
            get
            {
                return selected_skill;
            }
            set
            {
                selected_skill = value;
                OnPropertyChanged("Selected_skill");
            }
        }
        public List<Skill_Class> Skill_group
        {
            get
            {
                return skill_group;
            }
            set
            {
                skill_group = value;
                OnPropertyChanged("Skill_group");
            }
        }
        public string Selected_skill_description
        {
            get
            {
                return selected_skill_description;
            }
            set
            {
                selected_skill_description = value;
                OnPropertyChanged("Selected_skill_description");
            }
        }




    }
}
