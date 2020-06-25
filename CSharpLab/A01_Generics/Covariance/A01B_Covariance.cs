using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab.A01_Generics.Covariance
{
    public class A01B_Covariance
    {
        private LevelPool<Level2> Level2Pools { get; set; }
        public A01B_Covariance()
        {
            //共變性
            Level1 l1 = NewLevel2();
            //Level3 l3 = NewLevel2(); 編譯失敗

            //逆變性
            //GetLevel2(new Level1()); 編譯失敗
            GetLevel2(new Level3());

            this.Level2Pools = new LevelPool<Level2>();
        }

        private Level2 NewLevel2()
        { 
            //因為level2  有  level1的資料 所以  能  當作level1 回傳使用
            //因為level2 沒有 level3的資料 所以 不能  當作level3 回傳使用
            return new Level2();
        }

        private void GetLevel2(Level2 a)
        {
            //因為level1 沒有 level2的資料 所以 不能 當作level2 傳入參數
            //因為level3  有  level2的資料 所以  能  當作level2 傳入參數
        }

        public void testCovariant()
        {
            //out 共變性 Covariant
            //基底取代衍生
            GetLevel1(this.Level2Pools);
            GetLevel2(this.Level2Pools);
            //GetLevel3(this.Level2Pools);編譯失敗
        }

        //interface ILevelGet<out T>
        private void GetLevel1(ILevelGet<Level1> repository)
        {
            var level1 = repository.Get(301);
            Console.WriteLine(level1.LevelId);
        }

        private void GetLevel2(ILevelGet<Level2> repository)
        {
            var level2 = repository.Get(301);
            Console.WriteLine(level2.LevelId);
        }

        private void GetLevel3(ILevelGet<Level3> repository)
        {
            var level3 = repository.Get(301);
            Console.WriteLine(level3.LevelId);
        }

        public void testContravariant()
        {
            //in 逆變性 Contravariant
            //衍生取代基底
            //AddLevel1(this.Level2Pools);編譯失敗
            AddLevel2(this.Level2Pools);
            AddLevel3(this.Level2Pools);
        }

        //interface ILevelAdd<in T>
        private void AddLevel1(ILevelAdd<Level1> repository)
        {
            //repository.Add(new Level1 { LevelId = 101, Name = "Level1" });
        }

        private void AddLevel2(ILevelAdd<Level2> repository)
        {
            //repository.Add(new Level2 { LevelId = 201, Name = "Level2" });
        }

        private void AddLevel3(ILevelAdd<Level3> repository)
        {
            //repository.Add(new Level3 { LevelId = 301, Name = "Level3" });
        }
    }
}
