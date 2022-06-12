﻿using System;
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

        private List<All_skill_template> skill_group;
        private All_skill_template selected_skill;
        private List<All_skill_template> usual_skills_group;
        private List<All_skill_template> force_skills_group;

        private SolidColorBrush skills_border,
                                force_skills_border;

        private List<SolidColorBrush> Button_borders;

        private Color Chosen_color,
                      Unchosen_color;

        private int button_opacity;



        public Command Show_skills { get; private set; }
        public Command Show_force_skills { get; private set; }
        public List<All_skill_template> Skill_group
        {
            get { return skill_group; }
            set { skill_group = value; OnPropertyChanged("Skill_group"); }
        }
        public All_skill_template Selected_skill
        {
            get { return selected_skill; }
            set 
            { 
                selected_skill = value; 
                if (selected_skill != null)
                {
                    OnPropertyChanged("Skill_base");
                    OnPropertyChanged("Selected_skill_name");
                    OnPropertyChanged("Selected_skill_img_path");
                    OnPropertyChanged("Skill_description");
                }
                OnPropertyChanged("Selected_skill"); 
            }
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
                    Skill_Class skill = (Skill_Class)Selected_skill;
                    if (Selected_skill != null)
                    {
                        if (skill.Skill_base_2 != "")
                        {
                            result = skill.Skill_base_1 + "/" + skill.Skill_base_2;
                        }
                        else
                        {
                            result = skill.Skill_base_1;
                        }
                    }
                }
                else
                {
                    if (Selected_skill != null)
                    {
                        result = Selected_skill.Skill_base_1;
                    }
                }
                return result;
            }
        }
        public string Selected_skill_name
        {
            get { return Selected_skill.Name; }
        }
        public string Selected_skill_img_path
        {
            get { return Selected_skill.Img_path; }
        }
        public string Skill_description
        {
            get { return Selected_skill.Description; }
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
            skill_group = new List<All_skill_template>();
            usual_skills_group = Character.GetInstance().Skills_with_points;
            force_skills_group = Character.GetInstance().Force_skills_with_points;
            Skill_group = usual_skills_group;

            selected_skill = new All_skill_template();
            
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
