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
    /// Логика взаимодействия для Common_menu.xaml
    /// </summary>
    public partial class Common_menu : UserControl
    {
        public Common_menu()
        {
            InitializeComponent();
            DataContext = Common_menu_ViewModel.GetInstance();
        }
    }
}
