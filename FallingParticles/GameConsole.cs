namespace FallingParticles
{
    internal class GameConsole
    {
        public static void WriteRightAligned(int left, int top, string text)
        {
            Write(left - text.Length, top, text);
        }
        public static void WriteCentered(int top, string text)
        {
            var spaceWidth = Console.WindowWidth  - text.Length;
            Write(spaceWidth/2, top, text);
        }
        public static void Write(int left, int top, string text)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(text);
        }
    }
}
