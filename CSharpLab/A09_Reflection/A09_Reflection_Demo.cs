using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab.A09_Reflection
{
    public class A09_Reflection_Demo
    {
        /// <summary>
        /// 反射 起手式 取得 Type 類別
        /// </summary>
        public void Demo1()
        {
            string val = "10";
            Type stringType = val.GetType();

            PrintType(stringType);
            //取得MyClass的Type類型，等同於 o.GetType()
            var intType = typeof(int);
            PrintType(intType);

            var interfaceType = typeof(IMyClass);
            PrintType(interfaceType);

            //由讀Assembly開始反射 可以用在讀取dll
            Assembly assembly = Assembly.Load("CSharpLab");
            /*
            Assembly assembly1 = Assembly.LoadFrom(@"DB.MySql.dll");//當前路徑
            Assembly assembly2 = Assembly.LoadFile(@"D:\ruanmou\MyReflection\bin\Debug\DB.MySql.dll");//dll、exe文件完整路徑
            Assembly assembly3 = Assembly.Load(@"DB.MySql");//dll、exe名稱   dll/exe需要拷貝至程序bin文件夾下
            */
            Type myClassType = assembly.GetType(name: "CSharpLab.A09_Reflection.MyClass", throwOnError: true,ignoreCase: false);
            PrintType(myClassType);
            var myClass = (MyClass)Activator.CreateInstance(myClassType);
            myClass.SayHello();
            // 結果
            // Hello
            myClass.ShowMessage("Test");
            // 結果
            // Message: Test

        }
        /// <summary>
        /// 設定 值 跟 呼叫方法
        /// </summary>
        public void Demo2()
        {
            Type myClassType = typeof(MyClass);
            var myClass = (MyClass)Activator.CreateInstance(myClassType);

            // 以下重點整理
            // 取得屬性 PropertyInfo
            // myClassType.GetProperty
            // 取得欄位 FieldInfo
            // myClassType.GetField
            // 取得 值
            // publicPropertyInfo.GetValue(myClass)
            // 設定 值
            // publicPropertyInfo.SetValue(myClass, 20)
            // 取得 Public 
            // BindingFlags.Public
            // 取得 非Public 
            // BindingFlags.NonPublic

            //設定Public的屬性值
            var publicPropertyInfo = myClassType.GetProperty("PublicProperty",
                BindingFlags.Public | BindingFlags.Instance);
            Console.WriteLine($"PublicProperty Before Set: {publicPropertyInfo.GetValue(myClass)}");
            publicPropertyInfo.SetValue(myClass, 20); //裡面的第一個參數是表示要對該實體進行操作
            Console.WriteLine($"PublicProperty After Set: {publicPropertyInfo.GetValue(myClass)}");
            // 結果
            // PublicProperty Before Set: 0
            // PublicProperty After Set: 20
            // ------------------------------------------------------ 

            //設定Public的欄位值
            var publicFieldInfo = myClassType.GetField("PublicField",
                BindingFlags.Public | BindingFlags.Instance);
            Console.WriteLine($"PublicField Before Set: {publicFieldInfo.GetValue(myClass)}");
            publicFieldInfo.SetValue(myClass, 30);
            Console.WriteLine($"PublicField After Set: {publicFieldInfo.GetValue(myClass)}");
            // 結果
            // PublicField Before Set: 0
            // PublicField After Set: 30
            // ------------------------------------------------------ 

            //設定Internal的屬性值
            var internalPropertyInfo = myClassType.GetProperty("InternalProperty",
                    BindingFlags.NonPublic | BindingFlags.Instance); //這邊下方會進行解釋
            Console.WriteLine($"InternalProperty Before Set: {internalPropertyInfo.GetValue(myClass)}");
            internalPropertyInfo.SetValue(myClass, 40);
            Console.WriteLine($"InternalProperty After Set: {internalPropertyInfo.GetValue(myClass)}");
            // 結果
            // InternalProperty Before Set: 0
            // InternalProperty After Set: 40
            // ------------------------------------------------------ 

            //BindingFlags.Public    指定要在搜尋中包含公用成員。
            //BindingFlags.NonPublic 指定要在搜尋中包含非公用成員。
            //BindingFlags.Instance  指定要在搜尋中包含執行個體成員。

            // 設定Internal的欄位值
            var internalFieldInfo = myClassType.GetField("InternalField",
                    BindingFlags.NonPublic | BindingFlags.Instance);
            Console.WriteLine($"InternalField Before Set: {internalFieldInfo.GetValue(myClass)}");
            internalFieldInfo.SetValue(myClass, 50);
            Console.WriteLine($"InternalField After Set: {internalFieldInfo.GetValue(myClass)}");
            // 結果
            // Value 4 Before Set: 0
            // Value 4 After Set: 50
            // ------------------------------------------------------

            //設定Private的屬性值
            var privatePropertyInfo = myClassType.GetProperty("PrivateProperty",
                    BindingFlags.NonPublic | BindingFlags.Instance); //這邊下方會進行解釋
            Console.WriteLine($"PrivateProperty Before Set: {privatePropertyInfo.GetValue(myClass)}");
            privatePropertyInfo.SetValue(myClass, 60);
            Console.WriteLine($"PrivateProperty After Set: {privatePropertyInfo.GetValue(myClass)}");
            // 結果
            // PrivateProperty Before Set: 0
            // PrivateProperty After Set: 60
            // ------------------------------------------------------ 

            //設定Private的欄位值
            var privateFieldInfo = myClassType.GetField("PrivateField",
                    BindingFlags.NonPublic | BindingFlags.Instance);
            Console.WriteLine($"PrivateField Before Set: {privateFieldInfo.GetValue(myClass)}");
            privateFieldInfo.SetValue(myClass, 70);
            Console.WriteLine($"PrivateField After Set: {privateFieldInfo.GetValue(myClass)}");
            // 結果
            // PrivateField Before Set: 0
            // PrivateField After Set: 70
            // ------------------------------------------------------

            //呼叫 SayHello 函數
            var method1 = myClassType.GetMethod("SayHello");
            //使用invoke來調用函數，第二個參數null表示沒有參數傳入
            method1.Invoke(myClass, null);
            // 結果
            // Hello

            //呼叫 ShowMessage 函數
            var method2 = myClassType.GetMethod("ShowMessage");
            method2.Invoke(myClass, new object[] { "顯示訊息喔！！" });
            // 結果
            // Message: 顯示訊息喔！！

            //呼叫 SaySomething 函數
            var method3 = myClassType.GetMethod("SaySomething",
                    BindingFlags.Instance | BindingFlags.NonPublic); //記得要加上Flag
            method3.Invoke(myClass, new object[] { "這是私有函數" });
            // 結果
            // Say something: 這是私有函數

            // 取得所有的Public 屬性
            var publicProperties = myClassType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            Console.WriteLine($"List all Public Property");
            foreach (var item in publicProperties)
            {
                Console.WriteLine(item.Name);
            }
            //結果
            //PublicProperty
        }
        /// <summary>
        /// 需求 請紀錄編輯前跟編輯後資料的差異
        /// </summary>
        public void Demo3()
        {
            var before = new SiteDto() { SiteId = 3, SiteName = "Google", Server = "8.8.8.8" };
            var after = new SiteDto() { SiteId = 3, SiteName = "Hinet", Server = "168.95.1.1" };
            var result = LogHelper.DetailedCompare(before, after);
            Console.WriteLine(result);
            //結果
            //Google => Hinet
            //8.8.8.8 => 168.95.1.1

            var before2 = new ProductDto() { Price = 5.99, EnableProduct = false, CreateDate = DateTime.MinValue, DayOfWeek = DayOfWeek.Monday };
            var after2 = new ProductDto() { Price = 6.99, EnableProduct = true, CreateDate = DateTime.Now, DayOfWeek = DayOfWeek.Sunday };
            result = LogHelper.DetailedCompare(before2, after2);
            Console.WriteLine(result);
            //結果
            //5.99 => 6.99
            //False => True
            //0001 / 1 / 1 上午 12:00:00 => 2021 / 5 / 16 下午 06:35:07
            //Monday => Sunday
        }
        private static void PrintType(Type type)
        {
            Console.WriteLine($" Name: {type.Name}");
            Console.WriteLine($" FullName: {type.FullName}");
            Console.WriteLine($" IsArray: {type.IsArray}");
            Console.WriteLine($" IsClass: {type.IsClass}");
            Console.WriteLine($" IsEnum: {type.IsEnum}");
            Console.WriteLine($" IsInterface: {type.IsInterface}");
            Console.WriteLine($" IsPublic: {type.IsPublic}");
            Console.WriteLine($" IsGenericType: {type.IsGenericType}");
            Console.WriteLine($" IsValueType: {type.IsValueType}");
            Console.WriteLine($" =========================================");
        }




        public void DataTableToList()
        {

            var dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("PublicProperty");
            dt.Columns.Add("Value2");
            dt.Columns.Add("Value3");
            dt.Columns.Add("Value4");
            dt.Columns.Add("Value5");
            dt.Columns.Add("Value6");
            DataRow _ravi = dt.NewRow();
            _ravi["PublicProperty"] = 11;
            _ravi["Value2"] = 22;
            _ravi["Value3"] = 33;
            _ravi["Value4"] = 44;
            _ravi["Value5"] = 55;
            _ravi["Value6"] = 66;
            dt.Rows.Add(_ravi);
            var b = GetModelList(dt);
        }
        public List<MyClass> GetModelList(DataTable dt)
        {
            //一行解決
            return dt.MapToModelList<MyClass>();
        }
    }
}
