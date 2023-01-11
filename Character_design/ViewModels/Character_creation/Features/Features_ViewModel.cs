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

        private sbyte selected_feature_cost;

        private byte comboBoxOpacity,
                     textBlockOpacity;

        private bool comboBoxEnabled,
                     textBlockEnabled;

        private string feature_choose_warning,
                       feature_choose_advice,
                       charm_feature_block,
                       hero_feature_block,
                       sleep_feature_block,
                       alcohol_feature_block,
                       sith_feature_block,
                       jedi_feature_block;

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
            get
            {
                if (current_feature_list == positive_features)
                {
                    return Character.GetInstance().Limit_positive_features_left;
                }
                else
                {
                    return Character.GetInstance().Limit_negative_features_left;
                }
            }
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
                    OnPropertyChanged("Exp_points_left");
                    OnPropertyChanged("Num_skills_left");
                    OnPropertyChanged("Total_feature_score");
                }
            }
        }
        public int Exp_points_left
        {
            get 
            {   
                if (current_feature_list == positive_features)
                {
                    return Character.GetInstance().Positive_features_points_left;
                }
                else
                {
                    return Character.GetInstance().Negative_features_points_left;
                }
            }
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
        public int Total_feature_score
        {
            get { return Character.GetInstance().Return_total_feature_score(); }
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
                                                            o => Selected_feature.Is_chosen);

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

            Set_colors_for_chosen_item(Button_borders, Positive_feature_border, Chosen_color, Unchosen_color);

            hero_feature_block = "";
            charm_feature_block = "";
            sleep_feature_block = "";
            alcohol_feature_block = "";
            sith_feature_block = "";
            jedi_feature_block = "";
        }



        private void _Show_positive_features()
        {
            Set_colors_for_chosen_item(Button_borders, Positive_feature_border, Chosen_color, Unchosen_color);

            Current_feature_list = positive_features;
            Selected_feature = Current_feature_list[0];
            Select_show_cost(Selected_feature);
        }
        private void _Show_negative_features()
        {
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
                if (current_feature_list == positive_features)
                {
                    Character.GetInstance().Spend_positive_feature_points(Selected_feature_cost);
                    Character.GetInstance().Learn_positive_feature(feature);
                }
                else
                {
                    Character.GetInstance().Spend_negative_feature_points(Selected_feature_cost);
                    Character.GetInstance().Learn_negative_feature(feature);
                }
                feature.Chosen_cost = Selected_feature_cost;
                Block_features(feature, Character.GetInstance().Charm_features,  ref charm_feature_block);
                Block_features(feature, Character.GetInstance().Hero_features,   ref hero_feature_block);
                Block_features(feature, Character.GetInstance().Sleep_feature,   ref sleep_feature_block);
                Block_features(feature, Character.GetInstance().Alcohol_feature, ref alcohol_feature_block);
                Block_features(feature, Character.GetInstance().Sith_feature,    ref sith_feature_block);
                Block_features(feature, Character.GetInstance().Jedi_feature,    ref jedi_feature_block);
                OnPropertyChanged("Exp_points_left");
                OnPropertyChanged("Num_skills_left");
                OnPropertyChanged("Total_feature_score");

                Character_info_ViewModel.GetInstance().Refresh_atr_exp_points();
                Character_info_ViewModel.GetInstance().Refresh_karma_points();
            }
        }
        private void _Delete_feature(object o)
        {
            All_feature_template feature = o as All_feature_template;
            if (feature != null)
            {
                if (current_feature_list == positive_features)
                {
                    Character.GetInstance().Refund_positive_feature_points(feature.Chosen_cost);
                    Character.GetInstance().Delete_positive_feature(feature);
                }
                else
                {
                    Character.GetInstance().Refund_negative_feature_points(feature.Chosen_cost);
                    Character.GetInstance().Delete_negative_feature(feature);
                }
                UnBlock_features(feature, Character.GetInstance().Charm_features);
                UnBlock_features(feature, Character.GetInstance().Hero_features);
                UnBlock_features(feature, Character.GetInstance().Sleep_feature);
                UnBlock_features(feature, Character.GetInstance().Alcohol_feature);
                UnBlock_features(feature, Character.GetInstance().Sith_feature);
                UnBlock_features(feature, Character.GetInstance().Jedi_feature);
                OnPropertyChanged("Exp_points_left");
                OnPropertyChanged("Num_skills_left");
                OnPropertyChanged("Total_feature_score");

                Character_info_ViewModel.GetInstance().Refresh_atr_exp_points();
                Character_info_ViewModel.GetInstance().Refresh_karma_points();
            }
        }
        private bool Feature_learning_is_posible(All_feature_template feature)
        {
            if (feature.Is_chosen)
            {
                if (feature.Cost.Count > 1)
                {
                    Feature_choose_warning = "";
                    Feature_choose_advice = $"Особенность выбрана!\nСтоимость особенности: {feature.Chosen_cost}";
                }
                else
                {
                    Feature_choose_warning = "";
                    Feature_choose_advice = "Особенность выбрана!";
                }
                return false;
            }
            if (feature.Is_force_usered_only && Character.GetInstance().Forceuser != true)
            {
                Feature_choose_advice = "";
                Feature_choose_warning = "Особенность предназначена для адептов Силы!";
                return false;
            }
            if (feature.Is_usual_usered_only && Character.GetInstance().Forceuser == true)
            {
                Feature_choose_advice = "";
                Feature_choose_warning = "Особенность не предназначена для адептов Силы!";
                return false;
            }
            if (Character.GetInstance().Is_sith)
            {
                if (feature.ID == 89 || feature.ID == 90 || feature.ID == 102)
                {
                    Feature_choose_advice = "";
                    Feature_choose_warning = "Особенность предназначена для адептов Светлой стороны Силы!";
                    return false;
                }
            }
            if (Character.GetInstance().Is_jedi)
            {
                if (feature.ID == 91 || feature.ID == 100 || feature.ID == 101)
                {
                    Feature_choose_advice = "";
                    Feature_choose_warning = "Особенность предназначена для адептов Темной стороны Силы!";
                    return false;
                }
            }
            if (feature.Is_enabled == false)
            {
                Feature_choose_advice = "";
                Feature_choose_warning = "";
                if (hero_feature_block != "" && Character.GetInstance().Hero_features.Contains(feature))
                {
                    Feature_choose_warning = $"Изучение заблокировано особенностью:\n{hero_feature_block}";
                }
                if (charm_feature_block != "" && Character.GetInstance().Charm_features.Contains(feature))
                {
                    Feature_choose_warning = $"Изучение заблокировано особенностью:\n{charm_feature_block}";
                }
                if (sleep_feature_block != "" && Character.GetInstance().Sleep_feature.Contains(feature))
                {
                    Feature_choose_warning = $"Изучение заблокировано особенностью:\n{sleep_feature_block}";
                }
                if (alcohol_feature_block != "" && Character.GetInstance().Alcohol_feature.Contains(feature))
                {
                    Feature_choose_warning = $"Изучение заблокировано особенностью:\n{alcohol_feature_block}";
                }
                if (sith_feature_block != "" && Character.GetInstance().Sith_feature.Contains(feature))
                {
                    Feature_choose_warning = $"Изучение заблокировано особенностью:\n{sith_feature_block}";
                }
                if (jedi_feature_block != "" && Character.GetInstance().Jedi_feature.Contains(feature))
                {
                    Feature_choose_warning = $"Изучение заблокировано особенностью:\n{jedi_feature_block}";
                }
                return false;
            }
            if (current_feature_list == positive_features) 
            {
                if(Character.GetInstance().Limit_positive_features_left == 0)
                {
                    Feature_choose_advice = "";
                    Feature_choose_warning = "Достигнут лимит изучения особенностей!";
                    return false;
                }
                if (Selected_feature_cost > Character.GetInstance().Positive_features_points_left)
                {
                    Feature_choose_advice = "";
                    Feature_choose_warning = "Недостаточно очков особенностей!";
                    return false;
                }
            }
            if (current_feature_list == negative_features)
            {
                if (Character.GetInstance().Limit_negative_features_left == 0)
                {
                    Feature_choose_advice = "";
                    Feature_choose_warning = "Достигнут лимит изучения особенностей!";
                    return false;
                }
                if (Selected_feature_cost > Character.GetInstance().Negative_features_points_left)
                {
                    Feature_choose_advice = "";
                    Feature_choose_warning = "Недостаточно очков особенностей!";
                    return false;
                }
            }
            Feature_choose_warning = "";
            Feature_choose_advice = "";
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
        private void Block_features(All_feature_template feature, List<All_feature_template> feature_list, ref string status)
        {
            bool flag = false;
            foreach (All_feature_template ftr in feature_list)
            {
                if ((ftr.ID == feature.ID) && (feature.Is_chosen))
                {
                    flag = true;
                    status = feature.Name;
                    break;
                }
            }
            if (flag)
            {
                foreach (All_feature_template ftr in feature_list)
                {
                    if (ftr.ID != feature.ID)
                    {
                        ftr.Is_enabled = false;
                    }
                }
            }
        }
        private void UnBlock_features(All_feature_template feature, List<All_feature_template> feature_list)
        {
            bool flag = false;
            foreach (All_feature_template ftr in feature_list)
            {
                if (ftr.ID == feature.ID)
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                foreach (All_feature_template ftr in feature_list)
                {
                    ftr.Is_enabled = true;
                }
            }
        }
    }
}
