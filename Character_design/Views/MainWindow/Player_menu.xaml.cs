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

namespace Character_design.Views.MainWindow
{
    /// <summary>
    /// Логика взаимодействия для Player_menu.xaml
    /// </summary>
    public partial class Player_menu : UserControl
    {
        public Player_menu()
        {
            InitializeComponent();
            DataContext = Player_menu_ViewModel.GetInstance();
        }
    }
}
