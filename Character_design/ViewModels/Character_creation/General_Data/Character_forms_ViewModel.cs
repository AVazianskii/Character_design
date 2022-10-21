using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;

namespace Character_design
{
    internal class Character_forms_ViewModel : BaseViewModel
    {
        private static Character_forms_ViewModel _instance;



        private SolidColorBrush forms_border,
                        force_forms_border;

        private List<SolidColorBrush> Button_borders;

        private Color Chosen_color,
                      Unchosen_color;

        private int button_opacity;



        public Command Show_forms { get; private set; }
        public Command Show_force_forms { get; private set; }
        public SolidColorBrush Forms_border
        {
            get { return forms_border; }
            set { forms_border = value; OnPropertyChanged("Forms_border"); }
        }
        public SolidColorBrush Force_forms_border
        {
            get { return force_forms_border; }
            set { force_forms_border = value; OnPropertyChanged("Force_forms_border"); }
        }



        public static Character_forms_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Character_forms_ViewModel();
            }
            return _instance;
        }


        private Character_forms_ViewModel()
        {
            Forms_border = new SolidColorBrush();
            Force_forms_border = new SolidColorBrush();

            Button_borders = new List<SolidColorBrush>();
            Button_borders.Add(Forms_border);
            Button_borders.Add(Force_forms_border);

            Show_forms       = new Command(o => _Show_forms());
            Show_force_forms = new Command(o => _Show_force_forms());

            Chosen_color = Colors.Wheat;
            Unchosen_color = Colors.Black;

            Set_colors_for_chosen_item(Button_borders, Forms_border, Chosen_color, Unchosen_color);
        }



        private void _Show_forms()
        {
            //Skill_group = Usual_skills_group;

            Set_colors_for_chosen_item(Button_borders, Forms_border, Chosen_color, Unchosen_color);
        }
        private void _Show_force_forms()
        {
            //Skill_group = Force_skills_group;

            Set_colors_for_chosen_item(Button_borders, Force_forms_border, Chosen_color, Unchosen_color);
        }
    }
}
