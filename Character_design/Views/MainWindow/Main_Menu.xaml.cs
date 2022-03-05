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
    /// Логика взаимодействия для Main_Menu.xaml
    /// </summary>
    public partial class Main_Menu : UserControl
    {
        public Main_Menu()
        {
            InitializeComponent();
            DataContext = Main_Menu_ViewModel.GetInstance();
        }
    }
}
