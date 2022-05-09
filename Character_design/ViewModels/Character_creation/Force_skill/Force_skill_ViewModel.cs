using System;
using System.Collections.Generic;
using SW_Character_creation;
using System.Windows.Media;

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

        private Color Chosen_color,
                      Unchosen_color;



        public Command Show_neutral_force_skills { get; private set; }
        public Command Show_sith_force_skills { get; private set; }
        public Command Show_jedi_force_skills { get; private set; }
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
                OnPropertyChanged("Selected_force_skill_title");
                OnPropertyChanged("Selected_force_skill_description");
                OnPropertyChanged("Selected_force_skill_cost");
                OnPropertyChanged("Selected_force_skill_counter");
                OnPropertyChanged("Selected_force_skill_min_score");
                OnPropertyChanged("Selected_force_skill_max_score");
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
            Show_sith_force_skills      = new Command(o => _Show_sith_force_skills(),  o => Character.GetInstance().Is_sith);

            Neutral_force_border    = new SolidColorBrush();
            Light_force_border      = new SolidColorBrush();
            Dark_force_border       = new SolidColorBrush();

            Button_borders = new List<SolidColorBrush>();
            Button_borders.Add(Neutral_force_border);
            Button_borders.Add(Light_force_border);
            Button_borders.Add(Dark_force_border);

            Chosen_color = Colors.Wheat;
            Unchosen_color = Colors.Black;

            Set_colors_for_chosen_item(Button_borders, Neutral_force_border, Chosen_color, Unchosen_color);
        }


        private void _Show_neutral_force_skills()
        {
            Current_force_skill_list = neutral_force_skills;

            Set_colors_for_chosen_item(Button_borders, Neutral_force_border, Chosen_color, Unchosen_color);
        }
        private void _Show_jedi_force_skills()
        {
            Current_force_skill_list = jedi_force_skills;

            Set_colors_for_chosen_item(Button_borders, Light_force_border, Chosen_color, Unchosen_color);
        }
        private void _Show_sith_force_skills()
        {
            Current_force_skill_list = sith_force_skills;

            Set_colors_for_chosen_item(Button_borders, Dark_force_border, Chosen_color, Unchosen_color);
        }
    }
}
