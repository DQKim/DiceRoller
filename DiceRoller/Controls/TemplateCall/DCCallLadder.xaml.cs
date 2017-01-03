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
using DiceRoller.Controls.TemplateCall.Ladder;

namespace DiceRoller.Controls.TemplateCall
{
    /// <summary>
    /// DCCallLadder.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DCCallLadder : UserControl
    {
        public DiceCallLadder TemplateCall { protected set; get; }

        public DCCallLadder(DiceCall SavedCall, DRLib.Template.DiceTemplate Template)
        {
            if (SavedCall == null)
            {
                this.TemplateCall = new DiceCallLadder();
                for (int i = 0; i < Template.Roll.Number; i++)
                {
                    this.TemplateCall.Args.Add(string.Empty);
                }
            }
            else
            {
                this.TemplateCall = SavedCall as DiceCallLadder;
            }

            this.DataContext = this;
            InitializeComponent();
        }

        public int LadderLength
        {
            get
            {
                return this.TemplateCall.Args.Count;
            }
        }

        private void ButtonLadderModify_Click(object sender, RoutedEventArgs e)
        {
            WindowLadder windowLadder = new WindowLadder(this.TemplateCall);
            windowLadder.Show();
        }
    }
}
