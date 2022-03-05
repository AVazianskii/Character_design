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

namespace Character_design.Views.Character_creation.Main_Window_Creation_View
{
    /// <summary>
    /// Логика взаимодействия для Main_Window_Cration.xaml
    /// </summary>
    public partial class Main_Window_Creation : UserControl
    {
        public Main_Window_Creation()
        {
            InitializeComponent();
            DataContext = Main_Window_Creation_ViewModel.GetInstance();
        }
    }
}
