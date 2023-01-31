using System;
using System.Collections.Generic;
using System.IO;

namespace Character_design
{
    internal class Character_companion_ViewModel : BaseViewModel
    {
        private static Character_companion_ViewModel _instance;

        

        public string Img_path
        {
            get { return $@"{Directory.GetCurrentDirectory()}\Pictures\Common\Content_soon.jpg"; }
        }


        public static Character_companion_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Character_companion_ViewModel();
            }
            return _instance;
        }
        public static void OverWriteInstance()
        {
            if (_instance != null)
            {
                _instance = new Character_companion_ViewModel();
            }
        }



        private Character_companion_ViewModel()
        {

        }
    }
}
