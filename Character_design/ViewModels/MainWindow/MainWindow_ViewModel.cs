using System.Threading;
using System.Windows;

namespace Character_design
{
    public class MainWindow_ViewModel : BaseViewModel
    {
        private static MainWindow_ViewModel _instance;

        private BaseViewModel current_VM;
        private Main_Menu_ViewModel Main_menu;
        


        public BaseViewModel CurrentViewModel
        {
            get
            {
                return current_VM;
            }
            set
            {
                current_VM = value;
                OnPropertyChanged("CurrentViewModel");
            }
        }



        public static MainWindow_ViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MainWindow_ViewModel();
            }
            return _instance;
        }
        public void DeleteInstance()
        {
            if (_instance != null)
            {
                _instance = null;
            }
        }



        public MainWindow_ViewModel()
        {
            Views.Common_views.Loading_window loading_window = new Views.Common_views.Loading_window();

            Thread thrd1 = new Thread(() =>
            {
                Main_model.GetInstance().Download_all();

                Character.GetInstance();

                Application.Current.Dispatcher.Invoke(() => loading_window.Close());
            });

            thrd1.Start();
            Application.Current.Dispatcher.Invoke(() => loading_window.ShowDialog());
            
            Main_menu = Main_Menu_ViewModel.GetInstance();
            current_VM = Main_menu;
        }
    }
}
