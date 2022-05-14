using AspectInjector.Broker;

using Microsoft.Extensions.DependencyInjection;

using System;

namespace AspectInjectorNote
{
    [WriteLog]
    class Program
    {
        public static int MyProperty { get; set; }
        static void Main(string[] args)
        {
            MyProperty = 1;
            var b = MyProperty;
            DoSomething(99);
        }


        public static int DoSomething(int a)
        {
            Console.WriteLine("Work");
            return a;
        }
    }

    [Aspect(Scope.Global)]
    [Injection(typeof(WriteLogAttribute))]
    public class WriteLogAttribute : Attribute
{
        [Advice(Kind.Before, Targets = Target.Method)]
        public void Before([Argument(Source.Name)] string name, [Argument(Source.Arguments)] object[] arguments)
        {
            Console.WriteLine($"{name} On Before {arguments[0]}");
}

        [Advice(Kind.After, Targets = Target.Method)]
        public void After([Argument(Source.Name)] string name, [Argument(Source.Arguments)] object[] arguments, [Argument(Source.ReturnValue)] object returnValue)
        {
            Console.WriteLine("On After");
}

        [Advice(Kind.Around, Targets = Target.Getter | Target.Method)]
        public object Around(
            [Argument(Source.Name)] string name,
            [Argument(Source.Arguments)] object[] arguments,
            [Argument(Source.Target)] Func<object[], object> target)
        {
            Console.WriteLine("On Around Before");

            var result = target(arguments);

            Console.WriteLine("On Around After");

            return result;
        }
    }
}