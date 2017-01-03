using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRLib.Template.Calls
{
    public abstract class DiceCall
    {
        #region Creators
        public DiceCall()
        {
            this.Type = "NONE";
        }
        #endregion
        #region Properties
        public string Type { set; get; }
        public string TypeIs()
        {
            return this.Type.ToUpper();
        }
        public string[] Results { private set; get; }
        #endregion
        #region Functions
        public virtual string Run(params int[] Inputs)
        {
            return string.Join(",", Inputs.Select(ii => ii.ToString()).ToArray());
        }
        #endregion
        #region ToString
        public virtual string SelfExplain()
        {
            return string.Format("{0}", this.Type);
        }
        #endregion
    }
    public class DiceCallNone : DiceCall
    {
    }
}
