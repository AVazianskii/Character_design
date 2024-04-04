using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_design
{
    internal class Character_creation_model
    {
        private static Character_creation_model instance;
        
        
        
        public Character character { get; private set; }
        public Main_model creation_managers { get; private set; }
        public Combat_forms_ViewModel Combat_Forms_ViewModel { get; private set; }
        public Nothing_chosen_ViewModel Nothing_Chosen_ViewModel { get; private set; }
        public Companion_ViewModel Companion_ViewModel{ get; private set; }
        public Equipment_ViewModel Equipment_ViewModel{ get; private set; }
        public Features_ViewModel Features_ViewModel{ get; private set; }
        public Force_forms_ViewModel Force_Forms_ViewModel{ get; private set; }
        public Force_skill_ViewModel Force_Skill_ViewModel{ get; private set; }
        public Race_ViewModel Race_ViewModel{ get; private set; }
        public Skill_ViewModel Skill_ViewModel{ get; private set; }
        public Character_companion_ViewModel Character_Companion_ViewModel{ get; private set; }
        public Character_equipment_ViewModel Character_Equipment_ViewModel{ get; private set; }
        public Character_features_ViewModel Character_Features_ViewModel{ get; private set; }
        public Character_forms_ViewModel Character_Forms_ViewModel{ get; private set; }
        public Character_info_ViewModel Character_Info_ViewModel{ get; private set; }
        public Character_skills_ViewModel Character_Skills_ViewModel{ get; private set; }
        public General_Data_ViewModel General_data_ViewModel{ get; private set; }
        public Main_Window_Creation_ViewModel Main_Window_Creation_ViewModel{ get; private set; }



        public static Character_creation_model GetInstance()
        {
            if (instance == null)
            {
                instance = new Character_creation_model();
            }
            return instance;
        }
        public void Create_character_creating_model()
        {
            creation_managers = new Main_model();
            creation_managers.Download_all();
        }
        public void Create_character(Main_model model)
        {
            character = new Character(model);

            Initialize_character_pages(character, model);
        }
        public void Delete_character()
        {
            Kill_character_pages();

            character = null;
            creation_managers = null;
        }



        private Character_creation_model()
        {

        }



        private void Initialize_character_pages(Character character, Main_model model)
        {
            Combat_Forms_ViewModel = new Combat_forms_ViewModel(character, model); //
            Companion_ViewModel = new Companion_ViewModel(character); //
            Equipment_ViewModel = new Equipment_ViewModel(character); //
            Features_ViewModel = new Features_ViewModel(character, model); //
            Force_Forms_ViewModel = new Force_forms_ViewModel(character, model); //
            Force_Skill_ViewModel = new Force_skill_ViewModel(character, model); //
            Race_ViewModel = new Race_ViewModel(character, model); //
            Skill_ViewModel = new Skill_ViewModel(character, model); //
            Character_Companion_ViewModel = new Character_companion_ViewModel(character); //
            Character_Equipment_ViewModel = new Character_equipment_ViewModel(character); //
            Character_Features_ViewModel = new Character_features_ViewModel(character); //
            Character_Forms_ViewModel = new Character_forms_ViewModel(character); //
            Character_Skills_ViewModel = new Character_skills_ViewModel(character); //
            Character_Info_ViewModel = new Character_info_ViewModel(character, model); //
            General_data_ViewModel = new General_Data_ViewModel(character); //
            Main_Window_Creation_ViewModel = new Main_Window_Creation_ViewModel(character); //
        }
        private void Kill_character_pages()
        {
            Combat_Forms_ViewModel = null;
            Companion_ViewModel = null;
            Equipment_ViewModel = null;
            Features_ViewModel = null;
            Force_Forms_ViewModel = null;
            Force_Skill_ViewModel = null;
            Race_ViewModel = null;
            Skill_ViewModel = null;
            Character_Companion_ViewModel = null;
            Character_Equipment_ViewModel = null;
            Character_Features_ViewModel = null;
            Character_Forms_ViewModel = null;
            Character_Info_ViewModel = null;
            Character_Skills_ViewModel = null;
            General_data_ViewModel = null;
            Main_Window_Creation_ViewModel = null;
        }
    }
}
