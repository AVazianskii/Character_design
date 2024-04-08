using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using SW_Character_creation;

namespace Character_design
{
    internal class Character_forms_ViewModel : BaseViewModel
    {
        //private static Character_forms_ViewModel _instance;


        private List<Abilities_sequence_template> forms_group;
        private Abilities_sequence_template selected_form;
        private Abilities_sequence_template selected_value;
        private All_abilities_template current_form;
        private SolidColorBrush forms_border,
                                force_forms_border;

        private List<SolidColorBrush> Button_borders;

        private Color Chosen_color,
                      Unchosen_color;

        private int button_opacity;
        private Character _character;



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
            set
            {
                selected_form = value;
                OnPropertyChanged("Selected_form");
                if (selected_form != null)
                {

                    OnPropertyChanged("Selected_sequence_name");
                    OnPropertyChanged("Selected_sequence_img_path");
                    OnPropertyChanged("Selected_sequence_level");

                    if (selected_form.Base_ability_lvl != null)
                    {
                        OnPropertyChanged("Selected_sequence_description");
                        OnPropertyChanged("Base_exist");
                        OnPropertyChanged("Base_desc");
                    }
                    if (selected_form.Adept_ability_lvl != null)
                    {
                        OnPropertyChanged("Adept_exist");
                        OnPropertyChanged("Adept_desc");
                    }
                    if (selected_form.Master_ability_lvl != null)
                    {
                        OnPropertyChanged("Master_exist");
                        OnPropertyChanged("Master_desc");
                    }
                }
            }
        }
        public Abilities_sequence_template Selected_value
        {
            get { return selected_value; }
            set { selected_value = value; OnPropertyChanged("Selected_value"); }
        }
        public All_abilities_template Current_form
        {
            get { return current_form; }
            set { current_form = value; OnPropertyChanged("Current_form"); }
        }
        public List<Abilities_sequence_template> Usual_forms_group
        {
            get { return _character.Combat_sequences_with_points; }
        }
        public List<Abilities_sequence_template> Force_forms_group
        {
            get { return _character.Force_sequences_with_points; }
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
                if (_character.Forceuser)
                {
                    Button_opacity = 100;
                }
                else
                {
                    Button_opacity = 0;
                }
                return _character.Forceuser;
            }
        }
        public string Selected_sequence_name
        {
            get
            {
                if (Selected_form != null)
                {
                    return Selected_form.Name;
                }
                return "";
            }
        }
        public string Selected_sequence_img_path
        {
            get
            {
                if (Selected_form != null)
                {
                    return Selected_form.Icon_path;
                }
                return "";
            }
        }
        public string Selected_sequence_description
        {
            get
            {
                if (Selected_form != null)
                {
                    if (Selected_form.Base_ability_lvl != null)
                    {
                        return Selected_form.Base_ability_lvl.General_description;
                    }
                    if (Selected_form.Adept_ability_lvl != null)
                    {
                        return Selected_form.Adept_ability_lvl.General_description;
                    }
                    if (Selected_form.Master_ability_lvl != null)
                    {
                        return Selected_form.Master_ability_lvl.General_description;
                    }
                }
                return "";
            }
        }
        public string Selected_sequence_level
        {
            get
            {
                if (Selected_form != null)
                {
                    return Selected_form.Level;
                }
                return "";
            }
        }
        public int Base_exist
        {
            get
            {
                if (Selected_form != null)
                {
                    if (Selected_form.Base_ability_lvl != null)
                    {
                        return 100 * Convert.ToInt32(Selected_form.Base_ability_lvl.Is_chosen);
                    }
                }
                return 0;
            }
        }
        public int Adept_exist
        {
            get
            {
                if (Selected_form != null)
                {
                    if (Selected_form.Adept_ability_lvl != null)
                    {
                        return 100 * Convert.ToInt32(Selected_form.Adept_ability_lvl.Is_chosen);
                    }
                }
                return 0;
            }
        }
        public int Master_exist
        {
            get
            {
                if (Selected_form != null)
                {
                    if (Selected_form.Master_ability_lvl != null)
                    {
                        return 100 * Convert.ToInt32(Selected_form.Master_ability_lvl.Is_chosen);
                    }
                }
                return 0;
            }
        }
        public string Base_desc
        {
            get
            {
                if (Selected_form != null)
                {
                    if (Selected_form.Base_ability_lvl != null)
                    {
                        return Selected_form.Base_ability_lvl.Description;
                    }
                }
                return "";
            }
        }
        public string Adept_desc
        {
            get
            {
                if (Selected_form != null)
                {
                    if (Selected_form.Adept_ability_lvl != null)
                    {
                        return Selected_form.Adept_ability_lvl.Description;
                    }

                }
                return "";
            }
        }
        public string Master_desc
        {
            get
            {
                if (Selected_form != null)
                {
                    if (Selected_form.Master_ability_lvl != null)
                    {
                        return Selected_form.Master_ability_lvl.Description;
                    }
                }
                return "";
            }
        }


        /*
        public static Character_forms_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Character_forms_ViewModel();
            }
            return _instance;
        }
        public static void OverWriteInstance()
        {
            if (_instance != null)
            {
                _instance = new Character_forms_ViewModel();
            }
        }
        */


        public void Check_initial_state()
        {
            if (Usual_forms_group.Count > 0)
            {
                Forms_group = Usual_forms_group;
                Selected_form = Forms_group[0];
                Set_colors_for_chosen_item(Button_borders, Forms_border, Chosen_color, Unchosen_color);
            }
            else if (Force_forms_group.Count > 0)
            {
                Forms_group = Force_forms_group;
                Selected_form = Forms_group[0];
                Set_colors_for_chosen_item(Button_borders, Force_forms_border, Chosen_color, Unchosen_color);
            }
        }



        public Character_forms_ViewModel(Character character)
        {
            _character = character;
            Forms_border = new SolidColorBrush();
            Force_forms_border = new SolidColorBrush();

            Button_borders = new List<SolidColorBrush>();
            Button_borders.Add(Forms_border);
            Button_borders.Add(Force_forms_border);

            Show_forms = new Command(o => _Show_forms(), o => Usual_forms_group.Count > 0);
            Show_force_forms = new Command(o => _Show_force_forms(), o => Force_forms_group.Count > 0);

            Chosen_color = Colors.Wheat;
            Unchosen_color = Colors.Black;

            Forms_group = new List<Abilities_sequence_template>();
            selected_form = new Abilities_sequence_template();
            selected_value = new Abilities_sequence_template();

            Check_initial_state();
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
