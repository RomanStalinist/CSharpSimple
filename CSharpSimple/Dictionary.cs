using static simple.Helpful;
using static simple.Constants;
using static simple.Constants.SortCase;

namespace simple
{
    public static class Dictionary
    {
        public static int currentIndex = 0;
        /// <summary>
        ///     Меняет регистр всех значений словаря
        /// </summary>
        /// <param name="dict">Словарь</param>
        /// <param name="caseType">CASE_LOWER | CASE_UPPER</param>
        public static void dictionary_change_key_case(ref Dictionary<string, dynamic> dict, CaseType caseType)
        {
            Dictionary<string, dynamic> newDict = new();
            foreach (KeyValuePair<string, dynamic> value in dict)
            {
                string newKey = "";
                string newValue = value.Value;

                switch (caseType)
                {
                    case CaseType.CASE_LOWER:
                        newKey = value.Key.ToString().ToLower();
                        break;
                    case CaseType.CASE_UPPER:
                        newKey = value.Key.ToString().ToUpper();
                        break;
                    case CaseType.FIRST_UPPER:
                        newKey = char.ToUpper(value.Key[0]) + value.Key[1..];
                        break;
                    case CaseType.LAST_UPPER:
                        newKey = value.Key[..^1] + char.ToUpper(value.Key[^1]);
                        break;
                    case CaseType.UNDERSCORE:
                        newKey = $"__{value.Key}__";
                        break;
                }
                newDict.Add(newKey, newValue);
            }
            dict = newDict;
        }

        /// <summary>
        ///     Меняет регистр всех ключей словаря
        /// </summary>
        /// <param name="dict">Словарь</param>
        /// <param name="caseType">CASE_LOWER | CASE_UPPER</param>
        public static void dictionary_change_value_case(ref Dictionary<string, dynamic> dict, CaseType caseType)
        {
            Dictionary<string, dynamic> newDict = new();
            foreach (KeyValuePair<string, dynamic> value in dict)
            {
                string newKey = value.Key;
                string newValue = "";

                switch (caseType)
                {
                    case CaseType.CASE_LOWER:
                        newValue = value.Value.ToString().ToLower();
                        break;
                    case CaseType.CASE_UPPER:
                        newValue = value.Value.ToString().ToUpper();
                        break;
                    case CaseType.FIRST_UPPER:
                        newValue = char.ToUpper(value.Value[0]) + value.Value[1..];
                        break;
                    case CaseType.LAST_UPPER:
                        newValue = value.Value.Substring(0, value.Value.Length - 1) + char.ToUpper(value.Value[value.Value.Length - 1]);
                        break;
                    case CaseType.UNDERSCORE:
                        newValue = $"__{value.Value}__";
                        break;
                }
                newDict.Add(newKey, newValue);
            }
            dict = newDict;
        }
        /*   ---------------------------------------------------------------------------
         *   Пример использования:
         *   ---------------------------------------------------------------------------
         *   
         *   Dictionary<string, dynamic> assoc = new()
         *   {
         *       ["key"] = "value"
         *   };
         *   dictionary_change_key_case(ref assoc, UNDERSCORE);      // [__key__] = value
         *   dictionary_change_value_case(ref assoc, UNDERSCORE);    // [__key__] = __value__
         *   
         *   ---------------------------------------------------------------------------
         */

        /// <summary>
        ///     Вычисляет расхождение словарей с дополнительной проверкой индекса
        /// </summary>
        /// <param name="firstDict">Первый словарь</param>
        /// <param name="otherDicts">Остальные словари</param>
        /// <returns>Новый словарь, содержащий все значения из первого словаря, которых нет в любом из последующих словарей.</returns>
        public static Dictionary<TKey, TValue> dictionary_diff_assoc<TKey, TValue>(
            Dictionary<TKey, TValue> firstDict,
            params Dictionary<TKey, TValue>[] otherDicts
        ) where TKey : notnull
        {
            Dictionary<TKey, TValue> diffDict = new();

            // Ищем значения из первого словаря, которые отсутствуют в остальных словарях
            foreach (KeyValuePair<TKey, TValue> firstPair in firstDict)
            {
                bool foundInOtherDicts = false;
                foreach (Dictionary<TKey, TValue> otherDict in otherDicts)
                {
                    if (otherDict.ContainsKey(firstPair.Key) && otherDict[firstPair.Key].Equals(firstPair.Value))
                    {
                        foundInOtherDicts = true;
                        break;
                    }
                }

                // Добавление элемента из первого словаря, если он отсутствует в остальных словарях
                if (!foundInOtherDicts)
                {
                    diffDict.Add(firstPair.Key, firstPair.Value);
                }
            }

            return diffDict;
        }

        /*
         *   Пример использования:
         *   Dictionary<int, int> map1 = new()
         *   {
         *       { 1, 2 },
         *       { 3, 4 },
         *       { 4, 5 }
         *   };
         *   Dictionary<int, int> map2 = new()
         *   {
         *       { 3, 4 },
         *       { 4, 5 }
         *   };
         *   Dictionary<int, int> map3 = new()
         *   {
         *       { 3, 4 },
         *       { 6, 7 }
         *   };
         *   Dictionary<int, int> map4 =  dictionary_diff_assoc(map1, map2, map3);
         */

        /// <summary>
        ///     Вычисляет расхождение массивов, сравнивая ключи
        /// </summary>
        /// <param name="firstDict">Первый словарь</param>
        /// <param name="otherDicts">Остальные словари</param>
        /// <returns>Новый словарь, содержащий все элементы с ключами из первого словаря, которых нет во всех последующих словарях.</returns>
        public static Dictionary<TKey, TValue> dictionary_diff_key<TKey, TValue>(
            Dictionary<TKey, TValue> firstDict,
            params Dictionary<TKey, TValue>[] otherDicts
        ) where TKey : notnull
        {
            Dictionary<TKey, TValue> diffDict = new();

            // Ищем значения из первого словаря, которые отсутствуют в остальных словарях
            foreach (KeyValuePair<TKey, TValue> firstPair in firstDict)
            {
                bool foundInOtherDicts = false;
                string? firstKeyString = firstPair.Key.ToString();

                foreach (Dictionary<TKey, TValue> otherDict in otherDicts)
                {
                    foreach (KeyValuePair<TKey, TValue> otherPair in otherDict)
                    {
                        if (firstKeyString.Equals(otherPair.Key.ToString()))
                        {
                            foundInOtherDicts = true;
                            break;
                        }
                    }

                    if (foundInOtherDicts)
                    {
                        break;
                    }
                }

                // Добавление элемента из первого словаря, если его ключ отсутствует в остальных словарях
                if (!foundInOtherDicts)
                {
                    diffDict.Add(firstPair.Key, firstPair.Value);
                }
            }

            return diffDict;
        }

