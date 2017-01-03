using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRLib.Template.Picks
{
    public class DicePickRank : DicePick
    {
        #region Creator
        public DicePickRank()
            : base()
        {
            this.Type = "TOP";
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
            IEnumerable<int> RankedArgs;

            switch (thisType)
            {
                case "TOP":
                    {
                        RankedArgs = Inputs.OrderByDescending(ii => ii);
                    }
                    break;
                case "BOT":
                    {
                        RankedArgs = Inputs.OrderBy(ii => ii);
                    }
                    break;
                default:
                    {
                        return Inputs;
                    }
            }

            return RankedArgs.Take(this.Args).ToArray();
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
