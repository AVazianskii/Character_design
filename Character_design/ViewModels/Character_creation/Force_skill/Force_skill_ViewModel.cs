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

        private string force_skill_choose_warning;

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
            get { return selected_force_skill_max_score; }
            set { selected_force_skill_max_score = value; OnPropertyChanged("Selected_force_skill_max_score"); }
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
            get { return Selected_force_skill.Skill_base; }
        }



        public static Force_skill_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Force_skill_ViewModel();
            }
            return _instance;
        }



        private Force_skill_ViewModel()
        {
            neutral_force_skills    = Main_model.GetInstance().Force_skill_Manager.Get_Neutral_force_skills();
            jedi_force_skills       = Main_model.GetInstance().Force_skill_Manager.Get_Jedi_force_skills();
            sith_force_skills       = Main_model.GetInstance().Force_skill_Manager.Get_Sith_force_skills();

            Current_force_skill_list = neutral_force_skills;

            Selected_force_skill = Current_force_skill_list[0];

            Show_neutral_force_skills   = new Command(o => _Show_neutral_force_skills());
            Show_jedi_force_skills      = new Command(o => _Show_jedi_force_skills(), o => Character.GetInstance().Is_jedi);
            Show_sith_force_skills      = new Command(o => _Show_sith_force_skills(), o => Character.GetInstance().Is_sith);
            Increase_force_skill_score  = new Character_changing_command(o => _Increase_force_skill_score(Selected_force_skill),
                                                                         o => Increase_is_possible(Selected_force_skill, Selected_force_skill_max_score, Exp_points_left));
            Decrease_force_skill_score  = new Character_changing_command(o => _Decrease_force_skill_score(Selected_force_skill),
                                                                         o => Selected_force_skill.Score > 0);

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
            Selected_force_skill_max_score = 10;

            Set_colors_for_chosen_item(Button_borders, Neutral_force_border, Chosen_color, Unchosen_color);
        }



        private void _Show_neutral_force_skills()
        {
            Current_force_skill_list = neutral_force_skills;
            Selected_force_skill = Current_force_skill_list[0];

            Set_colors_for_chosen_item(Button_borders, Neutral_force_border, Chosen_color, Unchosen_color);
        }
        private void _Show_jedi_force_skills()
        {
            Current_force_skill_list = jedi_force_skills;
            Selected_force_skill = Current_force_skill_list[0];

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
                        OnPropertyChanged("Selected_force_skill_counter");

                        Character.GetInstance().Update_combat_parameters_due_ForceSkill(Character_skill, 1);
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
                        OnPropertyChanged("Selected_force_skill_counter");

                        Character.GetInstance().Update_combat_parameters_due_ForceSkill(Character_skill, -1);
                        break;
                    }
                }
            }
        }
        private bool Increase_is_possible(Force_skill_class skill, int limit, int exp_points_left)
        {
            bool result = false;
            if (Character.GetInstance().Character_race != Race__manager.GetInstance().Get_Race_list()[0])
            {
                if (((Character.GetInstance().Is_jedi && skill.Type == 3) || (Character.GetInstance().Is_sith && skill.Type == 2)) != true)
                {
                    if (exp_points_left >= skill.Cost)
                    {
                        if (skill.Score < limit)
                        {
                            result = true;
                            Force_skill_choose_warning = "";
                        } else { Force_skill_choose_warning = "Достигнут лимит развития навыка!"; }
                    } else { Force_skill_choose_warning = "Недостаточно опыта для развития навыка!"; }
                } else { Force_skill_choose_warning = "Недопустимая сторона Силы для изучения навыка!"; }
            } else { Force_skill_choose_warning = "Раса персонажа не выбрана!"; }
            return result;
        }
    }
}