        /// <summary>
        ///     Вычисляет расхождение словарей с дополнительной проверкой индекса
        /// </summary>
        /// <param name="firstDict">Первый словарь</param>
        /// <param name="secondDict">Второй словарь</param>
        /// <returns>Новый словарь, содержащий все значения из первого словаря, которых нет во втором, а также сортирует по усмотрению пользователя.</returns>
        public static Dictionary<TKey, TValue> dictionary_diff_uassoc<TKey, TValue>(
            Dictionary<TKey, TValue> firstDict,
            Dictionary<TKey, TValue> secondDict,
            Func<TValue?, TValue, int> valueCompareFunc
        ) where TKey : notnull
        {
            Dictionary<TKey, TValue> diffDict = firstDict.Except(secondDict).ToDictionary(pair => pair.Key, pair => pair.Value);

            // Сортировка словаря с помощью пользовательской функции-колбека
            var sortedList = diffDict.ToList();
            sortedList.Sort(
                            (firstPair, secondPair) => valueCompareFunc(firstPair.Value, secondPair.Value)
                           );

            return sortedList.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        /*   
         *   --------------------------------------------------------------------------------------------------------------------------------
         *   Пример использования:
         *   --------------------------------------------------------------------------------------------------------------------------------
         *   
         *   Dictionary<string, string> dict1 = new Dictionary<string, string> { { "c", "cherry" }, { "b", "banana" }, { "a", "apple" } };
         *   Dictionary<string, string> dict2 = new Dictionary<string, string> { { "b", "banana" }, { "d", "date" }, { "e", "elderberry" } };
         *   
         *   --------------------------------------------------------------------------------------------------------------------------------
         *   Новый словарь:
         *   --------------------------------------------------------------------------------------------------------------------------------
         *   
         *   Dictionary<string, string> result =  dictionary_diff_uassoc(
         *       dict1,
         *       dict2,
         *       (val1, val2) => val1.CompareTo(val2) // функция-колбек для сортировки значений по длине
         *   );
         *   
         *   --------------------------------------------------------------------------------------------------------------------------------
         *   Выведет:
         *   --------------------------------------------------------------------------------------------------------------------------------
         *   
         *   {
         *      { a: apple },
         *      { c: cherry } // Сортировка по длине значений
         *   }
         *   
         *   --------------------------------------------------------------------------------------------------------------------------------
         */

        /// <summary>
        ///     Вычисляет расхождение массивов, используя callback-функцию для сравнения ключей
        /// </summary>
        /// <param name="firstDict">Первый словарь</param>
        /// <param name="otherDicts">Остальные словари</param>
        /// <returns>Новый словарь, содержащий все элементы с ключами из первого словаря, которых нет во всех последующих словарях.</returns>
        public static Dictionary<TKey, TValue> dictionary_diff_ukey<TKey, TValue>(
            Dictionary<TKey, TValue> firstDict,
            Dictionary<TKey, TValue> secondDict,
            Func<TKey, TKey, int> keyCompareFunc
        ) where TKey : notnull
        {
            Dictionary<TKey, TValue> diffDict = new();

            foreach (var firstPair in firstDict)
            {
                if (!secondDict.Any(secondPair => keyCompareFunc(firstPair.Key, secondPair.Key) == 0))
                {
                    diffDict.Add(firstPair.Key, firstPair.Value);
                }
            }

            // Сортировка словаря с помощью пользовательской функции-колбека
            var sortedList = diffDict.ToList();
            sortedList.Sort(
                            (firstPair, secondPair) => keyCompareFunc(firstPair.Key, secondPair.Key)
                           );

            return sortedList.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        /*   
         *   --------------------------------------------------------------------------------------------------------------------------------
         *   Пример использования:
         *   --------------------------------------------------------------------------------------------------------------------------------
         *   
         *   Dictionary<string, string> dict1 = new Dictionary<string, string> { { "c", "cherry" }, { "b", "banana" }, { "a", "apple" } };
         *   Dictionary<string, string> dict2 = new Dictionary<string, string> { { "b", "banana" }, { "d", "date" }, { "e", "elderberry" } };
         *   
         *   --------------------------------------------------------------------------------------------------------------------------------
         *   Новый словарь:
         *   --------------------------------------------------------------------------------------------------------------------------------
         *   
         *   Dictionary<string, string> result =  dictionary_diff_uassoc(
         *       dict1,
         *       dict2,
         *       (val1, val2) => val1.CompareTo(val2) // функция-колбек для сортировки значений по длине
         *   );
         *   
         *   --------------------------------------------------------------------------------------------------------------------------------
         *   Выведет:
         *   --------------------------------------------------------------------------------------------------------------------------------
         *   
         *   {
         *      { c: cherry },
         *      { aboba: apple } // Сортировка по длине ключей
         *   }
         *   
         *   --------------------------------------------------------------------------------------------------------------------------------
         */

        /// <summary>
        ///     Вычисляет расхождение словарей
        /// </summary>
        /// <param name="dictionary">Исходный словарь</param>
        /// <param name="dictionaries">Остальные словари</param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> dictionary_diff<TKey, TValue>(Dictionary<TKey, TValue> dictionary, params Dictionary<TKey, TValue>[] dictionaries) where TKey : notnull
        {
            // Создаем новый словарь, который будет содержать результаты вычитания
            Dictionary<TKey, TValue> diffDict = new(dictionary);

            // Проходим по всем переданным словарям и удаляем из результирующего словаря элементы, которые есть в этих словарях
            foreach (Dictionary<TKey, TValue> dict in dictionaries)
            {
                foreach (TKey key in dict.Keys)
                {
                    if (diffDict.ContainsKey(key) && diffDict[key].Equals(dict[key]))
                    {
                        diffDict.Remove(key);
                    }
                }
            }

            return diffDict;
        }

        /// <summary>
        ///     Создаёт массив и заполняет его значениями с определёнными ключами
        /// </summary>
        /// <param name="keys">Массив с ключами</param>
        /// <param name="value">Заполняемое значение</param>
        /// <returns>Возвращает заполненный массив</returns>
        public static Dictionary<TKey, TValue> dictionary_fill_keys<TKey, TValue>(IEnumerable<TKey> keys, TValue value) where TKey : notnull
        {
            // Создаем новый словарь, который будет содержать результаты вычитания
            Dictionary<TKey, TValue> filledDict = new();

            // Проходим по всем переданным словарям и удаляем из результирующего словаря элементы, которые есть в этих словарях
            foreach (TKey key in keys)
            {
                filledDict.Add(key, value);
            }

            return filledDict;
        }

        /*
         *   ----------------------------------------------------------------
         *   Пример использования:
         *   ----------------------------------------------------------------
         *   
         *   string value = "banana";
         *   List<string> arr = new() { "aboba", "obema" };
         *   Dictionary<string, string> newArr =  dictionary_fill_keys(arr, value);
         *   
         *   ----------------------------------------------------------------
         *   Выведет:
         *   ----------------------------------------------------------------
         *   
         *   {
         *      {aboba => banana},
         *      {obema => banana}
         *   }
         *   
         *   ----------------------------------------------------------------
         */

        /// <summary>
        ///     Заполняет словарь значениями
        /// </summary>
        /// <param name="start_index">Первый индекс возвращаемого массива.</param>
        /// <param name="count">Количество добавляемых элементов, должно быть >= 0 и < 2147483647</param>
        /// <param name="value">Значение для заполнения</param>
        /// <returns>Заполненный словарь</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static Dictionary<int, T> dictionary_fill<T>(int start_index, int count, T value)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Количество вводимых значений не должно быть отрицательным.");
            }

            if (count >= 2147483647)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Количество вводимых значений не может быть больше " + int.MaxValue);
            }

            var result = new Dictionary<int, T>(count);

            for (int i = 0; i < count; i++)
            {
                result[start_index + i] = value;
            }

            return result;
        }

        /// <summary>
        ///     Фильтрует элементы словаря с помощью callback-функции
        /// </summary>
        /// <param name="dict">Словарь</param>
        /// <param name="callback">Пользовательская функция</param>
        /// <returns>Отфильтрованный массив</returns>
        public static Dictionary<TKey, TValue> dictionary_filter<TKey, TValue>(
            Dictionary<TKey, TValue> dict,
            Func<TKey, TValue, bool>? callback = null
        ) where TKey : notnull
        {
            callback ??= (key, value) =>
            {
                return key != null && value != null;
            };

            var result = new Dictionary<TKey, TValue>();
            foreach (KeyValuePair<TKey, TValue> item in dict)
            {
                if (callback(item.Key, item.Value))
                {
                    result.Add(item.Key, item.Value);
                }
            }

            return result;
        }

        /*
         *  --------------------------------------------------------------------------------------
         *  Пример использования:
         *  --------------------------------------------------------------------------------------
         *  
         *  Dictionary<string, int> dict = new()
         *  {
         *      {"a", 1},
         *      {"b", 2},
         *      {"ca", 3},
         *      {"d", 4},
         *      {"ebaaca", 5}
         *  };
         *  int[] arr = { 1, 2, 3, 4, 5 };
         *  Dictionary<string, int> dict2 = dictionary_filter(dict, (key, val) => key.Length % 2 == 0);
         *  int[] arr2 = array_filter(arr, x => x % 2 == 0);
         *  
         *  --------------------------------------------------------------------------------------
         *  Выводит:
         *  --------------------------------------------------------------------------------------
         *  
         *  {
         *      { ca, 3 },
         *      { ebaaca, 5 }
         *  }
         *  
         *  { 2, 4 }
         */

