using System;
using System.Collections.Generic;
using System.IO;

namespace Character_design
{
    internal class Character_equipment_ViewModel : BaseViewModel
    {
        private static Character_equipment_ViewModel _instance;


        public string Img_path
        {
            get { return $@"{Directory.GetCurrentDirectory()}\Pictures\Common\Content_soon.jpg"; }
        }



        public static Character_equipment_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Character_equipment_ViewModel();
            }
            return _instance;
        }
        public static void DeleteInstance()
        {
            if (_instance != null)
            {
                _instance = null;
            }
        }



        private Character_equipment_ViewModel()
        {

        }
    }
}
