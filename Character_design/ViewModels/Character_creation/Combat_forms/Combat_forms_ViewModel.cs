using System;
using System.Collections.Generic;
using SW_Character_creation;
using System.Windows.Media;
using System.IO;

namespace Character_design
{
    internal class Combat_forms_ViewModel : BaseViewModel
    {
        private static Combat_forms_ViewModel _instance;
        private Abilities_sequence_template selected_combat_sequence;
        private All_abilities_template selected_combat_ability;

        private SolidColorBrush base_border,
                                adept_border,
                                master_border;

        private List<SolidColorBrush> Button_borders;

        private Color   Chosen_color,
                        Unchosen_color;



        public Command Show_base_ability { get; private set; }
        public Command Show_adept_ability { get; private set; }
        public Command Show_master_ability { get; private set; }
        public SolidColorBrush Base_border
        {
            get { return base_border; }
            set { base_border = value; OnPropertyChanged("Base_border"); }
        }
        public SolidColorBrush Adept_border
        {
            get { return adept_border; }
            set { adept_border = value; OnPropertyChanged("Adept_border"); }
        }
        public SolidColorBrush Master_border
        {
            get { return master_border; }
            set { master_border = value; OnPropertyChanged("Master_border"); }
        }
        public int Exp_points_left
        {
            get { return Character.GetInstance().Experience_left; }
        }
        public List<Abilities_sequence_template> Current_combat_sequnces
        {
            get { return Main_model.GetInstance().Combat_ability_Manager.Get_sequences(); }
            
        }
        public All_abilities_template Selected_combat_ability
        {
            get { return selected_combat_ability; }
            set { selected_combat_ability = value; OnPropertyChanged("Selected_combat_ability"); Refresh_fields(); }
        }
        public Abilities_sequence_template Selected_combat_sequence
        {
            get { return selected_combat_sequence; }
            set
            {
                selected_combat_sequence = value;
                OnPropertyChanged("Selected_combat_sequence");
                if (selected_combat_sequence != null)
                {
                    if (selected_combat_sequence.Base_ability_lvl != null)
                    {
                        _Show_base_ability();
                    }
                    else if (selected_combat_sequence.Adept_ability_lvl != null)
                    {
                        _Show_adept_ability();
                    }
                    else
                    {
                        _Show_master_ability();
                    }
                    Refresh_fields();
                }
            }
        }
        public string Selected_combat_sequence_title
        {
            get { return Selected_combat_sequence.Name; }
        }
        public string Combat_ability_img_path
        {
            get { return Selected_combat_ability.Img_path; }
        }
        public string Combat_ability_general_description
        {
            get { return Selected_combat_ability.General_description; }
        }
        public int Selected_combat_ability_cost
        {
            get { return Selected_combat_ability.Cost; }
        }
        public string Combat_ability_description
        {
            get {return Selected_combat_ability.Description; }
        }



        public static Combat_forms_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Combat_forms_ViewModel();
            }
            return _instance;
        }


        private Combat_forms_ViewModel()
        {
            Show_base_ability   = new Command(o => _Show_base_ability());
            Show_adept_ability  = new Command(o => _Show_adept_ability());
            Show_master_ability = new Command(o => _Show_master_ability());

            Base_border     = new SolidColorBrush();
            Adept_border    = new SolidColorBrush();
            Master_border   = new SolidColorBrush();

            Button_borders = new List<SolidColorBrush>();
            Button_borders.Add(Base_border);
            Button_borders.Add(Adept_border);
            Button_borders.Add(Master_border);

            Chosen_color = Colors.Black;
            Unchosen_color = Colors.Wheat;

            Selected_combat_sequence = Current_combat_sequnces[0];
            if (selected_combat_sequence.Base_ability_lvl != null)
            {
                _Show_base_ability();
            }
            else if (selected_combat_sequence.Adept_ability_lvl != null)
            {
                _Show_adept_ability();
            }
            else
            {
                _Show_master_ability();
            }

            Set_colors_for_chosen_item(Button_borders, Base_border, Chosen_color, Unchosen_color);
        }



        private void _Show_base_ability()
        {
            Selected_combat_ability = Selected_combat_sequence.Base_ability_lvl;

            Set_colors_for_chosen_item(Button_borders, Base_border, Chosen_color, Unchosen_color);
        }
        private void _Show_adept_ability()
        {
            Selected_combat_ability = Selected_combat_sequence.Adept_ability_lvl;

            Set_colors_for_chosen_item(Button_borders, Adept_border, Chosen_color, Unchosen_color);
        }
        private void _Show_master_ability()
        {
            Selected_combat_ability = Selected_combat_sequence.Master_ability_lvl;

            Set_colors_for_chosen_item(Button_borders, Master_border, Chosen_color, Unchosen_color);
        }
        private void Refresh_fields()
        {
            OnPropertyChanged("Selected_combat_sequence_title");
            OnPropertyChanged("Combat_ability_general_description");
            OnPropertyChanged("Selected_combat_ability_cost");
            OnPropertyChanged("Combat_ability_img_path");
            OnPropertyChanged("Combat_ability_description");
        }
    }
}
