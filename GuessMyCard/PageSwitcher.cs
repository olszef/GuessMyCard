using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GuessMyCard
{
    public static class PageSwitcher
    {
        public static MainWindow mainWindow;

        public static void Switch(MainWindow thisPage, Page nextPage)
        {
            thisPage.gameArea.Content = nextPage;
        }
    }
}
