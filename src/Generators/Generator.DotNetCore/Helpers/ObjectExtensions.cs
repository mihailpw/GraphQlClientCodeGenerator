using System;
using System.Linq;

namespace Generator.DotNetCore.Helpers
{
    public static class ObjectExtensions
    {
        public static T ResolveArgument<T>(this object[] arguments)
        {
            if (arguments.Length != 1)
            {
                throw new InvalidOperationException("Should be 1 argument.");
            }
            if (!(arguments[0] is T value))
            {
                throw new InvalidOperationException($"Should be {typeof(T).Name} object.");
            }

            return value;
        }

        public static (TFirst first, TSecond second) ResolveArguments<TFirst, TSecond>(this object[] arguments)
        {
            if (arguments.Length != 2)
            {
                throw new InvalidOperationException("Should be 2 arguments.");
            }
            if (!(arguments[0] is TFirst firstValue))
            {
                throw new InvalidOperationException($"First parameter should be {typeof(TFirst).Name} object.");
            }
            if (!(arguments[1] is TSecond secondValue))
            {
                throw new InvalidOperationException($"Second parameter should be {typeof(TSecond).Name} object.");
            }

            return (firstValue, secondValue);
        }

        public static (TFirst first, TSecond[] second) ResolveArgumentsInfinity<TFirst, TSecond>(this object[] arguments)
        {
            if (arguments.Length < 2)
            {
                throw new InvalidOperationException("Should be at list 2 arguments.");
            }
            if (!(arguments[0] is TFirst firstValue))
            {
                throw new InvalidOperationException($"First parameter should be {typeof(TFirst).Name} object.");
            }

            var array = arguments.Skip(1).Cast<TSecond>().ToArray();
            return (firstValue, array);
        }
    }
}