using System.Collections;
namespace lw
{
    public static class Bool
    {
        // Проверка строки на наличие только цифр, запятых и пробелов - если да, то true, иначе false
        //public static bool is_
        /// <summary>
        ///     Определяет, является ли переменная числом
        /// </summary>
        /// <param name="value">Переменная</param>
        /// <returns>Возвращает true, если число, иначе false</returns>
        public static bool is_numeric(dynamic value)
        {
            if (!int.TryParse(value, out int _)) return false;
            return true;
        }

        /// <summary>
        ///     Определяет, является ли переменная булевой (логической)
        /// </summary>
        /// <param name="value">Проверяемая переменная.</param>
        /// <returns>Возвращает true, если логическая переменная, иначе false</returns>
        public static bool is_bool(dynamic value)
        {
            if (!bool.TryParse(value, out bool _)) return false;
            return true;
        }

        public static bool is_float(dynamic value)
        {
            if (!float.TryParse(value, out float _)) return false;
            return true;
        }

        /// <summary>
        ///     Проверяет, является ли переменная массивом
        /// </summary>
        /// <param name="value">Переменная</param>
        /// <returns>Возвращает true, если массив, иначе false</returns>
        public static bool is_array(dynamic value)
        {
            if (!value.GetType().IsArray) return false;
            return true;
        }

        /// <summary>
        ///     Проверяет, является ли переменная объектом
        /// </summary>
        /// <param name="value">Переменная</param>
        /// <returns>Возвращает true, если объект, иначе false</returns>
        public static bool is_object(dynamic value)
        {
            if (!value is object && (value is IEnumerable || value.GetType().IsArray)) return false;
            return true;
        }

        /// <summary>
        ///     Проверяет, является ли переменная списком
        /// </summary>
        /// <param name="value">Переменная</param>
        /// <returns>Возвращает true, если список, иначе false</returns>
        public static bool is_list(dynamic value)
        {
            if (!value is IList && value.GetType().IsArray) return false;
            return true;
        }

        /// <summary>
        ///     Меняет значения местами
        /// </summary>
        /// <param name="a">Первый аргумент</param>
        /// <param name="b">Второй аргумент</param>
        /// <returns> Меняет значения аргументов в программе </returns>
        public static void swap<T>(ref T a, ref T b)
        {
            (a, b) = (b, a);
        }

        /// <summary>
        ///     Функция empty предназначена для выявления пустых аргументов
        /// </summary>
        /// <param name="str"></param>
        /// <returns>Возвращает true если элемент пустой, иначе false</returns>
        public static bool empty(dynamic value)
        {
            if (string.IsNullOrWhiteSpace(value)) return true;
            return false;
        }
        public static bool isset(dynamic value)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value)) return false;
            return true;
        }
    }
}