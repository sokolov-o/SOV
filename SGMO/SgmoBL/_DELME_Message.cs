using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Amur.Meta;

namespace FERHRI.SGMO
{
    /// <summary>
    /// Текстовое предупреждение.
    /// </summary>
    public class Message
    {
        static public EnumLanguage GetLanguage(string lang)
        {
            switch (lang)
            {
                case "rus": return EnumLanguage.Rus;
                case "eng": return EnumLanguage.Eng;
                default: return EnumLanguage.Unk;
            }
        }

        static public string ToString(EnumLanguage lang)
        {
            return lang.ToString().ToLower();
        }

        public int Id { get; set; }
        public Catalog Catalog { get; set; }
        public Enums.MessageType MessageType { get; set; }
        public DateTime DateIni { get; set; }
        public string Text { get; set; }
        /// <summary>
        /// Информация об источнике, сгенерировавшем сообщение.
        /// Входит в уникальный ключ сообщения.
        /// </summary>
        public string SourceInfo { get; set; }
        public EnumLanguage Lang { get; set; }

        public static Message GetMessage
        (
            DateTime dateIni,
            Catalog catalog,
            Method method,
            Unit unitVar,
            Unit unitLeadTime,
            double[] leadTimes,
            WarningGeoPoint[/*LeadTime index*/] warnGPs,
            EnumLanguage lang,
            Enums.MessageType messType,
            Enums.TextLength messLength
        )
        {
            // CHECK
            if (leadTimes.Length != warnGPs.Length)
                throw new Exception(string.Format("Количество заблаговременностей не совпадают: {0} != {1}.", leadTimes.Length, warnGPs.Length));

            // DECLARE
            string text = null;
            string addInfo = null;
            bool isRus = lang == EnumLanguage.Rus;
            FERHRI.Amur.Meta.MethodForecast methodFcs = method.MethodForecast;
            long leadTimeMilliSecondsCount = Time.GetMilliSecondsCount(unitLeadTime.Id);
            string dateFcsFormat = isRus ? "dd.MM.yyyy HH:mm" : "yyyy.MM.dd HH:mm";

            // SCAN LEAD TIMES
            PileItemValues piv = null;
            for (int iLT = 0; iLT < warnGPs.Length; iLT++)
            {
                if (warnGPs[iLT] != null)
                {
                    if (piv == null)
                    {
                        piv = warnGPs[iLT].PileItemValues;
                        text = piv.ToString(lang, unitVar.AbbreviationEng);
                    }
                    // CHECK PIV
                    if (piv.PileItem.PileSet.Id != warnGPs[iLT].PileItemValues.PileItem.PileSet.Id)
                        throw new Exception("В массиве предупреждений для разных заблаговременностей присутствуют предупреждения из разных наборов пайлов.");

                    DateTime dateFcs = dateIni.AddMilliseconds(leadTimeMilliSecondsCount * leadTimes[iLT]);

                    // Small length message
                    if (addInfo == null)
                        addInfo = "\n\n" + string.Format(
                            isRus
                            ? "Экстремумы\nДата\tЯвление\tУзел сетки\tЗначение ({0})"
                            : "Extremums\nDateTime\tPhenomena\tGrid node\tValue ({0})",
                            unitVar.AbbreviationEng);

                    addInfo += string.Format("\n{0:" + dateFcsFormat + "}\t{1}\t{2}\t{3}", dateFcs,
                        warnGPs[iLT].PileItemValues.PileItem.ToString(lang),
                        warnGPs[iLT].GeoPoint,
                        warnGPs[iLT].PileItemValues.Values[0]);
                }
            }
            // Middle length message
            if ((int)messLength > (int)Enums.TextLength.Short)
            {
                addInfo +=
                    "\n\n" + (isRus ? "Комментарий" : "Comment:") +
                    string.Format("\n- {0}: {1}",
                        isRus
                        ? (methodFcs == null ? "Метод" : "Метод прогноза")
                        : (methodFcs == null ? "Method" : "Forecasting method"),
                        method.Name);

                // Long length message
                if ((int)messLength > (int)Enums.TextLength.Middle)
                {
                    if (methodFcs != null)
                    {
                        addInfo += string.Format(
                            isRus
                            ? "\n- Максимальная заблаговременность метода: {0} ч"
                            : "\n- Maximum forecast-time interval: {0} {1}",
                            methodFcs.LeadTimes[methodFcs.LeadTimes.Length - 1],
                            isRus ? unitLeadTime.Name : unitLeadTime.NameEng);
                    }
                    addInfo += string.Format(
                        isRus ? "\n- Описание метода: {0}" : "\n- Method description: {0}", method.Description);
                    string s;
                    if (method.MethodOutputStoreParameters.TryGetValue("WEB_VIEW", out s) && !string.IsNullOrEmpty(s))
                        addInfo += (isRus ? "\n- Подробнее см. по ссылке " : "\n- See at ") + s;
                }
            }

            // Полный текст предупреждения
            return new SGMO.Message()
            {
                Id = -1,
                DateIni = dateIni,
                Catalog = catalog,
                MessageType = messType,
                SourceInfo = string.Format("Method.Id={0}; PileSet.Id={1}; {2}", method.Id, piv.PileItem.PileSet.Id, lang),
                Text = text + addInfo,
                Lang = lang
            };
        }
    }
}
