using System;
using System.IO;

namespace Injector.Console
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Injector injector = new Injector("BeanBattles");
            Inject(injector);
        }

        private static void PrintHelp()
        {
            const string help =
                "SharpMonoInjector 2.2\r\n\r\n" +
                "Usage:\r\n" +
                "smi.exe <inject/eject>";
            System.Console.WriteLine(help);
        }

        private static void Inject(Injector injector)
        {
            string assemblyPath = "BeanAssembly.dll",
                @namespace = "BeanAssembly",
                className = "Loader",
                methodName = "Load";
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
                    remoteAssembly = injector.Inject(assembly, @namespace, className, methodName);
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
            }
        }
    }
}
