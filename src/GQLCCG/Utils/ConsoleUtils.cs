using System;
using System.Linq;

namespace GQLCCG.Utils
{
    public static class ConsoleUtils
    {
        public static TType ReadObject<TType>(string repeatMessage)
        {
            return (TType) ReadObject(repeatMessage, typeof(TType));
        }

        public static object ReadObject(string repeatMessage, Type type)
        {
            object result;

            bool TryRead()
            {
                return TryChangeType(Console.ReadLine(), type, out result);
            }

            var isSuccess = TryRead();
            while (!isSuccess)
            {
                Console.WriteLine(repeatMessage);
                isSuccess = TryRead();
            }

            return result;
        }

        public static bool ReadYesNoKey(string message = null, string repeatMessage = "Wrong key. Only Y/N allowed. Please try again.")
        {
            if (!string.IsNullOrEmpty(message))
            {
                Console.Write($"{message} (y/n) ");
            }
            return ReadSpecificKey(repeatMessage, ConsoleKey.Y, ConsoleKey.N) == ConsoleKey.Y;
        }

        public static ConsoleKey ReadSpecificKey(string repeatMessage, params ConsoleKey[] keys)
        {
            ConsoleKey Read()
            {
                return Console.ReadKey().Key;
            }

            var key = Read();
            while (!keys.Contains(key))
            {
                Console.WriteLine();
                Console.WriteLine(repeatMessage);
                key = Read();
            }

            return key;
        }


        private static bool TryChangeType(object target, Type convertType, out object result)
        {
            try
            {
                result = Convert.ChangeType(target, convertType);
                return true;
            }
            catch (FormatException)
            {
                result = null;
                return false;
            }
        }
    }
}