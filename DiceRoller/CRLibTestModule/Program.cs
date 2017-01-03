using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DRLib.Template;
using DRLib.Template.Calls;
using DRLib.Template.Rolls;
using DRLib.Template.Picks;
using Newtonsoft.Json.Linq;
using Newtonsoft;
using Newtonsoft.Json;

namespace CRLibTestModule
{
    class Program
    {
        static void Main(string[] args)
        {
            DiceTemplate dice = new DiceTemplate();
            dice.Name = "TEST";
            dice.Text = "테스트 결과 : {0}";
            dice.Explain = "테스트용 템플릿, 10D6, TOP9, AVR";

            dice.Roll = new DiceRollNormal()
            {
                Count = 10,
                Number = 6
            };

            dice.Pick = new DicePickRank() {
                Type = "TOP",
                Args = 9
            };

            dice.Call = new DiceCallCalc()
            {
                Type = "AVR"
            };

            List<DiceTemplate> dices = new List<DiceTemplate>();
            dices.Add(dice);
            dices.Add(new DiceTemplate());

            string diceJSON = dices.DiceTemplateListToJsonString();
            List<DiceTemplate> diceListDecoded = diceJSON.JsonStringToDiceTemplateList();
            DiceTemplate diceDecoded = diceListDecoded.First();
            DiceHistory history;

            diceDecoded.Run();
            history = diceDecoded.Current;
            Console.WriteLine(history.LogRolled());
            Console.WriteLine(history.LogPicked());
            Console.WriteLine(history.LogCalled());
            Console.WriteLine(history.LogResult());
            Console.WriteLine();

            diceDecoded.Run();
            history = diceDecoded.Current;
            Console.WriteLine(history.LogRolled());
            Console.WriteLine(history.LogPicked());
            Console.WriteLine(history.LogCalled());
            Console.WriteLine(history.LogResult());
            Console.WriteLine();

            Console.ReadLine();

            while (true)
            {
                diceDecoded.Run();
            }
        }
    }
}
