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
                    current_atr_points_fontsize,
                    karma_opacity,
                    current_character_karma,
                    current_karma_points_fontsize,
                    jedi_karma,
                    sith_karma;

        private string character_age;
        private string character_exp;
        private string character_atr;
        private string character_range_description;
        private string character_age_status_description;
        private string default_question;
        private string default_text;
        private string character_karma;

        private Range_Class character_current_range;

        private Age_status_class character_current_age_status;

        private SolidColorBrush age_text_color;
        private SolidColorBrush exp_text_color;
        private SolidColorBrush atr_text_color;
        private SolidColorBrush forceuser_border_color;
        private SolidColorBrush male_sign_border_color;
        private SolidColorBrush female_sign_border_color;
        private SolidColorBrush current_karma_color;
        private SolidColorBrush karma_text_color;

        private Color Help_text_color;
        private Color Usual_text_color;
        private Color Chosen_color;
        private Color Unchosen_color;
        private Color Sith_color;
        private Color Jedi_color;
        private Color Neutral_color;



        public Character_changing_command Choose_male { get; private set; }
        public Character_changing_command Choose_female { get; private set; }
        public Character_changing_command Manage_forceuser { get; private set; }
        public Character_changing_command Increase_strength { get; private set; }
        public Character_changing_command Decrease_strength { get; private set; }
        public Character_changing_command Increase_agility { get; private set; }
        public Character_changing_command Decrease_agility { get; private set; }
        public Character_changing_command Increase_stamina { get; private set; }
        public Character_changing_command Decrease_stamina { get; private set; }
        public Character_changing_command Increase_perception { get; private set; }
        public Character_changing_command Decrease_perception { get; private set; }
        public Character_changing_command Increase_quickness { get; private set; }
        public Character_changing_command Decrease_quickness { get; private set; }
        public Character_changing_command Increase_intelligence { get; private set; }
        public Character_changing_command Decrease_intelligence { get; private set; }
        public Character_changing_command Increase_charm { get; private set; }
        public Character_changing_command Decrease_charm { get; private set; }
        public Character_changing_command Increase_willpower { get; private set; }
        public Character_changing_command Decrease_willpower { get; private set; }
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
        public int Current_karma_points_fontsize
        {
            get { return current_karma_points_fontsize; }
            set { current_karma_points_fontsize = value; OnPropertyChanged("Current_karma_points_fontsize"); }
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
        public SolidColorBrush Forceuser_border_color
        {
            get { return forceuser_border_color; }
            set { forceuser_border_color = value; OnPropertyChanged("Forceuser_border_color"); }
        }
        public SolidColorBrush Male_sign_border_color
        {
            get { return male_sign_border_color; }
            set { male_sign_border_color = value; OnPropertyChanged("Male_sign_border_color"); }
        }
        public SolidColorBrush Female_sign_border_color
        {
            get { return female_sign_border_color; }
            set { female_sign_border_color = value; OnPropertyChanged("Female_sign_border_color"); }
        }
        public SolidColorBrush Current_karma_color
        {
            get { return current_karma_color; }
            set { current_karma_color = value; OnPropertyChanged("Current_karma_color"); }
        }
        public SolidColorBrush Karma_text_color
        {
            get { return karma_text_color; }
            set { karma_text_color = value; OnPropertyChanged("Karma_text_color"); }
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
                Character.GetInstance().Refresh_fields();
                Character_skills_ViewModel.GetInstance().Refresh_fields();

                Refresh_atr_fields();
                Character.GetInstance().Change_character_state_to_unsave();
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
                Character.GetInstance().Refresh_fields();
                Character_skills_ViewModel.GetInstance().Refresh_fields();

                Apply_age_status_atr_bonus(Character.GetInstance()); // Устанавливаем бонусы от возратсного статуса при смене этого статуса
                Refresh_atr_fields();
                Character.GetInstance().Change_character_state_to_unsave();
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
        public string Sith_sign
        {
            get { return $@"{Directory.GetCurrentDirectory()}\Pictures\Common\sith.jpg"; }
        }
        public string Jedi_sign
        {
            get { return $@"{Directory.GetCurrentDirectory()}\Pictures\Common\jedi.jpg"; }
        }
        public string Reaction_sign
        {
            get { return $@"{Directory.GetCurrentDirectory()}\Pictures\Common\reaction.png"; }
        }
        public string Armor_sign
        {
            get { return $@"{Directory.GetCurrentDirectory()}\Pictures\Common\armor.png"; }
        }
        public string Watchfulness_sign
        {
            get { return $@"{Directory.GetCurrentDirectory()}\Pictures\Common\watchfullness.png"; }
        }
        public string Stealth_sign
        {
            get { return $@"{Directory.GetCurrentDirectory()}\Pictures\Common\stealth.png"; }
        }
        public string Force_resistance_sign
        {
            get { return $@"{Directory.GetCurrentDirectory()}\Pictures\Common\force_resistance.png"; }
        }
        public string Concentration_sign
        {
            get { return $@"{Directory.GetCurrentDirectory()}\Pictures\Common\concentration.png"; }
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
            set 
            { 
                character_name = value; 
                OnPropertyChanged("Character_name");

                Character.GetInstance().Change_character_state_to_unsave();
            }
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

                Character.GetInstance().Change_character_state_to_unsave();
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

                Character.GetInstance().Change_character_state_to_unsave();
            }
        }
        public string Character_age_status
        {
            get { return character_age_status; }
            set 
            { 
                character_age_status = value;
                OnPropertyChanged("Character_age_status");

                Character.GetInstance().Change_character_state_to_unsave();
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
                OnPropertyChanged("Exp_points_left");
                OnPropertyChanged("Exp_points_for_atr");

                Character.GetInstance().Change_character_state_to_unsave();
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
                OnPropertyChanged("Atr_points_for_atr");
                OnPropertyChanged("Atr_points_left");
                OnPropertyChanged("Character_atr");

                Character.GetInstance().Change_character_state_to_unsave();
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
                return Return_atr_max(Character.GetInstance().Character_race.Get_race_bonus_strength(), Return_atr_limit(Character.GetInstance().Age_status.Age_status_strength_limit,
                                                                                                                         Character.GetInstance().Range.Strength_limit)); 
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
                return Return_atr_max(Character.GetInstance().Character_race.Get_race_bonus_agility(), Return_atr_limit(Character.GetInstance().Age_status.Age_status_agility_limit,
                                                                                                                        Character.GetInstance().Range.Agility_limit));
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
                return Return_atr_max(Character.GetInstance().Character_race.Get_race_bonus_stamina(), Return_atr_limit(Character.GetInstance().Age_status.Age_status_stamina_limit,
                                                                                                                        Character.GetInstance().Range.Stamina_limit));
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
                return Return_atr_max(Character.GetInstance().Character_race.Get_race_bonus_quickness(), Return_atr_limit(Character.GetInstance().Age_status.Age_status_quickness_limit,
                                                                                                                          Character.GetInstance().Range.Quickness_limit));
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
                return Return_atr_max(Character.GetInstance().Character_race.Get_race_bonus_perception(), Return_atr_limit(Character.GetInstance().Age_status.Age_status_perception_limit,
                                                                                                                           Character.GetInstance().Range.Perception_limit));
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
                return Return_atr_max(Character.GetInstance().Character_race.Get_race_bonus_intelligence(), Return_atr_limit(Character.GetInstance().Age_status.Age_status_intelligence_limit,
                                                                                                                             Character.GetInstance().Range.Intelligence_limit));
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
                return Return_atr_max(Character.GetInstance().Character_race.Get_race_bonus_charm(), Return_atr_limit(Character.GetInstance().Age_status.Age_status_charm_limit,
                                                                                                                      Character.GetInstance().Range.Charm_limit));
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
                return Return_atr_max(Character.GetInstance().Character_race.Get_race_bonus_willpower(), Return_atr_limit(Character.GetInstance().Age_status.Age_status_willpower_limit,
                                                                                                                          Character.GetInstance().Range.Willpower_limit));
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
        public string Atr_price_description
        {
            get { return "Атрибуты можно развить за 1 очко атрибутов или за 8 очков опыта"; }
        }
        public int Exp_points_left
        {
            get { return Character.GetInstance().Experience_left; }
        }
        public int Atr_points_left
        {
            get { return Character.GetInstance().Attributes_left; }
        }
        public int Exp_points_for_atr
        {
            get
            {
                int res = 0;
                if (Int32.TryParse(Character_exp, out int result))
                {
                    res = result;
                }
                return res;
            }
        }
        public int Atr_points_for_atr
        {
            get
            {
                int res = 0;
                if (Int32.TryParse(Character_atr, out int result))
                {
                    res = result;
                }
                return res;
            }
        }
        public string Forceuser_description
        {
            get { return "Параметр показывает, обладает ли персонаж Великой Силой и умеет ли ее применять"; }
        }
        public int Karma_opacity
        {
            get { return karma_opacity; }
            set { karma_opacity = value; OnPropertyChanged("Karma_opacity"); }
        }
        public int Minimum_karma
        {
            get { return -50; }
        }
        public int Maximum_karma
        {
            get { return 50; }
        }
        public int Current_character_karma
        {
            get { return current_character_karma; }
            set 
            { 
                current_character_karma = value;
                Check_karma_range(current_character_karma, Current_karma_color, Neutral_color, Sith_color, Jedi_color);
                Check_forceuser_status(Character.GetInstance(), current_character_karma);
                OnPropertyChanged("Current_character_karma"); 
            }
        }
        public string Character_karma
        {
            get { return character_karma; }
            set 
            {
                if (Int32.TryParse(value, out int result))
                {
                    if ((Convert.ToInt32(value) >= Minimum_karma) && (Convert.ToInt32(value) <= Maximum_karma))
                    {
                        character_karma = value;
                        Current_character_karma = Convert.ToInt32(value);
                        Character.GetInstance().Karma = result;
                        Current_karma_points_fontsize = usual_text_fontsize;
                        Karma_text_color.Color = Usual_text_color;
                    }
                    else
                    {
                        MessageBox.Show("Ошибка, пробуй еще раз", "Error", MessageBoxButton.OK);
                        character_karma = default_text;
                        Current_karma_points_fontsize = help_text_fontsize;
                        Karma_text_color.Color = Help_text_color;
                    }
                }
                else if (value == "")
                {
                    character_karma = default_text;
                    Current_karma_points_fontsize = help_text_fontsize;
                    Karma_text_color.Color = Help_text_color;
                }
                else
                {
                    MessageBox.Show("Ошибка, пробуй еще раз", "Error", MessageBoxButton.OK);
                    character_karma = default_text;
                    Current_karma_points_fontsize = help_text_fontsize;
                    Karma_text_color.Color = Help_text_color;
                }

                OnPropertyChanged("Character_karma");

                Character.GetInstance().Change_character_state_to_unsave();
            }
        }
        public string Character_karma_description
        {
            get { return "Этот параметр показывает, к какой стороне Силы тяготеет ваш персонаж"; }
        }
        public bool Character_is_forceuser
        {
            get { return Character.GetInstance().Forceuser; }
        }
        public string Sith_description
        {
            get { return "При красном цвете шкалы вашему персонажу открываются умения Темной стороны Силы"; }
        }
        public string Jedi_description
        {
            get { return "При зеленом цвете шкалы вашему персонажу открываются умения Светлой стороны Силы"; }
        }
        public string Reaction_description
        {
            get { return "Этот суммарный параметр определяет, кто будет первым действовать в боевых сценах"; }
        }
        public string Armor_description
        {
            get { return "Этот суммарный параметр определяет, наскольно хорошо ваш персонаж противостоит получаемому урону"; }
        }
        public string Observation_description
        {
            get { return "Этот суммарный параметр определяет, насколько хорошо ваш персонаж замечает детали"; }
        }
        public string Stealth_description
        {
            get { return "Этот суммарный параметр определяет, насколько хорошо ваш персонаж скрывается"; }
        }
        public string Force_resistance_description
        {
            get { return "Этот суммарный параметр определяет, насколько хорошо ваш персонаж противостоит воздействиям Силы"; }
        }
        public string Concetration_description
        {
            get { return "Этот суммарный параметр определяет, насколько успешным будет проверка навыка 'Поток Силы'"; }
        }
        public int Character_reaction
        {
            get { return Character.GetInstance().Reaction; }
        }
        public int Character_armor
        {
            get { return Character.GetInstance().Armor; }
        }
        public int Character_watchfullness
        {
            get { return Character.GetInstance().Watchfullness; }
        }
        public int Character_hideness
        {
            get { return Character.GetInstance().Hideness; }
        }
        public int Character_force_resistance
        {
            get { return Character.GetInstance().Force_resistance; }
        }
        public int Character_concentration
        {
            get { return Character.GetInstance().Concentration; }
        }



        public static Character_info_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Character_info_ViewModel();
            }
            return _instance;
        }
        public void Refresh_atr_exp_points()
        {
            OnPropertyChanged("Atr_points_left");
            OnPropertyChanged("Exp_points_left");
        }
        public void Refresh_karma_points()
        {
            OnPropertyChanged("Character_karma");
        }
        public void Refresh_atr_score(Attribute_libs.Atribute_class attribute)
        {
            switch (attribute.Get_atribute_code())
            {
                case 1: OnPropertyChanged("Character_strength"); break;
                case 2: OnPropertyChanged("Character_agility"); break;
                case 3: OnPropertyChanged("Character_stamina"); break;
                case 4: OnPropertyChanged("Character_perception"); break;
                case 5: OnPropertyChanged("Character_quickness"); break;
                case 6: OnPropertyChanged("Character_intelligence"); break;
                case 7: OnPropertyChanged("Character_charm"); break;
                case 8: OnPropertyChanged("Character_willpower"); break;
            }
        }
        public void Clear_page_fields()
        {
            _Choose_male(Character.GetInstance());

            Forceuser_border_color.Color = Unchosen_color;

            Karma_opacity = 0;

            Character_age = "";
            Character_exp = "";
            Character_atr = "";
            Character_karma = "";

            Current_age_text_fontsize = help_text_fontsize;
            Current_exp_points_fontsize = help_text_fontsize;
            Current_atr_points_fontsize = help_text_fontsize;
            Current_karma_points_fontsize = help_text_fontsize;
            Age_text_color.Color = Help_text_color;
            Exp_text_color.Color = Help_text_color;
            Atr_text_color.Color = Help_text_color;
            Karma_text_color.Color = Help_text_color;

            Character_current_range = Character_ranges[0];
            Character_current_age_status = Character_ages[0];

            Refresh_atr_score(Character.GetInstance().Strength);
            Refresh_atr_score(Character.GetInstance().Agility);
            Refresh_atr_score(Character.GetInstance().Stamina);
            Refresh_atr_score(Character.GetInstance().Perception);
            Refresh_atr_score(Character.GetInstance().Quickness);
            Refresh_atr_score(Character.GetInstance().Intelligence);
            Refresh_atr_score(Character.GetInstance().Charm);
            Refresh_atr_score(Character.GetInstance().Willpower);
        }



        private Character_info_ViewModel()
        {
            Character_name = "Дарт Сидиус";
            default_question = "Сколько назначил Мастер?";
            default_text = $"от {Minimum_karma} до {Maximum_karma}";

            Character_current_range = Character_ranges[0];
            Character_current_age_status = Character_ages[0];

            Help_text_color     = Colors.Gray;
            Usual_text_color    = Colors.Black;
            Chosen_color        = Colors.Wheat;
            Unchosen_color      = Colors.White;
            Sith_color          = Colors.Red;
            Neutral_color       = Colors.Gray;
            Jedi_color          = Colors.Green;

            Age_text_color = new SolidColorBrush();
            Exp_text_color = new SolidColorBrush();
            Atr_text_color = new SolidColorBrush();
            Forceuser_border_color = new SolidColorBrush();
            Male_sign_border_color = new SolidColorBrush();
            Female_sign_border_color = new SolidColorBrush();
            Current_karma_color = new SolidColorBrush();
            Karma_text_color = new SolidColorBrush();

            help_text_fontsize = 10;
            usual_text_fontsize = 14;
            Karma_opacity = 0;
            jedi_karma = 10;
            sith_karma = -10;

            Current_age_text_fontsize = help_text_fontsize;
            Current_exp_points_fontsize = help_text_fontsize;
            Current_atr_points_fontsize = help_text_fontsize;
            Current_karma_points_fontsize = help_text_fontsize;
            Age_text_color.Color = Help_text_color;
            Exp_text_color.Color = Help_text_color;
            Atr_text_color.Color = Help_text_color;
            Karma_text_color.Color = Help_text_color;

            Character_age = "";
            Character_exp = "";
            Character_atr = "";
            Character_karma = "";

            Increase_strength = new Character_changing_command(o => _Increase_atr(Character.GetInstance().Strength),
                                                               o => Increase_atr_is_allowed(Character.GetInstance(), Character.GetInstance().Strength, Max_character_strength));
            Decrease_strength = new Character_changing_command(o => _Decrease_atr(Character.GetInstance().Strength),
                                                               o => Decrease_atr_is_allowed(Character.GetInstance().Strength, Min_character_strength));

            Increase_agility = new Character_changing_command(o => _Increase_atr(Character.GetInstance().Agility),
                                                              o => Increase_atr_is_allowed(Character.GetInstance(), Character.GetInstance().Agility, Max_character_agility));
            Decrease_agility = new Character_changing_command(o => _Decrease_atr(Character.GetInstance().Agility),
                                                              o => Decrease_atr_is_allowed(Character.GetInstance().Agility, Min_character_agility));

            Increase_stamina = new Character_changing_command(o => _Increase_atr(Character.GetInstance().Stamina),
                                                              o => Increase_atr_is_allowed(Character.GetInstance(), Character.GetInstance().Stamina, Max_character_stamina));
            Decrease_stamina = new Character_changing_command(o => _Decrease_atr(Character.GetInstance().Stamina),
                                                              o => Decrease_atr_is_allowed(Character.GetInstance().Stamina, Min_character_stamina));

            Increase_perception = new Character_changing_command(o => _Increase_atr(Character.GetInstance().Perception),
                                                                 o => Increase_atr_is_allowed(Character.GetInstance(), Character.GetInstance().Perception, Max_character_perception));
            Decrease_perception = new Character_changing_command(o => _Decrease_atr(Character.GetInstance().Perception),
                                                                 o => Decrease_atr_is_allowed(Character.GetInstance().Perception, Min_character_perception));

            Increase_quickness = new Character_changing_command(o => _Increase_atr(Character.GetInstance().Quickness),
                                                                o => Increase_atr_is_allowed(Character.GetInstance(), Character.GetInstance().Quickness, Max_character_quickness));
            Decrease_quickness = new Character_changing_command(o => _Decrease_atr(Character.GetInstance().Quickness),
                                                                o => Decrease_atr_is_allowed(Character.GetInstance().Quickness, Min_character_quickness));

            Increase_intelligence = new Character_changing_command(o => _Increase_atr(Character.GetInstance().Intelligence),
                                                                   o => Increase_atr_is_allowed(Character.GetInstance(), Character.GetInstance().Intelligence, Max_character_intelligence));
            Decrease_intelligence = new Character_changing_command(o => _Decrease_atr(Character.GetInstance().Intelligence),
                                                                   o => Decrease_atr_is_allowed(Character.GetInstance().Intelligence, Min_character_intelligence));

            Increase_charm = new Character_changing_command(o => _Increase_atr(Character.GetInstance().Charm),
                                                            o => Increase_atr_is_allowed(Character.GetInstance(), Character.GetInstance().Charm, Max_character_charm));
            Decrease_charm = new Character_changing_command(o => _Decrease_atr(Character.GetInstance().Charm),
                                                            o => Decrease_atr_is_allowed(Character.GetInstance().Charm, Min_character_charm));

            Increase_willpower = new Character_changing_command(o => _Increase_atr(Character.GetInstance().Willpower),
                                                                o => Increase_atr_is_allowed(Character.GetInstance(), Character.GetInstance().Willpower, Max_character_willpower));
            Decrease_willpower = new Character_changing_command(o => _Decrease_atr(Character.GetInstance().Willpower),
                                                                o => Decrease_atr_is_allowed(Character.GetInstance().Willpower, Min_character_willpower));

            Manage_forceuser = new Character_changing_command(o => _Manage_forceuser(Character.GetInstance()));

            Choose_male     = new Character_changing_command(o => _Choose_male(Character.GetInstance()));
            Choose_female   = new Character_changing_command(o => _Choose_female(Character.GetInstance()));

            _Choose_male(Character.GetInstance());
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

            character.Update_combat_parameters(character.Strength,        character.Age_status.Get_age_status_strength_bonus());
            character.Update_combat_parameters(character.Agility,         character.Age_status.Get_age_status_agility_bonus());
            character.Update_combat_parameters(character.Stamina,         character.Age_status.Get_age_status_stamina_bonus());
            character.Update_combat_parameters(character.Perception,      character.Age_status.Get_age_status_perception_bonus());
            character.Update_combat_parameters(character.Quickness,       character.Age_status.Get_age_status_quickness_bonus());
            character.Update_combat_parameters(character.Intelligence,    character.Age_status.Get_age_status_intelligence_bonus());
            character.Update_combat_parameters(character.Charm,           character.Age_status.Get_age_status_charm_bonus());
            character.Update_combat_parameters(character.Willpower,       character.Age_status.Get_age_status_willpower_bonus());

            Refresh_atr_score(character.Strength);
            Refresh_atr_score(character.Agility);
            Refresh_atr_score(character.Stamina);
            Refresh_atr_score(character.Perception);
            Refresh_atr_score(character.Quickness);
            Refresh_atr_score(character.Intelligence);
            Refresh_atr_score(character.Charm);
            Refresh_atr_score(character.Willpower);

            Refresh_combat_parameters();
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

            character.Update_combat_parameters(character.Strength,        -character.Age_status.Get_age_status_strength_bonus());
            character.Update_combat_parameters(character.Agility,         -character.Age_status.Get_age_status_agility_bonus());
            character.Update_combat_parameters(character.Stamina,         -character.Age_status.Get_age_status_stamina_bonus());
            character.Update_combat_parameters(character.Perception,      -character.Age_status.Get_age_status_perception_bonus());
            character.Update_combat_parameters(character.Quickness,       -character.Age_status.Get_age_status_quickness_bonus());
            character.Update_combat_parameters(character.Intelligence,    -character.Age_status.Get_age_status_intelligence_bonus());
            character.Update_combat_parameters(character.Charm,           -character.Age_status.Get_age_status_charm_bonus());
            character.Update_combat_parameters(character.Willpower,       -character.Age_status.Get_age_status_willpower_bonus());

            Refresh_atr_score(character.Strength);
            Refresh_atr_score(character.Agility);
            Refresh_atr_score(character.Stamina);
            Refresh_atr_score(character.Perception);
            Refresh_atr_score(character.Quickness);
            Refresh_atr_score(character.Intelligence);
            Refresh_atr_score(character.Charm);
            Refresh_atr_score(character.Willpower);

            Refresh_combat_parameters();
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
        private int Return_atr_max(int race_atr_min, int atr_limit)
        {
            int result = atr_limit;
            if (race_atr_min > 0)
            {
                result = result + race_atr_min;
            }
            return result;
        }
        private bool Increase_atr_is_allowed(Character character, Attribute_libs.Atribute_class attribute, int high_atr_limit)
        {
            bool result = false;
            if (attribute.Get_atribute_score() < high_atr_limit)
            {
                if (character.Attributes_left >= attribute.Get_attribute_cost_for_atr())
                {
                    result = true;
                }
                else if (character.Experience_left >= attribute.Get_attribute_cost_for_exp())
                {
                    result = true;
                }
            }
            return result;
        }
        private bool Decrease_atr_is_allowed(Attribute_libs.Atribute_class attribute, int low_atr_limit)
        {
            bool result = false;
            if (attribute.Get_atribute_score() > low_atr_limit)
            {
                if (attribute.Get_atr_for_atr() > 0 || attribute.Get_atr_for_exp() > 0)
                {
                    result = true;
                }
            }
            return result;
        }
        private void _Increase_atr (Attribute_libs.Atribute_class attribute)
        {
            Character.GetInstance().Increase_atr(attribute);
            Refresh_atr_exp_points();
            Refresh_atr_score(attribute);
            Character.GetInstance().Update_combat_parameters(attribute, 1);
            Refresh_combat_parameters();
        }
        private void _Decrease_atr(Attribute_libs.Atribute_class attribute)
        {
            Character.GetInstance().Decrease_atr(attribute);
            Refresh_atr_exp_points();
            Refresh_atr_score(attribute);
            Character.GetInstance().Update_combat_parameters(attribute, -1);
            Refresh_combat_parameters();
        }
        
        private void _Manage_forceuser (Character character)
        {
            if (character.Forceuser != true)
            {
                character.Forceuser = true;
                Forceuser_border_color.Color = Chosen_color;
                Skill_ViewModel.GetInstance().Refresh_fields(); // Изменяем стоимость навыков, если игрок начал делать персонажа-адепта Силы
                Show_forceuser_fields();
                OnPropertyChanged("Character_is_forceuser");
            }
            else
            {
                character.Forceuser = false;
                Forceuser_border_color.Color = Unchosen_color;
                Character.GetInstance().Refund_if_not_forceuser(); // Возвращаем очки опыта, если игрок перехотел делать персонажа-адепта Силы
                Skill_ViewModel.GetInstance().Refresh_fields(); // Изменяем стоимость навыков, если игрок перехотел делать персонажа-адепта Силы
                Unshow_forceuser_fields();
                OnPropertyChanged("Character_is_forceuser");
                OnPropertyChanged("Exp_points_left");
            }
        }
        private void _Choose_male (Character character)
        {
            character.Sex = "Мужской";
            Male_sign_border_color.Color = Chosen_color;
            Female_sign_border_color.Color = Unchosen_color;
        }
        private void _Choose_female(Character character)
        {
            character.Sex = "Женский";
            Female_sign_border_color.Color = Chosen_color;
            Male_sign_border_color.Color = Unchosen_color;
        }
        private void Show_forceuser_fields ()
        {
            Karma_opacity = 100;
            Check_karma_range(Current_character_karma, Current_karma_color, Neutral_color, Sith_color, Jedi_color);
        }
        private void Unshow_forceuser_fields()
        {
            Karma_opacity = 0;
        }
        private void Check_karma_range(int current_karma, SolidColorBrush karma_color, Color neutral, Color sith, Color jedi)
        {
            if (current_karma >= jedi_karma)
            {
                karma_color.Color = jedi;
            }
            else if (current_karma <= sith_karma)
            {
                karma_color.Color = sith;
            }
            else { karma_color.Color = neutral; }
        }
        private void Check_forceuser_status(Character character,int current_karma)
        {
            if (current_karma >= jedi_karma) 
            { 
                character.Is_jedi = true;
                character.Is_sith = false;
                character.Is_neutral = true;
            }
            else if (current_karma <= sith_karma) 
            {
                character.Is_sith = true;
                character.Is_jedi = false;
                character.Is_neutral = true;
            }
            else
            {
                character.Is_neutral = true;
                character.Is_sith = false;
                character.Is_jedi = false;
            }
        }
        private void Refresh_combat_parameters()
        {
            OnPropertyChanged("Character_reaction");
            OnPropertyChanged("Character_armor");
            OnPropertyChanged("Character_watchfullness");
            OnPropertyChanged("Character_hideness");
            OnPropertyChanged("Character_force_resistance");
            OnPropertyChanged("Character_concentration");
        }
    }
}
