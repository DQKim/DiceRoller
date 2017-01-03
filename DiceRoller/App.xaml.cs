using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DiceRoller
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        private List<MainWindow> windows;

        protected override void OnStartup(StartupEventArgs e)
        {
            OpenWindow();
        }

        public void OpenWindow()
        {
            if (this.windows == null)
            {
                this.windows = new List<MainWindow>();
            }

            MainWindow win = new MainWindow(this);
            win.Closed += win_Closed;

            windows.Add(win);

            win.Show();


        }

        private void win_Closed(object sender, EventArgs e)
        {
            this.windows.Remove(sender as MainWindow);
            if (this.windows.Count == 0)
            {
                this.Shutdown();
            }
        }
    }
}
