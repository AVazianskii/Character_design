using System;
using System.Collections.Generic;
using SW_Character_creation;
using Skills_libs;
using System.Windows.Media;
using System.IO;

namespace Character_design
{
    internal class Skill_ViewModel : BaseViewModel
    {
        private static Skill_ViewModel _instance;
        private BaseViewModel current_VM;

        private Skill_Class selected_skill;
        private List<Skill_Class> skill_group;
        private List<Skill_Class> combat_skills;
        private List<Skill_Class> surviving_skills;
        private List<Skill_Class> charming_skills;
        private List<Skill_Class> tech_skills;
        private List<Skill_Class> specific_skills;

        private SolidColorBrush combat_skills_button_border,
                                surviving_skills_button_border,
                                charming_skills_button_border,
                                tech_skills_button_border,
                                specific_skills_button_border;

        private List<SolidColorBrush> Button_borders;

        private Color Chosen_color,
                      Unchosen_color;

        private string selected_skill_description;
        private string skill_name;
        private string selected_skill_title;
        private string skill_choose_warning;

        private int selected_skill_score,
                    selected_skill_min_score,
                    selected_skill_max_score,
                    selected_skill_race_bonus,
                    selected_skill_range_limit,
                    selected_skill_age_limit,
                    selected_skill_cost,
                    selected_skill_counter;

        private int selected_skill_limit;



        public static Skill_ViewModel GetInstance()
        {
            if (_instance == null) { _instance = new Skill_ViewModel(); }
            return _instance;
        }
        public void Refresh_fields()
        {
            if(Selected_skill != null)
            {
                Selected_skill_description  = Selected_skill.Get_skill_description();
                Selected_skill_title        = Selected_skill.Get_skill_name();
                Selected_skill_race_bonus   = Return_race_skill_bonus(selected_skill, Character.GetInstance().Character_race);
                Selected_skill_min_score    = 0;
                Selected_skill_range_limit  = Return_range_skill_limit(selected_skill, Character.GetInstance().Range);
                Selected_skill_age_limit    = Return_age_skill_limit(Character.GetInstance().Age_status);
                Selected_skill_cost         = Return_skill_cost(selected_skill);
                Selected_skill_limit        = Return_skill_limit(selected_skill, Character.GetInstance().Age_status, Character.GetInstance().Range);
                Selected_skill_score        = Selected_skill.Get_score();
                Selected_skill_max_score    = Selected_skill_race_bonus + Selected_skill_limit;
                OnPropertyChanged("Skill_base_text");
                OnPropertyChanged("Skill_choose_warning");
                OnPropertyChanged("Skill_img_path");
            }
        }



