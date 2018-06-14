using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace TestInterceptor
{
    class Program
    {
        private static IWindsorContainer container;
        static void Main(string[] args)
        {
            InitializeCastle();

            var service = container.Resolve<IMyService>();
            service.DoStuffWithAttribute();
           var result = service.DoStuffWithOtherAttribute();
            Console.WriteLine(result);
            service.DoStuff();
            Console.ReadLine();
        }

        private static void InitializeCastle()
        {
            var currentAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            var codeBaseAbsolutePathUri = new Uri(currentAssembly.GetName().CodeBase).AbsolutePath;
            var path = Uri.UnescapeDataString(System.IO.Path.GetDirectoryName(codeBaseAbsolutePathUri));

            var filter = new AssemblyFilter(path);


            var castle = new WindsorContainer();

            castle.AddFacility<TypedFactoryFacility>();
            castle.Install(FromAssembly.InDirectory(filter));
            container = castle;
        }
    }
}
