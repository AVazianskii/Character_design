using System;
using System.Collections.Generic;
using SW_Character_creation;
using System.Windows.Media;
using System.IO;

namespace Character_design
{ 
    internal class Force_skill_ViewModel : BaseViewModel
    {
        private static Force_skill_ViewModel _instance;

        private SolidColorBrush neutral_force_border,
                                light_force_border,
                                dark_force_border;

        private Force_skill_class selected_force_skill;

        private List<SolidColorBrush> Button_borders;

        private List<Force_skill_class> current_force_skill_list,
                                        neutral_force_skills,
                                        jedi_force_skills,
                                        sith_force_skills;

        private int selected_force_skill_min_score,
                    selected_force_skill_max_score,
                    selected_force_skill_counter;

        private string force_skill_choose_warning,
                       force_skill_choose_advice;

        private bool learn_skill_enable,
                     delete_skill_enable;

        private Color Chosen_color,
                      Unchosen_color;



        public Command Show_neutral_force_skills { get; private set; }
        public Command Show_sith_force_skills { get; private set; }
        public Command Show_jedi_force_skills { get; private set; }
        public Character_changing_command Increase_force_skill_score { get; private set; }
        public Character_changing_command Decrease_force_skill_score { get; private set; }
        public SolidColorBrush Neutral_force_border
        {
            get { return neutral_force_border; }
            set { neutral_force_border = value; OnPropertyChanged("Neutral_force_border"); }
        }
        public SolidColorBrush Light_force_border
        {
            get { return light_force_border; }
            set { light_force_border = value; OnPropertyChanged("Light_force_border"); }
        }
        public SolidColorBrush Dark_force_border
        {
            get { return dark_force_border; }
            set { dark_force_border = value; OnPropertyChanged("Dark_force_border"); }
        }
        public int Exp_points_left
        {
            get { return Character.GetInstance().Experience_left; }
        }
        public List<Force_skill_class> Current_force_skill_list
        {
            get { return current_force_skill_list; }
            set { current_force_skill_list = value; OnPropertyChanged("Current_force_skill_list"); }
        }
        public Force_skill_class Selected_force_skill
        {
            get { return selected_force_skill; }
            set 
            {
                selected_force_skill = value;
                OnPropertyChanged("Selected_force_skill");
                if (selected_force_skill != null)
                {
                    Check_warning_advice();

                    OnPropertyChanged("Selected_force_skill_title");
                    OnPropertyChanged("Selected_force_skill_description");
                    OnPropertyChanged("Selected_force_skill_cost");
                    OnPropertyChanged("Selected_force_skill_counter");
                    OnPropertyChanged("Selected_force_skill_min_score");
                    OnPropertyChanged("Selected_force_skill_max_score");
                    OnPropertyChanged("Force_skill_img_path");
                    OnPropertyChanged("Skill_base_text");

                    if (selected_force_skill.Is_chosen)
                    {
                        Force_skill_choose_advice = "Навык владения Силой изучен!";
                    }
                    else
                    {
                        Force_skill_choose_advice = "";
                    }
                    OnPropertyChanged("Force_skill_choose_advice");
                    OnPropertyChanged("Force_skill_choose_warning");
                }
            }
        }
        public string Selected_force_skill_title
        {
            get { return Selected_force_skill.Name; }
        }
        public string Selected_force_skill_description
        { 
            get { return Selected_force_skill.Description; }
        }
        public int Selected_force_skill_cost
        {
            get { return Selected_force_skill.Cost; }
        }
        public int Selected_force_skill_counter
        {
            get { selected_force_skill_counter = Selected_force_skill.Score; return selected_force_skill_counter; }
            set { selected_force_skill_counter = value; OnPropertyChanged("Selected_force_skill_counter"); }
        }
        public int Selected_force_skill_min_score
        {
            get { return selected_force_skill_min_score; }
            set { selected_force_skill_min_score = value; OnPropertyChanged("Selected_force_skill_min_score"); }
        }
        public int Selected_force_skill_max_score
        {
            get { return Character.GetInstance().Age_status.Force_skill_limit; }
        }
        public string Force_skill_choose_warning
        {
            get { return force_skill_choose_warning; }
            set { force_skill_choose_warning = value; OnPropertyChanged("Force_skill_choose_warning"); }
        }
        public string Force_skill_img_path
        {
            get { return Selected_force_skill.Img_path; }
        }
        public string Question_sign
        {
            get { return $@"{Directory.GetCurrentDirectory()}\Pictures\Common\Tool_tip.png"; }
        }
        public string Skill_base_description
        {
            get { return "Значение атрибута - основы прибавляется к результату проверки навыка"; }
        }
        public string Skill_base_text
        {
            get { return Selected_force_skill.Skill_base_1; }
        }
        public int Num_skills_left
        {
            get { return Character.GetInstance().Limit_force_skills_left; }
        }
        public string Force_skill_choose_advice
        {
            get { return force_skill_choose_advice; }
            set { force_skill_choose_advice = value; OnPropertyChanged("Force_skill_choose_advice"); }
        }



