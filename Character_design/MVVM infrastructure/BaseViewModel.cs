using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Windows.Media;

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
    }
}
