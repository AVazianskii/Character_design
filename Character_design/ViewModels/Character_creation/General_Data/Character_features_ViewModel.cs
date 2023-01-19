using System;
using System.Collections.Generic;
using System.IO;
using SW_Character_creation;
using System.Windows.Media;

namespace Character_design
{
    internal class Character_features_ViewModel : BaseViewModel
    {
        private static Character_features_ViewModel _instance;

        private List<All_feature_template> selected_feature_list;
        private All_feature_template selected_feature;

        private SolidColorBrush positive_border,
                                negative_border;

        private List<SolidColorBrush> Button_borders;

        private Color Chosen_color,
                      Unchosen_color;



        public Command Show_positive { get; private set; }
        public Command Show_negative { get; private set; }
        public All_feature_template Selected_feature
        {
            get { return selected_feature; }
            set { selected_feature = value; OnPropertyChanged("Selected_feature"); }
        }
        public List<All_feature_template> Positive_features
        {
            get { return Character.GetInstance().Positive_features_with_points; }
        }
        public List<All_feature_template> Negative_features
        {
            get {  return Character.GetInstance().Negative_features_with_points; }
        }
        public List<All_feature_template> Selected_feature_list
        {
            get { return selected_feature_list; }
            set { selected_feature_list = value; OnPropertyChanged("Selected_feature_list"); }
        }
        public SolidColorBrush Positive_border
        {
            get { return positive_border; }
            set { positive_border = value; OnPropertyChanged("Positive_border"); }
        }
        public SolidColorBrush Negative_border
        {
            get { return negative_border; }
            set { negative_border = value; OnPropertyChanged("Negative_border"); }
        }
        public bool Positive_exists
        {
            get { return Character.GetInstance().Positive_features_with_points.Count > 0; }
        }
        public bool Negative_exists
        {
            get { return Character.GetInstance().Negative_features_with_points.Count > 0; }
        }
        public int Positive_exists_opacity
        {
            get { return Convert.ToByte(Positive_exists) * 100; }
        }
        public int Negative_exists_opacity
        {
            get { return Convert.ToByte(Negative_exists) * 100; }
        }




        public static Character_features_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Character_features_ViewModel();
            }
            return _instance;
        }



        private Character_features_ViewModel()
        {
            Selected_feature = new All_feature_template();
            Selected_feature_list = new List<All_feature_template>();
            Selected_feature_list = Positive_features;
            if (Selected_feature_list.Count > 0)
            {
                Selected_feature = Selected_feature_list[0];
            }

            Show_positive = new Command(o => _Show_positive());
            Show_negative = new Command(o => _Show_negative());

            Chosen_color = Colors.Wheat;
            Unchosen_color = Colors.Black;

            Positive_border = new SolidColorBrush();
            Negative_border = new SolidColorBrush();

            Button_borders = new List<SolidColorBrush>();
            Button_borders.Add(Positive_border);
            Button_borders.Add(Negative_border);

            Set_colors_for_chosen_item(Button_borders, Positive_border, Chosen_color, Unchosen_color);
        }



        private void _Show_positive()
        {
            Selected_feature_list = Positive_features;

            Set_colors_for_chosen_item(Button_borders, Positive_border, Chosen_color, Unchosen_color);
        }
        private void _Show_negative()
        {
            Selected_feature_list = Negative_features;

            Set_colors_for_chosen_item(Button_borders, Negative_border, Chosen_color, Unchosen_color);
        }
    }
}
