using DRLib.Template.Rolls;
using DRLib.Template.Picks;
using DRLib.Template.Calls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DRLib.Template
{
    #region Parser(JsonString To DiceTemplate)
    public static class DiceTemplateHelper
    {
        /// <summary>
        /// 기존에 저장된 JSON 형식의 DiceTemplate을 다시 원래의 오브젝트로 돌리는 기능
        /// string에 문제가 있는 경우에 예외 발생 가능
        /// </summary>
        /// <param name="JsonString"></param>
        /// <returns></returns>
        public static DiceTemplate JsonStringToDiceTemplate(this string JsonString)
        {
            try
            {
                JsonSerializerSettings convertSettings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                };
                return JsonConvert.DeserializeObject<DiceTemplate>(JsonString, convertSettings);
            }
            catch (Exception)
            {
                throw new Exception(string.Format("데이터 파싱이 불가능합니다. 손상된 데이터가 아닌지 확인해주세요. \n 원본 데이터 : {0}", JsonString));
            }
        }
        public static string DiceTemplateListToJsonString(this List<DiceTemplate> Templates)
        {
            try
            {
                JsonSerializerSettings convertSettings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                };
                return JsonConvert.SerializeObject(Templates, convertSettings);
            }
            catch (Exception)
            {
                throw new Exception(string.Format("데이터 파싱이 불가능합니다. 손상된 데이터가 아닌지 확인해주세요."));
            }
        }
        public static List<DiceTemplate> JsonStringToDiceTemplateList(this string JsonString)
        {
            try
            {
                JsonSerializerSettings convertSettings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                };
                return JsonConvert.DeserializeObject<List<DiceTemplate>>(JsonString, convertSettings);
            }
            catch (Exception)
            {
                throw new Exception(string.Format("데이터 파싱이 불가능합니다. 손상된 데이터가 아닌지 확인해주세요. \n 원본 데이터 : {0}", JsonString));
            }
        }
    }
    #endregion

    public class DiceTemplate
    {
        #region Creator
        public DiceTemplate()
        {
            this.History = new List<DiceHistory>();
            this.Roll = new DiceRollNormal();
            this.Pick = new DicePickNone();
            this.Call = new DiceCallNone();
            this.Text = "{0}";
        }
        #endregion
        #region JSON (JsonString from Self)
        public string MakeJsonString()
        {
            JsonSerializerSettings convertSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            return JsonConvert.SerializeObject(this, convertSettings);
        }
        #endregion

        /// <summary>
        /// 탬플렛 저장 및 표기시 나타나는 명칭
        /// 예외사항 : 저장 시에 특수문자가 섞인 경우 저장 불가능한 경우가 생길 수 있음
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 로그/히스토리 표기 시의 String Format (C#)
        /// 예외사항 : 포멧이 요구하는 Arg값과 실제 Arg값이 부족할 경우 에러 발생 가능
        /// </summary>
        public string Text { set; get; }
        /// <summary>
        /// 탬플렛 사용에 필요한 설명 또는 주석
        /// </summary>
        public string Explain { set; get; }

        /// <summary>
        /// 굴림 설정 구역
        /// </summary>
        public DiceRoll Roll { set; get; }
        /// <summary>
        /// 굴림 결과의 구분 구역
        /// </summary>
        public DicePick Pick { set; get; }
        /// <summary>
        /// 구분 결과의 처리 구역
        /// </summary>
        public DiceCall Call { set; get; }

        [JsonIgnore]
        public List<DiceHistory> History { set; get; }
        [JsonIgnore]
        public DiceHistory Current
        {
            get
            {
                return this.History.OrderByDescending(ii => ii.DiceDate).First();
            }
        }

        public void Run()
        {
            DiceHistory currentHistory = new DiceHistory(this.Name);

            int[] valuesRolled;
            int[] valuesPicked;
            string valuesCalled;

            valuesRolled = this.Roll.Run();
            currentHistory.SetRolled(this.Roll.SelfExplain(), "{0}", valuesRolled);

            valuesPicked = this.Pick.Run(valuesRolled);
            currentHistory.SetPicked(this.Pick.SelfExplain(), "{0}", valuesPicked);

            valuesCalled = this.Call.Run(valuesPicked);
            currentHistory.SetCalled(this.Call.SelfExplain(), "{0}", valuesCalled);

            currentHistory.SetResult(string.Format(Text, valuesCalled));

            this.History.Add(currentHistory);
        }

        public void CopiedBy(DiceTemplate dTemplate)
        {
            this.Name = dTemplate.Name;
            this.Text = dTemplate.Text;
            this.Explain = dTemplate.Explain;
            this.Roll = dTemplate.Roll;
            this.Pick = dTemplate.Pick;
            this.Call = dTemplate.Call;
        }
    }
}
