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

namespace Character_design.Views.Character_creation.Range_View
{
    /// <summary>
    /// Логика взаимодействия для Range.xaml
    /// </summary>
    public partial class Range : UserControl
    {
        public Range()
        {
            InitializeComponent();
            DataContext = Range_ViewModel.GetInstance();
        }
    }
}
