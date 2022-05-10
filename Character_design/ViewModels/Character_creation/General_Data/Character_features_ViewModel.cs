using System;
using System.Collections.Generic;
using System.IO;

namespace Character_design
{
    internal class Character_features_ViewModel : BaseViewModel
    {
        private static Character_features_ViewModel _instance;



        public string Img_path
        {
            get { return $@"{Directory.GetCurrentDirectory()}\Pictures\Common\Content_soon.jpg"; }
        }



        public static Character_features_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Character_features_ViewModel();
            }
            return _instance;
        }


        private Character_features_ViewModel()
        {

        }
    }
}
