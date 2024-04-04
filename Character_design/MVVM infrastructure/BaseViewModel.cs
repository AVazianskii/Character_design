using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Threading;
using System;

namespace Character_design
{
    public class BaseViewModel : BaseViewModel_2
    {
        public void Set_colors_for_chosen_item (List<SolidColorBrush> SolidBrushes, 
                                                SolidColorBrush Need_brush,
                                                Color Chosen_color,
                                                Color Unchosen_color)
        {
            foreach (var item in SolidBrushes)
            {
                if (item == Need_brush)
                {
                    Need_brush.Color = Chosen_color;
                }
                else
                {
                    item.Color = Unchosen_color;
                }
            }
        }

        public void Run_method_with_loading(string MessageBoxHead, Action input_method)
        {
            {
                Views.Common_views.Loading_window loading_window = new Views.Common_views.Loading_window();

                // Запускаем дополнительный поток с необходимым методом. после его заверщения будет закрыто окно загрузки

                Thread thrd1 = new Thread(() =>
                {
                    try
                    {
                        input_method();                        
                    }
                    catch (Exception ex)
                    {
                        Application.Current.Dispatcher.Invoke(() => loading_window.Close());

                        MessageBox.Show(ex.Message,
                                        MessageBoxHead,
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                    }
                    Application.Current.Dispatcher.Invoke(() => loading_window.Close());
                });

                thrd1.Start();
                Application.Current.Dispatcher.Invoke(() => loading_window.ShowDialog());
            }
        }
    }
}
