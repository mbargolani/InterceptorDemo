using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestInterceptor
{
    public class MyService : IMyService
    {
        public void DoStuff()
        {
            throw new NotImplementedException();
        }

        [MyLog]
        public void DoStuffWithAttribute()
        {
           Console.WriteLine("Doing stuff with attribute MyLog");
        }

        [MyTimer]
        public string DoStuffWithOtherAttribute()
        {
            return "I want to return a result, let's see what happens";
        }
    }

    public interface IMyService
    {
        void DoStuff();

        void DoStuffWithAttribute();

        string DoStuffWithOtherAttribute();
    }
}
