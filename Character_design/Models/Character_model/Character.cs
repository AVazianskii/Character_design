using System;
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
    internal class Character : Notify
    {
        private static Character Character_instance;


        private Race_class character_race;
        private Age_status_class Age_status;
        private Atribute_class Strength;
        private Atribute_class Agility;
        private Atribute_class Stamina;
        private Atribute_class Perception;
        private Atribute_class Quickness;
        private Atribute_class Intelligence;
        private Atribute_class Charm;
        private Atribute_class Willpower;

        public Character()
        {
            Character_race = Main_model.GetInstance().Race_Manager.Get_Empty_race();
            //Set_Character_Race(Main_model.GetInstance().Race_Manager.Get_Empty_race());

            //Load_all_from(Age_status_Manager);
            //Load_all_from(Attribute_Manager);

            //Set_Character_Race(Race_Manager.Get_Empty_race());

            //Set_Character_age_status(Age_status_Manager.Get_Unknown_age_status());

            //Set_Character_Strength      (Attribute_Manager.Get_strength());
            //Set_Character_Agility(Attribute_Manager.Get_agility());
            //Set_Character_Stamina(Attribute_Manager.Get_stamina());
            //Set_Character_Perception(Attribute_Manager.Get_perception());
            //Set_Character_Quickness(Attribute_Manager.Get_quickness());
            //Set_Character_Intelligence(Attribute_Manager.Get_intelligence());
            //Set_Character_Charm(Attribute_Manager.Get_charm());
            //Set_Character_Willpower(Attribute_Manager.Get_willpower());
        }
        public static Character GetInstance()
        {
            if (Character_instance == null)
            {
                Character_instance = new Character();
            }
            return Character_instance;
        }
        public Race_class Character_race
        {
            get 
            {
                return character_race;
            }
            set
            {
                character_race = value;
                OnPropertyChanged("Character_race");
            }
        }


        //public void Set_Character_Race(Race_class Choosen_race)
        //{
        //    Character_race = Choosen_race;
        //}
        public void Set_Character_age_status(Age_status_class Choosen_age_status)
            {
                Age_status = Choosen_age_status;
            }
            public void Set_Character_Strength(Atribute_class Strength)
            {
                this.Strength = Strength;
            }
            public void Set_Character_Agility(Atribute_class Agility)
            {
                this.Agility = Agility;
            }
            public void Set_Character_Stamina(Atribute_class Stamina)
            {
                this.Stamina = Stamina;
            }
            public void Set_Character_Perception(Atribute_class Perception)
            {
                this.Perception = Perception;
            }
            public void Set_Character_Quickness(Atribute_class Quickness)
            {
                this.Quickness = Quickness;
            }
            public void Set_Character_Intelligence(Atribute_class Intelligence)
            {
                this.Intelligence = Intelligence;
            }
            public void Set_Character_Charm(Atribute_class Charm)
            {
                this.Charm = Charm;
            }
            public void Set_Character_Willpower(Atribute_class Willpower)
            {
                this.Willpower = Willpower;
            }
            public Race_class Get_Character_race() { return Character_race; }
        }

}
