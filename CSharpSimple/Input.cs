namespace simple
{
    public static class In
    {
        public static T? Input<T>(T message) where T: notnull
        {
            Console.Write(message);
            string? input = Console.ReadLine();
            var result = (T?)Convert.ChangeType(input, typeof(T?));
            return result;
        }

        public static void Input<T>(T message, out T? var) where T: notnull
        {
            Console.Write(message);
            string? input = Console.ReadLine();
            var result = (T?)Convert.ChangeType(input, typeof(T?));
            var = result;
        }

        public static void Press()
        {
            Console.ReadKey();
        }
    }
}
