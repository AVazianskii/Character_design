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
        private string skill_name;
        private string selected_skill_title;

        private int exp_points_left,
                    selected_skill_score,
                    selected_skill_min_score,
                    selected_skill_max_score,
                    selected_skill_race_bonus,
                    selected_skill_range_limit,
                    selected_skill_age_limit,
                    selected_skill_cost;

        private int test;

        private int Return_skill_cost(Skill_Class skill)
        {
            if(Character.GetInstance().Forceuser)
            {
                return skill.Get_Forceuser_cost();
            }
            else
            {
                return skill.Get_Non_force_user_cost();
            }
        }
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
        public Command Decrease_skill_score { get; private set; }
        public Command Increase_skill_score { get; private set; }

        private Skill_ViewModel()
        {
            combat_skills = Skill_manager.GetInstance().Get_Combat_skills();
            surviving_skills = Skill_manager.GetInstance().Get_Survivng_skills();
            charming_skills = Skill_manager.GetInstance().Get_Charming_skills();
            tech_skills = Skill_manager.GetInstance().Get_Tech_skills();
            specific_skills = Skill_manager.GetInstance().Get_Specific_skills();

            skill_group = combat_skills;
            Selected_skill = skill_group[0];

            Show_combat_skills = new Command(o => _Show_combat_skills());
            Show_surviving_skills = new Command(o => _Show_surviving_skills());
            Show_charming_skills = new Command(o => _Show_charming_skills());
            Show_tech_skills = new Command(o => _Show_tech_skills());
            Show_specific_skills = new Command(o => _Show_specific_skills());

            //Decrease_skill_score = new Command(o => selected_skill.Decrease_score(ref exp_points_left,
            //                                                                      ref selected_skill_score,
            //                                                                      Selected_skill_cost));
            Increase_skill_score = new Command(o =>
            {
                Skill_Class skill = o as Skill_Class;
                if (skill != null)
                {
                    skill.Increase_score();
                    Selected_skill_score = skill.Get_score();
                }
            });
            //Character.GetInstance().Swimming.Increase_score(Character.GetInstance().Experience,
            //Character.GetInstance().Swimming
        }
        private void Increase_selected_skill()
        {
            foreach(Skill_Class Skill in Character.GetInstance().Get_skills())
            {
                if(Skill.Get_skill_name() == selected_skill.Get_skill_name())
                {
                    Skill.Increase_score();
                    Test = Skill.Get_score();
                    //OnPropertyChanged("Selected_skill");
                }
            }
        }
        private void _Show_combat_skills()
        {
            Skill_group = combat_skills;
            Selected_skill = Skill_group[0];
        }
        private void _Show_surviving_skills()
        {
            Skill_group = surviving_skills;
            Selected_skill = Skill_group[0];
        }
        private void _Show_charming_skills()
        {
            Skill_group = charming_skills;
            Selected_skill = Skill_group[0];
        }
        private void _Show_tech_skills()
        {
            Skill_group = tech_skills;
            Selected_skill = Skill_group[0];
        }
        private void _Show_specific_skills()
        {
            Skill_group = specific_skills;
            Selected_skill = Skill_group[0];
        }

        public int Exp_points_left
        {
            get
            {
                exp_points_left = Character.GetInstance().Experience;
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
                if (selected_skill != null)
                {
                    Selected_skill_description = selected_skill.Get_skill_description();
                    Selected_skill_title = selected_skill.Get_skill_name();
                    Selected_skill_score = selected_skill.Get_score();
                    // TODO: сделать логику определения максимального значения навыка, опираясь на возраст, ранг и прочее
                    Selected_skill_max_score = 10; 
                    Selected_skill_race_bonus = 1;
                    // TODO: сделать логику определения минимального значения навыка, опираясь на расовые бонусы и прочее
                    Selected_skill_min_score = Selected_skill_race_bonus;  
                    Selected_skill_range_limit = selected_skill.Get_range_skill_limit();
                    selected_skill_age_limit = selected_skill.Get_age_skill_limit();
                    Selected_skill_cost = Return_skill_cost(selected_skill);
                }
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
        public string Skill_name
        {
            get
            {
                return skill_name;
            }
            set
            {
                skill_name = value;
                OnPropertyChanged("Skill_name");
            }
        }
        public string Selected_skill_title
        {
            get
            {
                return selected_skill_title;
            }
            set
            {
                selected_skill_title = value;
                OnPropertyChanged("Selected_skill_title");
            }
        }
        public int Selected_skill_score
        {
            get
            {
                return selected_skill_score;
            }
            set
            {
                selected_skill_score = value;
                OnPropertyChanged("Selected_skill_score");
            }
        }
        public int Selected_skill_min_score
        {
            get
            {
                return selected_skill_min_score;
            }
            set
            {
                selected_skill_min_score = value;
                OnPropertyChanged("Selected_skill_min_score");
            }
        }
        public int Selected_skill_max_score
        {
            get
            {
                return selected_skill_max_score;
            }
            set
            {
                selected_skill_max_score = value;
                OnPropertyChanged("Selected_skill_max_score");
            }
        }
        public int Selected_skill_race_bonus
        {
            get
            {
                return selected_skill_race_bonus;
            }
            set
            {
                selected_skill_race_bonus = value;
                OnPropertyChanged("Selected_skill_race_bonus");
            }
        }
        public int Selected_skill_range_limit
        {
            get
            {
                return selected_skill_range_limit;
            }
            set
            {
                selected_skill_range_limit = value;
                OnPropertyChanged("Selected_skill_range_limit");
            }
        }
        public int Selected_skill_age_limit
        {
            get
            {
                return selected_skill_age_limit;
            }
            set
            {
                selected_skill_age_limit = value;
                OnPropertyChanged("Selected_skill_age_limit");
            }
        }
        public int Selected_skill_cost
        {
            get
            {
                return selected_skill_cost;
            }
            set
            {
                selected_skill_cost = value;
                OnPropertyChanged("Selected_skill_cost");
            }
        }
        public int Test
        {
            get { //test = Character.GetInstance().Swimming.Get_score(); 
                  return test; }
            set { test = value; OnPropertyChanged("Test"); }
        }
    }
}
