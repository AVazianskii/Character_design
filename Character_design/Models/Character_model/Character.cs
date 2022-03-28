﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SW_Character_creation;
using Races_libs;
using Skills_libs;
using Age_status_libs;
using Attribute_libs;
using Range_libs;

namespace Character_design
{
    internal class Character : BaseViewModel
    {
        private static Character Character_instance;

        private List<Skill_Class> skills;

        private Race_class character_race;
        private Atribute_class Strength;
        private Atribute_class Agility;
        private Atribute_class Stamina;
        private Atribute_class Perception;
        private Atribute_class Quickness;
        private Atribute_class Intelligence;
        private Atribute_class Charm;
        private Atribute_class Willpower;
        private Range_Class range;
        private Age_status_class age_status;

        private bool Is_forceuser;
        private int experience;



        public Character()
        {
            Character_race = Main_model.GetInstance().Race_Manager.Get_Empty_race();
            skills = new List<Skill_Class>();
            foreach(Skill_Class Skill in Main_model.GetInstance().Skill_Manager.Get_skills())
            {
                skills.Add(Skill);
            }
            
            Forceuser = false;
            Experience = 180;

        }



        public List<Skill_Class> Get_skills()
        {
            return skills;
        }
        public static Character GetInstance()
        {
            if (Character_instance == null)
            {
                Character_instance = new Character();
            }
            return Character_instance;
        }



        public Age_status_class Age_status 
        {
            get { return age_status; }
            set { age_status = value; OnPropertyChanged("Age_status"); }
        }
        public Range_Class Range
        {
            get { return range; }
            set { range = value; OnPropertyChanged("Range"); }
        }
        public Race_class Character_race
        {
            get { return character_race; }
            set
            {
                character_race = value;
                OnPropertyChanged("Character_race");
            }
        }
        public bool Forceuser
        {
            get { return Is_forceuser; }
            set { Is_forceuser = value; OnPropertyChanged("Forceuser"); }   
        }
        public int Experience
        {
            get { return experience; }
            set { experience = value; OnPropertyChanged("Experience"); }
        }
        public List<Skill_Class> Skills
        {
            get { return skills; }
            set { skills = value; OnPropertyChanged("Skills"); }
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
