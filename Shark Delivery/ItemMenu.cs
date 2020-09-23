using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Shark_Delivery
{
    public class ItemMenu
    {
        public ItemMenu(string header, List<SubItem> subItems)
        {
            Header = header;
            SubItems = subItems;
        }

        public ItemMenu(string header, UserControl screen)
        {
            Header = header;
            Screen = screen;
        }

        public string Header { get; private set; }
        public List<SubItem> SubItems { get; private set; }
        public UserControl Screen { get; private set; }
    }
}
