using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.DB
{
    public class Factory
    {
        static public object GetInstance(Dictionary<string, string> storeParams)
        {
            string storeAttr = "FORMAT";
            string storeFormat;
            if (!storeParams.TryGetValue(storeAttr, out storeFormat))
                throw new Exception(string.Format("В списке атрибутов хранилища отсутствует атрибут {0}.\nУкажите атрибут для метода, возможно, в БД Amur таблица method.\n", storeAttr));
            string s;

            switch (storeFormat)
            {
                case "FILE_VAN2011":
                    if (!storeParams.TryGetValue("FORMAT", out s)
                        || !storeParams.TryGetValue("FILE_DIRECTORY", out s)
                        || !storeParams.TryGetValue("FILE_NAME_MASK", out s)
                        || !storeParams.TryGetValue("IS_FILE_ZIPPED", out s)
                    )
                        throw new Exception(string.Format(
                            "Для получения экземпляра хранилища/базы данных в справочнике параметров хранилища" +
                            " должны присутствовать ключи: FORMAT_NAME, FILE_DIRECTORY, FILE_NAME_MASK, IS_FILE_ZIPPED.\n{0}", storeParams));

                    return new VanRepository(
                        storeParams["FORMAT"],
                        storeParams["FILE_DIRECTORY"],
                        storeParams["FILE_NAME_MASK"],
                        storeParams["IS_FILE_ZIPPED"]);

                case "GRIB2":
                    if (!storeParams.TryGetValue("FORMAT", out s)
                        || !storeParams.TryGetValue("FILE_DIRECTORY", out s)
                        || !storeParams.TryGetValue("FILE_NAME_MASK", out s)
                        || !storeParams.TryGetValue("IS_FILE_ZIPPED", out s)
                    )
                        throw new Exception(string.Format(
                            "Для получения экземпляра хранилища/базы данных в справочнике параметров хранилища" +
                            " должны присутствовать ключи: FORMAT, FILE_DIRECTORY, FILE_NAME_MASK, IS_FILE_ZIPPED.\n{0}", storeParams));

                    return new GfsRepository(
                        storeParams["FORMAT"],
                        storeParams["FILE_DIRECTORY"],
                        storeParams["FILE_SUBDIR_MASK"],
                        storeParams["FILE_NAME_MASK"],
                        storeParams["IS_FILE_ZIPPED"]);
                default:
                    return null;
            }
        }
    }
}
