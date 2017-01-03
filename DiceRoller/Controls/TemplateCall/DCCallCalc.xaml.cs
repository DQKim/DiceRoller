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
using DRLib.Template.Calls;

namespace DiceRoller.Controls.TemplateCall
{
    /// <summary>
    /// DCCallCalc.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DCCallCalc : UserControl
    {
        public DiceCallCalc TemplateCall { protected set; get; }

        public DCCallCalc(DiceCall SavedCall)
        {
            InitializeComponent();

            if (SavedCall == null)
            {
                this.TemplateCall = new DiceCallCalc();
            }
            else
            {
                this.TemplateCall = SavedCall as DiceCallCalc;
                this.ComboBoxType.Text = this.TemplateCall.Type;
            }
        }

        private void ComboBoxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string SelectedItem = (e.AddedItems[0] as ComboBoxItem).Content as string;
            switch (SelectedItem)
            {
                case "SUM":
                case "COUNT":
                case "AVR":
                    {
                        this.TemplateCall.Type = SelectedItem;
                    }
                    break;
                default:
                    {
                        if (this.IsInitialized)
                        {
                            this.ComboBoxType.SelectedIndex = 0;
                            this.TemplateCall.Type = "SUM";
                        }
                    }
                    break;
            }
        }
    }
}
