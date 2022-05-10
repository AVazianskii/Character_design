using System;
using System.Collections.Generic;
using System.IO;

namespace Character_design
{
    internal class Character_spaceship_ViewModel : BaseViewModel
    {
        private static Character_spaceship_ViewModel _instance;



        public string Img_path
        {
            get { return $@"{Directory.GetCurrentDirectory()}\Pictures\Common\Content_soon.jpg"; }
        }



        public static Character_spaceship_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Character_spaceship_ViewModel();
            }
            return _instance;
        }


        private Character_spaceship_ViewModel()
        {

        }
    }
}
