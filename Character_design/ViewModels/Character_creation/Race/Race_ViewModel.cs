using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Races_libs;
using System.Windows.Media;


namespace Character_design
{
    internal class Race_ViewModel : BaseViewModel
    {
        List<Race_class> destination_race_list = new List<Race_class>();
        
        private static Race_ViewModel _instance;

        private BaseViewModel current_VM;

        private string selected_race_description;
        private string selected_race_full_img_path;
        private string selected_race_personal_data;
        private string selected_race_physical_data;
        private string selected_race_home_world;
        private string selected_race_name;

        private int selected_race_strength_bonus,
                    selected_race_agility_bonus,
                    selected_race_stamina_bonus,
                    selected_race_quickness_bonus,
                    selected_race_perception_bonus,
                    selected_race_intelligence_bonus,
                    selected_race_charm_bonus,
                    selected_race_will_power_bonus;

        private bool race_chosen;

        private string selected_race_feature_list, 
                       race_choose_advice;
        private Race_class selected_race;



        public Character_changing_command Choose_race { get; private set; }
        public Character_changing_command Unchoose_race { get; private set; }



        public static Race_ViewModel GetInstance()
        {
            if (_instance == null) { _instance = new Race_ViewModel(); }
            return _instance;
        }



        public BaseViewModel CurrentViewModel
        {
            get { return current_VM; }
            set { current_VM = value; OnPropertyChanged("CurrentViewModel"); }
        }
        public List<Race_class> Races
        {
            get { return destination_race_list; }
        }
        public string Selected_race_description
        {
            get { return selected_race_description; }
            set { selected_race_description = value; OnPropertyChanged("Selected_race_description"); }
        }
        public string Selected_race_full_img_path
        {
            get { return selected_race_full_img_path; }
            set { selected_race_full_img_path = value; OnPropertyChanged("Selected_race_full_img_path"); }
        }
        public string Selected_race_personal_data
        {
            get { return selected_race_personal_data; }
            set { selected_race_personal_data = value; OnPropertyChanged("Selected_race_personal_data"); }
        }
        public string Selected_race_physical_data
        {
            get { return selected_race_physical_data; }
            set { selected_race_physical_data = value; OnPropertyChanged("Selected_race_physical_data"); }
        }
        public string Selected_race_home_world
        {
            get { return selected_race_home_world; }
            set { selected_race_home_world = value; OnPropertyChanged("Selected_race_home_world"); }
        }
        public int Selected_race_strength_bonus
        {
            get { return selected_race_strength_bonus; }
            set { selected_race_strength_bonus = value; OnPropertyChanged("Selected_race_strength_bonus"); }
        }
        public int Selected_race_agility_bonus
        {
            get { return selected_race_agility_bonus; }
            set { selected_race_agility_bonus = value; OnPropertyChanged("Selected_race_agility_bonus"); }
        }
        public int Selected_race_stamina_bonus
        {
            get { return selected_race_stamina_bonus; }
            set { selected_race_stamina_bonus = value; OnPropertyChanged("Selected_race_stamina_bonus"); }
        }
        public int Selected_race_quickness_bonus
        {
            get { return selected_race_quickness_bonus; }
            set { selected_race_quickness_bonus = value; OnPropertyChanged("Selected_race_quickness_bonus"); }
        }
        public int Selected_race_perception_bonus
        {
            get { return selected_race_perception_bonus; }
            set { selected_race_perception_bonus = value; OnPropertyChanged("Selected_race_perception_bonus"); }
        }
        public int Selected_race_intelligence_bonus
        {
            get { return selected_race_intelligence_bonus; }
            set { selected_race_intelligence_bonus = value; OnPropertyChanged("Selected_race_intelligence_bonus"); }
        }
        public int Selected_race_charm_bonus
        {
            get { return selected_race_charm_bonus; }
            set { selected_race_charm_bonus = value; OnPropertyChanged("Selected_race_charm_bonus"); }
        }
        public int Selected_race_will_power_bonus
        {
            get { return selected_race_will_power_bonus; }
            set { selected_race_will_power_bonus = value; OnPropertyChanged("Selected_race_will_power_bonus"); }
        }
        public string Selected_race_feature_list
        {
            get { return selected_race_feature_list; }
            set { selected_race_feature_list = value; OnPropertyChanged("Selected_race_feature_list"); }
        }
        public string Selected_race_name
        {
            get { return selected_race_name; }
            set { selected_race_name = value; OnPropertyChanged("Selected_race_name"); }
        }
        public Race_class Selected_race
        {
            get { return selected_race; }
            set
            {
                selected_race = value;
                Selected_race_name = selected_race.Get_race_name();
                Selected_race_description = selected_race.Get_general_description();
                Selected_race_full_img_path = selected_race.Get_img_path();
                Selected_race_personal_data = selected_race.Get_personal_properties();
                Selected_race_physical_data = selected_race.Get_physical_properties();
                Selected_race_home_world = selected_race.Get_home_world();
                Selected_race_strength_bonus = selected_race.Get_race_bonus_strength();
                Selected_race_agility_bonus = selected_race.Get_race_bonus_agility();
                Selected_race_stamina_bonus = selected_race.Get_race_bonus_stamina();
                Selected_race_quickness_bonus = selected_race.Get_race_bonus_quickness();
                Selected_race_perception_bonus = selected_race.Get_race_bonus_perception();
                Selected_race_intelligence_bonus = selected_race.Get_race_bonus_intelligence();
                Selected_race_charm_bonus = selected_race.Get_race_bonus_charm();
                Selected_race_will_power_bonus = selected_race.Get_race_bonus_willpower();
                Setup_race_features(selected_race.Get_feature_1(),
                                    selected_race.Get_feature_2(),
                                    selected_race.Get_feature_3(),
                                    selected_race.Get_feature_4(),
                                    selected_race.Get_feature_5(),
                                    selected_race.Get_feature_6(),
                                    selected_race.Get_feature_7(), ref selected_race_feature_list);
                OnPropertyChanged("Selected_race_feature_list");
                OnPropertyChanged("Selected_race");
            }
        }
        public string Race_choose_advice
        {
            get { return race_choose_advice; }
            set { race_choose_advice = value; OnPropertyChanged("Race_choose_advice"); }
        }



