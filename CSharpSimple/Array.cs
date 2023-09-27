using static simple.Bool;
using System.Collections;
using static simple.Helpful;
using static simple.Constants;
using static simple.Constants.SortCase;

namespace simple
{
    public static class Arr
    {
        public static int currentIndex = 0;

        /// <summary>
        ///     Создаёт массив
        /// </summary>
        /// <param name="args">Переменные</param>
        /// <returns>Массив из переменных</returns>
        public static T[] array<T>(params T[] args)
        {
            return args;
        }

        /// <summary>
        ///     Создаёт массив
        /// </summary>
        /// <param name="args">Переменные</param>
        /// <returns>Массив из переменных</returns>
        public static object[] array(params object[] args)
        {
            return args;
        }

        /// <summary>
        ///     Меняет регистр всех ключей массива
        /// </summary>
        /// <param name="array">Массив</param>
        /// <param name="caseType">CASE_LOWER | CASE_UPPER</param>
        public static void array_change_value_case(ref string[] array, CaseType caseType)
        {
            string[] newArray = new string[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                string newValue = "";

                switch (caseType)
                {
                    case CaseType.CASE_LOWER:
                        newValue = array[i].ToString().ToLower();
                        break;
                    case CaseType.CASE_UPPER:
                        newValue = array[i].ToString().ToUpper();
                        break;
                    case CaseType.FIRST_UPPER:
                        newValue = char.ToUpper(array[i][0]) + array[i][1..];
                        break;
                    case CaseType.LAST_UPPER:
                        newValue = array[i][..^1] + char.ToUpper(array[i][^1]);
                        break;
                    case CaseType.UNDERSCORE:
                        newValue = $"__{array[i]}__";
                        break;
                }
                newArray[i] = newValue;
            }
            array = newArray;
        }
        // Пример использования: array_change_value_case(ref arr, CASE_UPPER); при подключённой библиотеке Constants.CaseType

        /// <summary>
        ///     Меняет регистр всех значений массива
        /// </summary>
        /// <param name="array">Массив</param>
        /// <param name="caseType">CASE_LOWER | CASE_UPPER</param>
        public static string[] array_change_value_case<T>(T[] array, CaseType caseType)
        {
            string[] newArr = Array.Empty<string>();
            for (int i = 0; i < array.Length; i++)
            {
                string val = "";
                switch (caseType)
                {
                    case CaseType.CASE_LOWER:
                        val = val.ToString().ToLower();
                        break;
                    case CaseType.CASE_UPPER:
                        val = val.ToString().ToUpper();
                        break;
                    case CaseType.FIRST_UPPER:
                        val = char.ToUpper(val[0]) + val[1..];
                        break;
                    case CaseType.LAST_UPPER:
                        val = val[..^1] + char.ToUpper(val[^1]);
                        break;
                    case CaseType.UNDERSCORE:
                        val = $"__{val}__";
                        break;
                }
                array_push(ref newArr, val);
            }
            return newArr;
        }

