namespace lw
{
    public static class String
    {
        /// <summary>
        ///     Возвращает количество вхождений подстроки
        /// </summary>
        /// <param name="value">Строка</param>
        /// <param name="needle">Подстрока</param>
        /// <param name="offset">Смещение</param>
        /// <returns>Кол-во вхождений подстроки без учёта регистра</returns>
        public static int strpos(string value, string needle, int offset = 0)
        {
            int count = 0;
            int index = value.IndexOf(needle, offset, StringComparison.OrdinalIgnoreCase);
            while (index != -1)
            {
                count++;
                index = value.IndexOf(needle, index + needle.Length, StringComparison.OrdinalIgnoreCase);
            }
            return count;
        }

        /// <summary>
        ///     Возвращает количество вхождений подстроки с учётом регистра
        /// </summary>
        /// <param name="value">Строка</param>
        /// <param name="needle">Подстрока</param>
        /// <param name="offset">Смещение</param>
        /// <returns>Кол-во вхождений подстроки с учётом регистра</returns>
        public static int stripos(string value, string needle, int offset = 0)
        {
            int count = 0;
            int index = value.IndexOf(needle, offset, StringComparison.Ordinal);
            while (index != -1)
            {
                count++;
                index = value.IndexOf(needle, index + needle.Length, StringComparison.Ordinal);
            }
            return count;
        }

        /// <summary>
        ///     Функция substr выполняет выделение некоторой части строки.
        ///     Для работы требует хотя бы 2 аргумента. При наличии длины делает обрезание строки.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns>Возвращает обрезаную строку</returns>
        /// <exception cref="ArgumentException"></exception>
        public static void substr(ref string str, int offset = 0, int? length = null)
        {
            // Проверка на наличие параметра и значения длины 
            length ??= str.Length;

            // Проверка на наличие параметра сдвига
            if (offset < 1)
            {
                throw new ArgumentException("substr() expects at least 2 parameters, 1 given");
            }

            // Длина не должна превышать допустимую границу
            if (offset + length > str.Length)
            {
                length = str.Length - offset;
            }

            // Подстрока с учётом сдвига и длины
            str = str.Substring(offset, (int)length);
        }

        /*
            *
            * Пример использования substr:
            *
            *   try
            *   {
            *       Console.WriteLine(substr(str{, 2}[, 4]));
            *   }
            *   catch (ArgumentException ae)
            *   {
            *       StackTrace? stackTrace = new(true);
            *       StackFrame? stackFrame = stackTrace.GetFrame(0); // Получаем текущий вызов
            *       Console.Write($"Warning: {ae.Message} on line {stackFrame.GetFileLineNumber()}"); // Выводим номер строки
            *       Console.ReadKey();
            *       return;
            *   }
            *
            */
    }
}