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
using DiceRoller.Controls;
using DRLib.Template;
using System.IO;
using DiceRoller.Forms;

namespace DiceRoller
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private App Application;
        private DiceResult ResultWindow;

        public MainWindow(App thisApp)
        {
            this.Application = thisApp;

            InitializeComponent();

            AddDiceTemplate();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //SetColor
            Random rand = new Random();
            int R = rand.Next(50, 150);
            int G = rand.Next(50, 150);
            int B = rand.Next(50, 150);
            this.OuterBackground.Background = new SolidColorBrush(Color.FromArgb(255, (byte)R, (byte)G, (byte)B));
            this.InnerBackground.Background = new SolidColorBrush(Color.FromArgb(255, (byte)(R - 50), (byte)(G - 50), (byte)(B - 50)));
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonAddTemplate_Click(object sender, RoutedEventArgs e)
        {
            AddDiceTemplate();
        }
        private void ButtonRemoveTemplate_Click(object sender, RoutedEventArgs e)
        {
            if (this.GridTemplates.Children.Count > 0)
            {
                Grid lastGrid = this.GridTemplates.Children[this.GridTemplates.Children.Count - 1] as Grid;
                DiceRoller.Controls.DiceTemplate lastDiceTemplate = lastGrid.Children[0] as DiceRoller.Controls.DiceTemplate;
                this.DiceTemplates.Remove(lastDiceTemplate.DTemplate);
                this.GridTemplates.Children.Remove(lastGrid);
                this.GridTemplates.RowDefinitions.RemoveAt(this.GridTemplates.RowDefinitions.Count - 1);
            }
        }

        //public List<DRLib.Template.DiceTemplate> DiceTemplates { protected set; get; }
        public Dictionary<DRLib.Template.DiceTemplate, Grid> DiceTemplates { protected set; get; }
        protected DiceRoller.Controls.DiceTemplate AddDiceTemplate()
        {
            Grid newGrid = new Grid();
            if (this.DiceTemplates == null)
            {
                this.DiceTemplates = new Dictionary<DRLib.Template.DiceTemplate, Grid>();
            }
            Grid.SetRow(newGrid, this.DiceTemplates.Count);

            DiceRoller.Controls.DiceTemplate newDC = new DiceRoller.Controls.DiceTemplate();
            newGrid.Children.Add(newDC);

            RowDefinition newGridRow = new RowDefinition();
            newGridRow.Height = new GridLength(1, GridUnitType.Star);
            //newGridRow.Height = new GridLength(230, GridUnitType.Pixel);
            this.GridTemplates.RowDefinitions.Add(newGridRow);

            this.GridTemplates.Children.Add(newGrid);
            this.DiceTemplates.Add(newDC.DTemplate, newGrid);

            return newDC;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
            if (this.DiceTemplates == null || this.DiceTemplates.Count == 0)
            {
                dialog.FileName = "NewTemplate";
            }
            else
            {
                dialog.FileName = this.DiceTemplates.First().Key.Name;
            }
            dialog.DefaultExt = ".jdt";
            dialog.Filter = "JSON Dice Template (.jdt)|*.jdt";

            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {
                string FilePath = dialog.FileName;
                string JsonString = this.DiceTemplates.Keys.ToList().DiceTemplateListToJsonString();

                File.WriteAllText(FilePath, JsonString, Encoding.UTF8);
            }
        }
        private void ButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".jdt";
            dialog.Filter = "JSON Dice Template (.jdt)|*.jdt";

            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {
                string FilePath = dialog.FileName;
                string JsonString = File.ReadAllText(FilePath, Encoding.UTF8);
                List<DRLib.Template.DiceTemplate> dTemplates = JsonString.JsonStringToDiceTemplateList();

                this.GridTemplates.Children.Clear();
                this.DiceTemplates.Clear();
                foreach (DRLib.Template.DiceTemplate dTemplate in dTemplates)
                {
                    DiceRoller.Controls.DiceTemplate newDC = AddDiceTemplate();
                    newDC.ChangeTemplate(dTemplate);
                }
            }
        }
        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            this.Application.OpenWindow();
        }
        private void ButtonRoll_Click(object sender, RoutedEventArgs e)
        {
            foreach (DRLib.Template.DiceTemplate dTemplate in this.DiceTemplates.Keys)
            {
                dTemplate.Run();
            }

            IEnumerable<DiceHistory> currentHistory = this.DiceTemplates.Select(item => item.Key.Current);

            //Show Result
            if (this.ResultWindow != null)
            {
                this.ResultWindow.Close();
            }
            ResultWindow = new DiceResult(currentHistory, DiceResultShowLevel.Result);
            ResultWindow.Show();
        }

    }
}
