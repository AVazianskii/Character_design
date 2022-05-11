using System;
using System.Collections.Generic;
using System.IO;

namespace Character_design
{
    internal class Spaceship_ViewModel : BaseViewModel
    {
        private static Spaceship_ViewModel _instance;


        public string Img_path
        {
            get { return $@"{Directory.GetCurrentDirectory()}\Pictures\Common\Content_soon.jpg"; }
        }



        public static Spaceship_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Spaceship_ViewModel();
            }
            return _instance;
        }


        private Spaceship_ViewModel()
        {

        }
    }
}
