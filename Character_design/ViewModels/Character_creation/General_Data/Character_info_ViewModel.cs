using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Range_libs;

namespace Character_design
{
    internal class Character_info_ViewModel : BaseViewModel
    {
        private static Character_info_ViewModel _instance;

        private string character_name;
        private string character_race_name;
        private string character_age_status;
        private int character_age,
                    character_exp,
                    character_atr,
                    min_character_strength,
                    max_character_strength,
                    min_character_agility,
                    max_character_agility,
                    min_character_stamina,
                    max_character_stamina,
                    min_character_quickness,
                    max_character_quickness,
                    min_character_perception,
                    max_character_perception,
                    min_character_intelligence,
                    max_character_intelligence,
                    min_character_charm,
                    max_character_charm,
                    min_character_willpower,
                    max_character_willpower;
        private int character_strength,
                    character_agility,
                    character_stamina,
                    character_quickness,
                    character_perception,
                    character_intelligence,
                    character_charm,
                    character_willpower;

        private string _character_exp;

        private Range_Class character_current_range;


        public List<Range_Class> Character_ranges
        {
            get { return Main_model.GetInstance().Range_Manager.Ranges(); }
        }
        public Range_Class Character_current_range
        {
            get { return character_current_range; }
            set 
            { 
                character_current_range = value;
                Character.GetInstance().Range = character_current_range;
                OnPropertyChanged("Character_current_range"); 
            }
        }
        public string Character_name
        {
            get { return character_name; }
            set { character_name = value; OnPropertyChanged("Character_name"); }
        }
        public string Character_race_name
        {
            get { character_race_name = Character.GetInstance().Character_race.Race_name; return character_race_name; }
            set { character_race_name = value; OnPropertyChanged("Character_race_name"); }
        }
        public int Character_age
        {
            get { return character_age; }
            set { character_age = value; OnPropertyChanged("Character_age"); }
        }
        public string Character_age_status
        {
            get { return character_age_status; }
            set { character_age_status = value; OnPropertyChanged("Character_age_status"); }
        }
        public int Character_exp
        {
            get { return character_exp; }
            set { character_exp = value; OnPropertyChanged("Character_exp"); }
        }
        public int Character_atr
        {
            get { return character_atr; }
            set { character_atr = value; OnPropertyChanged("Character_atr"); }
        }
        public int Min_character_strength
        {
            get { return min_character_strength; }
            set { min_character_strength = value; OnPropertyChanged("Min_character_strength"); }
        }
        public int Max_character_strength
        {
            get { return max_character_strength; }
            set { max_character_strength = value; OnPropertyChanged("Max_character_strength"); }
        }
        public int Character_strength
        {
            get { return character_strength; }
            set { character_strength = value; OnPropertyChanged("Character_strength"); }
        }
        public int Min_character_agility
        {
            get { return min_character_agility; }
            set { min_character_agility = value; OnPropertyChanged("Min_character_agility"); }
        }
        public int Max_character_agility
        {
            get { return max_character_agility; }
            set { max_character_agility = value; OnPropertyChanged("Max_character_agility"); }
        }
        public int Character_agility
        {
            get { return character_agility; }
            set { character_agility = value; OnPropertyChanged("Character_agility"); }
        }
        public int Min_character_stamina
        {
            get { return min_character_stamina; }
            set { min_character_stamina = value; OnPropertyChanged("Min_character_stamina"); }
        }
        public int Max_character_stamina
        {
            get { return max_character_stamina; }
            set { max_character_stamina = value; OnPropertyChanged("Max_character_stamina"); }
        }
        public int Character_stamina
        {
            get { return character_stamina; }
            set { character_stamina = value; OnPropertyChanged("Character_stamina"); }
        }
        public int Min_character_quickness
        {
            get { return min_character_quickness; }
            set { min_character_quickness = value; OnPropertyChanged("Min_character_quickness"); }
        }
        public int Max_character_quickness
        {
            get { return max_character_quickness; }
            set { max_character_quickness = value; OnPropertyChanged("Max_character_quickness"); }
        }
        public int Character_quickness
        {
            get { return character_quickness; }
            set { character_quickness = value; OnPropertyChanged("Character_quickness"); }
        }
        public int Min_character_perception
        {
            get { return min_character_perception; }
            set { min_character_perception = value; OnPropertyChanged("Min_character_perception"); }
        }
        public int Max_character_perception
        {
            get { return max_character_perception; }
            set { max_character_perception = value; OnPropertyChanged("Max_character_perception"); }
        }
        public int Character_perception
        {
            get { return character_perception; }
            set { character_perception = value; OnPropertyChanged("Character_perception"); }
        }
        public int Min_character_intelligence
        {
            get { return min_character_intelligence; }
            set { min_character_intelligence = value; OnPropertyChanged("Min_character_intelligence"); }
        }
        public int Max_character_intelligence
        {
            get { return max_character_intelligence; }
            set { max_character_intelligence = value; OnPropertyChanged("Max_character_intelligence"); }
        }
        public int Character_intelligence
        {
            get { return character_intelligence; }
            set { character_intelligence = value; OnPropertyChanged("Character_intelligence"); }
        }
        public int Min_character_charm
        {
            get { return min_character_charm; }
            set { min_character_charm = value; OnPropertyChanged("Min_character_charm"); }
        }
        public int Max_character_charm
        {
            get { return max_character_charm; }
            set { max_character_charm = value; OnPropertyChanged("Max_character_charm"); }
        }
        public int Character_charm
        {
            get { return character_charm; }
            set { character_charm = value; OnPropertyChanged("Character_charm"); }
        }
        public int Min_character_willpower
        {
            get { return min_character_willpower; }
            set { min_character_willpower = value; OnPropertyChanged("Min_character_willpower"); }
        }
        public int Max_character_willpower
        {
            get { return max_character_willpower; }
            set { max_character_willpower = value; OnPropertyChanged("Max_character_willpower"); }
        }
        public int Character_willpower
        {
            get { return character_willpower; }
            set { character_willpower = value; OnPropertyChanged("Character_willpower"); }
        }
        public int Child_min_age
        {
            get { return Character.GetInstance().Character_race.Get_min_child_age(); }
        }
        public int Child_max_age
        {
            get { return Character.GetInstance().Character_race.Get_max_child_age(); }
        }
        public int Teen_min_age
        {
            get { return Character.GetInstance().Character_race.Get_min_teen_age(); }
        }
        public int Teen_max_age
        {
            get { return Character.GetInstance().Character_race.Get_max_teen_age(); }
        }
        public int Adult_min_age
        {
            get { return Character.GetInstance().Character_race.Get_min_adult_age(); }
        }
        public int Adult_max_age
        {
            get { return Character.GetInstance().Character_race.Get_max_adult_age(); }
        }
        public int Middle_min_age
        {
            get { return Character.GetInstance().Character_race.Get_min_middle_age(); }
        }
        public int Middle_max_age
        {
            get { return Character.GetInstance().Character_race.Get_max_middle_age(); }
        }
        public int Old_min_age
        {
            get { return Character.GetInstance().Character_race.Get_min_old_age(); }
        }
        public int Old_max_age
        {
            get { return Character.GetInstance().Character_race.Get_max_old_age(); }
        }
        public int Eldery_min_age
        {
            get { return Character.GetInstance().Character_race.Get_min_eldery_age(); }
        }
        public string _Character_exp
        {
            get { return _character_exp; }
            set { _character_exp = value; OnPropertyChanged("_Character_exp"); }
        }


        public static Character_info_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Character_info_ViewModel();
            }
            return _instance;
        }


        private Character_info_ViewModel()
        {
            Character_name = "Дарт Сидиус";
            _Character_exp = "Сколько назначил Мастер?";
            Character_current_range = Character_ranges[0];
        }
    }
}
