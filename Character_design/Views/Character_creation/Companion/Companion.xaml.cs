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

namespace Character_design.Views.Character_creation.Companion
{
    /// <summary>
    /// Логика взаимодействия для Companion.xaml
    /// </summary>
    public partial class Companion : UserControl
    {
        public Companion()
        {
            InitializeComponent();
            DataContext = Character_creation_model.GetInstance().Companion_ViewModel;
        }
    }
}