        /// <summary>
        ///     Меняет местами ключи с их значениями в словаре
        /// </summary>
        /// <param name="dict">Словарь</param>
        /// <returns>Массив переворачиваемых пар ключ/значение.</returns>
        public static Dictionary<TValue, TKey> dictionary_flip<TKey, TValue>(Dictionary<TKey, TValue> dict) where TKey : notnull where TValue : notnull
        {
            var result = new Dictionary<TValue, TKey>();
            foreach (KeyValuePair<TKey, TValue> item in dict)
            {
                result.Add(item.Value, item.Key);
            }
            return result;
        }

        /// <summary>
        ///     Вычисляет схождение словарей с дополнительной проверкой индекса
        /// </summary>
        /// <param name="firstDict">Первый словарь</param>
        /// <param name="otherDicts">Остальные словари</param>
        /// <returns>Новый словарь, содержащий все значения из первого словаря, которых нет в любом из последующих словарей.</returns>
        public static Dictionary<TKey, TValue> dictionary_intersect_assoc<TKey, TValue>(Dictionary<TKey, TValue> firstDict, params Dictionary<TKey, TValue>[] otherDicts) where TKey : notnull where TValue : notnull
        {
            Dictionary<TKey, TValue> diffDict = new();

            // Ищем значения из первого словаря, которые отсутствуют в остальных словарях
            foreach (KeyValuePair<TKey, TValue> firstPair in firstDict)
            {
                bool foundInOtherDicts = false;
                foreach (Dictionary<TKey, TValue> otherDict in otherDicts)
                {
                    if (otherDict.ContainsKey(firstPair.Key) && otherDict[firstPair.Key].Equals(firstPair.Value))
                    {
                        foundInOtherDicts = true;
                        break;
                    }
                }

                // Добавление элемента из первого словаря, если он присутствует в остальных словарях
                if (foundInOtherDicts)
                {
                    diffDict.Add(firstPair.Key, firstPair.Value);
                }
            }

            return diffDict;
        }

        /*
         *   --------------------------------------------------------------------
         *   Пример использования:
         *   --------------------------------------------------------------------
         *   
         *   Dictionary<int, int> map1 = new()
         *   {
         *       { 1, 2 },
         *       { 3, 4 },
         *       { 4, 5 }
         *   };
         *   Dictionary<int, int> map2 = new()
         *   {
         *       { 3, 4 },
         *       { 4, 5 }
         *   };
         *   Dictionary<int, int> map3 = new()
         *   {
         *       { 3, 4 },
         *       { 6, 7 }
         *   };
         *   Dictionary<int, int> map4 = dictionary_intersect_assoc(map1, map2, map3);
         *   
         *   --------------------------------------------------------------------
         *   Результат:
         *   --------------------------------------------------------------------
         *   
         *   {
         *      { 3, 4 },
         *      { 4, 5 }
         *   }
         */

        /// <summary>
        ///     Вычисляет расхождение массивов, сравнивая ключи
        /// </summary>
        /// <param name="firstDict">Первый словарь</param>
        /// <param name="otherDicts">Остальные словари</param>
        /// <returns>Новый словарь, содержащий все элементы с ключами из первого словаря, которых нет во всех последующих словарях.</returns>
        public static Dictionary<TKey, TValue> dictionary_intersect_key<TKey, TValue>(Dictionary<TKey, TValue> firstDict, params Dictionary<TKey, TValue>[] otherDicts) where TKey : notnull where TValue : notnull
        {
            Dictionary<TKey, TValue> diffDict = new();

            // Ищем значения из первого словаря, которые отсутствуют в остальных словарях
            foreach (KeyValuePair<TKey, TValue> firstPair in firstDict)
            {
                bool foundInOtherDicts = false;
                string firstKeyString = firstPair.Key.ToString();

                foreach (Dictionary<TKey, TValue> otherDict in otherDicts)
                {
                    foreach (KeyValuePair<TKey, TValue> otherPair in otherDict)
                    {
                        if (firstKeyString.Equals(otherPair.Key.ToString()))
                        {
                            foundInOtherDicts = true;
                            break;
                        }
                    }
                }

                // Добавление элемента из первого словаря, если его ключ отсутствует в остальных словарях
                if (foundInOtherDicts)
                {
                    diffDict.Add(firstPair.Key, firstPair.Value);
                }
            }

            return diffDict;
        }

