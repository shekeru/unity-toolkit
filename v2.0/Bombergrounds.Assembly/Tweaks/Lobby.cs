using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Bombergrounds
{
    partial class Instance
    {
        public void BypassPasswords()
        {
        }
        // Load Defaults
        public static Field PrivateField<Field>(object src, string name)
        {
            return (Field)src.GetType().GetField(name, BindingFlags.NonPublic
                | BindingFlags.Instance).GetValue(src);
        }
        // Load Defaults
        public static void ForceValue<Type>(object src, string name, Type value)
        {
            src.GetType().GetField(name, BindingFlags.NonPublic
                | BindingFlags.Instance).SetValue(src, value);
        }
        public static void TestingM(object src)
        {
            var methodToReplace = src.GetType().GetMethod("CollidingWithWallOrObject", BindingFlags.NonPublic | BindingFlags.Instance);
            var methodToInject = src.GetType().GetMethod("CheckWeaponHit", BindingFlags.NonPublic | BindingFlags.Instance);
            RuntimeHelpers.PrepareMethod(methodToReplace.MethodHandle);
            RuntimeHelpers.PrepareMethod(methodToInject.MethodHandle);
            unsafe
            {
                if (IntPtr.Size == 4)
                {
                    int* inj = (int*)methodToInject.MethodHandle.Value.ToPointer() + 2;
                    int* tar = (int*)methodToReplace.MethodHandle.Value.ToPointer() + 2;
#if DEBUG
                    Console.WriteLine("\nVersion x86 Debug\n");

                    byte* injInst = (byte*)*inj;
                    byte* tarInst = (byte*)*tar;

                    int* injSrc = (int*)(injInst + 1);
                    int* tarSrc = (int*)(tarInst + 1);

                    *tarSrc = (((int)injInst + 5) + *injSrc) - ((int)tarInst + 5);
#else
                    Console.WriteLine("\nVersion x86 Release\n");
                    *tar = *inj;
#endif
                }
                else
                {

                    long* inj = (long*)methodToInject.MethodHandle.Value.ToPointer() + 1;
                    long* tar = (long*)methodToReplace.MethodHandle.Value.ToPointer() + 1;
#if DEBUG
                    Console.WriteLine("\nVersion x64 Debug\n");
                    byte* injInst = (byte*)*inj;
                    byte* tarInst = (byte*)*tar;


                    int* injSrc = (int*)(injInst + 1);
                    int* tarSrc = (int*)(tarInst + 1);

                    *tarSrc = (((int)injInst + 5) + *injSrc) - ((int)tarInst + 5);
#else
                    Console.WriteLine("\nVersion x64 Release\n");
                    *tar = *inj;
#endif
                }
            }
        }
}
}
