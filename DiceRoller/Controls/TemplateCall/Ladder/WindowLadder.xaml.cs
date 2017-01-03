using DRLib.Template.Calls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace DiceRoller.Controls.TemplateCall.Ladder
{
    /// <summary>
    /// WindowLadder.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WindowLadder : Window
    {
        protected const string ResourceKey = "Args";
        public DiceCallLadder TemplateCall { protected set; get; }

        public WindowLadder(DiceCallLadder Call)
        {
            this.TemplateCall = Call;
            this.DataContext = this;

            InitializeComponent();

            MakeGrid();

            this.Background = new SolidColorBrush(Color.FromRgb(0,0,0));
        }
        protected void MakeGrid()
        {
            for (int i = 0; i < TemplateCall.Args.Count; i++)
            {
                //Set Grid
                Grid newGrid = new Grid();
                Grid.SetRow(newGrid, i);
                ColumnDefinition newGridCol0 = new ColumnDefinition();
                ColumnDefinition newGridCol1 = new ColumnDefinition();
                newGridCol0.Width = new GridLength(1, GridUnitType.Star);
                newGridCol1.Width = new GridLength(4, GridUnitType.Star);
                newGrid.ColumnDefinitions.Add(newGridCol0);
                newGrid.ColumnDefinitions.Add(newGridCol1);

                //Add Label
                Label newLabel = new Label();
                newLabel.Content = i;
                Grid.SetColumn(newLabel, 0);
                newGrid.Children.Add(newLabel);

                //Add TextBox
                TextBox newTextBox = new TextBox();
                newTextBox.Name = ResourceKey + i;
                newTextBox.Tag = i;
                newTextBox.Text = this.TemplateCall.Args[i];
                newTextBox.Margin = new System.Windows.Thickness(15, 5, 15, 5);
                Grid.SetColumn(newTextBox, 1);
                newGrid.Children.Add(newTextBox);
                newTextBox.TextChanged += newTextBox_TextChanged;
                
                //Add GridRow
                RowDefinition newGridRow = new RowDefinition();
                newGridRow.Height = new GridLength(1, GridUnitType.Star);
                this.GridValues.RowDefinitions.Add(newGridRow);

                //Add Grid in Grid
                this.GridValues.Children.Add(newGrid);
            }
        }

        void newTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            this.TemplateCall.Args[(int)textbox.Tag] = textbox.Text;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private void ButtonAccept_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
