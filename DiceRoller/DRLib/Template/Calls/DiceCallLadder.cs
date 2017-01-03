using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRLib.Template.Calls
{
    public class DiceCallLadder: DiceCall
    {
        #region Creators
        public DiceCallLadder()
        {
            this.Type = "LADDER";
            this.Args = new List<string>();
        }
        #endregion
        #region Properties
        public List<string> Args { set; get; }
        #endregion
        #region Functions
        public override string Run(params int[] inputs)
        {
            return string.Join(",", inputs.Select(ii => Args[ii-1]).ToArray());
        }
        #endregion
        #region ToString
        public override string SelfExplain()
        {
            if (this.Args.Count() >= 10)
            {
                return string.Format("{0}{1}", base.SelfExplain(), Args.Count());
            }
            else
            {
                return string.Format("{0}[{1}]", base.SelfExplain(), string.Join(",", Args));
            }
        }
        #endregion
    }
}
