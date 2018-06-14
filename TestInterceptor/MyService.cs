using System;

namespace TestInterceptor
{
    /// <summary>
    /// Implementation of IMyService
    /// Has three methods, one with MyLog attribute
    /// Another with MyTimer attribute and the last without any attribute and it throws an exception
    /// </summary>
    public class MyService : IMyService
    {
        public void DoStuff()
        {
            throw new Exception("Throwing exception here");
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

    /// <summary>
    /// Interface for the MyService
    /// </summary>
    public interface IMyService
    {
        void DoStuff();

        void DoStuffWithAttribute();

        string DoStuffWithOtherAttribute();
    }
}
