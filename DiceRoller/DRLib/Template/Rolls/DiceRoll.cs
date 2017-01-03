using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRLib.Template.Rolls
{
    public abstract class DiceRoll
    {
        private static Random Dice = new Random(Environment.TickCount);
        #region Creators
        public DiceRoll()
        {
            this.Number = 6;
            this.Count = 1;
            this.Adjust = 0;
        }
        public DiceRoll(int Number)
        {
            this.Number = Number;
            this.Count = 1;
            this.Adjust = 0;
        }
        public DiceRoll(int Number, int Count)
        {
            this.Number = Number;
            this.Count = Count;
            this.Adjust = 0;
        }
        #endregion
        #region Properties
        private int Seed { set; get; }
        public int Number { set; get; }
        public int Count { set; get; }
        public int Adjust { set; get; }
        #endregion
        #region Function
        public virtual int[] Run()
        {
            int[] result = new int[this.Count];
            for (int i = 0; i < this.Count; i++)
            {
                result[i] = Dice.Next(1, this.Number + 1) + this.Adjust;
            }
            return result;
        }
        #endregion
        #region ToString
        public virtual string SelfExplain()
        {
            return string.Format("{0}D{1}", this.Number, this.Count);
        }
        #endregion
    }
    public class DiceRollNormal : DiceRoll
    {
    }
}
