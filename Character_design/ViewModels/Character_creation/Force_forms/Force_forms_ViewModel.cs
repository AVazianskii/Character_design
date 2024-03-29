﻿using System;
using System.Collections.Generic;
using SW_Character_creation;
using System.Windows.Media;
using System.IO;

namespace Character_design
{
    public class Force_forms_ViewModel : BaseViewModel
    {
        //private static Force_forms_ViewModel _instance;


        private Abilities_sequence_template selected_force_sequence;
        private All_abilities_template selected_force_ability;

        private SolidColorBrush base_border,
                                adept_border,
                                master_border;

        private List<SolidColorBrush> Button_borders;

        private Color Chosen_color,
                      Unchosen_color;

        private string force_ability_choose_warning,
                       force_ability_choose_advice;

        private Main_model _model;
        private Character _character;



        public Command Show_base_ability { get; private set; }
        public Command Show_adept_ability { get; private set; }
        public Command Show_master_ability { get; private set; }
        public Character_changing_command Delete_force_ability { get; private set; }
        public Character_changing_command Learn_force_ability { get; private set; }
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
            get { return _character.Experience_left; }
        }
        public List<Abilities_sequence_template> Current_force_sequnces
        {
            get { return _model.Force_ability_Manager.Get_sequences(); }

        }
        public All_abilities_template Selected_force_ability
        {
            get { return selected_force_ability; }
            set { selected_force_ability = value; OnPropertyChanged("Selected_force_ability"); Refresh_fields(); }
        }
        public Abilities_sequence_template Selected_force_sequence
        {
            get { return selected_force_sequence; }
            set
            {
                selected_force_sequence = value;
                OnPropertyChanged("Selected_force_sequence");
                if (selected_force_sequence != null)
                {
                    if (selected_force_sequence.Base_ability_lvl != null)
                    {
                        _Show_base_ability();
                    }
                    else if (selected_force_sequence.Adept_ability_lvl != null)
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
        public string Selected_force_sequence_title
        {
            get { return Selected_force_sequence.Name; }
        }
        public string Force_ability_img_path
        {
            get { return Selected_force_ability.Img_path; }
        }
        public string Force_ability_general_description
        {
            get { return Selected_force_ability.General_description; }
        }
        public int Selected_force_ability_cost
        {
            get { return Selected_force_ability.Cost; }
        }
        public string Force_ability_description
        {
            get { return Selected_force_ability.Description; }
        }
        public string Force_ability_choose_warning
        {
            get { return force_ability_choose_warning; }
            set { force_ability_choose_warning = value; OnPropertyChanged("Force_ability_choose_warning"); }
        }
        public string Force_ability_choose_advice
        {
            get { return force_ability_choose_advice; }
            set { force_ability_choose_advice = value; OnPropertyChanged("Force_ability_choose_advice"); }
        }
        public int Num_skills_left
        {
            get { return _character.Limit_all_forms_left; }
        }


        /*
        public static Force_forms_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Force_forms_ViewModel();
            }
            return _instance;
        }
        public static void OverWriteInstance()
        {
            if (_instance != null)
            {
                _instance = new Force_forms_ViewModel();
            }
        }
        */


        public Force_forms_ViewModel(Character character, Main_model model)
        {
            _character = character;
            _model = model;
            Show_base_ability = new Command(o => _Show_base_ability(),
                                              o => _Base_exist());
            Show_adept_ability = new Command(o => _Show_adept_ability(),
                                              o => _Adept_exist());
            Show_master_ability = new Command(o => _Show_master_ability(),
                                              o => _Master_exist());

            Delete_force_ability = new Character_changing_command(o => _Delete_force_ability(Selected_force_ability),
                                                                  o => _Enable_delete_force_ability());
            Learn_force_ability = new Character_changing_command(o => _Learn_force_ability(Selected_force_ability),
                                                                 o => _Enable_learn_force_ability(Selected_force_sequence));

            Base_border = new SolidColorBrush();
            Adept_border = new SolidColorBrush();
            Master_border = new SolidColorBrush();

            Button_borders = new List<SolidColorBrush>();
            Button_borders.Add(Base_border);
            Button_borders.Add(Adept_border);
            Button_borders.Add(Master_border);

            Chosen_color = Colors.Black;
            Unchosen_color = Colors.Wheat;

            Selected_force_sequence = Current_force_sequnces[0];
            if (selected_force_sequence.Base_ability_lvl != null)
            {
                _Show_base_ability();
            }
            else if (selected_force_sequence.Adept_ability_lvl != null)
            {
                _Show_adept_ability();
            }
            else
            {
                _Show_master_ability();
            }

            Refresh_fields();
            Set_colors_for_chosen_item(Button_borders, Base_border, Chosen_color, Unchosen_color);
        }



        private void _Show_base_ability()
        {
            Selected_force_ability = Selected_force_sequence.Base_ability_lvl;

            Set_colors_for_chosen_item(Button_borders, Base_border, Chosen_color, Unchosen_color);
        }
        private void _Show_adept_ability()
        {
            Selected_force_ability = Selected_force_sequence.Adept_ability_lvl;

            Set_colors_for_chosen_item(Button_borders, Adept_border, Chosen_color, Unchosen_color);
        }
        private void _Show_master_ability()
        {
            Selected_force_ability = Selected_force_sequence.Master_ability_lvl;

            Set_colors_for_chosen_item(Button_borders, Master_border, Chosen_color, Unchosen_color);
        }
        private bool _Base_exist()
        {
            bool result = true;
            if (Selected_force_sequence.Base_ability_lvl == null)
            {
                result = false;
            }
            return result;
        }
        private bool _Adept_exist()
        {
            bool result = true;
            if (Selected_force_sequence.Adept_ability_lvl == null)
            {
                result = false;
            }
            return result;
        }
        private bool _Master_exist()
        {
            bool result = true;
            if (Selected_force_sequence.Master_ability_lvl == null)
            {
                result = false;
            }
            return result;
        }
        private void _Delete_force_ability(object o)
        {
            All_abilities_template ability = o as All_abilities_template;
            if (ability != null)
            {
                _character.Delete_force_ability(ability, Selected_force_sequence);

                ShowSomeAdvice();
                CheckAbilityCondition();

                OnPropertyChanged("Exp_points_left");
                OnPropertyChanged("Num_skills_left");
            }
        }
        private bool _Enable_delete_force_ability()
        {
            bool result = true;

            if ((Selected_force_ability == Selected_force_sequence.Base_ability_lvl) & (Selected_force_sequence.Adept_ability_lvl.Is_chosen | Selected_force_sequence.Master_ability_lvl.Is_chosen))
            {
                Force_ability_choose_warning = "Для удаления текущего уровня стиля, удалите более высокие уровни!";
                result = false;
            }
            else if ((Selected_force_ability == Selected_force_sequence.Adept_ability_lvl) & Selected_force_sequence.Master_ability_lvl.Is_chosen)
            {
                Force_ability_choose_warning = "Для удаления текущего уровня стиля, удалите более высокие уровни!";
                result = false;
            }
            else if (Selected_force_ability.Is_chosen == false)
            {
                result = false;
            }
            else { Force_ability_choose_warning = ""; }
            return result;
        }
        private void _Learn_force_ability(object o)
        {
            All_abilities_template ability = o as All_abilities_template;
            if (ability != null)
            {
                _character.Learn_force_ability(ability, Selected_force_sequence);

                ShowSomeAdvice();
                CheckAbilityCondition();

                OnPropertyChanged("Exp_points_left");
                OnPropertyChanged("Num_skills_left");
            }
        }
        private bool _Enable_learn_force_ability(Abilities_sequence_template sequence)
        {
            if (_character.Experience_left < Selected_force_ability.Cost)
            {
                Force_ability_choose_warning = "Недостаточно очков опыта для изучения!";
                return false;
            }
            if (Selected_force_ability.Is_enable == false)
            {
                Force_ability_choose_warning = "Изучение данного уровня стиля недоступно!";
                return false;
            }
            if (Selected_force_ability.Is_chosen)
            {
                return false;
            }
            if (_character.Limit_all_forms_left == 0 && _character.Force_sequences_with_points.Contains(sequence) == false)
            {
                Force_ability_choose_warning = "Достигнут лимит по количеству изучаемых стилей!";
                return false;
            }
            Force_ability_choose_warning = "";
            return true;
        }
        private void ShowSomeAdvice()
        {
            Force_ability_choose_advice = "";

            if ((Selected_force_ability == Selected_force_sequence.Adept_ability_lvl) && (Selected_force_ability.Is_enable == false))
            {
                Force_ability_choose_advice = $"Для выбора данного стиля изучите {Selected_force_sequence.Enable_condition_adept.Name}";
            }
            else if ((Selected_force_ability == Selected_force_sequence.Master_ability_lvl) && (Selected_force_ability.Is_enable == false))
            {
                Force_ability_choose_advice = $"Для выбора данного стиля изучите {Selected_force_sequence.Enable_condition_master.Name}";
            }
            else if (Selected_force_sequence.Master_ability_lvl.Is_chosen && Selected_force_sequence.Master_ability_lvl != null)
            {
                Force_ability_choose_advice = $"Достигнуто звание {Selected_force_sequence.Master_ability_lvl.Name} в стиле {Selected_force_sequence.Name}";
            }
            else if (Selected_force_sequence.Adept_ability_lvl.Is_chosen && Selected_force_sequence.Adept_ability_lvl != null)
            {
                Force_ability_choose_advice = $"Достигнуто звание {Selected_force_sequence.Adept_ability_lvl.Name} в стиле {Selected_force_sequence.Name}";
            }
            else if (Selected_force_sequence.Base_ability_lvl.Is_chosen && Selected_force_sequence.Base_ability_lvl != null)
            {
                Force_ability_choose_advice = $"Достигнуто звание {Selected_force_sequence.Base_ability_lvl.Name} в стиле {Selected_force_sequence.Name}";
            }
        }
        private void CheckAbilityCondition()
        {
            foreach (Abilities_sequence_template sequence in Current_force_sequnces)
            {
                sequence.Check_enable_state();
            }
        }
        private void Refresh_fields()
        {
            ShowSomeAdvice();
            OnPropertyChanged("Selected_force_sequence_title");
            OnPropertyChanged("Force_ability_general_description");
            OnPropertyChanged("Selected_force_ability_cost");
            OnPropertyChanged("Force_ability_img_path");
            OnPropertyChanged("Force_ability_description");
        }
    }
}

