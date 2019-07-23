using System;

namespace Injector
{
    public struct ExportedFunction
    {
        public string Name;

        public IntPtr Address;

        public ExportedFunction(string name, IntPtr address)
        {
            Name = name;
            Address = address;
        }
    }
    public enum MonoImageOpenStatus
    {
        MONO_IMAGE_OK,
        MONO_IMAGE_ERROR_ERRNO,
        MONO_IMAGE_MISSING_ASSEMBLYREF,
        MONO_IMAGE_IMAGE_INVALID
    }
    public class InjectorException : Exception
    {
        public InjectorException(string message) : base(message)
        {
        }

        public InjectorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
