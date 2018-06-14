using System;
using System.Reflection;
using Castle.DynamicProxy;

namespace TestInterceptor
{
    public class LogInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var method = invocation.GetConcreteMethod();

            method = invocation.InvocationTarget.GetType().
                GetMethod(method.Name);

            if (method != null)
            {
                var attribute = method.GetCustomAttribute<MyLogAttribute>();
                try
                {
                    if (attribute != null)
                    {
                        Console.WriteLine("Starting the log");
                        invocation.Proceed();

                        Console.WriteLine("Ending the log");
                    }
                    else
                        invocation.Proceed();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception occurred: " + ex);
                }
               

                
            }
        }
    }
}
