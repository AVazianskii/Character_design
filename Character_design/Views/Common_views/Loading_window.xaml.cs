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
using System.Windows.Shapes;

namespace Character_design.Views.Common_views
{
    /// <summary>
    /// Логика взаимодействия для Loading_window.xaml
    /// </summary>
    public partial class Loading_window : Window
    {
        public Loading_window()
        {
            InitializeComponent();
            //DataContext = new Loading_window_ViewModel();
        }
    }
}
