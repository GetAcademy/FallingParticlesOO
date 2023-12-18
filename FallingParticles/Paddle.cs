using System.Reflection.Metadata;

namespace FallingParticles
{
    internal class Paddle
    {
        private int _paddlePosition;
        private readonly int _paddleMoveDistance = 6;
        private readonly string _paddle = "========";

        public Paddle()
        {
            var centerX = Console.WindowWidth / 2;
            _paddlePosition = centerX - (centerX % _paddleMoveDistance);
        }

        public void Show()
        {
            GameConsole.Write(_paddlePosition, Console.WindowHeight - 1, _paddle);
        }

        public void Move()
        {
            if (!Console.KeyAvailable) return;
            var key = Console.ReadKey(true);
            var moveLeft = key.Key == ConsoleKey.LeftArrow && _paddlePosition >= _paddleMoveDistance;
            var moveRight = key.Key == ConsoleKey.RightArrow && _paddlePosition < Console.WindowWidth - _paddle.Length;
            if (!moveLeft && !moveRight) return;
            var direction = moveLeft ? -1 : 1;
            _paddlePosition += direction * 3 * _paddle.Length / 4;
        }

        public bool Covers(int x, int y)
        {
            return x < _paddlePosition
                || x > _paddlePosition + _paddle.Length;
        }
    }
}
