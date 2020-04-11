using System;
using System.IO;

namespace Injector.Console
{
    internal static class Program
    {
        static string assemblyPath = ".Assembly.dll", 
            @namespace, className = "Loader";
        static void Main()//string[] args)
        {
            string[] args = { "Bombergrounds" };
            Injector injector = new Injector(args[0]);
            assemblyPath = args[0] + assemblyPath;
            @namespace = args[0]; Inject(injector); 
        }

        static void Inject(Injector injector)
        {
            byte[] assembly;
            try
            {
                assembly = File.ReadAllBytes(assemblyPath);
            }
            catch
            {
                System.Console.WriteLine("Could not read the file " + assemblyPath);
                return;
            }
            using (injector)
            {
                IntPtr remoteAssembly = IntPtr.Zero;

                try
                {
                    remoteAssembly = injector.Inject(assembly, @namespace, className, "Load");
                }
                catch (InjectorException ie)
                {
                    System.Console.WriteLine("Failed to inject assembly: " + ie);
                }
                catch (Exception exc)
                {
                    System.Console.WriteLine("Failed to inject assembly (unknown error): " + exc);
                }

                if (remoteAssembly == IntPtr.Zero)
                    return;

                System.Console.WriteLine($"{Path.GetFileName(assemblyPath)}: " +
                    (injector.Is64Bit
                    ? $"0x{remoteAssembly.ToInt64():X16}"
                    : $"0x{remoteAssembly.ToInt32():X8}"));

                // Ejection
                //System.Console.Write("Press enter to eject...");
                //System.Console.ReadLine(); injector.Eject(remoteAssembly, 
                //    @namespace, className, "Unload");
            }
        }
    }
}