        /// <summary>
        ///     Разбивает массив на фрагменты и составляем из них  двумерный массив
        /// </summary>
        /// <param name="array">Исходный массив</param>
        /// <param name="length">Длина фрагментов</param>
        /// <param name="result">Результирующий массив</param>
        /// <exception cref="ArgumentException"></exception>
        public static void array_chunk<T>(T[] array, int length, out T[][] result)
        {
            if (length <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length), "Длина должна быть > 0");
            }
            if (length > array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(length), "Длина должна быть <=, чем длина массива");
            }

            int numOfChunks = (int) Math.Ceiling( (double) array.Length / length);
            result = new T[numOfChunks][];

            int index = 0;
            for (int i = 0; i < numOfChunks; i++)
            {
                int remainingItems = array.Length - index;
                int currentChunkLength = Math.Min(length, remainingItems);
                result[i] = new T[currentChunkLength];

                for (int j = 0; j < currentChunkLength; j++)
                {
                    result[i][j] = array[index++];
                }
            }
        }
        /*
         *  ------------------------------------------------------------------------------------
         *  Пример выполнения:
         *  ------------------------------------------------------------------------------------
         *  
         *  try
         *  {
         *      string[] arr = new string[] { "Abc", "abC", "Cba", "cba" }; // Длина массива = 4
         *      string[][] str = { Empty<string>() };
         *      array_chunk(arr, 2, out str);
         *      print_r(str); // Выведет массив с двумя массивами по 2 элемента
         *  }
         *  catch (Exception e)
         *  {
         *      printf("Предупреждение: %s", e.Message);
         *      press();
         *      return;
         *  }
         */

        /// <summary>
        ///     Возвращает массив из значений одного столбца входного массива
        /// </summary>
        /// <param name="key">Ключ массива</param>
        /// <param name="matrix">Массив</param>
        /// <returns>Возвращает новый массив, состоящий из key-элементов</returns>
        public static object[] array_column(int key, dynamic[][] matrix)
        {
            dynamic[] result = array();
            foreach (var item in matrix)
            {
                for (int i = 0; i < item.Length; i++)
                {
                    if (i == key)
                    {
                        array_push(ref result, item[i]);
                    }
                }
            }
            return result;
        }

        /// <summary>
        ///     Возвращает массив из значений одного столбца входного массива
        /// </summary>
        /// <param name="key">Ключ массива</param>
        /// <param name="matrix">Массив</param>
        /// <returns>Возвращает новый массив, состоящий из key-элементов</returns>
        public static object[] array_column(int key, dynamic[] arr)
        {
            dynamic[] result = array();
            for (int i = 0; i < arr.Length; i++)
            {
                if (i == key)
                {
                    array_push(ref result, arr[i]);
                }
            }
            return result;
        }

        /// <summary>
        ///     Возвращает словарь, ключи которого - первый массив, а значения - второй массив
        /// </summary>
        /// <param name="first_array">Ключи словаря</param>
        /// <param name="second_array">Значения словаря</param>
        /// <returns>Возвращает новый словарь, состоящий из first_array ключей и second_array значений</returns>
        public static Dictionary<dynamic, dynamic> array_combine(dynamic[] first_array, dynamic[] second_array)
        {
            if (first_array.Length == second_array.Length)
            {
                Dictionary<dynamic, dynamic> dict = new();
                for (int i = 0; i < first_array.Length; i++)
                {
                    dynamic key = first_array[i];
                    dynamic value = second_array[i];
                    dict.Add(key, value);
                }
                return dict;
            }
            else
            {
                throw new Exception("Длина массивов не сходится");
            }
        }

        /// <summary>
        ///     Подсчитывает количество вхождений каждого отдельного значения в массиве
        /// </summary>
        /// <param name="arr">Ключи словаря</param>
        /// <returns>Возвращает словарь со значениями <paramref name="arr"/> в качестве ключей и их количества в качестве значений.</returns>
        public static Dictionary<T, int> array_count_values<T>(IEnumerable<T> arr) where T : notnull
        {
            Dictionary<T, int> dict = new();
            foreach (var item in arr)
            {
                if (dict.ContainsKey(item))
                {
                    dict[item]++;
                }
                else
                {
                    dict[item] = 1;
                }
            }
            return dict;
        }

        /*
         *   ----------------------------------------------------------------
         *   Пример использования:
         *   ----------------------------------------------------------------
         *   
         *   string[] arr = { "Hello", "Array", "Array", "World" };
         *   Dictionary<string, int> dict = array_count_values(arr);
         *   print_r(arr, dict);
         */

        public static T[] array_fill_rand<T>(int length, T min, T max) where T : struct, IComparable<T>, IConvertible
        {
            Random random = new();
            T[] haystack = Array.Empty<T>();
            dynamic minValue = Convert.ChangeType(min, typeof(T));
            dynamic maxValue = Convert.ChangeType(max, typeof(T));
            for (int i = 0; i < length; i++)
            {
                dynamic value = Math.Round(random.NextDouble() * (maxValue - minValue) + minValue, 1);
                Array.Resize(ref haystack, haystack.Length + 1);
                haystack[i] = (T)value;
            }
            return haystack;
        }

        /// <summary>
        ///     Фильтрует элементы массива с помощью callback-функции
        /// </summary>
        /// <typeparam name="T">Любой тип</typeparam>
        /// <param name="array">Массив</param>
        /// <param name="callback">Пользовательская функция</param>
        /// <returns>Отфильтрованный массив</returns>
        public static T[] array_filter<T>(
            T[] array,
            Func<T, bool>? callback = null
        )
        {
            callback ??= value => value != null;

            var resultList = new List<T>();
            foreach (T item in array)
            {
                if (callback(item))
                {
                    resultList.Add(item);
                }
            }

            return resultList.ToArray();
        }

        /// <summary>
        ///     Проверяет, является ли массив списком
        /// </summary>
        /// <param name="value">Переменная</param>
        /// <returns>Возвращает true, если массив или список, иначе false</returns>
        public static bool array_is_list(dynamic value)
        {
            if (value is IList) return true;
            return false;
        }

        /// <summary>
        ///     Проверяет, присутствует ли в массиве указанный индекс
        /// </summary>
        /// <param name="key">Ключ массива</param>
        /// <param name="array">Массив</param>
        /// <returns>Возвращает true, если присутствует, иначе false</returns>
        public static bool array_key_exists<T>(int key, T[] array)
        {
            if (key < array.Length) return true;
            return false;
        }

        /// <summary>
        ///     Получает первый элемент массива
        /// </summary>
        /// <param name="array">Массив</param>
        /// <returns></returns>
        public static T array_key_first<T>(T[] array) where T : notnull
        {
            return array[0];
        }

        /// <summary>
        ///     Получает первый ключ словаря
        /// </summary>
        /// <param name="dictionary">Словарь</param>
        /// <returns></returns>
        public static TKey array_key_first<TKey, TValue>(Dictionary<TKey, TValue> dictionary) where TKey : notnull
        {
            return dictionary.Keys.First();
        }

        /// <summary>
        ///     Получает последний элемент массива
        /// </summary>
        /// <param name="array">Массив</param>
        /// <returns>Последний элемент массива</returns>
        public static T array_key_last<T>(T[] array) where T : notnull
        {
            return array[array.Length - 1];
        }

        

        public static int[] array_keys<T>(T[] haystack, string? value = null, bool strict = false)
        where T : notnull
        {
            List<int> keys = new();

            for (int i = 0; i < haystack.Length; i++)
            {
                if (value is null)
                {
                    keys.Add(i);
                }
                else
                {
                    if (strict)
                    {
                        if (value.ToString().Equals(haystack[i].ToString()))
                        {
                            keys.Add(i);
                        }
                    }
                    else
                    {
                        if (haystack[i] != null && haystack[i].ToString().Contains(value.ToString().Replace('.', ',')))
                        {
                            keys.Add(i);
                        }
                    }
                }
            }

            return keys.ToArray();
        }

        /*  
         *  --------------------------------------------------------------------------------------------
         *  Пример использования:
         *  --------------------------------------------------------------------------------------------
         *  
         *  double[] arr = array(1.1, -2.3, 3.7, 4.1, 5.6, 6.1, 7.1);
         *  int[] keys = array_keys(arr, "3.7", false);
         *  
         *  --------------------------------------------------------------------------------------------
         *  Выведет { 0, 2 }
         *  --------------------------------------------------------------------------------------------
         */

        

        /// <summary>
        ///     Применяет callback-функцию ко всем элементам указанных массивов
        /// </summary>
        /// <param name="array">Массив</param>
        /// <param name="userFunc">Пользовательская callback-функция</param>
        public static T[] array_map<T>(ref T[] array, Func<T, T> userFunc)
        {
            T[] newArray = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                newArray[i] = userFunc(array[i]);
            }
            array = newArray;
            return array;
        }

        /// <summary>
        ///     Применяет callback-функцию ко всем элементам указанных массивов
        /// </summary>
        /// <param name="array">Массив</param>
        /// <param name="userFunc">Пользовательская callback-функция</param>
        public static T[] array_map<T>(T[] array, Func<T, T> userFunc)
        {
            T[] newArray = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                newArray[i] = userFunc(array[i]);
            }
            return newArray;
        }

        /*
         *  ---------------------------------------------------------
         *  Пример использования:
         *  ---------------------------------------------------------
         *  
         *  static void test_print(object item, string key)
         *  T square<T>(T x) where T : struct
         *  {
         *      return Math.Round((dynamic)x * (dynamic)x, 2);
         *  }
         *  
         *  double[] arr = array(1.1, -2.3, 3.7, 4.1, 5.6, 6.1, 7.1);
         *  array_map(ref arr, square);
         *  
         *  ---------------------------------------------------------
         *  Выведет:
         *  ---------------------------------------------------------
         *  
         *  Array
         *  (
         *      0 => 1,21,
         *      1 => 5,29,
         *      2 => 13,69,
         *      3 => 16,81,
         *      4 => 31,36,
         *      5 => 37,21,
         *      6 => 50,41
         *  )
         */

        /// <summary>
        ///     Дополнить массив определённым значением до указанной длины
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">Массив</param>
        /// <param name="length">Новая длина</param>
        /// <param name="value">Значение</param>
        public static void array_pad<T>(ref T[] array, int length, dynamic value)
        {
            try
            {
                if (length >= array.Length)
                {
                    Array.Resize(ref array, length);
                    for (int i = array.Length - 1; i < length; i++)
                    {
                        array[i] = value;
                    }
                }
                else
                {
                    throw new Exception("Длина не может быть меньше длины массива");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Предупрежение: {0}", e.Message);
            }
        }

        /// <summary>
        ///     Извлекает последний элемент массива
        /// </summary>
        /// <param name="array">Массив</param>
        public static void array_pop<T>(ref T[] array)
        {
            Array.Resize(ref array, array.Length - 1);
        }

        /// <summary>
        ///     Вычислить произведение значений массива
        /// </summary>
        /// <typeparam name="T">Тип элементов массива, должен быть int или float.</typeparam>
        /// <param name="array">Массив</param>
        /// <returns>Возвращает произведение как тип integer или float.</returns>
        public static T? array_product<T>(T[] array, int? round = null) where T : notnull
        {
            if (!typeof(T?).IsAssignableFrom(typeof(int)) && !typeof(T?).IsAssignableFrom(typeof(float)) && !typeof(T?).IsAssignableFrom(typeof(double)))
            {
                throw new ArgumentException("Метод поддерживает только типы int, double и float.");
            }

            dynamic product = 1;
            for (int i = 0; i < array.Length; i++)
            {
                product *= array[i];
            }
            if (round is null)
                return product;

            if (round < 0)
                throw new ArgumentException("Кол-во знаков после запятой не может быть меньше 0");

            return Math.Round(product, (int)round);
        }

        public static T? Product<T>(this T[] array, int? round = null) where T: notnull
        {
            return array_product(array, round);
        }

        /// <summary>
        ///     Добавляет один или несколько элементов в конец массива
        /// </summary>
        /// <param name="array">Массив</param>
        /// <param name="args">Переменные</param>
        public static void array_push<T>(ref T[] array, params T[] args)
        {
            T[] newarray = new T[array.Length + args.Length];
            for (int i = 0; i < newarray.Length; i++)
            {
                if (i < array.Length)
                {
                    newarray[i] = array[i];
                }
                else
                {
                    newarray[i] = args[i - array.Length];
                }
            }
            array = newarray;
        }

        /// <summary>
        ///     Выбирает один или несколько случайных элементов из массива
        /// </summary>
        /// <param name="arr">Входной массив</param>
        /// <param name="num">Определяет количество выбираемых элементов.</param>
        public static T[] array_rand<T>(T[] arr, int num)
        {
            if (num <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(num), "Количество элементов должно быть больше или равно 0");
            }

            if (num > arr.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(num), "Количество элементов должно быть меньше или равно длине массива");
            }

            T[] randArray = new T[num];
            Random rand = new();

            for (int i = 0; i < num; i++)
            {
                int index = rand.Next(arr.Length - i);
                randArray[i] = arr[index];
                arr[index] = arr[arr.Length - i - 1];
            }

            return randArray;
        }

        /*
         *  Пример использования:
         *  Dictionary<string, int> dict1 = new()
         *  {
         *      { "key1", 1 },
         *      { "key2", 2 },
         *      { "key3", 3 },
         *      { "key4", 4 }
         *  };
         *  string[] str = dict1.ArrayRand(2);
         */

        /// <summary>
        ///     Итеративно уменьшает массив к единственному значению, используя callback-функцию
        /// </summary>
        /// <param name="source">Входной массив.</param>
        /// <param name="callbackFunc">Пользовательская callback-функция</param>
        /// <param name="initial">Стартовое значение, если введён пустой массив, вернётся это значение</param>
        /// <returns>Возвращает получившееся значение.</returns>
        public static TResult? array_reduce<TResult, TItem>(
            IEnumerable<TItem> source,
            Func<TResult, TItem, TResult?> callbackFunc,
            TResult? initial = default
        )
        {
            return source.Aggregate(initial, callbackFunc);
        }

        /*
         *  --------------------------------------------------------------------------
         *  Пример использования:
         *  --------------------------------------------------------------------------
         *  
         *  int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
         *  int val = array_reduce(arr, (accumulator, item) => accumulator + item, 0);
         *  
         *  --------------------------------------------------------------------------
         *  Выведет:
         *  --------------------------------------------------------------------------
         *  45 (сумма всех элементов)
         */

        

        /// <summary>
        ///     Возвращает массив с элементами в обратном порядке
        /// </summary>
        /// <param name="array">Входной массив.</param>
        /// <returns>Возвращает массив с элементами в обратном порядке</returns>
        public static T[] array_reverse<T>(T[] array)
        {
            T[] newArray = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                newArray[i] = array[array.Length - 1 - i];
            }
            return newArray;
        }

        /// <summary>
        ///     Возвращает массив с элементами в обратном порядке
        /// </summary>
        /// <param name="array">Входной массив.</param>
        /// <returns>Возвращает массив с элементами в обратном порядке</returns>
        public static T[] ArrayReverse<T>(this T[] array)
        {
            return array_reverse(array);
        }

        /// <summary>
        ///     Осуществляет поиск данного значения в массиве и возвращает ключ первого найденного элемента в случае успешного выполнения
        /// </summary>
        /// <param name="needle">Искомое значение.</param>
        /// <param name="haystack">Массив.</param>
        /// <param name="strict">Если третий параметр strict установлен в true, то функция array_search() будет искать идентичные элементы в haystack. Это означает, что также будут проверяться типы needle в haystack, а объекты должны быть одним и тем же экземпляром.</param>
        /// <returns>
        ///     Возвращает ключ для needle, если он был найден в массиве, иначе <b>false</b>. Если needle присутствует в haystack более одного раза,
        ///     будет возвращён первый найденный ключ.Для того, чтобы возвратить ключи для всех найденных значений, используйте функцию <code>array_keys()</code>
        ///     с необязательным параметром search_value.
        /// </returns>
        public static dynamic array_search<T>(dynamic needle, T[] haystack, bool strict = false) where T: notnull
        {
            if (string.IsNullOrEmpty(needle))
            {
                throw new ArgumentNullException(nameof(needle), "Искомое значение не может быть пустым или равным null");
            }
            if (haystack.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(haystack), "Массив не должен быть пустым");
            }

            if (strict)
            {
                for (int i = 0; i < haystack.Length; i++)
                {
                    if (needle.Equals(haystack[i]))
                        return i;
                }
            }
            else
            {
                if (haystack is string[])
                {
                    for (int i = 0; i < haystack.Length; i++)
                    {
                        string? arrayItem = haystack[i].ToString()?.ToLower();
                        if (needle.ToString().Equals(arrayItem))
                            return i;
                    }
                }
                else
                {
                    for (int i = 0; i < haystack.Length; i++)
                    {
                        string? arrayItem = haystack[i].ToString()?.Replace('.', ',');
                        if (needle.ToString().Equals(arrayItem))
                            return i;
                    }
                }
                
            }
            return false;
        }

        /*
         *  dynamic search = array_search(4, arr); -- dynamic, так как метод возвращает bool | int значения
         *  echo(search); -- выдаст false
         */

        /// <summary>
        ///     Извлекает первый элемент массива
        /// </summary>
        /// <param name="array">Входной массив</param>
        /// <returns>Возвращает извлекаемое значение или null, если array пуст или не является массивом.</returns>
        public static dynamic array_shift<T>(ref T[] array) where T: notnull
        {
            if (!array.GetType().IsArray || array.Length == 0 || array[0] == null)
            {
                return false;
            }
            dynamic firstVal = array[0];
            T[] newArray = Array.Empty<T>();
            Array.Resize(ref newArray, array.Length - 1);
            for (int i = 1; i < array.Length; i++)
            {
                newArray[i - 1] = array[i];
            }
            array = newArray;
            return firstVal;
        }

        /// <summary>
        ///     Выбирает срез массива
        /// </summary>
        /// <param name="array">
        ///     Входной массив.
        /// </param>
        /// <param name="offset">
        ///     Если параметр <b>offset</b> неотрицательный, последовательность начнётся на указанном расстоянии от начала <b>array</b>.<br/>
        ///     Если <b>offset</b> отрицательный, последовательность начнётся с конца <b>array</b>. <code><b>Замечание:</b><br/>
        ///     Обратите внимание, что параметр <b>offset</b> обозначает положение в массиве, а не ключ.</code>
        /// </param>
        /// <param name="length">
        ///     Если в эту функцию передан положительный параметр <b>length</b>, последовательность будет включать количество элементов меньшее или равное <b>length</b>.<br/>
        ///     Если количество элементов массива меньше чем параметр <b>length</b>, то только доступные элементы массива будут присутствовать.</br>
        ///     Если в эту функцию передан отрицательный параметр <b>length</b>, последовательность остановится на указанном расстоянии от конца массива.</br>
        ///     Если он опущен, последовательность будет содержать все элементы с <b>offset</b> до конца массива <b>array</b>.
        /// </param>
        /// <param name="preserve_keys"></param>
        /// <returns>Возвращает срез. Если смещение больше длины массива, то будет возвращён пустой массив.</returns>
        public static T[] array_slice<T>(T[] array, int offset, int? length = null, bool preserve_keys = false) where T : notnull
        {
            int len = length ?? array.Length - offset;

            if (len < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(len), "Длина не может быть меньше 0");
            }

            if (len == 0)
            {
                return Array.Empty<T>();
            }

            if (offset < 0)
            {
                offset = array.Length + offset;
            }

            if (offset >= array.Length)
            {
                return Array.Empty<T>();
            }

            int newLen = Math.Min(len, array.Length - offset);
            T[] newArray = new T[newLen];

            for (int i = 0; i < newLen; i++)
            {
                newArray[i] = array[i + offset];
            }

            return newArray;
        }

        /// <summary>
        ///     Выбирает срез массива
        /// </summary>
        /// <param name="array">
        ///     Входной массив.
        /// </param>
        /// <param name="offset">
        ///     Если параметр <b>offset</b> неотрицательный, последовательность начнётся на указанном расстоянии от начала <b>array</b>.<br/>
        ///     Если <b>offset</b> отрицательный, последовательность начнётся с конца <b>array</b>. <code><b>Замечание:</b><br/>
        ///     Обратите внимание, что параметр <b>offset</b> обозначает положение в массиве, а не ключ.</code>
        /// </param>
        /// <param name="length">
        ///     Если в эту функцию передан положительный параметр <b>length</b>, последовательность будет включать количество элементов меньшее или равное <b>length</b>.<br/>
        ///     Если количество элементов массива меньше чем параметр <b>length</b>, то только доступные элементы массива будут присутствовать.</br>
        ///     Если в эту функцию передан отрицательный параметр <b>length</b>, последовательность остановится на указанном расстоянии от конца массива.</br>
        ///     Если он опущен, последовательность будет содержать все элементы с <b>offset</b> до конца массива <b>array</b>.
        /// </param>
        /// <param name="replacement">
        ///     <para>
        ///         Если передан массив <b>replacement</b> в качестве аргумента, тогда удалённые элементы будут заменены элементами этого массива.
        ///     </para>
        ///     <para>
        ///         Если параметры <b>offset</b> и <b>length</b> таковы, что из исходного массива не будет ничего удалено, тогда элементы массива<br/>
        ///         <b>replacement</b> будут вставлены на позицию <b>offset</b>.
        ///     </para>
        /// </param>
        /// <returns>Возвращает массив, содержащий удалённые элементы.</returns>
        public static T[] array_splice<T>(
            T[]? array,
            int offset,
            int? length = null,
            T[]? replacement = null
        ) where T : notnull
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            int len = length ?? array.Length - offset;

            if (len < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(len), "Длина не может быть меньше 0");
            }

            if (len == 0)
            {
                return Array.Empty<T>();
            }

            if (offset < 0)
            {
                offset = array.Length + offset;
                if (offset < 0)
                {
                    offset = 0;
                }
            }

            if (offset >= array.Length)
            {
                return Array.Empty<T>();
            }

            int newLen = Math.Min(len, array.Length - offset);
            T[] newArray = new T[newLen];

            for (int i = 0; i < newLen; i++)
            {
                newArray[i] = array[i + offset];
            }

            if (replacement != null)
            {
                int replacementLen = replacement.Length;
                int diff = newLen - replacementLen;

                if (diff < 0)
                {
                    Array.Resize(ref newArray, newArray.Length - diff);
                }
                else if (diff > 0)
                {
                    Array.Resize(ref newArray, newArray.Length + diff);
                }

                for (int i = 0; i < replacementLen; i++)
                {
                    newArray[i + offset] = replacement[i];
                }
            }

            return newArray;
        }

        /// <summary>
        ///     Вычисляет сумму всех элементов массива
        /// </summary>
        /// <param name="arr">Массив</param>
        /// <returns>Сумму элементов массива</returns>
        public static dynamic? array_sum<T>(T[] arr) where T: notnull
        {
            T? type = default;
            return array_reduce(arr, (accum, item) => (dynamic)accum + (dynamic)item, type);
        }

        /*
         *  ----------------------------------------------------------------------------------------------------------------------
         *  Пример использования:
         *  ----------------------------------------------------------------------------------------------------------------------
         *  
         *  double[] arr = array(1.1, -2.3, 3.7, 4.1, 5.6, 6.1, 7.1);
         *  echo(array_sum(arr))
         *  
         *  ----------------------------------------------------------------------------------------------------------------------
         *  В результате выведет: 25.4
         *  ----------------------------------------------------------------------------------------------------------------------
         *  Замечание:
         *  print_r(array_sum(arr)) эквивалентно print_r(array_reduce(arr, (accum, item) => (dynamic)accum + (dynamic)item, 0.0));
         *  ----------------------------------------------------------------------------------------------------------------------
         */

        /// <summary>
        ///     Вычисляет сумму всех элементов массива
        /// </summary>
        /// <param name="arr">Массив</param>
        /// <returns>Сумму элементов массива</returns>
        public static dynamic? Sum<T>(this T[] arr) where T : notnull
        {
            return array_sum(arr);
        }

        public static T[] array_unique<T>(T[] array, SortCase flags = SORT_REGULAR)
        {
            switch (flags)
            {
                case SORT_REGULAR:
                    return array.Distinct().ToArray();
                case SORT_NUMERIC:
                    return array.Distinct().OrderBy(x => Convert.ToDouble(x)).ToArray();
                case SORT_STRING:
                    return array.Distinct().OrderBy(x => Convert.ToString(x)).ToArray();
                case SORT_LOCALE_STRING:
                    return array.Distinct().OrderBy(x => Convert.ToString(x), StringComparer.CurrentCulture).ToArray();
                default:
                    throw new ArgumentException("Неверный флаг");
            }
        }

        /*
         *  ---------------------------------------------------------
         *  Пример:
         *  ---------------------------------------------------------
         *  
         *  double[] arr = array(1.1, -2.3, 3.7, 4.1, 5.6, 6.1, 7.1);
         *  print_r(array_unique(arr, SORT_NUMERIC));
         *  
         *  ---------------------------------------------------------
         *  Выведет:
         *  ---------------------------------------------------------
         *  
         *  Array
         *  (
         *      -2,3,
         *      1,1,
         *      3,7,
         *      4,1,
         *      5,6,
         *      6,1,
         *      7,1
         *  )
         */

        public static T[] array_unshift<T>(ref T[] array, params dynamic[] values)
        {
            List<T> newArray = new();
            foreach (var value in values)
            {
                newArray.Add(value);
            }
            for (int i = 0; i < array.Length; i++)
            {
                newArray.Add(array[i]);
            }
            array = newArray.ToArray();
            return newArray.ToArray();
        }

        /*
         *  ---------------------------------------------------------
         *  Пример использования:
         *  ---------------------------------------------------------
         *  
         *  double[] arr = array(1.1, -2.3, 3.7, 4.1, 5.6, 6.1, 7.1);
         *  array_unshift(ref arr, 2.3, 4.3);
         *  print_r(arr);
         *  
         *  ---------------------------------------------------------
         *  Выведет:
         *  ---------------------------------------------------------
         *  
         *  Array
         *  (
         *      2,3,
         *      4,3,
         *      1,1,
         *      -2,3,
         *      3,7,
         *      4,1,
         *      5,6,
         *      6,1,
         *      7,1
         *  )
         */

        public static bool array_value_exists<T>(T[] array, dynamic value) where T: notnull
        {
            foreach (var item in array)
            {
                if (item.Equals(value))
                    return true;
            }
            return false;
        }

        /// <summary>
        ///     Рекурсивно применяет пользовательскую функцию к каждому элементу массива
        /// </summary>
        /// <param name="array"> <em>Входной словарь</em></param>
        /// <param name="callback">
        /// Обычно, <paramref name="callback"/> принимает два параметра. <br/>
        /// Первым параметром является значение элемента массива <paramref name="array"/>, а вторым - его ключ.
        /// </param>
        /// <remarks>
        /// <c>
        ///     Замечание:
        /// </c>
        /// Используйте <strong>callback</strong> функцию в виде void метода, например:
        /// <code>
        ///     void printl (object item, string key)
        ///     {
        ///         WriteLine($"{key} => {item}");
        ///     }
        /// </code>
        /// </remarks>
        public static void array_walk_recursive<T>(T[] array, Action<int, object> callback) where T : notnull
        {
            for (int i = 0; i < array.Length; i++)
            {
                callback(i, array[i]);
            }
        }

        /// <summary>
        ///     Применяет заданную пользователем <c>функцию</c> к каждому элементу массива.
        /// </summary>
        /// <param name="array">Входной словарь.</param>
        /// <param name="callback">Обычно функция <em>callback</em> принимает два параметра. В качестве первого параметра идёт значение элемента массива <c>array</c>, а ключ - в качестве второго.</param>
        public static void array_walk<T>(ref T[] array, Func<T, T> callback) where T : notnull
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = callback(array[i]);
            }
        }

        /// <summary>
        ///     Сортирует массив в алфавитном порядке
        /// </summary>
        /// <param name="array">Массив</param>
        /// <returns> Возвращает отсортированный массив </returns>
        public static string[] asort(string[] array)
        {
            // Array.Sort(arr, (x, y) => string.Compare(x, y));
            Array.Sort(array, StringComparer.OrdinalIgnoreCase);
            return array;
        }

        /// <summary>
        ///     Сортирует массив в алфавитном порядке
        /// </summary>
        /// <param name="array">Массив</param>
        /// <returns> Возвращает отсортированный массив </returns>
        public static string[] AlphaSort(this string[] array)
        {
            return asort(array);
        }

        /// <summary>
        ///     Сортирует массив в обратном алфавитном порядке
        /// </summary>
        /// <param name="array">Массив</param>
        /// <returns> Возвращает отсортированный массив </returns>
        public static string[] arsort(string[] array)
        {
            Array.Sort(array, (x, y) => string.Compare(y, x));
            return array;
        }

        /// <summary>
        ///     Сортирует массив в обратном алфавитном порядке
        /// </summary>
        /// <param name="array">Массив</param>
        /// <returns> Возвращает отсортированный массив </returns>
        public static string[] RevAlphaSort(this string[] array)
        {
            return arsort(array);
        }

        /// <summary>
        ///     Подсчитывает количество элементов массива
        /// </summary>
        /// <param name="array">Массив</param>
        /// <returns>Возвращает кол-во элементов в массиве</returns>
        public static int count<T>(T[] array)
        {
            int counter = 0;
            foreach (var _ in array) counter++;
            return counter;
        }

        /// <summary>
        ///     Подсчитывает количество элементов массива
        /// </summary>
        /// <param name="array">Массив</param>
        /// <returns>Возвращает кол-во элементов в массиве</returns>
        public static int Count<T>(this T[] array)
        {
            return count(array);
        }

        /// <summary>
        ///     Возвращает текущий элемент массива
        /// </summary>
        /// <param name="array">Массив</param>
        /// <returns>Текущий элемент массива</returns>
        public static T current<T>(T[] array)
        {
            return array[currentIndex];
        }

        /// <summary>
        ///     Возвращает текущий элемент массива
        /// </summary>
        /// <param name="array">Массив</param>
        /// <returns>Текущий элемент массива</returns>
        public static T Current<T>(this T[] array)
        {
            return current(array);
        }

        /// <summary>
        ///     Возвращает последний элемент массива
        /// </summary>
        /// <param name="array">Массив</param>
        /// <returns>Последний элемент массива</returns>
        public static T end<T>(T[] array)
        {
            currentIndex = array.Length - 1;
            return current(array);
        }

        /// <summary>
        ///     Возвращает последний элемент массива
        /// </summary>
        /// <param name="array">Массив</param>
        /// <returns>Последний элемент массива</returns>
        public static T End<T>(this T[] array)
        {
            return end(array);
        }

        /// <summary>
        ///     Проверяет, присутствует ли в массиве значение
        /// </summary>
        /// <param name="needle">Искомое значение. Если - строка, сравнение будет произведено с учётом регистра.</param>
        /// <param name="array">Массив</param>
        /// <param name="strict">Если третий параметр установлен в true, тогда функция in_array() также проверит соответствие типов параметра и соответствующего значения массива.</param>
        /// <returns> Возвращает true, если значение присутствует в массиве, иначе false </returns>
        public static bool in_array(object needle, IEnumerable array, bool strict = false)
        {
            foreach (var item in array)
            {
                if (strict)
                {
                    if (item.Equals(needle)) return true;
                }
                else
                {
                    if (item.ToString() == needle.ToString()) return true;
                }
            }
            return false;
        }
        // Принцип использования: in_array("3", new List<int> { 1, 2, 3 }, true) - Вернёт false

        /// <summary>
        ///     Псевдоним array_key_exists()
        /// </summary>
        /// <param name="key">Ключ массива</param>
        /// <param name="array">Массив</param>
        /// <returns>Возвращает true, если индекс присутствует, иначе false</returns>
        public static bool key_exists<T>(int key, T[] array)
        {
            if (key < array.Length) return true;
            return false;
        }

        /// <summary>
        ///     Выбирает текущий ключ из массива
        /// </summary>
        /// <param name="array">Массив</param>
        /// <returns>Возвращает текущий ключ</returns>
        public static dynamic key<T>(T[] array) where T: notnull
        {
            return currentIndex++;
        }

        /*
         *  ---------------------------------------------------------
         *  Пример использования:
         *  ---------------------------------------------------------
         *  
         *  double[] arr = array(1.1, -2.3, 3.7, 4.1, 5.6, 6.1, 7.1);
         *  int i = 0;
         *  foreach(var item in arr)
         *  {
         *      echo($"Key {i} = {key(arr)}");
         *      i++;
         *  }
         *  
         *  ---------------------------------------------------------
         *  Выведет:
         *  ---------------------------------------------------------
         *  
         *  Key 0 = 0
         *  Key 1 = 1
         *  Key 2 = 2
         *  .........
         *  Key 6 = 6
         *  
         *  ---------------------------------------------------------
         */

        /// <summary>
        ///     Присваивает переменным из списка значения подобно массиву.
        /// </summary>
        /// <typeparam name="T">Тип элементов в списке.</typeparam>
        /// <param name="source">Список элементов для присваивания переменным.</param>
        /// <param name="actions">Действия, которые нужно выполнить для каждого элемента в списке.</param>
        /// <remarks>
        /// Этот метод перебирает список элементов и присваивает каждый элемент переменной<br/>
        /// с помощью предоставленных действий. Действия выполняются в порядке их предоставления.
        /// </remarks>
        public static void list<T>(IEnumerable<T?> source, params Action<T?>[] actions) where T : class
        {
            var enumerator = source.GetEnumerator();
            foreach (var action in actions)
            {
                if (enumerator.MoveNext())
                {
                    action?.Invoke(enumerator.Current);
                }
                else
                {
                    break;
                }
            }
        }

        /*
         *  ---------------------------------------
         *  Пример выполнения:
         *  ---------------------------------------
         *  
         *  var info = new[] { "кофе", "коричневый", "кофеин" };
         *
         *  // Составить список всех переменных
         *  list(info, (drink) => Console.Write(drink + " "),
         *               (color) => Console.Write("- " + color + ", а "),
         *                 (power) => Console.WriteLine(power + " делает его особенным."));
         *
         *
         *  // Составить список только некоторых из них
         *  list(info, (drink) => Console.Write("В " + drink + " есть "),
         *               null,
         *                 (power) => Console.WriteLine(power + "."));
         *
         *  // Или пропустить все, кроме третьей
         *  list(info, null, null, (power) => Console.WriteLine("Мне нужен " + power + "!"));
         *
         *  // list() не работает со строками
         *  list("abcde", (bar) => Console.WriteLine(bar)); // Ошибка
         *  
         *  ---------------------------------------
         *  Выведет:
         *  ---------------------------------------
         *  
         *  кофе - коричневый, а кофеин делает его особенным.
         *  В кофе есть кофеин.
         *  Мне нужен кофеин!
         *  
         *  ---------------------------------------
         */

        /// <summary>
        ///     Сортирует массив, используя алгоритм "natural order" без учёта регистра символов
        /// </summary>
        /// <param name="arr">Массив</param>
        /// <returns>Функция всегда возвращает <strong>true.</strong></returns>
        public static bool natcasesort(ref string[] arr)
        {

            Array.Sort(arr, new NaturalStringComparer(StringComparer.OrdinalIgnoreCase));
            return true;
        }

        /*
         *  -----------------------------------------------------------------------------------------------
         *  Пример использования:
         *  -----------------------------------------------------------------------------------------------
         *  
         *  string[] array1 = { "IMG0.png", "img12.png", "img10.png", "img2.png", "img1.png", "IMG3.png" };
         *
         *  // Обычная сортировка
         *  Array.Sort(array1, StringComparer.Ordinal);
         *  echo("Обычная сортировка:");
         *  print_r(array1);
         *
         *  string[] array2 = { "IMG0.png", "img12.png", "img10.png", "img2.png", "img1.png", "IMG3.png" };
         *
         *  // Natural order сортировка (без учёта регистра)
         *  natcasesort(ref array2);
         *  echo("\nNatural order сортировка (без учёта регистра):");
         *  print_r(array2);
         *  print(lw.Constants.CS_EOL);
         *  
         *  -----------------------------------------------------------------------------------------------
         *  Выведет:
         *  -----------------------------------------------------------------------------------------------
         *  
         *  Array
         *  (
         *      IMG0.png
         *      IMG3.png
         *      img1.png
         *      img10.png
         *      img12.png
         *      img2.png
         *  )
         *  
         *  Array
         *  (
         *      IMG0.png
         *      img1.png
         *      img2.png
         *      IMG3.png
         *      img10.png
         *      img12.png
         *  )
         *  
         *  -----------------------------------------------------------------------------------------------
         */

        public static bool natsort(ref string[] arr)
        {
            Array.Sort(arr, new NaturalStringComparer(StringComparer.Ordinal));
            return true;
        }

        /// <summary>
        ///     Возвращает следующий элемент массива
        /// </summary>
        /// <param name="array">Массив</param>
        /// <returns>Следующий элемент массива</returns>
        public static T next<T>(T[] array)
        {
            if (currentIndex < array.Length - 1)
            {
                currentIndex++;
            }
            else
            {
                currentIndex = 0;
            }
            return current(array);
        }

        /// <summary>
        ///     Возвращает следующий элемент массива
        /// </summary>
        /// <param name="array">Массив</param>
        /// <returns>Следующий элемент массива</returns>
        public static T Next<T>(this T[] array)
        {
            return next(array);
        }

        /// <summary>
        ///     Возвращает предыдущий элемент массива
        /// </summary>
        /// <param name="array">Массив</param>
        /// <returns>Предыдущий элемент массива</returns>
        public static T prev<T>(T[] array)
        {
            if (currentIndex < 1)
                currentIndex = array.Length - 1;
            else
                currentIndex--;
            return current(array);
        }

        /// <summary>
        ///     Возвращает предыдущий элемент массива
        /// </summary>
        /// <param name="array">Массив</param>
        /// <returns>Предыдущий элемент массива</returns>
        public static T Prev<T>(this T[] array)
        {
            return prev(array);
        }

        /// <summary>
        ///     Возвращает первый элемент массива
        /// </summary>
        /// <param name="array">Массив</param>
        /// <returns>Первый элемент массива</returns>
        public static T reset<T>(T[] array)
        {
            currentIndex = 0;
            return current(array);
        }

        /// <summary>
        ///     Возвращает первый элемент массива
        /// </summary>
        /// <param name="array">Массив</param>
        /// <returns>Первый элемент массива</returns>
        public static T Reset<T>(this T[] array)
        {
            return reset(array);
        }

        /*  
         *  ------------------------------------------------
         *  Пример использования:
         *  ------------------------------------------------
         *  
         *  dynamic curr = arr.Current();
         *  echo($"Curr = {curr}");
         *  dynamic next = arr.Next();
         *  echo($"Next = {next}");
         *  dynamic prev = arr.Prev();
         *  echo($"Prev = {prev}");
         *  dynamic end = arr.End();
         *  echo($"End = {end}");
         *  dynamic reset = arr.Reset();
         *  echo($"Reset = {reset}");
         * 
         *  ------------------------------------------------
         *  При arr = { 1, 2, ..., 9} выведет: 1, 2, 1, 9, 1
         *  ------------------------------------------------
         */

        /// <summary>
        ///     Псевдоним count()
        /// </summary>
        /// <param name="array">Массив</param>
        /// <returns>Возвращает кол-во элементов в массиве</returns>
        public static int size_of<T>(T[] array)
        {
            return count(array);
        }

        /// <summary>
        ///     Псевдоним count()
        /// </summary>
        /// <param name="array">Массив</param>
        /// <returns>Возвращает кол-во элементов в массиве</returns>
        public static int SizeOf<T>(this T[] array)
        {
            return count(array);
        }

        /// <summary>
        ///     Перемешивает массив
        /// </summary>
        /// <param name="array">Массив</param>
        /// <returns>Перемешанный массив</returns>
        public static T[] shuffle<T>(T[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int j = new Random().Next(i);
                var temp = array[j];
                array[j] = array[i];
                array[i] = temp;
                // (array[i], array[j]) = (array[j], array[i])
            }
            return array;
        }

        /// <summary>
        ///     Перемешивает массив
        /// </summary>
        /// <param name="array">Массив</param>
        /// <returns>Перемешанный массив</returns>
        public static T[] Shuffle<T>(this T[] array)
        {
            return shuffle(array);
        }

        /// <summary>
        ///     Сортирует массив от минимального элемента к максимальному
        /// </summary>
        /// <param name="array">Массив</param>
        /// <returns> Возвращает отсортированный массив <strong>от минимума к максимуму</strong> </returns>
        public static T[] sort<T>(T[] array) where T: notnull
        {
            for (int i = 1; i < count(array); i++)
            {
                for (int j = 0; j < count(array) - i; j++)
                {
                    if ((dynamic)array[j] > (dynamic)array[j + 1])
                    {
                        swap(ref array[j + 1], ref array[j]);
                    }
                }
            }
            return array;
        }

        /// <summary>
        ///     Сортирует массив от минимального элемента к максимальному
        /// </summary>
        /// <param name="array">Массив</param>
        public static void sort<T>(ref T[] array) where T : notnull
        {
            array = sort(array);
        }

        /// <summary>
        ///     Сортирует массив от минимального элемента к максимальному
        /// </summary>
        /// <param name="array">Массив</param>
        /// <returns> Возвращает отсортированный массив <strong>от минимума к максимуму</strong> </returns>
        public static T[] MinSort<T>(this T[] array) where T: notnull
        {
            return sort(array);
        }

        /// <summary>
        ///     Сортирует массив от максимального элемента к минимальному
        /// </summary>
        /// <param name="array">Массив</param>
        /// <returns> Возвращает отсортированный массив <strong>от максимума к минимуму</strong> </returns>
        public static T[] rsort<T>(T[] array) where T: notnull
        {
            for (int i = 1; i < count(array); i++)
            {
                for (int j = 0; j < count(array) - i; j++)
                {
                    if ((dynamic)array[j] < (dynamic)array[j + 1])
                    {
                        swap(ref array[j + 1], ref array[j]);
                    }
                }
            }
            return array;
        }

        /// <summary>
        ///     Сортирует массив от минимального элемента к максимальному
        /// </summary>
        /// <param name="array">Массив</param>
        public static void rsort<T>(ref T[] array) where T : notnull
        {
            array = rsort(array);
        }

        /// <summary>
        ///     Сортирует массив от максимального элемента к минимальному
        /// </summary>
        /// <param name="array">Массив</param>
        /// <returns> Возвращает отсортированный массив <strong>от максимума к минимуму</strong> </returns>
        public static T[] MaxSort<T>(this T[] array) where T: notnull
        {
            return rsort(array);
        }
    }
}
