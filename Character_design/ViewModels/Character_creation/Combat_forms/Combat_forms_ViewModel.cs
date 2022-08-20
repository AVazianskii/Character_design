using System;
using System.Collections.Generic;
using SW_Character_creation;
using System.IO;

namespace Character_design
{
    internal class Combat_forms_ViewModel : BaseViewModel
    {
        private static Combat_forms_ViewModel _instance;
        private Abilities_sequence_template current_combat_sequnces;
        private Abilities_sequence_template current_combat_sequnce;


        public int Exp_points_left
        {
            get { return Character.GetInstance().Experience_left; }
        }
        public List<Abilities_sequence_template> Current_combat_sequnces
        {
            get { return Main_model.GetInstance().Combat_ability_Manager.Get_sequences(); }
            
        }
        public Abilities_sequence_template Current_combat_sequnce
        {
            get { return current_combat_sequnce; }
            set
            {
                current_combat_sequnce = value;
                OnPropertyChanged("Selected_force_skill");
                if (current_combat_sequnce != null)
                {
                    OnPropertyChanged("Selected_force_skill_title");
                    OnPropertyChanged("Selected_force_skill_description");
                    OnPropertyChanged("Selected_force_skill_cost");
                    OnPropertyChanged("Selected_force_skill_counter");
                    OnPropertyChanged("Selected_force_skill_min_score");
                    OnPropertyChanged("Selected_force_skill_max_score");
                    OnPropertyChanged("Force_skill_img_path");
                    OnPropertyChanged("Skill_base_text");
                }
            }
        }



        public static Combat_forms_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Combat_forms_ViewModel();
            }
            return _instance;
        }


        private Combat_forms_ViewModel()
        {

        }
    }
}
