using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRLib.Template.Picks
{
    public abstract class DicePick
    {
        #region Creator
        public DicePick()
        {
            this.Type = "NONE";
        }
        #endregion
        #region Properties
        public string Type { set; get; }
        #endregion
        #region Function
        public virtual string TypeIs()
        {
            return Type.ToUpper();
        }
        public virtual int[] Run(params int[] Inputs)
        {
            return Inputs;
        }
        #endregion
        #region ToString
        public virtual string SelfExplain()
        {
            return string.Format("{0}", this.Type);
        }
        #endregion
    }
    public class DicePickNone : DicePick
    {
    }
}
