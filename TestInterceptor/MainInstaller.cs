using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace TestInterceptor
{
    public class MainInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            path = Uri.UnescapeDataString(Path.GetDirectoryName(path));



            var filter = new AssemblyFilter(path);

            container.Register(
                Component.For<LogInterceptor>());

            container.Register(
                Component.For<TimerInterceptor>());

            container.Kernel.ComponentRegistered += KernelOnComponentRegistered;

            container.Register(
                Classes.
                    FromAssemblyInDirectory(filter).
                    BasedOn<IMyService>().
                    WithServiceAllInterfaces().
                    LifestyleTransient()
            );
        }

        private void KernelOnComponentRegistered(string key, IHandler handler)
        {
            handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(LogInterceptor)));
            handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(TimerInterceptor)));
        }
    }
}
