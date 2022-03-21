using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Character_design.Views.Character_creation.General_Data_View
{
    /// <summary>
    /// Логика взаимодействия для Character_equipment.xaml
    /// </summary>
    public partial class Character_equipment : UserControl
    {
        public Character_equipment()
        {
            InitializeComponent();
            DataContext = Character_equipment_ViewModel.GetInstance();
        }
    }
}
