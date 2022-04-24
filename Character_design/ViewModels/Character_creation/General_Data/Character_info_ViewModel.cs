using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Range_libs;
using Age_status_libs;
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

        private int help_text_fontsize,
                    usual_text_fontsize,
                    current_age_text_fontsize,
                    current_exp_points_fontsize,
                    current_atr_points_fontsize;

        private string character_age;
        private string character_exp;
        private string character_atr;
        private string character_range_description;
        private string character_age_status_description;
        private string default_question;

        private Range_Class character_current_range;

        private Age_status_class character_current_age_status;

        private SolidColorBrush age_text_color;
        private SolidColorBrush exp_text_color;
        private SolidColorBrush atr_text_color;

        private Color Help_text_color;
        private Color Usual_text_color;



        public int Current_age_text_fontsize
        {
            get { return current_age_text_fontsize; }
            set { current_age_text_fontsize = value; OnPropertyChanged("Current_age_text_fontsize"); }
        }
        public int Current_exp_points_fontsize
        {
            get { return current_exp_points_fontsize; }
            set { current_exp_points_fontsize = value; OnPropertyChanged("Current_exp_points_fontsize"); }
        }
        public int Current_atr_points_fontsize
        {
            get { return current_atr_points_fontsize; }
            set { current_atr_points_fontsize = value; OnPropertyChanged("Current_atr_points_fontsize"); }
        }
        public SolidColorBrush Atr_text_color
        {
            get { return atr_text_color; }
            set { atr_text_color = value; OnPropertyChanged("Atr_text_color"); }
        }
        public SolidColorBrush Exp_text_color
        {
            get { return exp_text_color; }
            set { exp_text_color = value; OnPropertyChanged("Exp_text_color"); }
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

                Skill_ViewModel.GetInstance().Refresh_fields();

                Refresh_atr_fields();
            }
        }
        public List<Age_status_class> Character_ages
        {
            get{ return Main_model.GetInstance().Age_status_Manager.Age_Statuses(); }
        }
        public Age_status_class Character_current_age_status
        {
            get { return character_current_age_status; }
            set 
            {
                UnApply_age_status_atr_bonus(Character.GetInstance()); // Снимаем бонусы от возратсного статуса при смене этого статуса

                character_current_age_status = value;
                Character.GetInstance().Age_status = character_current_age_status;
                Character_age_status_description = Character.GetInstance().Age_status.Get_age_status_descr();
                OnPropertyChanged("Character_age");
                OnPropertyChanged("Character_current_age_status");

                Skill_ViewModel.GetInstance().Refresh_fields();

                Apply_age_status_atr_bonus(Character.GetInstance()); // Устанавливаем бонусы от возратсного статуса при смене этого статуса
                Refresh_atr_fields();
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
                OnPropertyChanged("Character_age");
                OnPropertyChanged("Character_race_name");

                Refresh_atr_fields();

            }
        }
        public string Character_age
        {
            get 
            {
                Current_age_text_fontsize = usual_text_fontsize;
                Age_text_color.Color = Usual_text_color;
                if (!(Int32.TryParse(character_age, out int result)))
                {
                    character_age = Default_Character_age_text(Character.GetInstance().Age_status,
                                                               Character.GetInstance().Character_race);
                    Current_age_text_fontsize = help_text_fontsize;
                    Age_text_color.Color = Help_text_color;
                }
                return character_age; 
            }
            set 
            {
                if (Int32.TryParse(value, out int result))
                {
                    if (Check_age_limits(Convert.ToInt32(value), Character.GetInstance().Age_status, Character.GetInstance().Character_race))
                    {
                        character_age = value;
                        Character.GetInstance().Age = result;
                        Current_age_text_fontsize = usual_text_fontsize;
                        Age_text_color.Color = Usual_text_color;
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
                else if (value == "")
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
                OnPropertyChanged("Character_age"); 
            }
        }
        public string Character_age_status
        {
            get { return character_age_status; }
            set 
            { 
                character_age_status = value;
                OnPropertyChanged("Character_age_status"); 
            }
        }
        public string Character_exp
        {
            get { return character_exp; }
            set 
            { 
                if(Int32.TryParse(value, out int result))
                {
                    character_exp = value;
                    Character.GetInstance().Experience = result;
                    Current_exp_points_fontsize = usual_text_fontsize;
                    Exp_text_color.Color = Usual_text_color;
                }
                else if (value == "")
                {
                    character_exp = default_question;
                    Current_exp_points_fontsize = help_text_fontsize;
                    Exp_text_color.Color = Help_text_color;
                }
                else 
                {
                    MessageBox.Show("Ошибка, пробуй еще раз", "Error", MessageBoxButton.OK);
                    character_exp = default_question;
                    Current_exp_points_fontsize = help_text_fontsize;
                    Exp_text_color.Color = Help_text_color;
                }
                OnPropertyChanged("Character_exp"); 
            }
        }
        public string Character_atr
        {
            get { return character_atr; }
            set 
            {
                if (Int32.TryParse(value, out int result))
                {
                    character_atr = value;
                    Character.GetInstance().Attributes = result;
                    Current_atr_points_fontsize = usual_text_fontsize;
                    Atr_text_color.Color = Usual_text_color;
                }
                else if (value == "")
                {
                    character_atr = default_question;
                    Current_atr_points_fontsize = help_text_fontsize;
                    Atr_text_color.Color = Help_text_color;
                }
                else
                {
                    MessageBox.Show("Ошибка, пробуй еще раз", "Error", MessageBoxButton.OK);
                    character_atr = default_question;
                    Current_atr_points_fontsize = help_text_fontsize;
                    Atr_text_color.Color = Help_text_color;
                }
                OnPropertyChanged("Character_atr"); 
            }
        }
        public int Character_strength
        {
            get { return Character.GetInstance().Strength.Get_atribute_score(); }
        }
        public int Min_character_strength
        {
            get { return Return_atr_min(Character.GetInstance().Character_race.Get_race_bonus_strength(), Character.GetInstance().Age_status.Get_age_status_strength_bonus()); }
        }
        public int Max_character_strength
        {
            get 
            { 
                return Character.GetInstance().Character_race.Get_race_bonus_strength() + Return_atr_limit(Character.GetInstance().Age_status.Age_status_strength_limit,
                                                                                                           Character.GetInstance().Range.Strength_limit); 
            }
        }
        public int Character_agility
        {
            get { return Character.GetInstance().Agility.Get_atribute_score(); }
        }
        public int Min_character_agility
        {
            get { return Return_atr_min(Character.GetInstance().Character_race.Get_race_bonus_agility(), Character.GetInstance().Age_status.Get_age_status_agility_bonus()); }
        }
        public int Max_character_agility
        {
            get 
            { 
                return Character.GetInstance().Character_race.Get_race_bonus_agility() + Return_atr_limit(Character.GetInstance().Age_status.Age_status_agility_limit,
                                                                                                          Character.GetInstance().Range.Agility_limit);
            }
        }
        public int Character_stamina
        {
            get { return Character.GetInstance().Stamina.Get_atribute_score(); }
        }
        public int Min_character_stamina
        {
            get { return Return_atr_min(Character.GetInstance().Character_race.Get_race_bonus_stamina(), Character.GetInstance().Age_status.Get_age_status_stamina_bonus()); }
        }
        public int Max_character_stamina
        {
            get
            {
                return Character.GetInstance().Character_race.Get_race_bonus_stamina() + Return_atr_limit(Character.GetInstance().Age_status.Age_status_stamina_limit,
                                                                                                          Character.GetInstance().Range.Stamina_limit);
            }
        }
        public int Character_quickness
        {
            get { return Character.GetInstance().Quickness.Get_atribute_score(); }
        }
        public int Min_character_quickness
        {
            get { return Return_atr_min(Character.GetInstance().Character_race.Get_race_bonus_quickness(), Character.GetInstance().Age_status.Get_age_status_quickness_bonus()); }
        }
        public int Max_character_quickness
        {
            get
            {
                return Character.GetInstance().Character_race.Get_race_bonus_quickness() + Return_atr_limit(Character.GetInstance().Age_status.Age_status_quickness_limit,
                                                                                                            Character.GetInstance().Range.Quickness_limit);
            }
        }
        public int Character_perception
        {
            get { return Character.GetInstance().Perception.Get_atribute_score(); }
        }
        public int Min_character_perception
        {
            get { return Return_atr_min(Character.GetInstance().Character_race.Get_race_bonus_perception(), Character.GetInstance().Age_status.Get_age_status_perception_bonus()); }
        }
        public int Max_character_perception
        {
            get
            {
                return Character.GetInstance().Character_race.Get_race_bonus_perception() + Return_atr_limit(Character.GetInstance().Age_status.Age_status_perception_limit,
                                                                                                             Character.GetInstance().Range.Perception_limit);
            }
        }
        public int Character_intelligence
        {
            get { return Character.GetInstance().Intelligence.Get_atribute_score(); }
        }
        public int Min_character_intelligence
        {
            get { return Return_atr_min(Character.GetInstance().Character_race.Get_race_bonus_intelligence(), Character.GetInstance().Age_status.Get_age_status_intelligence_bonus()); }
        }
        public int Max_character_intelligence
        {
            get
            {
                return Character.GetInstance().Character_race.Get_race_bonus_intelligence() + Return_atr_limit(Character.GetInstance().Age_status.Age_status_intelligence_limit,
                                                                                                              Character.GetInstance().Range.Intelligence_limit);
            }
        }
        public int Character_charm
        {
            get { return Character.GetInstance().Charm.Get_atribute_score(); }
        }
        public int Min_character_charm
        {
            get { return Return_atr_min(Character.GetInstance().Character_race.Get_race_bonus_charm(), Character.GetInstance().Age_status.Get_age_status_charm_bonus()); }
        }
        public int Max_character_charm
        {
            get
            {
                return Character.GetInstance().Character_race.Get_race_bonus_charm() + Return_atr_limit(Character.GetInstance().Age_status.Age_status_charm_limit,
                                                                                                        Character.GetInstance().Range.Charm_limit);
            }
        }
        public int Character_willpower
        {
            get { return Character.GetInstance().Willpower.Get_atribute_score(); }
        }
        public int Min_character_willpower
        {
            get { return Return_atr_min(Character.GetInstance().Character_race.Get_race_bonus_willpower(), Character.GetInstance().Age_status.Get_age_status_willpower_bonus()); }
        }
        public int Max_character_willpower
        {
            get
            {
                return Character.GetInstance().Character_race.Get_race_bonus_willpower() + Return_atr_limit(Character.GetInstance().Age_status.Age_status_willpower_limit,
                                                                                                            Character.GetInstance().Range.Willpower_limit);
            }
        }
        public string Strength_description
        {
            get { return Character.GetInstance().Strength.Get_description(); }
        }
        public string Agility_description
        {
            get { return Character.GetInstance().Agility.Get_description(); }
        }
        public string Stamina_description
        {
            get { return Character.GetInstance().Stamina.Get_description(); }
        }
        public string Quickness_description
        {
            get { return Character.GetInstance().Quickness.Get_description(); }
        }
        public string Perception_description
        {
            get { return Character.GetInstance().Perception.Get_description(); }
        }
        public string Intelligence_description
        {
            get { return Character.GetInstance().Intelligence.Get_description(); }
        }
        public string Charm_description
        {
            get { return Character.GetInstance().Charm.Get_description(); }
        }
        public string Willpower_description
        {
            get { return Character.GetInstance().Willpower.Get_description(); }
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
            default_question = "Сколько назначил Мастер?";

            Character_current_range = Character_ranges[0];
            Character_current_age_status = Character_ages[0];

            Help_text_color  = Colors.Gray;
            Usual_text_color = Colors.Black;

            Age_text_color = new SolidColorBrush();
            Exp_text_color = new SolidColorBrush();
            Atr_text_color = new SolidColorBrush();

            help_text_fontsize = 10;
            usual_text_fontsize = 14;

            Current_age_text_fontsize = help_text_fontsize;
            Current_exp_points_fontsize = help_text_fontsize;
            Current_atr_points_fontsize = help_text_fontsize;
            Age_text_color.Color = Help_text_color;
            Exp_text_color.Color = Help_text_color;
            Atr_text_color.Color = Help_text_color;

            Character_age = "";
            Character_exp = "";
            Character_atr = "";
        }



        private string Default_Character_age_text (Age_status_libs.Age_status_class character_age_status, Races_libs.Race_class character_race)
        {
            string local_text = "";
            switch (character_age_status.Get_age_status_code()) //character_age_status.Get_age_status_code()
            {
                case 0: local_text = $"Возрастной статус не выбран!"; break;
                case 1: local_text = $"Введите число от {character_race.Get_min_child_age()} до {character_race.Get_max_child_age()}"; break;
                case 2: local_text = $"Введите число от {character_race.Get_min_teen_age()} до {character_race.Get_max_teen_age()}"; break;
                case 3: local_text = $"Введите число от {character_race.Get_min_adult_age()} до {character_race.Get_max_adult_age()}"; break;
                case 4: local_text = $"Введите число от {character_race.Get_min_middle_age()} до {character_race.Get_max_middle_age()}"; break;
                case 5: local_text = $"Введите число от {character_race.Get_min_old_age()} до {character_race.Get_max_old_age()}"; break;
                case 6: local_text = $"Введите число от {character_race.Get_min_eldery_age()} "; break;
            }
            return local_text;
        }

        private bool Check_age_limits(int input_age, Age_status_libs.Age_status_class character_age_status, Races_libs.Race_class character_race)
        {
            bool limits_comfirmed = false;
            switch (character_age_status.Get_age_status_code()) //character_age_status.Get_age_status_code()
            {
                case 0: limits_comfirmed = false; break;
                case 1: 
                    if (input_age >= character_race.Get_min_child_age())
                    {
                        if (input_age <= character_race.Get_max_child_age()) { limits_comfirmed = true; }
                        else { limits_comfirmed = false; }
                    }
                    else { limits_comfirmed = false; }
                    break;
                case 2:
                    if (input_age >= character_race.Get_min_teen_age())
                    {
                        if (input_age <= character_race.Get_max_teen_age()) { limits_comfirmed = true; }
                        else { limits_comfirmed = false; }
                    }
                    else { limits_comfirmed = false; }
                    break;
                case 3:
                    if (input_age >= character_race.Get_min_adult_age())
                    {
                        if (input_age <= character_race.Get_max_adult_age()) { limits_comfirmed = true; }
                        else { limits_comfirmed = false; }
                    }
                    else { limits_comfirmed = false; }
                    break;
                case 4:
                    if (input_age >= character_race.Get_min_middle_age())
                    {
                        if (input_age <= character_race.Get_max_middle_age()) { limits_comfirmed = true; }
                        else { limits_comfirmed = false; }
                    }
                    else { limits_comfirmed = false; }
                    break;
                case 5:
                    if (input_age >= character_race.Get_min_old_age())
                    {
                        if (input_age <= character_race.Get_max_old_age()) { limits_comfirmed = true; }
                        else { limits_comfirmed = false; }
                    }
                    else { limits_comfirmed = false; }
                    break;
                case 6:
                    if (input_age >= character_race.Get_min_eldery_age()) { limits_comfirmed = true; }
                    else { limits_comfirmed = false; }
                    break;
            }
            return limits_comfirmed;
        }

        private int Return_atr_limit(int age_status_limit, int range_limit)
        {
            int result = 0;
            if (age_status_limit <= range_limit)
            {
                result = age_status_limit;
            }
            else { result = range_limit; }
            return result;
        }
        private void Refresh_atr_fields()
        {
            OnPropertyChanged("Max_character_strength");
            OnPropertyChanged("Min_character_strength");
            OnPropertyChanged("Character_strength");
            OnPropertyChanged("Max_character_agility");
            OnPropertyChanged("Min_character_agility");
            OnPropertyChanged("Character_agility");
            OnPropertyChanged("Max_character_stamina");
            OnPropertyChanged("Min_character_stamina");
            OnPropertyChanged("Character_stamina");
            OnPropertyChanged("Max_character_quickness");
            OnPropertyChanged("Min_character_quickness");
            OnPropertyChanged("Character_quickness");
            OnPropertyChanged("Max_character_perception");
            OnPropertyChanged("Min_character_perception");
            OnPropertyChanged("Character_perception");
            OnPropertyChanged("Max_character_intelligence");
            OnPropertyChanged("Min_character_intelligence");
            OnPropertyChanged("Character_intelligence");
            OnPropertyChanged("Max_character_charm");
            OnPropertyChanged("Min_character_charm");
            OnPropertyChanged("Character_charm");
            OnPropertyChanged("Max_character_willpower");
            OnPropertyChanged("Min_character_willpower");
            OnPropertyChanged("Character_willpower");
        }
        private void Apply_age_status_atr_bonus(Character character)
        {
            character.Strength.Increase_atr     (character.Age_status.Get_age_status_strength_bonus());
            character.Agility.Increase_atr      (character.Age_status.Get_age_status_agility_bonus());
            character.Stamina.Increase_atr      (character.Age_status.Get_age_status_stamina_bonus());
            character.Perception.Increase_atr   (character.Age_status.Get_age_status_perception_bonus());
            character.Quickness.Increase_atr    (character.Age_status.Get_age_status_quickness_bonus());
            character.Intelligence.Increase_atr (character.Age_status.Get_age_status_intelligence_bonus());
            character.Charm.Increase_atr        (character.Age_status.Get_age_status_charm_bonus());
            character.Willpower.Increase_atr    (character.Age_status.Get_age_status_willpower_bonus());
        }
        private void UnApply_age_status_atr_bonus(Character character)
        {
            character.Strength.Decrease_atr     (character.Age_status.Get_age_status_strength_bonus());
            character.Agility.Decrease_atr      (character.Age_status.Get_age_status_agility_bonus());
            character.Stamina.Decrease_atr      (character.Age_status.Get_age_status_stamina_bonus());
            character.Perception.Decrease_atr   (character.Age_status.Get_age_status_perception_bonus());
            character.Quickness.Decrease_atr    (character.Age_status.Get_age_status_quickness_bonus());
            character.Intelligence.Decrease_atr (character.Age_status.Get_age_status_intelligence_bonus());
            character.Charm.Decrease_atr        (character.Age_status.Get_age_status_charm_bonus());
            character.Willpower.Decrease_atr    (character.Age_status.Get_age_status_willpower_bonus());
        }
        private int Return_atr_min(int race_atr_min, int age_status_atr_min)
        {
            int result = 0;
            if(age_status_atr_min + race_atr_min < 0)
            {
                result = age_status_atr_min + race_atr_min;
            }
            else
            {
                result = 0;
            }
            return result;
        }
    }
}
