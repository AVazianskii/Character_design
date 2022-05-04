using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Character_design
{ 
    internal class Force_skill_ViewModel : BaseViewModel
    {
        private static Force_skill_ViewModel _instance;
        private SolidColorBrush neutral_force_border,
                                light_force_border,
                                dark_force_border;

        private List<SolidColorBrush> Button_borders;

        private Color Chosen_color,
                      Unchosen_color;



        public Command Show_neutral_force_skills { get; private set; }
        public Command Show_dark_force_skills { get; private set; }
        public Command Show_light_force_skills { get; private set; }
        public SolidColorBrush Neutral_force_border
        {
            get { return neutral_force_border; }
            set { neutral_force_border = value; OnPropertyChanged("Neutral_force_border"); }
        }
        public SolidColorBrush Light_force_border
        {
            get { return light_force_border; }
            set { light_force_border = value; OnPropertyChanged("Light_force_border"); }
        }
        public SolidColorBrush Dark_force_border
        {
            get { return dark_force_border; }
            set { dark_force_border = value; OnPropertyChanged("Dark_force_border"); }
        }
        public int Exp_points_left
        {
            get { return Character.GetInstance().Experience_left; }
        }



        public static Force_skill_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Force_skill_ViewModel();
            }
            return _instance;
        }



        private Force_skill_ViewModel()
        {
            Show_neutral_force_skills   = new Command(o => _Show_neutral_force_skills());
            Show_light_force_skills     = new Command(o => _Show_light_force_skills());
            Show_dark_force_skills      = new Command(o => _Show_dark_force_skills());

            Neutral_force_border    = new SolidColorBrush();
            Light_force_border      = new SolidColorBrush();
            Dark_force_border       = new SolidColorBrush();

            Button_borders = new List<SolidColorBrush>();
            Button_borders.Add(Neutral_force_border);
            Button_borders.Add(Light_force_border);
            Button_borders.Add(Dark_force_border);

            Chosen_color = Colors.Wheat;
            Unchosen_color = Colors.Black;

            Set_colors_for_chosen_item(Button_borders, Neutral_force_border, Chosen_color, Unchosen_color);
        }


        private void _Show_neutral_force_skills()
        {
            Set_colors_for_chosen_item(Button_borders, Neutral_force_border, Chosen_color, Unchosen_color);
        }
        private void _Show_light_force_skills()
        {
            Set_colors_for_chosen_item(Button_borders, Light_force_border, Chosen_color, Unchosen_color);
        }
        private void _Show_dark_force_skills()
        {
            Set_colors_for_chosen_item(Button_borders, Dark_force_border, Chosen_color, Unchosen_color);
        }
    }
}
