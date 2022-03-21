using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_design
{
    internal class Character_info_ViewModel : Notify
    {
        private static Character_info_ViewModel _instance;

        private string character_name;
        private string character_race_name;
        private string character_age_status;
        private int character_age,
                    character_exp,
                    character_atr;

        public string Character_name
        {
            get { return character_name; }
            set { character_name = value; OnPropertyChanged("Character_name"); }
        }
        public string Character_race_name
        {
            get { character_race_name = Character.GetInstance().Character_race.Race_name; return character_race_name; }
            set { character_race_name = value; OnPropertyChanged("Character_race_name"); }
        }
        public int Character_age
        {
            get { return character_age; }
            set { character_age = value; OnPropertyChanged("Character_age"); }
        }
        public string Character_age_status
        {
            get { return character_age_status; }
            set { character_age_status = value; OnPropertyChanged("Character_age_status"); }
        }
        public int Character_exp
        {
            get { return character_exp; }
            set { character_exp = value; OnPropertyChanged("Character_exp"); }
        }
        public int Character_atr
        {
            get { return character_atr; }
            set { character_atr = value; OnPropertyChanged("Character_atr"); }
        }

        public static Character_info_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Character_info_ViewModel();
            }
            return _instance;
        }


        private Character_info_ViewModel()
        {
            Character_name = "Дарт Сидиус";
        }
    }
}
