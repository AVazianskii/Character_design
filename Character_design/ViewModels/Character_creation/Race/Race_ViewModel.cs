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
        List<Race_class> destination_race_list = new List<Race_class>();
        private void Initial_load_race_list(List<Race_class> _destination_race_list)
        {
            foreach (Race_class race in Character_properties.GetInstance().Race_Manager.Get_Race_list())
            {
                _destination_race_list.Add(race);
            }
            _destination_race_list.RemoveAt(0);
        }

        private static Race_ViewModel _instance;
        private Notify current_VM;
        private string selected_race_description;
        private Race_class selected_race;

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
            Initial_load_race_list(destination_race_list);
            Selected_race = Character_properties.GetInstance().Race_Manager.Get_Empty_race();
            Show_human_race = new Command(o => _Show_human_race());
        }
        public List<Race_class> Races
        {
            get
            {
                return destination_race_list;
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
        public Race_class Selected_race
        {
            get
            {
                return selected_race;
            }
            set
            {
                selected_race = value;
                Selected_race_description = selected_race.Get_general_description();
                OnPropertyChanged("Selected_race");
                OnPropertyChanged("Selected_race_description");
            }
        }






    }
}
