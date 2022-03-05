using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Character;
using Races_libs;
using SW_Character_creation;

namespace Character_design
{
    public class MainWindow_ViewModel : Notify
    {
        private Notify current_VM;
        private Main_Menu_ViewModel Main_menu;
        //private 
        public Notify CurrentViewModel
        {
            get
            {
                return current_VM;
            }
            set
            {
                current_VM = value;
                OnPropertyChanged("CurrentViewModel");
            }
        }
        public MainWindow_ViewModel()
        {
            Main_menu = Main_Menu_ViewModel.GetInstance();
            current_VM = Main_menu;
        }
    }
}
