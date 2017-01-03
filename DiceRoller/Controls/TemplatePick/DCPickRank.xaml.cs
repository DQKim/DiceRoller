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
using DRLib.Template.Picks;
using System.ComponentModel;

namespace DiceRoller.Controls.TemplatePick
{
    /// <summary>
    /// DCPickRank.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DCPickRank : UserControl
    {
        public DicePickRank TemplatePick { protected set; get; }

        public DCPickRank( DicePick SavedPick)
        {
            InitializeComponent();
            if (SavedPick == null)
            {
                this.TemplatePick = new DicePickRank();
            }
            else
            {
                this.TemplatePick = SavedPick as DicePickRank;
                this.ComboBoxType.Text = this.TemplatePick.Type;
            }
            this.DataContext = this;
        }

        private void TextBox_OnlyPositiveNumber(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text[0]);
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
        public string TemplateType
        {
            set
            {
                if (value != this.TemplatePick.Type)
                {
                    this.TemplatePick.Type = value;
                    OnPropertyChanged("TemplateType");
                }
            }
            get
            {
                return this.TemplatePick.Type;
            }
        }
        public int TemplateArg
        {
            set
            {
                if (value != this.TemplatePick.Args)
                {
                    this.TemplatePick.Args = value;
                    OnPropertyChanged("TemplateArg");
                }
            }
            get
            {
                return this.TemplatePick.Args;
            }
        }

        private void ComboBoxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsInitialized)
            {
                string SelectedItem = (e.AddedItems[0] as ComboBoxItem).Content as string;
                this.TemplatePick.Type = SelectedItem;
            }
        }
    }
}