        private Race_ViewModel()
        {
            Initial_load_race_list(destination_race_list);
            Selected_race = destination_race_list[0];

            race_chosen = false;
            Choose_race = new Character_changing_command(o => _Choose_race(),
                                                         o => race_chosen == false);
            Unchoose_race = new Character_changing_command(o => _Unchoose_race(),
                                                           o => race_chosen == true);

            Race_choose_advice = "";

        }



        private void Initial_load_race_list(List<Race_class> _destination_race_list)
        {
            foreach (Race_class race in Main_model.GetInstance().Race_Manager.Get_Race_list())
            {
                _destination_race_list.Add(race);
            }
            _destination_race_list.RemoveAt(0);
        }
        private void Setup_race_features(string feature_1, string feature_2, string feature_3,
                                         string feature_4, string feature_5, string feature_6, string feature_7,
                                         ref string out_text)
        {
            out_text = "";
            if (feature_1 != "") { out_text = out_text + feature_1 + "\n" + "\n"; }
            if (feature_2 != "") { out_text = out_text + feature_2 + "\n" + "\n"; }
            if (feature_3 != "") { out_text = out_text + feature_3 + "\n" + "\n"; }
            if (feature_4 != "") { out_text = out_text + feature_4 + "\n" + "\n"; }
            if (feature_5 != "") { out_text = out_text + feature_5 + "\n" + "\n"; }
            if (feature_6 != "") { out_text = out_text + feature_6 + "\n" + "\n"; }
            if (feature_7 != "") { out_text = out_text + feature_7; }
        }
        private void _Choose_race()
        {
            Character.GetInstance().Character_race = Selected_race;
            Character.GetInstance().Calculate_reaction          (Selected_race.Get_race_reaction_bonus());
            Character.GetInstance().Calculate_armor             (Selected_race.Get_race_armor_bonus());
            Character.GetInstance().Calculate_hideness          (Selected_race.Get_race_stealthiness_combat_bonus());
            Character.GetInstance().Calculate_watchfullness     (Selected_race.Get_race_watchfulness_combat_bonus());
            Character.GetInstance().Calculate_force_resistance  (Selected_race.Get_race_force_resist_bonus());
            Character.GetInstance().Calculate_concentration     (Selected_race.Get_race_flow_control_bonus());

            Character.GetInstance().Update_combat_parameters(Character.GetInstance().Strength,      Selected_race.Get_race_bonus_strength());
            Character.GetInstance().Update_combat_parameters(Character.GetInstance().Agility,       Selected_race.Get_race_bonus_agility());
            Character.GetInstance().Update_combat_parameters(Character.GetInstance().Stamina,       Selected_race.Get_race_bonus_stamina());
            Character.GetInstance().Update_combat_parameters(Character.GetInstance().Perception,    Selected_race.Get_race_bonus_perception());
            Character.GetInstance().Update_combat_parameters(Character.GetInstance().Quickness,     Selected_race.Get_race_bonus_quickness());
            Character.GetInstance().Update_combat_parameters(Character.GetInstance().Intelligence,  Selected_race.Get_race_bonus_intelligence());
            Character.GetInstance().Update_combat_parameters(Character.GetInstance().Charm,         Selected_race.Get_race_bonus_charm());
            Character.GetInstance().Update_combat_parameters(Character.GetInstance().Willpower,     Selected_race.Get_race_bonus_willpower());



            Apply_race_atr_bonus(Character.GetInstance(), Character.GetInstance().Character_race);

            Skill_ViewModel.GetInstance().Apply_race_skill_bonus(Selected_race);
            Skill_ViewModel.GetInstance().Refresh_fields();

            Race_choose_advice = $"Выбрана раса: {Character.GetInstance().Character_race.Get_race_name()}!";
            race_chosen = true;
        }
        private void _Unchoose_race()
        {
            Skill_ViewModel.GetInstance().UnApply_race_skill_bonus(Character.GetInstance().Character_race);
            UnApply_race_atr_bonus(Character.GetInstance(), Character.GetInstance().Character_race);

            Character.GetInstance().Calculate_reaction          (-Character.GetInstance().Character_race.Get_race_reaction_bonus());
            Character.GetInstance().Calculate_armor             (-Character.GetInstance().Character_race.Get_race_armor_bonus());
            Character.GetInstance().Calculate_hideness          (-Character.GetInstance().Character_race.Get_race_stealthiness_combat_bonus());
            Character.GetInstance().Calculate_watchfullness     (-Character.GetInstance().Character_race.Get_race_watchfulness_combat_bonus());
            Character.GetInstance().Calculate_force_resistance  (-Character.GetInstance().Character_race.Get_race_force_resist_bonus());
            Character.GetInstance().Calculate_concentration     (-Character.GetInstance().Character_race.Get_race_flow_control_bonus());

            Character.GetInstance().Update_combat_parameters(Character.GetInstance().Strength,      -Character.GetInstance().Character_race.Get_race_bonus_strength());
            Character.GetInstance().Update_combat_parameters(Character.GetInstance().Agility,       -Character.GetInstance().Character_race.Get_race_bonus_agility());
            Character.GetInstance().Update_combat_parameters(Character.GetInstance().Stamina,       -Character.GetInstance().Character_race.Get_race_bonus_stamina());
            Character.GetInstance().Update_combat_parameters(Character.GetInstance().Perception,    -Character.GetInstance().Character_race.Get_race_bonus_perception());
            Character.GetInstance().Update_combat_parameters(Character.GetInstance().Quickness,     -Character.GetInstance().Character_race.Get_race_bonus_quickness());
            Character.GetInstance().Update_combat_parameters(Character.GetInstance().Intelligence,  -Character.GetInstance().Character_race.Get_race_bonus_intelligence());
            Character.GetInstance().Update_combat_parameters(Character.GetInstance().Charm,         -Character.GetInstance().Character_race.Get_race_bonus_charm());
            Character.GetInstance().Update_combat_parameters(Character.GetInstance().Willpower,     -Character.GetInstance().Character_race.Get_race_bonus_willpower());

            Character.GetInstance().Character_race = Main_model.GetInstance().Race_Manager.Get_Race_list()[0];
            Skill_ViewModel.GetInstance().Refresh_fields();

            Race_choose_advice = "";
            race_chosen = false;
        }
        private void Apply_race_atr_bonus (Character character, Race_class race)
        {
            character.Strength.Increase_atr     (race.Get_race_bonus_strength());
            character.Agility.Increase_atr      (race.Get_race_bonus_agility());
            character.Stamina.Increase_atr      (race.Get_race_bonus_stamina());
            character.Quickness.Increase_atr    (race.Get_race_bonus_quickness());
            character.Perception.Increase_atr   (race.Get_race_bonus_perception());
            character.Intelligence.Increase_atr (race.Get_race_bonus_intelligence());
            character.Charm.Increase_atr        (race.Get_race_bonus_charm());
            character.Willpower.Increase_atr    (race.Get_race_bonus_willpower());
        }
        private void UnApply_race_atr_bonus(Character character, Race_class race)
        {
            character.Strength.Decrease_atr     (race.Get_race_bonus_strength());
            character.Agility.Decrease_atr      (race.Get_race_bonus_agility());
            character.Stamina.Decrease_atr      (race.Get_race_bonus_stamina());
            character.Quickness.Decrease_atr    (race.Get_race_bonus_quickness());
            character.Perception.Decrease_atr   (race.Get_race_bonus_perception());
            character.Intelligence.Decrease_atr (race.Get_race_bonus_intelligence());
            character.Charm.Decrease_atr        (race.Get_race_bonus_charm());
            character.Willpower.Decrease_atr    (race.Get_race_bonus_willpower());
        }
    }
}
