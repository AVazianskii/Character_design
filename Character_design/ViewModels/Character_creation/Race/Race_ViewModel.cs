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
        




            
    }
}
