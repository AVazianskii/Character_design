using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Range_libs;
using System.Windows;
using System.Windows.Media;

namespace Character_design
{
    internal class Character_info_ViewModel : BaseViewModel
    {
        private static Character_info_ViewModel _instance;

        private string character_name;
        private string character_race_name;
        private string character_age_status;
        private int character_exp,
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

        private int help_text_fontsize,
                    usual_text_fontsize,
                    current_age_text_fontsize;

        private string character_age;
        private string _character_exp;
        private string character_range_description;
        private string character_age_status_description;

        private Range_Class character_current_range;

        private SolidColorBrush age_text_color;

        private Color Help_text_color;
        private Color Usual_text_color;



        public int Current_age_text_fontsize
        {
            get { return current_age_text_fontsize; }
            set { current_age_text_fontsize = value; OnPropertyChanged("Current_age_text_fontsize"); }
        }
        public SolidColorBrush Age_text_color
        {
            get { return age_text_color; }
            set { age_text_color = value; OnPropertyChanged("Age_text_color"); }
        }
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
                Character_range_description = Character.GetInstance().Range.Get_range_descr();
                OnPropertyChanged("Character_current_range"); 
            }
        }
        public string Question_sign
        {
            get { return $@"{Directory.GetCurrentDirectory()}\Pictures\Common\Tool_tip.png"; }
        }
        public string Female_sign
        {
            get { return $@"{Directory.GetCurrentDirectory()}\Pictures\Common\female.jpg"; }
        }
        public string Male_sign
        {
            get { return $@"{Directory.GetCurrentDirectory()}\Pictures\Common\male.jpg"; }
        }
        public string Character_range_description
        {
            get { return character_range_description; }
            set { character_range_description = value; OnPropertyChanged("Character_range_description"); }
        }
        public string Character_age_status_description
        {
            get { return character_age_status_description; }
            set { character_age_status_description = value; OnPropertyChanged("Character_age_status_description"); }
        }
        public string Character_name
        {
            get { return character_name; }
            set { character_name = value; OnPropertyChanged("Character_name"); }
        }
        public string Character_race_name
        {
            get { character_race_name = Character.GetInstance().Character_race.Race_name; return character_race_name; }
            set 
            { 
                character_race_name = value;
                OnPropertyChanged("Character_race_name"); 
            }
        }
        public string Character_age
        {
            get 
            {
                return character_age; 
            }
            set 
            { 
                character_age = value; 
                if(Int32.TryParse(character_age, out int result))
                {
                    Character.GetInstance().Age = result;
                    Current_age_text_fontsize = usual_text_fontsize;
                    Age_text_color.Color = Usual_text_color;
                }
                else
                {
                    if (character_age == string.Empty)
                    {
                        character_age = Default_Character_age_text(Character.GetInstance().Age_status,
                                                                   Character.GetInstance().Character_race);
                        Current_age_text_fontsize = help_text_fontsize;
                        Age_text_color.Color = Help_text_color;
                    }
                    else
                    {
                        MessageBox.Show("Ошибка, пробуй еще раз", "Error", MessageBoxButton.OK);
                        character_age = Default_Character_age_text(Character.GetInstance().Age_status,
                                                                   Character.GetInstance().Character_race);
                        Current_age_text_fontsize = help_text_fontsize;
                        Age_text_color.Color = Help_text_color;
                    }
                }
                OnPropertyChanged("Character_age"); 
            }
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

            Help_text_color = Colors.Gray;
            Usual_text_color = Colors.Black;

            Age_text_color = new SolidColorBrush();

            help_text_fontsize = 10;
            usual_text_fontsize = 14;

            Current_age_text_fontsize = help_text_fontsize;
            Age_text_color.Color = Help_text_color;

        }

        private string Default_Character_age_text (Age_status_libs.Age_status_class character_age_status, Races_libs.Race_class character_race)
        {
            string local_text = "";
            switch (0) //character_age_status.Get_age_status_code()
            {
                case 0: local_text = $"Введите число от {character_race.Get_min_child_age()} до {character_race.Get_max_child_age()}"; break;
                case 1: local_text = $"от {character_race.Get_min_child_age()} до {character_race.Get_max_child_age()}"; break;
                case 2: local_text = $"от {character_race.Get_min_teen_age()} до {character_race.Get_max_teen_age()}"; break;
                case 3: local_text = $"от 7 до 8"; break;
                case 4: local_text = $"от 9 до 10"; break;
                case 5: local_text = $"от 11 до 12"; break;
            }
            return local_text;
        }
    }
}
