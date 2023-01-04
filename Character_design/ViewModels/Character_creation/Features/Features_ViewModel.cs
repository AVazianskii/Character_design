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
                                negative_feature_border,
                                common_feature_border;

        private List<SolidColorBrush> Button_borders;

        private List<All_feature_template> current_feature_list,
                                           positive_features,
                                           negative_features,
                                           common_features;

        private int num_skills_left;

        private Color Chosen_color,
                      Unchosen_color;



        public Command Show_positive_features { get; private set; }
        public Command Show_negative_features { get; private set; }
        public Command Show_common_features { get; private set; }
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
        public SolidColorBrush Common_feature_border
        {
            get { return common_feature_border; }
            set { common_feature_border = value; OnPropertyChanged("Common_feature_border"); }
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
            Show_common_features   = new Command(o => _Show_common_features());

            Positive_feature_border = new SolidColorBrush();
            Negative_feature_border = new SolidColorBrush();
            Common_feature_border   = new SolidColorBrush();

            Button_borders = new List<SolidColorBrush>();
            Button_borders.Add(Positive_feature_border);
            Button_borders.Add(Negative_feature_border);
            Button_borders.Add(Common_feature_border);

            Current_feature_list    = new List<All_feature_template>();
            positive_features       = new List<All_feature_template>();
            negative_features       = new List<All_feature_template>();
            common_features         = new List<All_feature_template>();

            positive_features   = Feature_manager.GetInstance().Get_positive_features();
            negative_features   = Feature_manager.GetInstance().Get_negative_features();
            common_features     = Feature_manager.GetInstance().Get_common_features();

            Current_feature_list = positive_features;

            Chosen_color = Colors.Wheat;
            Unchosen_color = Colors.Black;

            Num_skills_left = Character.GetInstance().Limit_positive_features_left;
            Set_colors_for_chosen_item(Button_borders, Positive_feature_border, Chosen_color, Unchosen_color);
        }



        private void _Show_positive_features()
        {
            Num_skills_left = Character.GetInstance().Limit_positive_features_left;
            Set_colors_for_chosen_item(Button_borders, Positive_feature_border, Chosen_color, Unchosen_color);


        }
        private void _Show_negative_features()
        {
            Num_skills_left = Character.GetInstance().Limit_negative_features_left;
            Set_colors_for_chosen_item(Button_borders, Negative_feature_border, Chosen_color, Unchosen_color);
        }
        private void _Show_common_features()
        {
            // TODO: Строка ниже под вопросом
            Num_skills_left = Character.GetInstance().Limit_negative_features_left;
            Set_colors_for_chosen_item(Button_borders, Common_feature_border, Chosen_color, Unchosen_color);
        }
    }
}
