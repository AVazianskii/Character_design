using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using SW_Character_creation;

namespace Character_design
{
    internal class Character_forms_ViewModel : BaseViewModel
    {
        private static Character_forms_ViewModel _instance;


        private List<Abilities_sequence_template> forms_group;
        private Abilities_sequence_template selected_form;
        private All_abilities_template current_form;
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
        public List<Abilities_sequence_template> Forms_group
        {
            get { return forms_group; }
            set { forms_group = value; OnPropertyChanged("Forms_group"); }
        }
        public Abilities_sequence_template Selected_form
        {
            get { return selected_form; }
            set { selected_form = value; OnPropertyChanged("Selected_form"); }
        }
        public All_abilities_template Current_form
        {
            get { return current_form; }
            set { current_form = value; OnPropertyChanged("Current_form"); }
        }
        public List<Abilities_sequence_template> Usual_forms_group
        {
            get { return Character.GetInstance().Combat_sequences_with_points; }
        }
        public List<Abilities_sequence_template> Force_forms_group
        {
            get { return Character.GetInstance().Force_sequences_with_points; }
        }
        public int Button_opacity
        {
            get { return button_opacity; }
            set { button_opacity = value; OnPropertyChanged("Button_opacity"); }
        }
        public bool Character_forceuser
        {
            get
            {
                Forms_group = Usual_forms_group;
                Set_colors_for_chosen_item(Button_borders, Forms_border, Chosen_color, Unchosen_color);
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
            Forms_group = Usual_forms_group;

            Set_colors_for_chosen_item(Button_borders, Forms_border, Chosen_color, Unchosen_color);
        }
        private void _Show_force_forms()
        {
            Forms_group = Force_forms_group;

            Set_colors_for_chosen_item(Button_borders, Force_forms_border, Chosen_color, Unchosen_color);
        }
    }
}
