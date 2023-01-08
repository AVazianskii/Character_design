using System;
using System.Collections.Generic;
using SW_Character_creation;
using System.Windows.Media;
using System.IO;

namespace Character_design
{
    internal class Features_ViewModel : BaseViewModel
    {
        private static Features_ViewModel _instance;
        private SolidColorBrush positive_feature_border,
                                negative_feature_border;

        private List<SolidColorBrush> Button_borders;

        private List<All_feature_template> current_feature_list,
                                           positive_features,
                                           negative_features;

        private All_feature_template selected_feature;

        private List<sbyte> selected_feature_cost_list;

        private int num_skills_left;

        private sbyte selected_feature_cost;

        private byte comboBoxOpacity,
                     textBlockOpacity;

        private bool comboBoxEnabled,
                     textBlockEnabled;

        private string feature_choose_warning,
                       feature_choose_advice;

        private Color Chosen_color,
                      Unchosen_color;



        public Command Show_positive_features { get; private set; }
        public Command Show_negative_features { get; private set; }
        public Character_changing_command Learn_feature {get; private set;}
        public Character_changing_command Delete_feature { get; private set; }
        public SolidColorBrush Positive_feature_border
        {
            get { return positive_feature_border; }
            set { positive_feature_border = value; OnPropertyChanged("Positive_feature_border"); }
        }
        public SolidColorBrush Negative_feature_border
        {
            get { return negative_feature_border; }
            set { negative_feature_border = value; OnPropertyChanged("Negative_feature_border"); }
        }
        public int Num_skills_left
        {
            get { return num_skills_left; }
            set { num_skills_left = value; OnPropertyChanged("Num_skills_left"); }
        }
        public List<All_feature_template> Current_feature_list
        {
            get { return current_feature_list; }
            set { current_feature_list = value; OnPropertyChanged("Current_feature_list"); }
        }
        public All_feature_template Selected_feature
        {
            get { return selected_feature; }
            set 
            { 
                selected_feature = value; 
                OnPropertyChanged("Selected_feature"); 

                if (selected_feature != null)
                {
                    selected_feature_cost_list = selected_feature.Cost;
                    Selected_feature_cost = selected_feature_cost_list[0];
                    Select_show_cost(Selected_feature);
                    OnPropertyChanged("Selected_feature_description");
                    OnPropertyChanged("Selected_feature_title");
                    OnPropertyChanged("Selected_feature_img_path");
                    OnPropertyChanged("Selected_feature_cost_list");
                }
            }
        }
        public int Exp_points_left
        {
            //TODO: сделать логику выбора особенностей персонажа
            get { return 10; }
        }
        public string Question_sign
        {
            get { return $@"{Directory.GetCurrentDirectory()}\Pictures\Common\Tool_tip_old.jpg"; }
        }
        public string Feature_description
        {
            get { return "Сумма очков положительны и отрицательных особенностей персонажей должна равняться 0"; }
        }
        public string Selected_feature_description
        {
            get { return Selected_feature.Description; }
        }
        public string Selected_feature_title
        {
            get { return Selected_feature.Name; }
        }
        public string Selected_feature_img_path
        {
            get { return Selected_feature.Image_path; }
        }
        public string Feature_choose_warning
        {
            get { return feature_choose_warning; }
            set { feature_choose_warning = value; OnPropertyChanged("Feature_choose_warning"); }
        }
        public string Feature_choose_advice
        {
            get { return feature_choose_advice; }
            set { feature_choose_advice = value; OnPropertyChanged("Feature_choose_advice"); }
        }
        public List<sbyte> Selected_feature_cost_list
        {
            get { return selected_feature_cost_list; }
        }
        public sbyte Selected_feature_cost
        {
            get { return selected_feature_cost; }
            set { selected_feature_cost = value; OnPropertyChanged("Selected_feature_cost"); }
        }
        public byte ComboBoxOpacity
        {
            get { return comboBoxOpacity; }
            set { comboBoxOpacity = value; OnPropertyChanged("ComboBoxOpacity"); }
        }
        public byte TextBlockOpacity
        {
            get { return textBlockOpacity; }
            set { textBlockOpacity = value; OnPropertyChanged("TextBlockOpacity"); }
        }
        public bool ComboBoxEnabled
        {
            get { return comboBoxEnabled; }
            set { comboBoxEnabled = value; OnPropertyChanged("ComboBoxEnabled"); }
        }
        public bool TextBlockEnabled
        {
            get { return textBlockEnabled; }
            set { textBlockEnabled = value; OnPropertyChanged("TextBlockEnabled"); }
        }



