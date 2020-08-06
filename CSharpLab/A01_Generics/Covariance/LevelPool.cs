using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab.A01_Generics.Covariance
{
    public interface ILevelGet<out T>
    {
        T Get(int id);
    }

    public interface ILevelAdd<in T>
    {
        void Add(T level);
    }

    public class LevelPool<T> : ILevelGet<T>, ILevelAdd<T>
        where T : class, ILevel, new()

    {
        private List<T> Pool { get; }

        public LevelPool()
        {
            this.Pool = new List<T>();
        }
        public void Add(T level)
        {
            this.Pool.Add(level);
        }

        public T Get(int id)
        {
            var t = this.Pool.SingleOrDefault(x => x.LevelId == id) ?? new T();
            return t;
        }
    }
}
