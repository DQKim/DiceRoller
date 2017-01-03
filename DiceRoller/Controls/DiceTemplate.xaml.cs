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
using System.ComponentModel;
using System.Text.RegularExpressions;
using DRLib.Template.Picks;
using DRLib.Template.Calls;
using DiceRoller.Controls.TemplateCall;
using DiceRoller.Controls.TemplatePick;
using DiceRoller.Forms;

namespace DiceRoller.Controls
{
    /// <summary>
    /// DiceTemplate.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DiceTemplate : UserControl, INotifyPropertyChanged
    {
        public DiceTemplate()
        {
            InitializeComponent();

            this.DTemplate = new DRLib.Template.DiceTemplate();
            this.DataContext = this;
        }

        private DiceResult ResultWindow;
        public DRLib.Template.DiceTemplate DTemplate { protected set; get; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #region Properties for Binding (Settings)
        public string TemplateName
        {
            set
            {
                if (value != this.DTemplate.Name)
                {
                    this.DTemplate.Name = value;
                    OnPropertyChanged("TemplateName");
                }
            }
            get
            {
                return this.DTemplate.Name;
            }
        }
        public string TemplateText
        {
            set
            {
                if (value != this.DTemplate.Text)
                {
                    this.DTemplate.Text = value;
                    OnPropertyChanged("TemplateText");
                }
            }
            get
            {
                return this.DTemplate.Text;
            }
        }
        public string TemplateExplain
        {
            set
            {
                if (value != this.DTemplate.Text)
                {
                    this.DTemplate.Explain = value;
                    OnPropertyChanged("TemplateExplain");
                }
            }
            get
            {
                return this.DTemplate.Explain;
            }
        }
        #endregion
        #region Properties for Binding (Roll)
        public int RollCount
        {
            set
            {
                if (value != this.DTemplate.Roll.Count)
                {
                    this.DTemplate.Roll.Count = value;
                    OnPropertyChanged("RollCount");
                }
            }
            get
            {
                return this.DTemplate.Roll.Count;
            }
        }
        public int RollNumber
        {
            set
            {
                if (value != this.DTemplate.Roll.Number)
                {
                    this.DTemplate.Roll.Number = value;
                    OnPropertyChanged("RollNumber");
                }
            }
            get
            {
                return this.DTemplate.Roll.Number;
            }
        }
        public int RollAdjust
        {
            set
            {
                if (value != this.DTemplate.Roll.Adjust)
                {
                    this.DTemplate.Roll.Adjust = value;
                    OnPropertyChanged("RollAdjust");
                }
            }
            get
            {
                return this.DTemplate.Roll.Adjust;
            }
        }
        #endregion

        private void TextBox_OnlyPositiveNumber(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text[0]);
        }
        private void TextBox_OnlyNumber(object sender, TextCompositionEventArgs e)
        {
            string Text = (sender as TextBox).Text + e.Text;
            e.Handled = IsTextNumeric(Text);
        }
        private static bool IsTextNumeric(string Text)
        {
            Regex regex = new Regex("[^0-9-]+");
            return regex.IsMatch(Text);
        }

        private void ComboboxPick_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                string SelectedItem = (e.AddedItems[0] as ComboBoxItem).Content as string;
                SetGridPick(SelectedItem);
            }
        }
        private void SetGridPick(string Type, DicePick Saved = null)
        {
            if (this.GridPick != null)
            {
                this.GridPick.Children.Clear();
            }

            switch (Type)
            {
                case "NONE":
                    {
                        this.DTemplate.Pick = new DicePickNone();
                    }
                    break;
                case "COMPARE":
                    {
                        DCPickCompare newPick = new DCPickCompare(Saved);
                        this.DTemplate.Pick = newPick.TemplatePick;
                        this.GridPick.Children.Add(newPick);
                    }
                    break;
                case "RANK":
                    {
                        DCPickRank newPick = new DCPickRank(Saved);
                        this.DTemplate.Pick = newPick.TemplatePick;
                        this.GridPick.Children.Add(newPick);
                    }
                    break;
                default:
                    {
                        if (this.IsInitialized)
                        {
                            this.DTemplate.Pick = new DicePickNone();
                            this.ComboboxPick.SelectedIndex = 0;
                        }
                    }
                    break;
            }
        }

        private void ComboboxCall_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                string SelectedItem = (e.AddedItems[0] as ComboBoxItem).Content as string;
                SetGridCall(SelectedItem);
            }
        }
        private void SetGridCall(string Type, DiceCall Saved = null)
        {
            if (this.GridCall != null)
            {
                this.GridCall.Children.Clear();
            }

            switch (Type)
            {
                case "NONE":
                    {
                        this.DTemplate.Call = new DiceCallNone();
                    }
                    break;
                case "CALC":
                    {
                        DCCallCalc newCall = new DCCallCalc(Saved);
                        this.DTemplate.Call = newCall.TemplateCall;
                        this.GridCall.Children.Add(newCall);
                    }
                    break;
                case "LADDER":
                    {
                        DCCallLadder newCall = new DCCallLadder(Saved, this.DTemplate);
                        this.DTemplate.Call = newCall.TemplateCall;
                        this.GridCall.Children.Add(newCall);
                    }
                    break;
                default:
                    {
                        if (this.IsInitialized)
                        {
                            this.DTemplate.Call = new DiceCallNone();
                            this.ComboboxPick.SelectedIndex = 0;
                        }
                    }
                    break;
            }
        }

        private void ButtonHistory_Click(object sender, RoutedEventArgs e)
        {
            if (this.ResultWindow != null)
            {
                this.ResultWindow.Close();
            }
            this.ResultWindow = new DiceResult(this.DTemplate.History, DiceResultShowLevel.Log);
            this.ResultWindow.Show();
        }
        public void ChangeTemplate(DRLib.Template.DiceTemplate dTemplate)
        {
            this.DTemplate.CopiedBy(dTemplate);

            Type pickType = dTemplate.Pick.GetType();
            if (pickType == typeof(DicePickCompare))
            {
                ComboboxPick.Text = "COMPARE";
                SetGridPick("COMPARE", dTemplate.Pick);
            }
            else if (pickType == typeof(DicePickRank))
            {
                ComboboxPick.Text = "RANK";
                SetGridPick("RANK", dTemplate.Pick);
            }
            else if (pickType == typeof(DicePickNone))
            {
                ComboboxPick.Text = "NONE";
                SetGridPick("NONE", dTemplate.Pick);
            }

            Type callType = dTemplate.Call.GetType();
            if (callType == typeof(DiceCallCalc))
            {
                ComboboxCall.Text = "CALC";
                SetGridCall("CALC", dTemplate.Call);
            }
            else if (callType == typeof(DiceCallLadder))
            {
                ComboboxCall.Text = "LADDER";
                SetGridCall("LADDER", dTemplate.Call);
            }
            else if (callType == typeof(DiceCallNone))
            {
                ComboboxCall.Text = "NONE";
                SetGridCall("NONE", dTemplate.Call);
            }
        }

        public event RoutedEventHandler SwitchHide;
        private void ButtonContentHide_Click(object sender, RoutedEventArgs e)
        {
            switch (this.GridContent.Visibility)
            {
                case Visibility.Hidden:
                case Visibility.Collapsed:
                    {
                        this.GridContent.Visibility = Visibility.Visible;
                    }
                    break;
                case Visibility.Visible:
                    {
                        this.GridContent.Visibility = Visibility.Collapsed;
                    }
                    break;
            }

            RoutedEventHandler h = SwitchHide;
            if (h != null)
            {
                SwitchHide(this, e);
            }
        }

    }
}