        public static Features_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Features_ViewModel();
            }
            return _instance;
        }


        private Features_ViewModel()
        {
            Show_positive_features = new Command(o => _Show_positive_features());
            Show_negative_features = new Command(o => _Show_negative_features());

            Learn_feature  = new Character_changing_command(o => _Learn_feature(Selected_feature),
                                                            o => Feature_learning_is_posible(Selected_feature));
            Delete_feature = new Character_changing_command(o => _Delete_feature(Selected_feature),
                                                            o => Feature_deleting_is_posible(Selected_feature));

            Positive_feature_border = new SolidColorBrush();
            Negative_feature_border = new SolidColorBrush();
            
            Button_borders = new List<SolidColorBrush>();
            Button_borders.Add(Positive_feature_border);
            Button_borders.Add(Negative_feature_border);
            
            current_feature_list    = new List<All_feature_template>();
            positive_features       = new List<All_feature_template>();
            negative_features       = new List<All_feature_template>();
            
            positive_features   = Feature_manager.GetInstance().Get_positive_features();
            negative_features   = Feature_manager.GetInstance().Get_negative_features();
            
            Current_feature_list = positive_features;

            selected_feature = new All_feature_template();

            selected_feature = Current_feature_list[0];
            Select_show_cost(Selected_feature);
            selected_feature_cost_list = selected_feature.Cost;
            Selected_feature_cost = selected_feature_cost_list[0];

            Chosen_color = Colors.Wheat;
            Unchosen_color = Colors.Black;

            Num_skills_left = Character.GetInstance().Limit_positive_features_left;
            Set_colors_for_chosen_item(Button_borders, Positive_feature_border, Chosen_color, Unchosen_color);
        }



        private void _Show_positive_features()
        {
            Num_skills_left = Character.GetInstance().Limit_positive_features_left;
            Set_colors_for_chosen_item(Button_borders, Positive_feature_border, Chosen_color, Unchosen_color);

            Current_feature_list = positive_features;
            Selected_feature = Current_feature_list[0];
            Select_show_cost(Selected_feature);
        }
        private void _Show_negative_features()
        {
            Num_skills_left = Character.GetInstance().Limit_negative_features_left;
            Set_colors_for_chosen_item(Button_borders, Negative_feature_border, Chosen_color, Unchosen_color);

            Current_feature_list = negative_features;
            Selected_feature = Current_feature_list[0];
            Select_show_cost(Selected_feature);
        }
        private void _Learn_feature(object o)
        {
            All_feature_template feature = o as All_feature_template;
            if (feature != null)
            {
                Character.GetInstance().Learn_feature(feature);
            }
        }
        private void _Delete_feature(object o)
        {
            All_feature_template feature = o as All_feature_template;
            if (feature != null)
            {
                Character.GetInstance().Delete_feature(feature);
            }
        }
        private bool Feature_learning_is_posible(All_feature_template feature)
        {
            if (current_feature_list == positive_features)
            {
                if (Selected_feature_cost > Character.GetInstance().Limit_positive_features_left)
                {
                    Feature_choose_advice = "";
                    Feature_choose_warning = "Недостаточно очков особенностей!";
                    return false;
                }
            }
            if (current_feature_list == negative_features)
            {
                if (Selected_feature_cost > Character.GetInstance().Limit_negative_features_left)
                {
                    Feature_choose_advice = "";
                    Feature_choose_warning = "Недостаточно очков особенностей!";
                    return false;
                }
            }
            if (feature.Is_chosen)
            {
                Feature_choose_advice = "";
                Feature_choose_warning = "";
                return false;
            }
            Feature_choose_advice = "";
            Feature_choose_warning = "";
            return true;
        }
        private bool Feature_deleting_is_posible(All_feature_template feature)
        {
            if (current_feature_list == positive_features)
            {
                if(Character.GetInstance().Positive_features_with_points.Count == 0)
                {
                    Feature_choose_advice = "";
                    Feature_choose_warning = "";
                    return false;
                }
            }
            if (current_feature_list == negative_features)
            {
                if (Character.GetInstance().Negative_features_with_points.Count == 0)
                {
                    Feature_choose_advice = "";
                    Feature_choose_warning = "";
                    return false;
                }
            }
            if(feature.Is_chosen == false)
            {
                Feature_choose_advice = "";
                Feature_choose_warning = "";
                return false;
            }
            Feature_choose_advice = "";
            Feature_choose_warning = "";
            return true;
        }
        private void Select_show_cost(All_feature_template feature)
        {
            if(feature.Cost.Count > 1)
            {
                ComboBoxOpacity = 100;
                TextBlockOpacity = 0;
                ComboBoxEnabled = true;
                TextBlockEnabled = false;
            }
            else
            {
                ComboBoxOpacity = 0;
                TextBlockOpacity = 100;
                ComboBoxEnabled = false;
                TextBlockEnabled = true;
            }
        }
    }
}
