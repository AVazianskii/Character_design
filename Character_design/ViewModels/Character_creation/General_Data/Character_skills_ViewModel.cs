using System;
using System.Collections.Generic;
using Skills_libs;
using SW_Character_creation;
using System.Windows.Media;
using System.IO;

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
            set { skill_group = value; OnPropertyChanged("Skill_group"); }
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
        public bool Character_forceuser
        {
            get 
            {
                Skill_group = usual_skills_group;
                Set_colors_for_chosen_item(Button_borders, Skills_border, Chosen_color, Unchosen_color);
                if (Character.GetInstance().Forceuser)
                {
                    Button_opacity = 100;
                }
                else
                {
                    Button_opacity = 0;
                }
                return Character.GetInstance().Forceuser; 
            }
        }
        public string Question_sign
        {
            get { return $@"{Directory.GetCurrentDirectory()}\Pictures\Common\Tool_tip.png"; }
        }
        public string Skill_base_description
        {
            get { return "Значение атрибута - основы прибавляется к результату проверки навыка"; }
        }
        public string Skill_base
        {
            get 
            {
                string result = "";
                if (Skill_group == usual_skills_group)
                {
                    Skill_Class skill = Selected_skill as Skill_Class;
                    if (skill != null)
                    {
                        if (skill.Get_skill_base_2() != "")
                        {
                            result = skill.Get_skill_base_1() + "/" + skill.Get_skill_base_2();
                        }
                        else
                        {
                            result = skill.Get_skill_base_1();
                        }
                    }
                }
                else
                {
                    Force_skill_class skill = Selected_skill as Force_skill_class;
                    if (skill != null)
                    {
                        result = skill.Skill_base;
                    }
                }
                return result;
            }
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
            force_skills_group = Character.GetInstance().Force_skills_with_points;
            Skill_group = usual_skills_group;

            Skills_border       = new SolidColorBrush();
            Force_skills_border = new SolidColorBrush();

            Button_borders = new List<SolidColorBrush>();
            Button_borders.Add(Skills_border);
            Button_borders.Add(Force_skills_border);

            Show_skills         = new Command(o => _Show_skills());
            Show_force_skills   = new Command(o => _Show_force_skills());

            Chosen_color    = Colors.Wheat;
            Unchosen_color  = Colors.Black;

            Set_colors_for_chosen_item(Button_borders, Skills_border, Chosen_color, Unchosen_color);
        }



        private void _Show_skills()
        {
            Skill_group = usual_skills_group;

            Set_colors_for_chosen_item(Button_borders, Skills_border, Chosen_color, Unchosen_color);
        }
        private void _Show_force_skills()
        {
            Skill_group = force_skills_group;

            Set_colors_for_chosen_item(Button_borders, Force_skills_border, Chosen_color, Unchosen_color);
        }
    }
}
