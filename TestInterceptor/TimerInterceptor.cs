using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace TestInterceptor
{
    public class TimerInterceptor : IInterceptor
    {
        public void Intercept(Castle.DynamicProxy.IInvocation invocation)
        {
            var method = invocation.GetConcreteMethod();

            method = invocation.InvocationTarget.GetType().
                GetMethod(method.Name);

            if (method != null)
            {
                var attribute = method.GetCustomAttribute<MyTimerAttribute>();
                try
                {
                    if (attribute != null)
                    {
                        var sw = new Stopwatch();
                        sw.Start();
                        Console.WriteLine("Starting the log in Timer Interceptor");


                        invocation.Proceed();

                        sw.Stop();
                        Console.WriteLine($"Ending the log, ellapsed: {sw.ElapsedMilliseconds} ms");
                    }
                    else
                        invocation.Proceed();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception occurred in method: {invocation.Method.Name}: {ex.Message} ");
                }
               
            }
        }
    }
}
