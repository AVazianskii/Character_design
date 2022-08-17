using System;
using System.Collections.Generic;
using System.IO;

namespace Character_design
{
    internal class Force_forms_ViewModel : BaseViewModel
    {
        private static Force_forms_ViewModel _instance;


        public string Img_path
        {
            get { return $@"{Directory.GetCurrentDirectory()}\Pictures\Common\Content_soon.jpg"; }
        }



        public static Force_forms_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Force_forms_ViewModel();
            }
            return _instance;
        }


        private Force_forms_ViewModel()
        {

        }
    }
}
