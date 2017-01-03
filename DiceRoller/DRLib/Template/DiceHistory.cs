using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRLib.Template
{
    public class DiceHistory
    {
        public DiceHistory(string TemplateName)
        {
            this.DiceDate = DateTime.Now;
            this.TemplateName = TemplateName;
        }

        public string TemplateName { protected set; get; }
        public DateTime DiceDate { protected set; get; }
        public int[] ValuesRolled { protected set; get; }
        public int[] ValuesPicked { protected set; get; }
        public string[] ValuesCalled { protected set; get; }
        public string FormatForRolled { protected set; get; }
        public string FormatForPicked { protected set; get; }
        public string FormatForCalled { protected set; get; }
        public string TextRolled { protected set; get; }
        public string TextPicked { protected set; get; }
        public string TextCalled { protected set; get; }
        public string TextResult { protected set; get; }

        public void SetRolled(string Text, string FormatForValues, params int[] Values)
        {
            this.TextRolled = Text;
            this.FormatForRolled = FormatForValues;
            this.ValuesRolled = Values;
        }
        public void SetPicked(string Text, string FormatForValues, params int[] Values)
        {
            this.TextPicked = Text;
            this.FormatForPicked = FormatForValues;
            this.ValuesPicked = Values;
        }
        public void SetCalled(string Text, string FormatForValues, params string[] Values)
        {
            this.TextCalled = Text;
            this.FormatForCalled = FormatForValues;
            this.ValuesCalled = Values;
        }
        public void SetResult(string Text)
        {
            this.TextResult = Text;
        }

        public string LogRolled()
        {
            return string.Format("[" + TextRolled + "]\t" + FormatForRolled, string.Join(", ",ValuesRolled));
        }
        public string LogPicked()
        {
            return string.Format("[" + TextPicked + "]\t" + FormatForPicked, string.Join(", ", ValuesPicked));
        }
        public string LogCalled()
        {
            return string.Format("[" + TextCalled + "]\t" + FormatForCalled, string.Join(", ", ValuesCalled));
        }
        public string LogResult()
        {
            return this.TextResult;
        }
    }
}
