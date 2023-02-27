using System;
using System.Collections.Generic;
using System.IO;

namespace Character_design
{
    internal class Companion_ViewModel : BaseViewModel
    {
        //private static Companion_ViewModel _instance;
        private Character _character;


        public string Img_path
        {
            get { return $@"{Directory.GetCurrentDirectory()}\Pictures\Common\Content_soon.jpg"; }
        }


        /*
        public static Companion_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Companion_ViewModel();
            }
            return _instance;
        }
        public static void OverWriteInstance()
        {
            if (_instance != null)
            {
                _instance = new Companion_ViewModel();
            }
        }
        */


        public Companion_ViewModel(Character character)
        {
            _character = character;
        }
    }
}
