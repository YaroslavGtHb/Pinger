using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using Ninject;
using Ninject.Modules;
using Ninject.Parameters;

namespace Pinger
{
    class Program
    {
        static void Main()
        {
            List<string> hosts = new List<string>(File.ReadAllLines("./hosts.txt"));

            IKernel kernel = new StandardKernel(new NinjectConfig());

            UniversalPinger pinger = kernel.Get<UniversalPinger>(new Parameter("rowhosts", hosts, true));
            pinger.Run();
        }
        //https://habr.com/ru/post/235995/
        //http://codeclimber.net.nz/archive/2009/02/05/how-to-use-ninject-with-aspnet-mvc/
        //https://steemit.com/utopian-io/@rufu/get-started-with-ninject-in-c-programming
    }
}
