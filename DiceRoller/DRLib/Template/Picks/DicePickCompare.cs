using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRLib.Template.Picks
{
    public class DicePickCompare : DicePick
    {
        #region Creator
        public DicePickCompare()
            : base()
        {
            this.Type = "EQ";
            this.Args = 0;
        }
        #endregion
        #region Properties
        public int Args { set; get; }
        #endregion
        #region Functions
        public override int[] Run(params int[] Inputs)
        {
            string thisType = this.TypeIs();
            Func<int, bool> selector;

            switch (thisType)
            {
                case "GTE":
                    {
                        selector = arg => arg >= this.Args;
                    }
                    break;
                case "GT":
                    {
                        selector = arg => arg > this.Args;
                    }
                    break;
                case "LT":
                    {
                        selector = arg => arg < this.Args;
                    }
                    break;
                case "LTE":
                    {
                        selector = arg => arg <= this.Args;
                    }
                    break;
                case "EQ":
                    {
                        selector = arg => arg == this.Args;
                    }
                    break;
                case "NE":
                    {
                        selector = arg => arg != this.Args;
                    }
                    break;
                default:
                    {
                        return Inputs;
                    }
            }

            return Inputs.Where(selector).ToArray();
        }
        #endregion
        #region ToString
        public override string SelfExplain()
        {
            return string.Format("{0}{1}", base.SelfExplain(), this.Args);
        }
        #endregion
    }
}
