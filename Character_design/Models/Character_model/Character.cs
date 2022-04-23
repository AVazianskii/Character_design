using System;
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
        private Atribute_class strength;
        private Atribute_class agility;
        private Atribute_class stamina;
        private Atribute_class perception;
        private Atribute_class quickness;
        private Atribute_class intelligence;
        private Atribute_class charm;
        private Atribute_class willpower;
        private Range_Class range;
        private Age_status_class age_status;


        private bool Is_forceuser;
        private int experience,
                    experience_left,
                    experience_sold,
                    attributes,
                    attributes_left,
                    attributes_sold;
        private int age;



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
        public void Spend_exp_points(int cost)
        {
            Experience_sold = Experience_sold + cost;
            Experience_left = Experience - Experience_sold;
        }
        public void Refund_exp_points (int cost)
        {
            Experience_sold = Experience_sold - cost;
            Experience_left = Experience - Experience_sold;
        }



        public Atribute_class Strength
        {
            get { return strength; }
            set { strength = value; OnPropertyChanged("Strength"); }
        }
        public Atribute_class Agility
        {
            get { return agility; }
            set { agility = value; OnPropertyChanged("Agility"); }
        }
        public Atribute_class Stamina
        {
            get { return stamina; }
            set { stamina = value; OnPropertyChanged("Stamina"); }
        }
        public Atribute_class Perception
        {
            get { return perception; }
            set { perception = value; OnPropertyChanged("Perception"); }
        }
        public Atribute_class Quickness
        {
            get { return quickness; }
            set { quickness = value; OnPropertyChanged("Quickness"); }
        }
        public Atribute_class Intelligence
        {
            get { return intelligence; }
            set { intelligence = value; OnPropertyChanged("Intelligence"); }
        }
        public Atribute_class Charm
        {
            get { return charm; }
            set { charm = value; OnPropertyChanged("Charm"); }
        }
        public Atribute_class Willpower
        {
            get { return willpower; }
            set { willpower = value; OnPropertyChanged("Willpower"); }
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
            set { character_race = value; OnPropertyChanged("Character_race"); }
        }
        public bool Forceuser
        {
            get { return Is_forceuser; }
            set { Is_forceuser = value; OnPropertyChanged("Forceuser"); }
        }
        public int Experience
        {
            get { return experience; }
            set 
            { 
                experience = value;
                Spend_exp_points(0);
                OnPropertyChanged("Experience"); 
            }
        }
        public int Experience_left
        {
            get { return experience_left; }
            set { experience_left = value; OnPropertyChanged("Experience_left"); }
        }
        public int Experience_sold
        {
            get { return experience_sold; }
            set { experience_sold = value; OnPropertyChanged("Experience_sold"); }
        }
        public int Attributes
        {
            get { return attributes; }
            set { attributes = value; OnPropertyChanged("Attributes"); }
        }
        public List<Skill_Class> Skills
        {
            get { return skills; }
            set { skills = value; OnPropertyChanged("Skills"); }
        }
        public int Age
        {
            get { return age; }
            set { age = value; OnPropertyChanged("Age"); }
        }



        public Character()
        {
            Character_race = Main_model.GetInstance().Race_Manager.Get_Empty_race();
            Age_status = Main_model.GetInstance().Age_status_Manager.Age_Statuses()[0]; // устанавливаем возрастной статус "Неизвестно" персонажу
            Range = Main_model.GetInstance().Range_Manager.Ranges()[0]; // устанавливаем ранг "Рядовой" персонажу

            skills = new List<Skill_Class>();
            foreach(Skill_Class Skill in Main_model.GetInstance().Skill_Manager.Get_skills())
            {
                skills.Add(Skill);
            }
            
            Forceuser = false;
        }       
    }
}
 