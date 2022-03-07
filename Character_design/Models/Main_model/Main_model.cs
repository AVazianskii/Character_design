﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SW_Character_creation;
using Races_libs;
using Age_status_libs;
using Attribute_libs;

namespace Character_design
{
    internal class Main_model
    {
        private static Main_model instance;

        internal Race_manager Race_Manager;
        internal Age_status_manager Age_status_Manager;
        internal Attribute_manager Attribute_Manager;

        private Main_model()
        {
            Race_Manager = Race_manager.GetInstance();
            Load_async(Race_Manager);
        }

        public static Main_model GetInstance()
        {
            if (instance == null)
            {
                instance = new Main_model();
            }
            return instance;
        }
        private void Load_all_from(Abstract_manager Manager)
        {
            Manager.Run_download_and_upload_process();
        }
        private async void Load_async(Abstract_manager _Manager)
        {
            await Task.Run(() => Load_all_from(_Manager));
        }
    }
}
