using System;
using System.Collections.Generic;
using System.IO;

namespace Character_design
{
    internal class Nothing_chosen_ViewModel : BaseViewModel
    {
        private static Nothing_chosen_ViewModel _instance;


        public string Img_path
        {
            get { return $@"{Directory.GetCurrentDirectory()}\Pictures\Common\nothing_chosen.jpg"; }
        }


        public static Nothing_chosen_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Nothing_chosen_ViewModel();
            }
            return _instance;
        }


        private Nothing_chosen_ViewModel()
        {

        }
    }
}
