using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab.A01_Generics.Covariance
{
    public interface ILevel
    {
        int LevelId { get; set; }
    }

    public class Level1: ILevel
    {
        public Level1()
        {

        }
        public int LevelId { get; set; }

        public string Name { get; set; }

        public int test1 { get; set; }
    }

    public class Level2: Level1
    {
        public int test2 { get; set; }
    }

    public class Level3 : Level2
    {
        public int test3 { get; set; }
    }
}
