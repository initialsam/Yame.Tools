using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab.A04_Dictionary
{
    //看單元測試
    public sealed class ImmutableObjects : IEquatable<ImmutableObjects>
    {
        public int Id { get; }
        public string Name { get; }

        public ImmutableObjects(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public bool Equals(ImmutableObjects other) => other != null && this.Id == other.Id;

        public override int GetHashCode()
        {
            return this.Id.GetHashCode() + this.Name.GetHashCode();
        }
    }
}
