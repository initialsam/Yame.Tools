using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab.A09_Reflection
{
    public static class ReflectionExtension
    {


        //DataTable與Model List轉換
        //並應用泛型來使所有物件都可以使用這方法
        public static List<T> MapToModelList<T>(this DataTable dataTable)
        {
            var result = new List<T>();
            foreach (DataRow dr in dataTable.Rows)
            {
                result.Add(dr.MapToModel<T>());
            }
            return result;
        }

        //DataRow與Model轉換
        private static T MapToModel<T>(this DataRow dataRow)
        {
            //先建立一個預設回傳值
            var result = Activator.CreateInstance<T>();
            //透過Reflection取得型別與屬性清單
            foreach (var property in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                //透過Reflection綁定值
                property.SetValue(result, Convert.ChangeType(dataRow[property.Name], property.PropertyType));
            }

            foreach (var field in typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (field.Name.StartsWith("<")) continue;
                field.SetValue(result, Convert.ChangeType(dataRow[field.Name], field.FieldType));
            }
            return result;
        }
    }
}
