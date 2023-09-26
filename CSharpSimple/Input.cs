namespace lw
{
    public static class Input
    {
        public static T? input<T>(T message)
        {
            Console.Write(message);
            string? input = Console.ReadLine();
            var result = (T?)Convert.ChangeType(input, typeof(T?));
            return result;
        }
        public static void press()
        {
            Console.ReadKey();
        }
    }
}
