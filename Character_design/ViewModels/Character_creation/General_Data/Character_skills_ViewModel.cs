using System;
using System.Collections.Generic;
using Skills_libs;
using SW_Character_creation;
using System.Windows.Media;

namespace Character_design
{
    internal class Character_skills_ViewModel : BaseViewModel
    {
        private static Character_skills_ViewModel _instance;

        private object skill_group;
        private object selected_skill;
        private List<Skill_Class> usual_skills_group;
        private List<Force_skill_class> force_skills_group;

        private SolidColorBrush skills_border,
                                force_skills_border;

        private List<SolidColorBrush> Button_borders;

        private Color Chosen_color,
                      Unchosen_color;

        private int button_opacity;



        public Command Show_skills { get; private set; }
        public Command Show_force_skills { get; private set; }
        public object Skill_group
        {
            get { return skill_group; }
        }
        public object Selected_skill
        {
            get { return selected_skill; }
            set { selected_skill = value; OnPropertyChanged("Selected_skill"); }
        }
        public int Button_opacity
        {
            get { return button_opacity; }
            set { button_opacity = value; OnPropertyChanged("Button_opacity"); }
        }
        public SolidColorBrush Skills_border
        {
            get { return skills_border; }
            set { skills_border = value; OnPropertyChanged("Skills_border"); }
        }
        public SolidColorBrush Force_skills_border
        {
            get { return force_skills_border; }
            set { force_skills_border = value; OnPropertyChanged("Force_skills_border"); }
        }



        public static Character_skills_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Character_skills_ViewModel();
            }
            return _instance;
        }



        private Character_skills_ViewModel()
        {
            skill_group = new object();
            usual_skills_group = Character.GetInstance().Skills_with_points;
            //force_skills_group = Character.GetInstance().Force_skills_with_points;

            Skills_border       = new SolidColorBrush();
            Force_skills_border = new SolidColorBrush();

            Button_borders = new List<SolidColorBrush>();
            Button_borders.Add(Skills_border);
            Button_borders.Add(Force_skills_border);

            Show_skills         = new Command(o => _Show_skills());
            Show_force_skills   = new Command(o => _Show_force_skills());

            Chosen_color    = Colors.Wheat;
            Unchosen_color  = Colors.Black;

            Button_opacity = 100;

            Set_colors_for_chosen_item(Button_borders, Skills_border, Chosen_color, Unchosen_color);
        }



        private void _Show_skills()
        {
            skill_group = usual_skills_group;

            Set_colors_for_chosen_item(Button_borders, Skills_border, Chosen_color, Unchosen_color);
        }
        private void _Show_force_skills()
        {
            //skill_group = force_skills_group;

            Set_colors_for_chosen_item(Button_borders, Force_skills_border, Chosen_color, Unchosen_color);
        }
    }
}