        public static Force_skill_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Force_skill_ViewModel();
            }
            return _instance;
        }
        public static void DeleteInstance()
        {
            if (_instance != null)
            {
                _instance = null;
            }
        }



        public void Refresh_fields()
        {
            OnPropertyChanged("Selected_force_skill");
            Check_warning_advice();
        }



        private Force_skill_ViewModel()
        {
            neutral_force_skills    = Main_model.GetInstance().Force_skill_Manager.Get_Neutral_force_skills();
            jedi_force_skills       = Main_model.GetInstance().Force_skill_Manager.Get_Jedi_force_skills();
            sith_force_skills       = Main_model.GetInstance().Force_skill_Manager.Get_Sith_force_skills();

            Current_force_skill_list = neutral_force_skills;

            Selected_force_skill = Current_force_skill_list[0];

            Show_neutral_force_skills   = new Command(o => _Show_neutral_force_skills());
            Show_jedi_force_skills      = new Command(o => _Show_jedi_force_skills());
            Show_sith_force_skills      = new Command(o => _Show_sith_force_skills());
            Increase_force_skill_score = new Character_changing_command(o => _Increase_force_skill_score(Selected_force_skill),
                                                                         o => learn_skill_enable); 
            Decrease_force_skill_score = new Character_changing_command(o => _Decrease_force_skill_score(Selected_force_skill),
                                                                         o => delete_skill_enable);

            Neutral_force_border    = new SolidColorBrush();
            Light_force_border      = new SolidColorBrush();
            Dark_force_border       = new SolidColorBrush();

            Button_borders = new List<SolidColorBrush>();
            Button_borders.Add(Neutral_force_border);
            Button_borders.Add(Light_force_border);
            Button_borders.Add(Dark_force_border);

            Chosen_color = Colors.Wheat;
            Unchosen_color = Colors.Black;

            // TODO: Внедрить систему лимитов. См. правила
            Selected_force_skill_min_score = 0;
            
            Set_colors_for_chosen_item(Button_borders, Neutral_force_border, Chosen_color, Unchosen_color);

            Force_skill_choose_advice = "";
        }



        private void _Show_neutral_force_skills()
        {
            Current_force_skill_list = neutral_force_skills;
            Selected_force_skill = Current_force_skill_list[0];
            Check_warning_advice();

            Set_colors_for_chosen_item(Button_borders, Neutral_force_border, Chosen_color, Unchosen_color);
        }
        private void _Show_jedi_force_skills()
        {
            Current_force_skill_list = jedi_force_skills;
            Selected_force_skill = Current_force_skill_list[0];
            Check_warning_advice();

            Set_colors_for_chosen_item(Button_borders, Light_force_border, Chosen_color, Unchosen_color);
        }
        private void _Show_sith_force_skills()
        {
            Current_force_skill_list = sith_force_skills;
            Selected_force_skill = Current_force_skill_list[0];

            Set_colors_for_chosen_item(Button_borders, Dark_force_border, Chosen_color, Unchosen_color);
        }
        private void _Increase_force_skill_score(object o)
        {
            Force_skill_class skill = o as Force_skill_class;
            if (skill != null)
            {
                foreach (Force_skill_class Character_skill in Character.GetInstance().Get_force_skills())
                {
                    if (skill.Name == Character_skill.Name)
                    {
                        Character_skill.Increase_score();
                        Selected_force_skill_counter = Character_skill.Score;
                        Character.GetInstance().Spend_exp_points(Selected_force_skill_cost);
                        Character.GetInstance().Increase_exp_sold_for_force_skills(Character_skill);
                        Character.GetInstance().Update_character_force_skills_list(Character_skill);
                        OnPropertyChanged("Exp_points_left");
                        OnPropertyChanged("Num_skills_left");
                        OnPropertyChanged("Selected_force_skill_counter");

                        Character.GetInstance().Update_combat_parameters_due_ForceSkill(Character_skill, 1);
                        Check_special_force_skill_bonus(previous_score: Character_skill.Score - 1);


                        if (Character_skill.Is_chosen != true)
                        {
                            Character_skill.Is_chosen = true;
                            Force_skill_choose_advice = "Навык владения Силой изучен!";
                        }
                        Check_warning_advice(); 
                        //OnPropertyChanged("Force_skill_choose_advice");
                        //OnPropertyChanged("Force_skill_choose_warning");

                        break;                  
                    }
                }
            }
        }
        private void _Decrease_force_skill_score(object o)
        {
            Force_skill_class skill = o as Force_skill_class;
            if (skill != null)
            {
                foreach (Force_skill_class Character_skill in Character.GetInstance().Get_force_skills())
                {
                    if (skill.Name == Character_skill.Name)
                    {
                        Character_skill.Decrease_score();
                        Selected_force_skill_counter = Character_skill.Score;
                        Character.GetInstance().Refund_exp_points(Selected_force_skill_cost);
                        Character.GetInstance().Decrease_exp_sold_for_force_skills(Character_skill);
                        Character.GetInstance().Update_character_force_skills_list(Character_skill);
                        OnPropertyChanged("Exp_points_left");
                        OnPropertyChanged("Num_skills_left");
                        OnPropertyChanged("Selected_force_skill_counter");

                        Character.GetInstance().Update_combat_parameters_due_ForceSkill(Character_skill, -1);
                        Check_special_force_skill_bonus(previous_score: Character_skill.Score + 1);

                        if (Character_skill.Score == 0)
                        {
                            Character_skill.Is_chosen = false;
                            Force_skill_choose_advice = "";
                        }
                        Check_warning_advice();
                        OnPropertyChanged("Force_skill_choose_advice");
                        OnPropertyChanged("Force_skill_choose_warning");

                        break;
                    }
                }
            }
        }
        private void Check_warning_advice()
        {
            force_skill_choose_warning = "";
            force_skill_choose_advice = "";
            learn_skill_enable = true;
            delete_skill_enable = true;

            // Запреты на изучение\увеличение навыка
            if (Character.GetInstance().Character_race == Race__manager.GetInstance().Get_Race_list()[0])
            {
                Force_skill_choose_warning = "Раса персонажа не выбрана!";
                learn_skill_enable = false;
            }
            else if (Character.GetInstance().Age_status == Age_status_manager.GetInstance().Get_Unknown_age_status())
            {
                Force_skill_choose_warning = "Возрастной стаус персонажа не выбран!";
                learn_skill_enable = false;
            }
            else if (((Character.GetInstance().Is_jedi && Selected_force_skill.Type == 3) || (Character.GetInstance().Is_sith && Selected_force_skill.Type == 2)) == true)
            {
                Force_skill_choose_warning = "Недопустимая сторона Силы для изучения навыка!";
                learn_skill_enable = false;
            }
            else if (Selected_force_skill.Score >= Selected_force_skill_max_score)
            {
                Force_skill_choose_warning = "Достигнут лимит развития навыка!";
                learn_skill_enable = false;
            }
            else if (Character.GetInstance().Experience_left < Selected_force_skill.Cost)
            {
                Force_skill_choose_warning = "Недостаточно опыта для развития навыка!";
                learn_skill_enable = false;
            }
            else if (Selected_force_skill.ID == 11)
            {
                bool flag = false;
                foreach(Force_skill_class force_skill in Character.GetInstance().Force_skills_with_points)
                {
                    if (force_skill.ID == 3)
                    {
                        flag = true;
                        if (force_skill.Score < 7)
                        {
                            Force_skill_choose_warning = "Необходим 7 уровень развития навыка 'Стойкость к Силе' для изучения данного навыка";
                            learn_skill_enable = false;
                        }
                        break;
                    }
                }
                if (flag == false)
                {
                    Force_skill_choose_warning = "Необходим 7 уровень развития навыка 'Стойкость к Силе' для изучения данного навыка";
                    learn_skill_enable = false;
                }
            }
            else if (Character.GetInstance().Limit_force_skills_left == 0 && Character.GetInstance().Force_skills_with_points.Contains(Selected_force_skill) == false)
            {
                Force_skill_choose_warning = "Достигнут лимит по количеству изучаемых навыков!";
                learn_skill_enable = false;
            }
            else if (Current_force_skill_list == jedi_force_skills)
            {
                if ((Character.GetInstance().Is_neutral || Character.GetInstance().Is_sith) && Character.GetInstance().Is_jedi == false)
                {
                    Force_skill_choose_warning = "Данный навык доступен только адептам Силы с положительной кармой!";
                    learn_skill_enable = false;
                }
            }
            else if (Current_force_skill_list == sith_force_skills)
            {
                if ((Character.GetInstance().Is_neutral || Character.GetInstance().Is_jedi) && Character.GetInstance().Is_sith == false)
                {
                    Force_skill_choose_warning = "Данный навык доступен только адептам Силы с отрицательной кармой!";
                    learn_skill_enable = false;
                }
            }
            else if (Character.GetInstance().Forceuser == false)
            {
                Force_skill_choose_warning = "Данный навык доступен только адептам Силы!";
                learn_skill_enable = false;
            }

            // Запреты на удаление\уменьшение навыка
            if (Selected_force_skill.Is_chosen == false)
            {
                delete_skill_enable = false;
            }
        }
        private void Check_special_force_skill_bonus(int previous_score)
        {
            if (Selected_force_skill.ID == 13)
            {
                sbyte   scratch_bonus = 0, 
                        light_wound_bonus = 0, 
                        medium_wound_bonus = 0, 
                        tough_wound_bonus = 0;
                bool learning_flag = false,
                     bonus_1 = false,
                     bonus_2 = false;
                if (Selected_force_skill.Score > previous_score)
                {
                    learning_flag = true;
                }
                else
                {
                    learning_flag = false;
                }
                if (learning_flag)
                {
                    if (previous_score == 9) { bonus_2 = true; }
                    if (previous_score == 4 || previous_score == 0) { bonus_1 = true; }
                }
                else
                {
                    if (previous_score == 10) { bonus_2 = true; }
                    if (previous_score == 5 || previous_score == 1) { bonus_1 = true; }
                }
                if (bonus_1)
                {
                    scratch_bonus = 2;
                    light_wound_bonus = 2;
                    medium_wound_bonus = 2;
                    tough_wound_bonus = 2;
                }
                if (bonus_2)
                {
                    scratch_bonus = 4;
                    light_wound_bonus = 4;
                    medium_wound_bonus = 4;
                    tough_wound_bonus = 4;
                }

                if (learning_flag)
                {
                    Character.GetInstance().Scratch_penalty = (sbyte)(Character.GetInstance().Scratch_penalty + scratch_bonus);
                    Character.GetInstance().Light_wound_penalty = (sbyte)(Character.GetInstance().Light_wound_penalty + light_wound_bonus);
                    Character.GetInstance().Medium_wound_penalty = (sbyte)(Character.GetInstance().Medium_wound_penalty + medium_wound_bonus);
                    Character.GetInstance().Tough_wound_penalty = (sbyte)(Character.GetInstance().Tough_wound_penalty + tough_wound_bonus);
                }
                else
                {
                    Character.GetInstance().Scratch_penalty = (sbyte)(Character.GetInstance().Scratch_penalty - scratch_bonus);
                    Character.GetInstance().Light_wound_penalty = (sbyte)(Character.GetInstance().Light_wound_penalty - light_wound_bonus);
                    Character.GetInstance().Medium_wound_penalty = (sbyte)(Character.GetInstance().Medium_wound_penalty - medium_wound_bonus);
                    Character.GetInstance().Tough_wound_penalty = (sbyte)(Character.GetInstance().Tough_wound_penalty - tough_wound_bonus);
                }
            }
        }
    }
}
