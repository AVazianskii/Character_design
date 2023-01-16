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

        private string combat_ability_choose_warning,
                       combat_ability_choose_advice;



        public Command Show_base_ability { get; private set; }
        public Command Show_adept_ability { get; private set; }
        public Command Show_master_ability { get; private set; }
        public Character_changing_command Delete_combat_ability { get; private set; }
        public Character_changing_command Learn_combat_ability { get; private set; }
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
        public string Combat_ability_choose_warning
        {
            get { return combat_ability_choose_warning; }
            set { combat_ability_choose_warning = value; OnPropertyChanged("Combat_ability_choose_warning"); }
        }
        public string Combat_ability_choose_advice
        {
            get { return combat_ability_choose_advice; }
            set { combat_ability_choose_advice = value; OnPropertyChanged("Combat_ability_choose_advice"); }
        }
        public int Num_skills_left
        {
            get { return Character.GetInstance().Limit_all_forms_left; }
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
            Show_base_ability   = new Command(o => _Show_base_ability(),
                                              o => _Base_exist()); 
            Show_adept_ability  = new Command(o => _Show_adept_ability(),
                                              o => _Adept_exist());
            Show_master_ability = new Command(o => _Show_master_ability(),
                                              o => _Master_exist());

            Delete_combat_ability   = new Character_changing_command(o => _Delete_combat_ability(Selected_combat_ability),
                                                                     o => _Enable_delete_combat_ability());
            Learn_combat_ability    = new Character_changing_command(o => _Learn_combat_ability(Selected_combat_ability),
                                                                     o => _Enable_learn_combat_ability(Selected_combat_sequence));

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
        private bool _Base_exist()
        {
            bool result = true;
            if (Selected_combat_sequence.Base_ability_lvl == null)
            {
                result = false;
            }
            return result;
        }
        private bool _Adept_exist()
        {
            bool result = true;
            if (Selected_combat_sequence.Adept_ability_lvl == null)
            {
                result = false;
            }
            return result;
        }
        private bool _Master_exist()
        {
            bool result = true;
            if (Selected_combat_sequence.Master_ability_lvl == null)
            {
                result = false;
            }
            return result;
        }
        private void _Delete_combat_ability(object o)
        {
            All_abilities_template ability = o as All_abilities_template;
            if (ability != null)
            {
                Character.GetInstance().Delete_combat_ability(ability, Selected_combat_sequence);
                ShowSomeAdvice();
                Selected_combat_sequence.Check_enable_state();
                Skill_ViewModel.GetInstance().Refresh_fields();
                OnPropertyChanged("Exp_points_left");
                OnPropertyChanged("Num_skills_left");
            }
        }
        private bool _Enable_delete_combat_ability()
        {
            bool result = true;
            
            if (Selected_combat_ability.Is_chosen == false)
            {
                result = false;
            }
            else if ((Selected_combat_ability == Selected_combat_sequence.Base_ability_lvl) & (Selected_combat_sequence.Adept_ability_lvl.Is_chosen | Selected_combat_sequence.Master_ability_lvl.Is_chosen))
            {
                Combat_ability_choose_warning = "Для удаления текущего уровня стиля, удалите более высокие уровни!";
                result = false;
            }
            else if ((Selected_combat_ability == Selected_combat_sequence.Adept_ability_lvl) & Selected_combat_sequence.Master_ability_lvl.Is_chosen)
            {
                Combat_ability_choose_warning = "Для удаления текущего уровня стиля, удалите более высокие уровни!";
                result = false;
            }
            else { Combat_ability_choose_warning = ""; }
            return result;
        }
        private void _Learn_combat_ability(object o)
        {
            All_abilities_template ability = o as All_abilities_template;
            if (ability != null)
            {
                Character.GetInstance().Learn_combat_ability(ability, Selected_combat_sequence);
                ShowSomeAdvice();
                Selected_combat_sequence.Check_enable_state();
                Skill_ViewModel.GetInstance().Refresh_fields();
                OnPropertyChanged("Exp_points_left");
                OnPropertyChanged("Num_skills_left");
            }
        }
        private bool _Enable_learn_combat_ability(Abilities_sequence_template sequence)
        {
            
            if (Character.GetInstance().Experience_left < Selected_combat_ability.Cost)
            {
                Combat_ability_choose_warning = "Недостаточно очков опыта для изучения!";
                return false;
            }
            if(Selected_combat_ability.Is_enable == false)
            {
                Combat_ability_choose_warning = "Изучение данного уровня стиля недоступно!";
                return false;
            }
            if (Selected_combat_ability.Is_chosen)
            {
                return false;
            }
            if (Character.GetInstance().Limit_all_forms_left == 0 && Character.GetInstance().Combat_sequences_with_points.Contains(sequence) == false)
            {
                Combat_ability_choose_warning = "Достигнут лимит по количеству изучаемых стилей!";
                return false;
            }
            Combat_ability_choose_warning = ""; 
            return true;
        }
        private void ShowSomeAdvice()
        {
            Combat_ability_choose_advice = "";

            if ((Selected_combat_ability == Selected_combat_sequence.Adept_ability_lvl) && (Selected_combat_ability.Is_enable == false))
            {
                Combat_ability_choose_advice = $"Для выбора данного стиля изучите {Selected_combat_sequence.Enable_condition_adept.Name}";
            }
            else if ((Selected_combat_ability == Selected_combat_sequence.Master_ability_lvl) && (Selected_combat_ability.Is_enable == false))
            {
                Combat_ability_choose_advice = $"Для выбора данного стиля изучите {Selected_combat_sequence.Enable_condition_master.Name}";
            }
            else if (Selected_combat_sequence.Master_ability_lvl.Is_chosen && Selected_combat_sequence.Master_ability_lvl != null)
            {
                Combat_ability_choose_advice = $"Достигнуто звание {Selected_combat_sequence.Master_ability_lvl.Name} в стиле {Selected_combat_sequence.Name}";
            }
            else if (Selected_combat_sequence.Adept_ability_lvl.Is_chosen && Selected_combat_sequence.Adept_ability_lvl != null)
            {
                Combat_ability_choose_advice = $"Достигнуто звание {Selected_combat_sequence.Adept_ability_lvl.Name} в стиле {Selected_combat_sequence.Name}";
            }
            else if (Selected_combat_sequence.Base_ability_lvl.Is_chosen && Selected_combat_sequence.Base_ability_lvl != null)
            {
                Combat_ability_choose_advice = $"Достигнуто звание {Selected_combat_sequence.Base_ability_lvl.Name} в стиле {Selected_combat_sequence.Name}";
            }
        }
        private void Refresh_fields()
        {
            ShowSomeAdvice();
            OnPropertyChanged("Selected_combat_sequence_title");
            OnPropertyChanged("Combat_ability_general_description");
            OnPropertyChanged("Selected_combat_ability_cost");
            OnPropertyChanged("Combat_ability_img_path");
            OnPropertyChanged("Combat_ability_description");
        }
    }
}
