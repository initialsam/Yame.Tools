using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab
{
    //ref
    //https://blog.nearsoftjobs.com/escribe-un-contenedor-de-ioc-minimo-en-c-47fcf7c8bf70
    //https://ayende.com/blog/2886/building-an-ioc-container-in-15-lines-of-code
    public class MinimalContainer
    {
        private readonly Dictionary<Type, Type> types = new Dictionary<Type, Type>();
        public void Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            types[typeof(TInterface)] = typeof(TImplementation);
        }
        public TInterface Create<TInterface>()
        {
            return (TInterface)Create(typeof(TInterface));
        }
        private object Create(Type type)
        {
            var concreteType = types[type];
            var defaultConstructor = concreteType.GetConstructors()[0];
            var defaultParams = defaultConstructor.GetParameters();
            var parameters = defaultParams.Select(param => Create(param.ParameterType)).ToArray();
            return defaultConstructor.Invoke(parameters);
        }
    }

    public interface IWelcomer
    {
        void SayHelloTo(string name);
    }
    public class Welcomer : IWelcomer
    {
        private IWriter writer;
        public Welcomer(IWriter writer)
        {
            this.writer = writer;
        }
        public void SayHelloTo(string name)
        {
            writer.Write($"Hello {name}!");
        }
    }
    public interface IWriter
    {
        void Write(string s);
    }
    public class ConsoleWriter : IWriter
    {
        public void Write(string s)
        {
            Console.WriteLine(s);
        }
    }
}
