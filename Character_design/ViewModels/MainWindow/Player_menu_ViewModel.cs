using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Win32;

namespace Character_design
{
    internal class Player_menu_ViewModel : BaseViewModel
    {
        private static Player_menu_ViewModel _instance;



        public Command Open_Common_Menu { get; private set; }
        public Command Open_Profi_Character_creation { get; private set; }
        public Command Open_Character_editor { get; private set; }
        public Command Open_Character_story_helper { get; private set; }
        


        public static Player_menu_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Player_menu_ViewModel();
            }
            return _instance;
        }
        public void DeleteInstance()
        {
            if (_instance != null)
            {
                _instance = null;
            }
        }



        private Player_menu_ViewModel()
        {
            Open_Common_Menu = new Command(o => Main_Menu_ViewModel.GetInstance()._Open_Common_Menu());
            Open_Profi_Character_creation = new Command(o => Main_Menu_ViewModel.GetInstance()._Open_main_window_creation_user_control());
            Open_Character_editor = new Command(o => Edit_Excel_character_card());
            Open_Character_story_helper = new Command(o => _Test());
        }



        private void _Test()
        {

        }
        private void Edit_Excel_character_card()
        {
            string character_card_path = "";
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Character file (*.character)|*.character";
            if (dlg.ShowDialog() == true)
            {
                character_card_path = dlg.FileName;
            }
            Character_card character_card = new Character_card();

            Run_method_with_loading("Создание персонажа", () =>
            {
                Character_creation_model.GetInstance().Create_character_creating_model();
            });

            Character_creation_model.GetInstance().Create_character(Character_creation_model.GetInstance().creation_managers);

            Run_method_with_loading("Редактирование персонажа", () =>
            {
                character_card.Edit_character_card_from_Excel(character_card_path, Character_creation_model.GetInstance().character, Character_creation_model.GetInstance().creation_managers);
            });
            Main_Menu_ViewModel.GetInstance()._Open_main_window_creation_user_control();
        }
    }
}
