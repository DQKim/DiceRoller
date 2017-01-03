using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRLib.Template.Calls
{
    public class DiceCallCalc : DiceCall
    {
        #region Creators
        public DiceCallCalc()
        {
            this.Type = "SUM";
        }
        #endregion
        #region Properties
        #endregion
        #region Functions
        public override string Run(params int[] inputs)
        {
            string thisType = this.TypeIs();
            switch (thisType)
            {
                case "SUM":
                    {
                        return inputs.Sum().ToString();
                    }
                case "COUNT":
                    {
                        return inputs.Count().ToString();
                    }
                case "AVR":
                    {
                        return inputs.Average().ToString();
                    }
                default:
                    {
                        return string.Join(",", inputs.Select(ii => ii.ToString()).ToArray());
                    }
            }
        }
        #endregion
        #region ToString
        public override string SelfExplain()
        {
            return string.Format("{0}", base.SelfExplain());
        }
        #endregion
    }
}