        /// <summary>
        ///     Вычисляет сходства словарей с дополнительной проверкой индекса
        /// </summary>
        /// <param name="firstDict">Первый словарь</param>
        /// <param name="secondDict">Второй словарь</param>
        /// <returns>Новый словарь, содержащий все значения из первого словаря, которые есть во втором словаре, а также сортирует по усмотрению пользователя.</returns>
        public static Dictionary<TKey, TValue> dictionary_intersect_uassoc<TKey, TValue>(
            Dictionary<TKey, TValue> firstDict,
            Dictionary<TKey, TValue> secondDict,
            Func<TValue, TValue, int> valueCompareFunc
        ) where TKey : notnull
        {
            Dictionary<TKey, TValue> diffDict = firstDict.Intersect(secondDict).ToDictionary(pair => pair.Key, pair => pair.Value);

            // Сортировка словаря с помощью пользовательской функции-колбека
            var sortedList = diffDict.ToList();
            sortedList.Sort(
                            (firstPair, secondPair) => valueCompareFunc(firstPair.Value, secondPair.Value)
                           );

            return sortedList.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        /*
         *   --------------------------------------------------------------------------------------------------------------------------------
         *   Пример использования:
         *   --------------------------------------------------------------------------------------------------------------------------------
         *   
         *   Dictionary<string, string> dict1 = new Dictionary<string, string> { { "c", "cherry" }, { "b", "banana" }, { "a", "apple" } };
         *   Dictionary<string, string> dict2 = new Dictionary<string, string> { { "b", "banana" }, { "d", "date" }, { "e", "elderberry" } };
         *   
         *   --------------------------------------------------------------------------------------------------------------------------------
         *   Новый словарь:
         *   --------------------------------------------------------------------------------------------------------------------------------
         *   
         *   Dictionary<string, string> result = dictionary_diff_uassoc(
         *       dict1,
         *       dict2,
         *       (val1, val2) => val1.CompareTo(val2) // функция-колбек для сортировки значений по длине
         *   );
         *   
         *   --------------------------------------------------------------------------------------------------------------------------------
         *   Выведет:
         *   --------------------------------------------------------------------------------------------------------------------------------
         *   
         *   {
         *      { b, banana } // Сортировка по длине значений
         *   }
         *   
         *   --------------------------------------------------------------------------------------------------------------------------------
         *   
         */

        /// <summary>
        ///     Вычисляет сходства массивов, используя callback-функцию для сравнения ключей
        /// </summary>
        /// <param name="firstDict">Первый словарь</param>
        /// <param name="secondDict">Второй словарь</param>
        /// <returns>Новый словарь, содержащий все элементы с ключами из первого словаря, которые есть во втором словаре.</returns>
        public static Dictionary<TKey, TValue> dictionary_intersect_ukey<TKey, TValue>(
            Dictionary<TKey, TValue> firstDict,
            Dictionary<TKey, TValue> secondDict,
            Func<TKey, TKey, int> keyCompareFunc
        ) where TKey : notnull
        {
            Dictionary<TKey, TValue> diffDict = new();

            foreach (var firstPair in firstDict)
            {
                if (secondDict.Any(secondPair => keyCompareFunc(firstPair.Key, secondPair.Key) == 0))
                {
                    diffDict.Add(firstPair.Key, firstPair.Value);
                }
            }

            // Сортировка словаря с помощью пользовательской функции-колбека
            var sortedList = diffDict.ToList();
            sortedList.Sort(
                            (firstPair, secondPair) => keyCompareFunc(firstPair.Key, secondPair.Key)
                           );

            return sortedList.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        /*
         *   Пример использования:
         *   
         *   Dictionary<string, int> map1 = new()
         *   {
         *       { "aboba", 2 },
         *       { "str", 4 }
         *    };
         *   Dictionary<string, int> map2 = new()
         *   {
         *       { "banana", 2 },
         *       { "aboba", 3 },
         *       { "zhebra", 3 }
         *   };
         *   Dictionary<string, int> map3 = dictionary_intersect_ukey(map1, map2, (key, val) => key.CompareTo(val));
         *   
         *   Выведет:
         *   {
         *      { aboba, 2 }
         *   }
         *   
         */

        /// <summary>
        ///     Вычисляет сходства словарей
        /// </summary>
        /// <param name="dictionary">Исходный словарь</param>
        /// <param name="dictionaries">Остальные словари</param>
        /// <returns>Новый словарь, состоящий из схожих элементов</returns>
        public static Dictionary<TKey, TValue> dictionary_intersect<TKey, TValue>(
            Dictionary<TKey, TValue> dictionary,
            params Dictionary<TKey, TValue>[] dictionaries
        ) where TKey : notnull where TValue : notnull
        {
            // Создаем новый словарь, который будет содержать результаты пересечения
            Dictionary<TKey, TValue> interDict = new();

            // Проходим по всем элементам исходного словаря
            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
            {
                bool foundInAll = true;

                // Проходим по всем переданным словарям и проверяем, есть ли такой элемент в каждом из них
                foreach (Dictionary<TKey, TValue> dict in dictionaries)
                {
                    if (!dict.TryGetValue(pair.Key, out TValue? value) || !EqualityComparer<TValue>.Default.Equals(pair.Value, value))
                    {
                        foundInAll = false;
                        break;
                    }
                }

                // Если элемент нашелся во всех словарях, добавляем его в результирующий словарь
                if (foundInAll)
                {
                    interDict.Add(pair.Key, pair.Value);
                }
            }

            return interDict;
        }

        /// <summary>
        ///     Проверяет, присутствует ли в словаре указанный индекс
        /// </summary>
        /// <param name="key">Ключ словаря</param>
        /// <param name="dictionary">Словарь</param>
        /// <returns>Возвращает true, если присутствует, иначе false</returns>
        public static bool dictionary_key_exists<TKey, TValue>(dynamic key, Dictionary<TKey, TValue> dictionary) where TKey : notnull
        {
            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
            {
                if (pair.Key.Equals(key))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        ///     Получает последний ключ словаря
        /// </summary>
        /// <param name="dictionary">Словарь</param>
        /// <returns>Последний ключ словаря</returns>
        public static TKey dictionary_key_last<TKey, TValue>(Dictionary<TKey, TValue> dictionary) where TKey : notnull
        {
            return dictionary.Keys.Last();
        }

        // 3-им аргументом true для строго равенства и false для нестрого равенства
        /// <summary>
        ///     Возвращает все или некоторое подмножество ключей словаря.
        /// </summary>
        /// <param name="dictionary">Массив, содержащий возвращаемые ключи.</param>
        /// <param name="value">Если указано, будут возвращены только ключи у которых значения элементов массива совпадают с этим параметром.</param>
        /// <param name="strict">Определяет использование строгой проверки на равенство при поиске.</param>
        /// <returns></returns>
        public static List<TKey> dictionary_keys<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TValue? value = null, bool strict = false)
        where TKey : notnull
        where TValue : class
        {
            List<TKey> keyList = new();

            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
            {
                if (value == null)
                {
                    keyList.Add(pair.Key);
                }
                else
                {
                    if (strict)
                    {
                        if (EqualityComparer<TValue?>.Default.Equals(value, pair.Value))
                            keyList.Add(pair.Key);
                    }
                    else
                    {
                        if (TryChangeType(pair.Value, value.GetType(), out TValue? valueToCompare) && value.Equals(valueToCompare))
                        {
                            keyList.Add(pair.Key);
                        }
                        else if (EqualityComparer<TValue?>.Default.Equals(value, pair.Value))
                        {
                            keyList.Add(pair.Key);
                        }
                    }
                }
            }

            return keyList;
        }

        private static bool TryChangeType<T>(object value, Type conversionType, out T? result)
        {
            try
            {
                result = (T)Convert.ChangeType(value, conversionType);
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }

        /// <summary>
        ///     Рекурсивное слияние одного или более массивов
        /// </summary>
        /// <param name="dicts">Массивы, подлежащие слиянию</param>
        /// <returns>Массив значений, полученный в результате слияния аргументов вместе. Если вызывается без аргументов, возвращает пустой словарь</returns>
        public static Dictionary<string, dynamic> dictionary_merge_recursive(params Dictionary<string, dynamic>[] dicts)
        {
            var result = new Dictionary<string, dynamic>();

            if (dicts == null || dicts.Length == 0)
            {
                return result;
            }

            foreach (var dict in dicts)
            {
                if (dict == null)
                {
                    continue;
                }

                foreach (var keyValuePair in dict)
                {
                    if (result.ContainsKey(keyValuePair.Key))
                    {
                        if (result[keyValuePair.Key] is Dictionary<string, dynamic> subDict1 &&
                            keyValuePair.Value is Dictionary<string, dynamic> subDict2)
                        {
                            result[keyValuePair.Key] = dictionary_merge_recursive(subDict1, subDict2);
                        }
                        else if (result[keyValuePair.Key] is List<dynamic> existingList &&
                                 keyValuePair.Value is List<dynamic> newList)
                        {
                            existingList.AddRange(newList);
                        }
                        else if (result[keyValuePair.Key] is List<dynamic> existingList2)
                        {
                            existingList2.Add(keyValuePair.Value);
                        }
                        else
                        {
                            List<dynamic> list = new() { result[keyValuePair.Key], keyValuePair.Value };
                            result[keyValuePair.Key] = list;
                        }
                    }
                    else
                    {
                        if (keyValuePair.Value is List<dynamic> originalList)
                        {
                            List<dynamic> copiedList = new(originalList);
                            result[keyValuePair.Key] = copiedList;
                        }
                        else
                        {
                            result[keyValuePair.Key] = keyValuePair.Value;
                        }
                    }
                }
            }
            return result;
        }

        /*
         *  --------------------------------------------------------------------------------------------
         *  Пример использования:
         *  --------------------------------------------------------------------------------------------
         *  
         *  var arr1 = new Dictionary<string, object>
         *  {
         *      { "color", new Dictionary<string, object> { { "favorite", "red" } } },
         *      { "0", 5 }
         *  };
         *
         *  var arr2 = new Dictionary<string, object>
         *  {
         *      { "0", 10 },
         *      { "color", new Dictionary<string, object> { { "favorite", "green" }, { "0", "blue" } } }
         *  };
         *
         *  var result = dictionary_merge_recursive(arr1, arr2);
         *  
         *  --------------------------------------------------------------------------------------------
         *  Возвращает:
         *  --------------------------------------------------------------------------------------------
         *  
         *  Array
         *  (
         *      color => Dictionary
         *      (
         *          favorite => Array
         *          (
         *              0 => red,
         *              1 => green
         *          )
         *          0 => blue,
         *          0 => Array
         *          (
         *              0 => 5,
         *              1 => 10
         *          )
         *      )
         *  )
         */

        /// <summary>
        ///     Слияние одного или более словарей
        /// </summary>
        /// <param name="dicts">Словари, подлежащие слиянию</param>
        /// <returns>Возвращает результирующий словарь. Если вызывается без аргументов, возвращает пустой словарь</returns>
        public static Dictionary<string, dynamic> dictionary_merge(params Dictionary<string, dynamic>[] dicts)
        {
            var result = new Dictionary<string, dynamic>();

            if (dicts == null || dicts.Length == 0)
            {
                return result;
            }

            int numberIndex = 0;
            foreach (var dict in dicts)
            {
                if (dict == null)
                {
                    continue;
                }

                foreach (var keyValuePair in dict)
                {
                    if (!result.ContainsKey(keyValuePair.Key))
                    {
                        result[keyValuePair.Key] = keyValuePair.Value;
                    }
                    else
                    {
                        if (int.TryParse(keyValuePair.Key, out _))
                        {
                            result[numberIndex.ToString()] = keyValuePair.Value;
                            numberIndex++;
                        }
                        else
                        {
                            if (keyValuePair.Value is Dictionary<string, dynamic> nestedDict)
                            {
                                if (result[keyValuePair.Key] is Dictionary<string, dynamic> existingNestedDict)
                                {
                                    result[keyValuePair.Key] = dictionary_merge(existingNestedDict, nestedDict);
                                }
                            }
                            else
                            {
                                result[keyValuePair.Key] = keyValuePair.Value;
                            }
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        ///     Выбирает один или несколько случайных ключей из словаря.
        /// </summary>
        /// <param name="dictionary">Словарь</param>
        /// <param name="num">Количество элементов</param>
        /// <returns>Возвращает ключи</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static TKey[] dictionary_rand<TKey, TValue>(Dictionary<TKey, TValue> dictionary, int num) where TKey : notnull
        {
            if (num <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(num), "Количество элементов должно быть больше или равно 0");
            }

            if (num > dictionary.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(num), "Количество элементов должно быть меньше или равно длине словаря");
            }

            TKey[] keys = dictionary.Keys.ToArray();
            TKey[] randKeys = new TKey[num];
            Random rand = new();

            for (int i = 0; i < num; i++)
            {
                int index = rand.Next(keys.Length - i);
                randKeys[i] = keys[index];
                keys[index] = keys[keys.Length - i - 1];
            }

            return randKeys;
        }

        /// <summary>
        ///     Выбирает один или несколько случайных ключей из словаря
        /// </summary>
        /// <param name="dictionary">Входной словарь</param>
        /// <param name="num">Определяет количество выбираемых элементов.</param>
        public static TKey[] ArrayRand<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, int num) where TKey : notnull
        {
            return dictionary_rand(dictionary, num);
        }

        /*
         *  -------------------------------------
         *  Пример использования:
         *  -------------------------------------
         *  
         *  Dictionary<string, int> dict1 = new()
         *  {
         *      { "key1", 1 },
         *      { "key2", 2 },
         *      { "key3", 3 },
         *      { "key4", 4 }
         *  };
         *  string[] str = array_rand(dict1, 2);
         */

        /// <summary>
        ///     Заменяет элементы словаря элементами других переданных словарей
        /// </summary>
        /// <param name="dict">Словарь</param>
        /// <param name="replacements">Остальные словари</param>
        /// <returns>Возвращает замененный массив</returns>
        public static Dictionary<TKey, TValue> dictionary_replace<TKey, TValue>(Dictionary<TKey, TValue> dict, params Dictionary<TKey, TValue>[] replacements) where TKey : notnull
        {
            Dictionary<TKey, TValue> newDict = new(dict);
            foreach (Dictionary<TKey, TValue> replacement in replacements)
            {
                foreach (var key in replacement.Keys)
                {
                    if (dict.ContainsKey(key))
                    {
                        newDict[key] = replacement[key];
                    }
                }
            }
            return newDict;
        }

        /*
         *  -----------------------------------------------------------------------
         *  Пример использования:
         *  -----------------------------------------------------------------------
         *  
         *  Dictionary<string, object> dict1 = new()
         *  {
         *      { "key1", 1 },
         *      { "key2", 2 },
         *      { "key3", 3 }
         *  };
         *  Dictionary<string, object> dict2 = new()
         *  {
         *      { "key1", 3 },
         *      { "key5", 2 },
         *      { "key4", 1 }
         *  };
         *  Dictionary<string, object> dict3 = new()
         *  {
         *      { "key1", 2 },
         *      { "key2", 5 },
         *      { "key4", 6 }
         *  };
         *  Dictionary<string, object> dict4 = dictionary_replace(dict1, dict2, dict3); 
         *  
         *  -----------------------------------------------------------------------
         *  Выведет:
         *  -----------------------------------------------------------------------
         *  
         *  {
         *      { "key1", 2 },
         *      { "key2", 5 },
         *      { "key3", 3 }
         *  };
         */

        /// <summary>
        ///     Возвращает словарь с элементами в обратном порядке
        /// </summary>
        /// <param name="dictionary">Входной словарь.</param>
        /// <param name="preserve_keys">Если установлено в true, то числовые ключи будут сохранены. Нечисловые ключи не подвержены этой опции и всегда сохраняются.</param>
        /// <returns>Возвращает словарь с элементами в обратном порядке</returns>
        public static Dictionary<TKey, TValue> dictionary_reverse<TKey, TValue>(Dictionary<TKey, TValue> dictionary, bool preserve_keys = false) where TKey : notnull
        {
            Dictionary<TKey, TValue> newDictionary = new();
            List<TValue> values = new(dictionary.Values);
            List<TKey> keys = new(dictionary.Keys);
            if (preserve_keys)
            {
                values.Reverse();
            }
            else
            {
                keys.Reverse();
                values.Reverse();
            }
            for (int i = 0; i < keys.Count; i++)
            {
                newDictionary.Add(keys[i], values[i]);
            }
            return newDictionary;
        }

        /// <summary>
        ///     Возвращает словарь с элементами в обратном порядке
        /// </summary>
        /// <param name="dictionary">Входной словарь.</param>
        /// <param name="preserve_keys">Если установлено в true, то числовые ключи будут сохранены. Нечисловые ключи не подвержены этой опции и всегда сохраняются.</param>
        /// <returns>Возвращает словарь с элементами в обратном порядке</returns>
        public static Dictionary<TKey, TValue> DictReverse<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, bool preserve_keys = false) where TKey : notnull
        {
            return dictionary_reverse(dictionary, preserve_keys);
        }

        /// <summary>
        ///     Выбирает срез словаря
        /// </summary>
        /// <param name="dictionary">
        ///     Входной словарь.
        /// </param>
        /// <param name="offset">
        ///     Если параметр <b>offset</b> неотрицательный, последовательность начнётся на указанном расстоянии от начала <b>dictionary</b>.<br/>
        ///     Если <b>offset</b> отрицательный, последовательность начнётся с конца <b>dictionary</b>. <code><b>Замечание:</b><br/>
        ///     Обратите внимание, что параметр <b>offset</b> обозначает положение в массиве, а не ключ.</code>
        /// </param>
        /// <param name="length">
        ///     Если в эту функцию передан положительный параметр <b>length</b>, последовательность будет включать количество элементов меньшее или равное <b>length</b>.<br/>
        ///     Если количество элементов словаря меньше чем параметр <b>length</b>, то только доступные элементы словаря будут присутствовать.</br>
        ///     Если в эту функцию передан отрицательный параметр <b>length</b>, последовательность остановится на указанном расстоянии от конца словаря.</br>
        ///     Если он опущен, последовательность будет содержать все элементы с <b>offset</b> до конца словаря <b>dictionary</b>.
        /// </param>
        /// <param name="preserve_keys"></param>
        /// <returns>Возвращает срез. Если смещение больше длины словаря, то будет возвращён пустой массив.</returns>
        public static Dictionary<TKey, TValue> dictionary_slice<TKey, TValue>
        (
            Dictionary<TKey, TValue> dictionary,
            int offset,
            int? length = null,
            bool preserve_keys = false
        )
        where TKey : notnull
        {
            int len = length ?? dictionary.Count - offset;

            if (len < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(len), "Длина не может быть меньше 0");
            }

            if (len == 0)
            {
                return new Dictionary<TKey, TValue>();
            }

            if (offset < 0)
            {
                offset = dictionary.Count + offset;
                if (offset < 0)
                {
                    offset = 0;
                }
            }

            if (offset >= dictionary.Count)
            {
                return new Dictionary<TKey, TValue>();
            }

            int newLen = Math.Min(len, dictionary.Count - offset);
            Dictionary<TKey, TValue> newDictionary = new(newLen);

            int i = 0;
            foreach (var kvp in dictionary)
            {
                if (i >= offset && i < offset + newLen)
                {
                    if (preserve_keys)
                    {
                        newDictionary.Add(kvp.Key, kvp.Value);
                    }
                    else
                    {
                        if (newDictionary.Count == 0)
                        {
                            newDictionary.Add(kvp.Key, kvp.Value);
                        }
                        else
                        {
                            var lastKey = newDictionary.Keys.Last();
                            var newKey = (TKey)Convert.ChangeType(Convert.ToInt32(lastKey) + 1, typeof(TKey));
                            newDictionary.Add(newKey, kvp.Value);
                        }
                    }
                }
                i++;
            }

            return newDictionary;
        }

        /*
         *  ---------------------------------------
         *  Пример использования:
         *  ---------------------------------------
         *  
         *  Dictionary<string, string> dict = new()
         *  {
         *      { "1", "val1" },
         *      { "2", "val2" },
         *      { "3", "val3" }
         *  };
         *  print_r(dictionary_slice(dict, -1));
         *  
         *  ---------------------------------------
         *  Результат:
         *  ---------------------------------------
         *  
         *  Array
         *  (
         *      3, val3
         *  )
         */

        /// <summary>
        ///     Выбирает срез словаря
        /// </summary>
        /// <param name="dictionary">
        ///     Входной словарь.
        /// </param>
        /// <param name="offset">
        ///     Если параметр <b>offset</b> неотрицательный, последовательность начнётся на указанном расстоянии от начала <b>dictionary</b>.<br/>
        ///     Если <b>offset</b> отрицательный, последовательность начнётся с конца <b>dictionary</b>. <code><b>Замечание:</b><br/>
        ///     Обратите внимание, что параметр <b>offset</b> обозначает положение в массиве, а не ключ.</code>
        /// </param>
        /// <param name="length">
        ///     Если в эту функцию передан положительный параметр <b>length</b>, последовательность будет включать количество элементов меньшее или равное <b>length</b>.<br/>
        ///     Если количество элементов словаря меньше чем параметр <b>length</b>, то только доступные элементы словаря будут присутствовать.</br>
        ///     Если в эту функцию передан отрицательный параметр <b>length</b>, последовательность остановится на указанном расстоянии от конца словаря.</br>
        ///     Если он опущен, последовательность будет содержать все элементы с <b>offset</b> до конца словаря <b>dictionary</b>.
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
        public static Dictionary<TKey, TValue> dictionary_splice<TKey, TValue>
        (
            Dictionary<TKey, TValue>? dictionary,
            int offset,
            int? length = null,
            Dictionary<TKey, TValue>? replacement = null
        )
        where TKey : notnull
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            int len = length ?? dictionary.Count - offset;

            if (len < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(len), "Длина не может быть меньше 0");
            }

            if (len == 0)
            {
                return new Dictionary<TKey, TValue>();
            }

            if (offset < 0)
            {
                offset = dictionary.Count + offset;
                if (offset < 0)
                {
                    offset = 0;
                }
            }

            if (offset >= dictionary.Count)
            {
                return new Dictionary<TKey, TValue>();
            }

            int newLen = Math.Min(len, dictionary.Count - offset);
            Dictionary<TKey, TValue> newDictionary = new(newLen);

            int i = 0;
            foreach (KeyValuePair<TKey, TValue> kvp in dictionary)
            {
                if (i >= offset && i < offset + newLen)
                {
                    newDictionary.Add(kvp.Key, kvp.Value);
                }
                i++;
            }

            if (replacement != null)
            {
                int replacementLen = replacement.Count;
                int diff = newLen - replacementLen;

                if (diff < 0)
                {
                    for (int j = offset + newLen; j < dictionary.Count; j++)
                    {
                        KeyValuePair<TKey, TValue> kvp = dictionary.ElementAt(j);
                        if (!newDictionary.ContainsKey(kvp.Key))
                        {
                            newDictionary.Add(kvp.Key, kvp.Value);
                        }
                    }
                }

                for (int j = 0; j < replacementLen; j++)
                {
                    var kvp = replacement.ElementAt(j);
                    if (newDictionary.ContainsKey(kvp.Key))
                    {
                        throw new ArgumentException("Ключ с таким именем уже был");
                    }
                    newDictionary.Add(kvp.Key, kvp.Value);
                }
            }

            return newDictionary;
        }

        /*
         *  -----------------------------------------------
         *  Пример использования:
         *  -----------------------------------------------
         *  
         *  Dictionary<string, string> dict = new()
         *  {
         *      { "1", "val1" },
         *      { "2", "val2" },
         *      { "3", "val3" }
         *  }
         *  Dictionary<string, string> dict2 = new()
         *  {
         *      { "4", "val4" }
         *  };
         *  print_r(dictionary_splice(dict, 0, 2, dict2)); 
         *  
         *  -----------------------------------------------
         *  Результат:
         *  -----------------------------------------------
         *  
         *  Array
         *  (
         *      1, val1,
         *      2, val2,
         *      4, val4
         *  )
         */

        /// <summary>
        ///     Выбирает все значения массива
        /// </summary>
        /// <param name="dictionary">Словарь</param>
        /// <param name="return_list">Если установлено в <b>true</b> -> возвращается в список, иначе -> <b>массив</b></param>
        /// <returns>Возвращает индексированный лист значений.</returns>
        public static IEnumerable<TValue> dictionary_values<TKey, TValue>(Dictionary<TKey, TValue> dictionary, bool return_list = true) where TKey : notnull
        {
            List<TValue> newList = new();
            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
            {
                newList.Add(pair.Value);
            }

            if (return_list)
                return newList;

            return newList.ToArray();
        }

        /*
         *  -------------------------------------
         *  Пример использования:
         *  -------------------------------------
         *  
         *  Dictionary<int, string> dict = new()
         *  {
         *      { 1, "val1" },
         *      { 2, "val2" },
         *      { 3, "val3" }
         *  };
         *  print_r(dictionary_values(dict));
         *  
         *  -------------------------------------
         *  Выведет:
         *  -------------------------------------
         *  
         *  Array
         *  (
         *      0 => val1,
         *      1 => val2,
         *      2 => val3
         *  )
         */

        /// <summary>
        ///     Рекурсивно применяет пользовательскую функцию к каждому элементу массива
        /// </summary>
        /// <param name="dictionary"> <em>Входной словарь</em></param>
        /// <param name="callback">
        /// Обычно, <paramref name="callback"/> принимает два параметра. <br/>
        /// Первым параметром является значение элемента массива <paramref name="dictionary"/>, а вторым - его ключ.
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
        public static void dictionary_walk_recursive<TKey, TValue>(Dictionary<TKey, TValue> dictionary, Action<TValue, TKey> callback) where TKey : notnull
        {
            foreach (var item in dictionary)
            {
                if (item.Value is Dictionary<TKey, TValue>)
                {
                    dictionary_walk_recursive(item.Value as Dictionary<TKey, TValue>, callback);
                }
                else
                {
                    callback(item.Value, item.Key);
                }
            }
        }

        /*
         *  --------------------------------------------------
         *  Пример выполнения:
         *  --------------------------------------------------
         *  
         *  Dictionary<string, object> sweet = new()
         *  {
         *      { "a", "apple" },
         *      { "b", "banana" }
         *  };
         *  
         *  Dictionary<string, object> fruits = new()
         *  {
         *      { "sweet", sweet },
         *      { "sour", "lemon" }
         *  };
         *  
         *  static void test_print(object item, string key)
         *  {
         *      Console.WriteLine($"{key} содержит {item}");
         *  }
         *  
         *  array_walk_recursive(fruits, test_print);
         *  
         *  --------------------------------------------------
         *  Выведет:
         *  --------------------------------------------------
         *  
         *  a содержит apple
         *  b содержит banana
         *  sour содержит lemon
         *  
         *  --------------------------------------------------
         *  Примечание:
         *  --------------------------------------------------
         *  
         *  sweet a => Array
         *             (
         *                 a => apple,
         *                 b => banana
         *             )
         *             
         *  -- Значение a не будет выведено, так как это 
         *  -- ОБЪЕКТ, а не скалярный тип данных
         */

        /// <summary>
        ///     Применяет заданную пользователем функцию к каждому элементу словаря.
        /// </summary>
        /// <param name="dictionary">Входной словарь.</param>
        /// <param name="callback">Обычно функция <strong>callback</strong> принимает два параметра. В качестве первого параметра идёт значение элемента словаря <c>dictionary</c>, а ключ - в качестве второго.</param>
        public static void dictionary_walk<TKey, TValue>(Dictionary<TKey, TValue> dictionary, Action<TValue, TKey> callback) where TKey : notnull
        {
            foreach (var item in dictionary)
            {
                callback(item.Value, item.Key);
            }
        }

        /// <summary>
        ///     Выбирает текущий ключ из словаря
        /// </summary>
        /// <param name="dictionary">Словарь</param>
        /// <param name="value">Значение элемента словаря</param>
        /// <returns></returns>
        public static dynamic? key<TKey, TValue>(Dictionary<TKey, TValue> dictionary) where TKey : notnull
        {
            dynamic? result = null;
            if (currentIndex >= 0 && currentIndex < dictionary.Count)
            {
                result = dictionary.Keys.ElementAt(currentIndex);
            }
            currentIndex++;
            return result;
        }

        /*
         *  ---------------------------------------
         *  Пример использования:
         *  ---------------------------------------
         *  int i = 0;
         *  Dictionary<string, string> dict = new()
         *  {
         *      { "key1", "val1" },
         *      { "key2", "val2" },
         *      { "key3", "val2" }
         *  };
         *  foreach (var pair in dict)
         *  {
         *      echo($"Key = {key(dict)}");
         *      i++;
         *  }
         *  
         *  ---------------------------------------
         *  Выведет:
         *  ---------------------------------------
         *  
         *  Key 0 = key1
         *  Key 1 = key2
         *  Key 2 = key3
         *  
         *  ---------------------------------------
         */

        /// <summary>
        ///     Сортирует массив по ключу в порядке возрастания
        /// </summary>
        /// <param name="dictionary">Входной словарь</param>
        /// <param name="flags">
        /// <list type="bullet">
        ///     <item>
        ///         <term>
        ///             <strong>SORT_REGULAR</strong>
        ///         </term>
        ///         <description>
        ///             Обычное сравнение элементов; подробности описаны в разделе операторы сравнения
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>
        ///             <strong>SORT_NUMERIC</strong>
        ///         </term>
        ///         <description>
        ///             Числовое сравнение элементов
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>
        ///             <strong>SORT_STRING</strong>
        ///         </term>
        ///         <description>
        ///             Строковое сравнение элементов
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>
        ///             <strong>SORT_LOCALE_STRING</strong>
        ///         </term>
        ///         <description>
        ///             Сравнение элементов как строки на основе текущего языкового стандарта.Используется языковой стандарт, который можно изменить с помощью CultureInfo.GetCultureInfo()
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>
        ///             <strong>SORT_NATURAL</strong>
        ///         </term>
        ///         <description>
        ///             Сравнение элементов как строки, используя "естественный порядок", например StringComparer.Ordinal | StringComparer.OrdinalIgnoreCase
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>
        ///             <strong>SORT_FLAG_CASE</strong>
        ///         </term>
        ///         <description>
        ///             Можно объединять (побитовое ИЛИ) с SORT_STRING или SORT_NATURAL для сортировки строк без учёта регистра.
        ///         </description>
        ///     </item>
        /// </list>
        /// </param>
        /// <exception cref="ArgumentException">Вызывается при неправильном указании флага</exception>
        public static void ksort<TKey, TValue>(
            ref Dictionary<TKey, TValue> dictionary,
            SortCase flags = SORT_REGULAR
        ) where TKey : notnull
        {
            dictionary = flags switch
            {
                SORT_REGULAR => dictionary.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value),
                SORT_NUMERIC => dictionary.OrderBy(x => Convert.ToDouble(x.Key)).ToDictionary(x => x.Key, x => x.Value),
                SORT_STRING => dictionary.OrderBy(x => Convert.ToString(x.Key)).ToDictionary(x => x.Key, x => x.Value),
                SORT_LOCALE_STRING => dictionary.OrderBy(x => Convert.ToString(x.Key), StringComparer.CurrentCulture).ToDictionary(x => x.Key, x => x.Value),
                SORT_NATURAL => dictionary.OrderBy(x => x.Key.ToString(), new NaturalStringComparer(StringComparer.Ordinal)).ToDictionary(x => x.Key, x => x.Value),
                SORT_STRING | SORT_FLAG_CASE => dictionary.OrderBy(x => Convert.ToString(x.Key), new NaturalStringComparer(StringComparer.Ordinal)).ToDictionary(x => x.Key, x => x.Value),
                SORT_NATURAL | SORT_FLAG_CASE => dictionary.OrderBy(x => x.Key.ToString(), new NaturalStringComparer(StringComparer.OrdinalIgnoreCase)).ToDictionary(x => x.Key, x => x.Value),
                _ => throw new ArgumentException("Неверный флаг"),
            };
            /*switch (flags)
            {
                case SORT_REGULAR:
                    dictionary = dictionary.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
                    break;
                case SORT_NUMERIC:
                    dictionary = dictionary.OrderBy(x => Convert.ToDouble(x.Key)).ToDictionary(x => x.Key, x => x.Value);
                    break;
                case SORT_STRING:
                    dictionary = dictionary.OrderBy(x => Convert.ToString(x.Key)).ToDictionary(x => x.Key, x => x.Value);
                    break;
                case SORT_LOCALE_STRING:
                    dictionary = dictionary.OrderBy(x => Convert.ToString(x.Key), StringComparer.CurrentCulture).ToDictionary(x => x.Key, x => x.Value);
                    break;
                case SORT_NATURAL:
                    dictionary = dictionary.OrderBy(x => x.Key.ToString(), StringComparer.Ordinal).ToDictionary(x => x.Key, x => x.Value);
                    break;
                case SORT_STRING | SORT_FLAG_CASE:
                    dictionary = dictionary.OrderBy(x => Convert.ToString(x.Key), StringComparer.OrdinalIgnoreCase).ToDictionary(x => x.Key, x => x.Value);
                    break;
                case SORT_NATURAL | SORT_FLAG_CASE:
                    dictionary = dictionary.OrderBy(x => x.Key.ToString(), StringComparer.OrdinalIgnoreCase).ToDictionary(x => x.Key, x => x.Value);
                    break;
                default:
                    throw new ArgumentException("Неверный флаг");
            }*/
        }

        /// <summary>
        ///     Сортирует массив по ключу в порядке возрастания
        /// </summary>
        /// <param name="dictionary">Входной словарь</param>
        /// <param name="flags">
        /// <list type="bullet">
        ///     <item>
        ///         <term>
        ///             <strong>SORT_REGULAR</strong>
        ///         </term>
        ///         <description>
        ///             Обычное сравнение элементов; подробности описаны в разделе операторы сравнения
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>
        ///             <strong>SORT_NUMERIC</strong>
        ///         </term>
        ///         <description>
        ///             Числовое сравнение элементов
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>
        ///             <strong>SORT_STRING</strong>
        ///         </term>
        ///         <description>
        ///             Строковое сравнение элементов
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>
        ///             <strong>SORT_LOCALE_STRING</strong>
        ///         </term>
        ///         <description>
        ///             Сравнение элементов как строки на основе текущего языкового стандарта.Используется языковой стандарт, который можно изменить с помощью CultureInfo.GetCultureInfo()
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>
        ///             <strong>SORT_NATURAL</strong>
        ///         </term>
        ///         <description>
        ///             Сравнение элементов как строки, используя "естественный порядок", например StringComparer.Ordinal | StringComparer.OrdinalIgnoreCase
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>
        ///             <strong>SORT_FLAG_CASE</strong>
        ///         </term>
        ///         <description>
        ///             Можно объединять (побитовое ИЛИ) с SORT_STRING или SORT_NATURAL для сортировки строк без учёта регистра.
        ///         </description>
        ///     </item>
        /// </list>
        /// </param>
        /// <exception cref="ArgumentException">Вызывается при неправильном указании флага</exception>
        public static Dictionary<TKey, TValue> ksort<TKey, TValue>(
            Dictionary<TKey, TValue> dictionary,
            SortCase flags = SORT_REGULAR
        ) where TKey : notnull
        {
            switch (flags)
            {
                case SORT_REGULAR:
                    return dictionary.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
                case SORT_NUMERIC:
                    return dictionary.OrderBy(x => Convert.ToDouble(x.Key)).ToDictionary(x => x.Key, x => x.Value);
                case SORT_STRING:
                    return dictionary.OrderBy(x => Convert.ToString(x.Key)).ToDictionary(x => x.Key, x => x.Value);
                case SORT_LOCALE_STRING:
                    return dictionary.OrderBy(x => Convert.ToString(x.Key), StringComparer.CurrentCulture).ToDictionary(x => x.Key, x => x.Value);
                case SORT_NATURAL:
                    return dictionary.OrderBy(x => x.Key.ToString(), new NaturalStringComparer(StringComparer.Ordinal)).ToDictionary(x => x.Key, x => x.Value);
                case SORT_STRING | SORT_FLAG_CASE:
                    return dictionary.OrderBy(x => Convert.ToString(x.Key), new NaturalStringComparer(StringComparer.Ordinal)).ToDictionary(x => x.Key, x => x.Value);
                case SORT_NATURAL | SORT_FLAG_CASE:
                    return dictionary.OrderBy(x => x.Key.ToString(), new NaturalStringComparer(StringComparer.OrdinalIgnoreCase)).ToDictionary(x => x.Key, x => x.Value);
                default:
                    throw new ArgumentException("Неверный флаг");
            }
        }

        /// <summary>
        ///     Сортирует массив по ключу в порядке убывания
        /// </summary>
        /// <param name="dictionary">Входной словарь</param>
        /// <param name="flags">
        /// <list type="bullet">
        ///     <item>
        ///         <term>
        ///             <em>SORT_REGULAR</em>
        ///         </term>
        ///         <description>
        ///             Обычное сравнение элементов; подробности описаны в разделе операторы сравнения
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>
        ///             <em>SORT_NUMERIC</em>
        ///         </term>
        ///         <description>
        ///             Числовое сравнение элементов
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>
        ///             <em>SORT_STRING</em>
        ///         </term>
        ///         <description>
        ///             Строковое сравнение элементов
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>
        ///             <em>SORT_LOCALE_STRING</em>
        ///         </term>
        ///         <description>
        ///             Сравнение элементов как строки на основе текущего языкового стандарта.Используется языковой стандарт, который можно изменить с помощью CultureInfo.GetCultureInfo()
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>
        ///             <em>SORT_NATURAL</em>
        ///         </term>
        ///         <description>
        ///             Сравнение элементов как строки, используя "естественный порядок", например StringComparer.Ordinal | StringComparer.OrdinalIgnoreCase
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>
        ///             <em>SORT_FLAG_CASE</em>
        ///         </term>
        ///         <description>
        ///             Можно объединять (побитовое ИЛИ) с SORT_STRING или SORT_NATURAL для сортировки строк без учёта регистра.
        ///         </description>
        ///     </item>
        /// </list>
        /// </param>
        /// <exception cref="ArgumentException"></exception>
        public static Dictionary<TKey, TValue> krsort<TKey, TValue>(
            Dictionary<TKey, TValue> dictionary,
            SortCase flags = SORT_REGULAR
        ) where TKey : notnull
        {
            switch (flags)
            {
                case SORT_REGULAR:
                    return dictionary.OrderByDescending(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
                case SORT_NUMERIC:
                    return dictionary.OrderByDescending(x => Convert.ToDouble(x.Key)).ToDictionary(x => x.Key, x => x.Value);
                case SORT_STRING:
                    return dictionary.OrderByDescending(x => Convert.ToString(x.Key)).ToDictionary(x => x.Key, x => x.Value);
                case SORT_LOCALE_STRING:
                    return dictionary.OrderByDescending(x => Convert.ToString(x.Key), StringComparer.CurrentCulture).ToDictionary(x => x.Key, x => x.Value);
                case SORT_NATURAL:
                    return dictionary.OrderByDescending(x => x.Key.ToString(), new NaturalStringComparer(StringComparer.Ordinal)).ToDictionary(x => x.Key, x => x.Value);
                case SORT_STRING | SORT_FLAG_CASE:
                    return dictionary.OrderByDescending(x => Convert.ToString(x.Key), new NaturalStringComparer(StringComparer.Ordinal)).ToDictionary(x => x.Key, x => x.Value);
                case SORT_NATURAL | SORT_FLAG_CASE:
                    return dictionary.OrderByDescending(x => x.Key.ToString(), new NaturalStringComparer(StringComparer.OrdinalIgnoreCase)).ToDictionary(x => x.Key, x => x.Value);
                default:
                    throw new ArgumentException("Неверный флаг");
            }
        }

        /// <summary>
        ///     Сортирует массив по ключу в порядке убывания
        /// </summary>
        /// <param name="dictionary">Входной словарь</param>
        /// <param name="flags">
        /// <list type="bullet">
        ///     <item>
        ///         <term>
        ///             <strong>SORT_REGULAR</strong>
        ///         </term>
        ///         <description>
        ///             Обычное сравнение элементов; подробности описаны в разделе операторы сравнения
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>
        ///             <strong>SORT_NUMERIC</strong>
        ///         </term>
        ///         <description>
        ///             Числовое сравнение элементов
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>
        ///             <strong>SORT_STRING</strong>
        ///         </term>
        ///         <description>
        ///             Строковое сравнение элементов
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>
        ///             <strong>SORT_LOCALE_STRING</strong>
        ///         </term>
        ///         <description>
        ///             Сравнение элементов как строки на основе текущего языкового стандарта.Используется языковой стандарт, который можно изменить с помощью CultureInfo.GetCultureInfo()
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>
        ///             <strong>SORT_NATURAL</strong>
        ///         </term>
        ///         <description>
        ///             Сравнение элементов как строки, используя "естественный порядок", например StringComparer.Ordinal | StringComparer.OrdinalIgnoreCase
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term>
        ///             <strong>SORT_FLAG_CASE</strong>
        ///         </term>
        ///         <description>
        ///             Можно объединять (побитовое ИЛИ) с SORT_STRING или SORT_NATURAL для сортировки строк без учёта регистра.
        ///         </description>
        ///     </item>
        /// </list>
        /// </param>
        /// <exception cref="ArgumentException">Вызывается при неправильном указании флага</exception>
        public static void krsort<TKey, TValue>(
            ref Dictionary<TKey, TValue> dictionary,
            SortCase flags = SORT_REGULAR
        ) where TKey : notnull
        {
            switch (flags)
            {
                case SORT_REGULAR:
                    dictionary = dictionary.OrderByDescending(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
                    break;
                case SORT_NUMERIC:
                    dictionary = dictionary.OrderByDescending(x => Convert.ToDouble(x.Key)).ToDictionary(x => x.Key, x => x.Value);
                    break;
                case SORT_STRING:
                    dictionary = dictionary.OrderByDescending(x => Convert.ToString(x.Key)).ToDictionary(x => x.Key, x => x.Value);
                    break;
                case SORT_LOCALE_STRING:
                    dictionary = dictionary.OrderByDescending(x => Convert.ToString(x.Key), StringComparer.CurrentCulture).ToDictionary(x => x.Key, x => x.Value);
                    break;
                case SORT_NATURAL:
                    dictionary = dictionary.OrderByDescending(x => x.Key.ToString(), new NaturalStringComparer(StringComparer.Ordinal)).ToDictionary(x => x.Key, x => x.Value);
                    break;
                case SORT_STRING | SORT_FLAG_CASE:
                    dictionary = dictionary.OrderByDescending(x => Convert.ToString(x.Key), new NaturalStringComparer(StringComparer.Ordinal)).ToDictionary(x => x.Key, x => x.Value);
                    break;
                case SORT_NATURAL | SORT_FLAG_CASE:
                    dictionary = dictionary.OrderByDescending(x => x.Key.ToString(), new NaturalStringComparer(StringComparer.OrdinalIgnoreCase)).ToDictionary(x => x.Key, x => x.Value);
                    break;
                default:
                    throw new ArgumentException("Неверный флаг");
            }
        }

        /*
         *  ---------------------------------------
         *  Пример выполнения:
         *  ---------------------------------------
         *  
         *  Dictionary<object, string> dict = new()
         *  {
         *      { "2", "val1" },
         *      { 1, "val2" },
         *      { "3", "val2" }
         *  };
         *  print_r(dict);
         *  ksort(ref dict, SORT_NUMERIC);
         *  
         *  ---------------------------------------
         *  Выведет:
         *  ---------------------------------------
         *  
         *  Array
         *  (
         *      1, val2,
         *      2, val1,
         *      3, val2
         *  )
         *  
         *  ---------------------------------------
         *  Без SORT_NUMERIC выведет:
         *  ---------------------------------------
         *  
         *  Array
         *  (
         *      2, val1,
         *      1, val2,
         *      3, val2
         *  )
         *  
         *  ---------------------------------------
         */
    }
}
