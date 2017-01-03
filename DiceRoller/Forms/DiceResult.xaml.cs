using DRLib.Template;
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

namespace DiceRoller.Forms
{
    public enum DiceResultShowLevel
    {
        Log,
        Result
    }
    /// <summary>
    /// DiceResult.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DiceResult : Window
    {
        public int LogFontSize { protected set; get; }
        public DiceResult(IEnumerable<DiceHistory> Results, DiceResultShowLevel LogLevel)
        {
            InitializeComponent();

            switch (LogLevel)
            {
                case DiceResultShowLevel.Log:
                    {
                        this.LogFontSize = 10;
                        ShowLogs(Results.ToList());
                    }
                    break;
                case DiceResultShowLevel.Result:
                    {
                        this.LogFontSize = 12;
                        ShowResults(Results.ToList());
                    }
                    break;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowLogs(List<DiceHistory> Logs)
        {
            List<DiceHistory> OrderedLogs = Logs.OrderByDescending(ii => ii.DiceDate).ToList();
            for (int i = 0; i < Logs.Count; i++)
            {
                DiceHistory Log = OrderedLogs[i];
                MakeRow("");
                MakeRow(string.Format("Roll : {0}", Log.LogRolled()));
                MakeRow(string.Format("Pick : {0}", Log.LogPicked()));
                MakeRow(string.Format("Call : {0}", Log.LogCalled()));
                MakeRow("");

                if (i >= 10) break;
            }
        }
        private void ShowResults(List<DiceHistory> Results)
        {
            for (int i = 0; i < Results.Count; i++) 
            {
                DiceHistory Result = Results[i];
                MakeRow(string.Format("{0} : {1}", Result.TemplateName, Result.TextResult));
            }
        }

        private void MakeRow(string Text)
        {
            RowDefinition newRow = new RowDefinition();
            newRow.Height = new System.Windows.GridLength(1, GridUnitType.Star);

            Grid newGrid = new Grid();
            Grid.SetRow(newGrid, this.GridContent.RowDefinitions.Count);

            TextBlock newText = new TextBlock();
            newText.Text = Text;
            newText.FontSize = this.LogFontSize;

            newGrid.Children.Add(newText);

            this.GridContent.RowDefinitions.Add(newRow);
            this.GridContent.Children.Add(newGrid);
        }
    }
}
