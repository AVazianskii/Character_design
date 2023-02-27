using System;
using System.Collections.Generic;
using System.IO;

namespace Character_design
{
    internal class Character_equipment_ViewModel : BaseViewModel
    {
        //private static Character_equipment_ViewModel _instance;
        private Character _character;

        public string Img_path
        {
            get { return $@"{Directory.GetCurrentDirectory()}\Pictures\Common\Content_soon.jpg"; }
        }


        /*
        public static Character_equipment_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Character_equipment_ViewModel();
            }
            return _instance;
        }
        public static void OverWriteInstance()
        {
            if (_instance != null)
            {
                _instance = new Character_equipment_ViewModel();
            }
        }
        */


        public Character_equipment_ViewModel(Character character)
        {
            _character = character;
        }
    }
}
