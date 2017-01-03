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

namespace DiceRoller.Controls
{
    /// <summary>
    /// CustomButton.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CustomButton : UserControl
    {
        public event RoutedEventHandler Click;
        public object Value
        {
            set
            {
                this.Button.Content = value;
            }
            get
            {
                return this.Button.Content;
            }
        }
        public new bool IsEnabled
        {
            set
            {
                this.Button.IsEnabled = value;
            }
            get
            {
                return this.Button.IsEnabled;
            }
        }
        public Border Border
        {
            set { this.ButtonBorder = value; }
            get { return this.ButtonBorder; }
        }
        public Brush BorderColor
        {
            set { this.ButtonBorder.BorderBrush = value; }
            get { return this.ButtonBorder.BorderBrush; }
        }
        public CornerRadius BorderRadius
        {
            set { this.ButtonBorder.CornerRadius = value; }
            get { return this.ButtonBorder.CornerRadius; }
        }
        public Thickness BorderThick
        {
            set { this.ButtonBorder.BorderThickness = value; }
            get { return this.ButtonBorder.BorderThickness; }
        }
        public CustomButton()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Click != null)
            {
                Click(this, e);
            }
        }
    }
}
