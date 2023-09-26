using static lw.Bool;
using System.Collections;
using static lw.Constants;
using System.Text.RegularExpressions;
using System.Linq;

namespace lw
{
    public static class console
    {
        /// <summary>
        ///     Выводит сообщение в консоль
        /// </summary>
        /// <param name="args">Значения</param>
        public static void log(params object[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                Console.Write(i < args.Length - 1 ? $"{args[i]}, " : args[i] + "\n");
            }
        }
    }

    public static class Out
    {
        /// <summary>
        ///     Выводит строку
        /// </summary>
        /// <param name="arg">Строка</param>
        /// <returns> Всегда возвращает 1 </returns>
        public static int print(dynamic arg)
        {
            if (arg is string str && str.Contains('*'))
            {
                var parts = str.Split('*');
                if (parts.Length == 2 && int.TryParse(parts[1], out int num))
                {
                    Console.Write(string.Concat(Enumerable.Repeat(parts[0].Trim(), num)));
                    return num;
                }
            }
            else
            {
                Console.Write(arg);
                return 1;
            }
            return 0;
        }

        // BRAND NEW FUNC: print("x * 20"); PRINTS xxxxxxxxxxxxxxxxxxxx

        /// <summary>
        ///     Выводит строки через пробел с переносом
        /// </summary>
        /// <param name="arg">Строка</param>
        /// <returns> Ничего не возвращает </returns>
        public static void echo(params dynamic?[] arg)
        {
            foreach (var item in arg)
            {
                Console.Write($"{item} ");
            }
            Console.Write(CS_EOL);
        }

        /// <summary>
        ///     Выводит строки через пробел с переносом
        /// </summary>
        /// <param name="arg">Строка</param>
        /// <returns> Ничего не возвращает </returns>
        public static void para(params dynamic?[] arg)
        {
            Console.Write(CS_EOL);
            foreach (var item in arg)
            {
                Console.Write($"{item} ");
            }
            Console.Write(CS_EOL + CS_EOL);
        }

        /// <summary>
        ///     Выводит форматированную строку
        /// </summary>
        /// <param name="format">Строка</param>
        /// <param name="args">Параметры</param>
        /// <returns> Возвращает длину строки </returns>

        public static int printf(string format, params object[] args)
        {
            string[] placeholderFormats = { "%s", "%d", "%b", "%c", "%f", "%u", "%x", "%+d" };
            int argsIndex = 0;

            for (int i = 0; i < placeholderFormats.Length; i++)
            {
                while (new Regex(placeholderFormats[i]).IsMatch(format))
                {
                    if (placeholderFormats[i] != "%f")
                    {
                        string replacement = $"{{{argsIndex}}}";
                        format = new Regex(placeholderFormats[i]).Replace(format, replacement, 1);

                        if (argsIndex < args.Length)
                        {
                            switch (placeholderFormats[i])
                            {
                                case "%b": args[argsIndex] = Convert.ToString((int)args[argsIndex], 2); break;
                                case "%c": args[argsIndex] = Convert.ToChar((int)args[argsIndex]); break;
                                case "%u": args[argsIndex] = Convert.ToUInt32(args[argsIndex]); break;
                                case "%x": args[argsIndex] = Convert.ToString((int)args[argsIndex], 16); break;
                                case "%+d":
                                    int number = (int)args[argsIndex];
                                    if (number > 0)
                                    {
                                        args[argsIndex] = $"+{number}";
                                    }
                                    else
                                    {
                                        args[argsIndex] = $"{number}";
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            throw new FormatException("Количество аргументов недостаточно для строки формата.");
                        }
                        argsIndex++;
                    }
                }
            }

            Console.Write(format, args);
            return format.Length + 1;
        }

        /* Пример:   printf("Hi %s, how old are you - Im %d?", "John", 16); Выведет: Hi John, how old are you - Im 16
         * Пример 2: printf("Hi, %s, i am %c yo.\n", "Josh", '0' + 1);      Выведет: Hi Josh, i am 1 yo. */

        /// <summary>
        ///     Выводит удобночитаемую информацию о переменной
        /// </summary>
        /// <param name="obj">Выражение для вывода на экран.</param>
        /// <param name="showTypes">По ум. <c>false</c>, при <c>true</c> дополнительно выводит тип выражения</param>
        /// <returns>Удобночитаемый массив</returns>
        public static void print_r(object obj, bool showTypes = false, int indent = 0)
        {
            string indentation = new(' ', indent * 4);
            if (obj == null)
            {
                Console.WriteLine($"{indentation}null");
            }
            else if (obj is string)
            {
                Console.WriteLine($"{indentation}\"{obj}\"{(showTypes ? " (string)" : "")}");
            }
            else if (obj is IDictionary dict)
            {
                Console.WriteLine($"Dictionary{(showTypes ? " (IDictionary)" : "")}");
                Console.WriteLine($"{indentation}(");
                int index = 0;
                foreach (var key in dict.Keys)
                {
                    var value = dict[key];
                    if (value is IEnumerable innerEnumerable && !(value is string))
                    {
                        Console.Write($"{indentation}    [{key}] => ");
                        print_r(innerEnumerable, showTypes, indent + 1);
                    }
                    else
                    {
                        Console.WriteLine($"{indentation}    [{key}] => {value}");
                    }
                    index++;
                }
                Console.WriteLine($"{indentation})");
            }
            else if (obj is IEnumerable enumerable)
            {
                Console.WriteLine($"Array{(showTypes ? " (IEnumerable)" : "")}");
                Console.WriteLine($"{indentation}(");
                int index = 0;
                foreach (var item in enumerable)
                {
                    if (item is IEnumerable innerEnumerable)
                    {
                        Console.Write($"{indentation}    [{index}] => ");
                        print_r(innerEnumerable, showTypes, indent + 1);
                    }
                    else
                    {
                        Console.WriteLine($"{indentation}    [{index}] => {item}");
                    }
                    index++;
                }
                Console.WriteLine($"{indentation})");
            }
            else
            {
                Console.WriteLine($"{indentation}{obj}{(showTypes ? $" ({obj.GetType().Name})" : "")}");
            }
        }
    }
}
