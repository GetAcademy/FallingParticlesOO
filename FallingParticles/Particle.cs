namespace FallingParticles
{
    internal class Particle
    {
        private float _x;
        private float _y;
        public int X => (int)Math.Floor(_x);
        public int Y => (int)Math.Floor(_y);
        private static Random random = new Random();

        public Particle()
        {
            _x = random.Next(0, Console.WindowWidth);
        }

        public void Show()
        {
            GameConsole.Write(X, Y, "O");
        }

        public bool MoveAndCheckForExit()
        {
            _y += 0.5f;
            var exited = Y > Console.WindowHeight - 1;
            return exited;
        }

        public bool DidMiss(Paddle paddle)
        {
            if (Y != Console.WindowHeight - 1) return false;
            return !paddle.Covers(X, Y);
        }
    }
}