        public Command Show_combat_skills { get; private set; }
        public Command Show_surviving_skills { get; private set; }
        public Command Show_charming_skills { get; private set; }
        public Command Show_tech_skills { get; private set; }
        public Command Show_specific_skills { get; private set; }
        public Command Decrease_skill_score { get; private set; }
        public Command Increase_skill_score { get; private set; }
        public BaseViewModel CurrentViewModel
        {
            get { return current_VM; }
            set { current_VM = value; OnPropertyChanged("CurrentViewModel"); }
        }
        public SolidColorBrush Combat_skills_button_border
        {
            get { return combat_skills_button_border; }
            set { combat_skills_button_border = value; OnPropertyChanged("Combat_skills_button_border"); }
        }
        public SolidColorBrush Surviving_skills_button_border
        {
            get { return surviving_skills_button_border; }
            set { surviving_skills_button_border = value; OnPropertyChanged("Survivng_skills_button_border"); }
        }
        public SolidColorBrush Charming_skills_button_border
        {
            get { return charming_skills_button_border; }
            set { charming_skills_button_border = value; OnPropertyChanged("Charming_skills_button_border"); }
        }
        public SolidColorBrush Tech_skills_button_border
        {
            get { return tech_skills_button_border; }
            set { tech_skills_button_border = value; OnPropertyChanged("Tech_skills_button_border"); }
        }
        public SolidColorBrush Specific_skills_button_border
        {
            get { return specific_skills_button_border; }
            set { specific_skills_button_border = value; OnPropertyChanged("Specific_skills_button_border"); }
        }
        public int Exp_points_left
        {
            get { return Character.GetInstance().Experience_left; }
        }
        public Skill_Class Selected_skill
        {
            get { return selected_skill; }
            set
            {
                selected_skill = value;
                if (selected_skill != null)
                {
                    Selected_skill_description  = selected_skill.Get_skill_description();
                    Selected_skill_title        = selected_skill.Get_skill_name();
                    Selected_skill_race_bonus   = Return_race_skill_bonus(selected_skill, Character.GetInstance().Character_race);
                    Selected_skill_min_score    = 0;
                    Selected_skill_range_limit  = Return_range_skill_limit(selected_skill, Character.GetInstance().Range);
                    Selected_skill_age_limit    = Return_age_skill_limit(Character.GetInstance().Age_status);
                    Selected_skill_cost         = Return_skill_cost(selected_skill);
                    Selected_skill_limit        = Return_skill_limit(selected_skill, Character.GetInstance().Age_status, Character.GetInstance().Range);
                    Selected_skill_score        = selected_skill.Get_score();
                    Selected_skill_max_score    = Selected_skill_race_bonus + Selected_skill_limit;

                    OnPropertyChanged("Skill_img_path");
                }
                OnPropertyChanged("Skill_base_text");
                OnPropertyChanged("Skill_choose_warning");
                OnPropertyChanged("Selected_skill_counter");
                OnPropertyChanged("Selected_skill");
            }
        }
        public List<Skill_Class> Skill_group
        {
            get { return skill_group; }
            set { skill_group = value; OnPropertyChanged("Skill_group"); }
        }
        public string Selected_skill_description
        {
            get { return selected_skill_description;  }
            set { selected_skill_description = value; OnPropertyChanged("Selected_skill_description"); }
        }
        public string Skill_name
        {
            get { return skill_name; }
            set { skill_name = value; OnPropertyChanged("Skill_name"); }
        }
        public string Selected_skill_title
        {
            get { return selected_skill_title; }
            set { selected_skill_title = value; OnPropertyChanged("Selected_skill_title"); }
        }
        public int Selected_skill_score
        {
            get { return selected_skill_score; }
            set { selected_skill_score = value; OnPropertyChanged("Selected_skill_score"); }
        }
        public int Selected_skill_counter
        {
            get 
            {
                selected_skill_counter = Selected_skill_score + Selected_skill_race_bonus;
                return selected_skill_counter; 
            }
            set { selected_skill_counter = value; OnPropertyChanged("Selected_skill_counter"); }
        }
        public int Selected_skill_min_score
        {
            get { return selected_skill_min_score; }
            set { selected_skill_min_score = value; OnPropertyChanged("Selected_skill_min_score"); }
        }
        public int Selected_skill_max_score
        {
            get { return selected_skill_max_score; }
            set { selected_skill_max_score = value; OnPropertyChanged("Selected_skill_max_score"); }
        }
        public int Selected_skill_race_bonus
        {
            get { return selected_skill_race_bonus; }
            set { selected_skill_race_bonus = value; OnPropertyChanged("Selected_skill_race_bonus"); }
        }
        public int Selected_skill_range_limit
        {
            get { return selected_skill_range_limit; }
            set { selected_skill_range_limit = value; OnPropertyChanged("Selected_skill_range_limit"); }
        }
        public int Selected_skill_age_limit
        {
            get { return selected_skill_age_limit; }
            set { selected_skill_age_limit = value; OnPropertyChanged("Selected_skill_age_limit"); }
        }
        public int Selected_skill_cost
        {
            get { return selected_skill_cost; }
            set { selected_skill_cost = value; OnPropertyChanged("Selected_skill_cost"); }
        }
        public int Selected_skill_limit
        {
            get { return selected_skill_limit; }
            set { selected_skill_limit = value; OnPropertyChanged("Selected_skill_limit"); }
        }
        public string Skill_base_text
        {
            get { return _Skill_base_text(Selected_skill); }
        }
        public string Skill_base_description
        {
            get { return "Значение атрибута - основы прибавляется к результату проверки навыка"; }
        }
        public string Question_sign
        {
            get { return $@"{Directory.GetCurrentDirectory()}\Pictures\Common\Tool_tip.png"; }
        }
        public string Skill_choose_warning
        {
            get { return skill_choose_warning; }
            set { skill_choose_warning = value; OnPropertyChanged("Skill_choose_warning"); }
        }
        public string Skill_img_path
        {
            get { return Selected_skill.Img_path; }
        }



