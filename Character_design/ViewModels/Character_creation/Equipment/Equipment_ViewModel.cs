using System;
using System.Collections.Generic;
using System.IO;

namespace Character_design
{
    internal class Equipment_ViewModel : BaseViewModel
    {
        private static Equipment_ViewModel _instance;


        public string Img_path
        {
            get { return $@"{Directory.GetCurrentDirectory()}\Pictures\Common\Content_soon.jpg"; }
        }



        public static Equipment_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Equipment_ViewModel();
            }
            return _instance;
        }
        public static void OverWriteInstance()
        {
            if (_instance != null)
            {
                _instance = new Equipment_ViewModel();
            }
        }



        private Equipment_ViewModel()
        {

        }
    }
}
