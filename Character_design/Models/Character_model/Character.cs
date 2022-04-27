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


        private bool Is_forceuser,
                     is_sith,
                     is_jedi,
                     is_neutral;

        private int experience,
                    experience_left,
                    experience_sold,
                    attributes,
                    attributes_left,
                    attributes_sold,
                    karma;
        private int age;

        private string sex;



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
        public void Refund_exp_points(int cost)
        {
            Experience_sold = Experience_sold - cost;
            Experience_left = Experience - Experience_sold;
        }
        public void Increase_atr(Attribute_libs.Atribute_class attribute)
        {
            if (Attributes_left >= attribute.Get_attribute_cost_for_atr())
            {
                Spend_atr_points(attribute.Get_attribute_cost_for_atr());
                attribute.Increase_atr(1);
                attribute.Increase_atr_for_atr();
                OnPropertyChanged("Attributes_left");
            }
            else
            {
                Spend_exp_points(attribute.Get_attribute_cost_for_exp());
                attribute.Increase_atr_for_exp();
                attribute.Increase_atr(1);
                OnPropertyChanged("Experience_left");
            }
        }
        public void Decrease_atr(Attribute_libs.Atribute_class attribute)
        {
            if (attribute.Get_atr_for_exp() > 0)
            {
                Refund_exp_points(attribute.Get_attribute_cost_for_exp());
                attribute.Decrease_atr(1);
                attribute.Decrease_atr_for_exp();
                OnPropertyChanged("Experience_left");
            }
            else
            {
                Refund_atr_points(attribute.Get_attribute_cost_for_atr());
                attribute.Decrease_atr(1);
                attribute.Decrease_atr_for_atr();
                OnPropertyChanged("Attributes_left");
            }
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
            set 
            { 
                attributes = value;
                Spend_atr_points(0);
                OnPropertyChanged("Attributes"); 
            }
        }
        public int Attributes_left
        {
            get { return attributes_left; }
            set { attributes_left = value; OnPropertyChanged("Attributes_left"); }
        }
        public int Attributes_sold
        {
            get { return attributes_sold; }
            set { attributes_sold = value; OnPropertyChanged("Attributes_sold"); }
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
        public string Sex
        {
            get { return sex; }
            set { sex = value; OnPropertyChanged("Sex"); }
        }
        public int Karma
        {
            get { return karma; }
            set { karma = value; OnPropertyChanged("Karma"); }
        }
        public bool Is_sith
        {
            get { return is_sith; }
            set { is_sith = value; OnPropertyChanged("Is_sith"); }
        }
        public bool Is_jedi
        {
            get { return is_jedi; }
            set { is_jedi = value; OnPropertyChanged("Is_jedi"); }
        }
        public bool Is_neutral
        {
            get { return is_neutral; }
            set { is_neutral = value; OnPropertyChanged("Is_neutral"); }
        }



        public Character()
        {
            Character_race = Main_model.GetInstance().Race_Manager.Get_Empty_race();
            Age_status = Main_model.GetInstance().Age_status_Manager.Age_Statuses()[0]; // устанавливаем возрастной статус "Неизвестно" персонажу
            Range = Main_model.GetInstance().Range_Manager.Ranges()[0]; // устанавливаем ранг "Рядовой" персонажу

            Strength        = Main_model.GetInstance().Attribute_Manager.Get_strength();
            Agility         = Main_model.GetInstance().Attribute_Manager.Get_agility();
            Stamina         = Main_model.GetInstance().Attribute_Manager.Get_stamina();
            Quickness       = Main_model.GetInstance().Attribute_Manager.Get_quickness();
            Perception      = Main_model.GetInstance().Attribute_Manager.Get_perception();
            Intelligence    = Main_model.GetInstance().Attribute_Manager.Get_intelligence();
            Charm           = Main_model.GetInstance().Attribute_Manager.Get_charm();
            Willpower       = Main_model.GetInstance().Attribute_Manager.Get_willpower();

            skills = new List<Skill_Class>();
            foreach(Skill_Class Skill in Main_model.GetInstance().Skill_Manager.Get_skills())
            {
                skills.Add(Skill);
            }
            
            Forceuser = false;
        }



        private void Spend_atr_points(int cost)
        {
            Attributes_sold = Attributes_sold + cost;
            Attributes_left = Attributes - Attributes_sold;
        }
        private void Refund_atr_points(int cost)
        {
            Attributes_sold = Attributes_sold - cost;
            Attributes_left = Attributes - Attributes_sold;
        }
    }
}
 