        private Skill_ViewModel()
        {
            combat_skills       = Skill_manager.GetInstance().Get_Combat_skills();
            surviving_skills    = Skill_manager.GetInstance().Get_Survivng_skills();
            charming_skills     = Skill_manager.GetInstance().Get_Charming_skills();
            tech_skills         = Skill_manager.GetInstance().Get_Tech_skills();
            specific_skills     = Skill_manager.GetInstance().Get_Specific_skills();

            skill_group     = combat_skills;
            Selected_skill  = skill_group[0];

            Show_combat_skills      = new Command(o => _Show_combat_skills());
            Show_surviving_skills   = new Command(o => _Show_surviving_skills());
            Show_charming_skills    = new Command(o => _Show_charming_skills());
            Show_tech_skills        = new Command(o => _Show_tech_skills());
            Show_specific_skills    = new Command(o => _Show_specific_skills());

            Decrease_skill_score = new Command(o => _Decrease_skill_score(o), 
                                               o => selected_skill.Get_counter() > 0);

            Increase_skill_score = new Command(o => _Increase_skill_score(o), 
                                               o => Increase_is_possible(selected_skill, 
                                                                         Selected_skill_cost, 
                                                                         Selected_skill_limit,
                                                                         Character.GetInstance().Experience_left));

            Combat_skills_button_border     = new SolidColorBrush();
            Surviving_skills_button_border  = new SolidColorBrush();
            Charming_skills_button_border   = new SolidColorBrush();
            Tech_skills_button_border       = new SolidColorBrush();
            Specific_skills_button_border   = new SolidColorBrush();

            Button_borders = new List<SolidColorBrush>();

            Button_borders.Add(Combat_skills_button_border);
            Button_borders.Add(Surviving_skills_button_border);
            Button_borders.Add(Charming_skills_button_border);
            Button_borders.Add(Tech_skills_button_border);
            Button_borders.Add(Specific_skills_button_border);

            Chosen_color = Colors.Wheat;
            Unchosen_color = Colors.Black;

            Set_colors_for_chosen_item(Button_borders,Combat_skills_button_border,Chosen_color,Unchosen_color);
        }


        
        private int Return_skill_cost(Skill_Class skill)
        {
            if (Character.GetInstance().Forceuser)
            {
                return skill.Get_Forceuser_cost();
            }
            else
            {
                return skill.Get_Non_force_user_cost();
            }
        }
        private int Return_skill_limit(Skill_Class skill, Age_status_libs.Age_status_class age_status, Range_libs.Range_Class range)
        {
            int result = 0;
            switch (skill.Skill_type)
            {
                case 1:
                    if (age_status.Skill_limit <= range.Combat_skill_limit) { result = age_status.Skill_limit; }
                    else { result = range.Combat_skill_limit; }
                    break;
                case 2:
                    if (age_status.Skill_limit <= range.Surviving_skill_limit) { result = age_status.Skill_limit; }
                    else { result = range.Surviving_skill_limit; }
                    break;
                case 3:
                    if (age_status.Skill_limit <= range.Charming_skill_limit) { result = age_status.Skill_limit; }
                    else { result = range.Charming_skill_limit; }
                    break;
                case 4:
                    if (age_status.Skill_limit <= range.Tech_skill_limit) { result = age_status.Skill_limit; }
                    else { result = range.Tech_skill_limit; }
                    break;
                case 5:
                    if (age_status.Skill_limit <= range.Specific_skill_limit) { result = age_status.Skill_limit; }
                    else { result = range.Specific_skill_limit; }
                    break;
            }
            return result;
        }
        private void _Show_combat_skills()
        {
            Skill_group = combat_skills;
            Selected_skill = Skill_group[0];

            Set_colors_for_chosen_item(Button_borders, Combat_skills_button_border, Chosen_color, Unchosen_color);
        }
        private void _Show_surviving_skills()
        {
            Skill_group = surviving_skills;
            Selected_skill = Skill_group[0];

            Set_colors_for_chosen_item(Button_borders, Surviving_skills_button_border, Chosen_color, Unchosen_color);
        }
        private void _Show_charming_skills()
        {
            Skill_group = charming_skills;
            Selected_skill = Skill_group[0];

            Set_colors_for_chosen_item(Button_borders, Charming_skills_button_border, Chosen_color, Unchosen_color);
        }
        private void _Show_tech_skills()
        {
            Skill_group = tech_skills;
            Selected_skill = Skill_group[0];

            Set_colors_for_chosen_item(Button_borders, Tech_skills_button_border, Chosen_color, Unchosen_color);
        }
        private void _Show_specific_skills()
        {
            Skill_group = specific_skills;
            Selected_skill = Skill_group[0];

            Set_colors_for_chosen_item(Button_borders, Specific_skills_button_border, Chosen_color, Unchosen_color);
        }
        private void _Increase_skill_score(object o)
        {
            Skill_Class skill = o as Skill_Class;
            if (skill != null)
            {
                foreach (Skill_Class Character_skill in Character.GetInstance().Get_skills())
                {
                    if (skill.Get_skill_name() == Character_skill.Get_skill_name())
                    {
                        Character_skill.Increase_score();
                        Selected_skill_score = Character_skill.Get_score();
                        Character.GetInstance().Spend_exp_points(Selected_skill_cost);
                        OnPropertyChanged("Exp_points_left");
                        OnPropertyChanged("Selected_skill_counter");
                        break;
                    }
                }
            }
        }
        private void _Decrease_skill_score(object o)
        {
            Skill_Class skill = o as Skill_Class;
            if (skill != null)
            {
                foreach (Skill_Class Character_skill in Character.GetInstance().Get_skills())
                {
                    if (skill.Get_skill_name() == Character_skill.Get_skill_name())
                    {
                        Character_skill.Decrease_score();
                        Selected_skill_score = Character_skill.Get_score();
                        Character.GetInstance().Refund_exp_points(Selected_skill_cost);
                        OnPropertyChanged("Exp_points_left");
                        OnPropertyChanged("Selected_skill_counter");
                        break;
                    }
                }
            }
        }
        private int Return_race_skill_bonus(Skill_Class skill, Races_libs.Race_class race)
        {
            return race.Race_skill_bonus[skill.Get_skill_code() - 1];
        }
        private int Return_age_skill_limit(Age_status_libs.Age_status_class age_status)
        {
            return age_status.Skill_limit;
        }
        private int Return_range_skill_limit(Skill_Class skill, Range_libs.Range_Class range)
        {
            int limit = 0;
            switch(skill.Skill_type)
            {
                case 1: limit = range.Combat_skill_limit; break;
                case 2: limit = range.Surviving_skill_limit; break;
                case 3: limit = range.Charming_skill_limit; break;
                case 4: limit = range.Tech_skill_limit; break;
                case 5: limit = range.Specific_skill_limit; break;
            }
            return limit;
        }
        private bool Increase_is_possible (Skill_Class skill,int cost, int limit, int exp_points_left)
        {
            bool result = false;
            if (Character.GetInstance().Character_race != Race__manager.GetInstance().Get_Race_list()[0])
            {
                if (exp_points_left >= cost)
                {
                    if (skill.Get_counter() < limit)
                    {
                        result = true;
                        Skill_choose_warning = "";
                    }
                    else { Skill_choose_warning = "Не выбран ранг и возрастной статус персонажа!"; }
                }
                else { Skill_choose_warning = "Недостаточно опыта для развития навыка!"; }
            }
            else { Skill_choose_warning = "Раса персонажа не выбрана!"; }
            return result;
        }
        private string _Skill_base_text(Skill_Class skill)
        {
            string result = "";
            if (skill != null)
            {
                result = skill.Get_skill_base_1();
                if (skill.Get_skill_base_2() != "")
                {
                    result = result + " / " + skill.Get_skill_base_2();
                }
            }
            return result;
        }
    }
}
