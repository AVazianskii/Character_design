using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Races_libs;
using Character;


namespace Character_design
{
    internal class Race_ViewModel : Notify
    {
        private static Race_ViewModel _instance;
        private Notify current_VM;
        private string selected_race_description;

        public Command Show_human_race { get; private set; }
        public static Race_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Race_ViewModel();
            }
            return _instance;
        }
        public Notify CurrentViewModel
        {
            get
            {
                return current_VM;
            }
            set
            {
                current_VM = value;
                OnPropertyChanged("CurrentViewModel");
            }
        }

        private Race_ViewModel()
        {
            Show_human_race = new Command(o => _Show_human_race());
        }
        public List<Race_class> Races
        {
            get
            {
                List<Race_class> temp_list = new List<Race_class>();
                foreach (Race_class race in Character_properties.GetInstance().Race_Manager.Get_Race_list())
                {
                    temp_list.Add(race);
                }
                temp_list.RemoveAt(0);
                return temp_list;
            }
        }
        public string Human_race_name
        {
            get
            {
                return Character_properties.GetInstance().Race_Manager.Get_Human_race().Get_race_name();
            }
        }
        public string Human_race_small_pic
        {
            get
            {
                return Character_properties.GetInstance().Race_Manager.Get_Human_race().Get_small_img_path();
            }
            
        }
        public string Selected_race_description
        {
            get
            {
                return selected_race_description;
            }
            set
            {
                selected_race_description = value;
                OnPropertyChanged("Selected_race_description");
            }
        }
        private void _Show_human_race()
        {
            Selected_race_description = Character_properties.GetInstance().Race_Manager.Get_Human_race().Get_general_description();
        }






    }
}
