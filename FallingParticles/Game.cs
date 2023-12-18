namespace FallingParticles
{
    internal class Game
    {
        private readonly List<Particle> _particles = new List<Particle>();
        private int _level;
        private int _score;
        private int _gameRoundsBetweenSpawn;
        private int _levelCount;
        private int _roundCount;
        private readonly Paddle _paddle;
        public bool IsRunning { get; private set; }

        public Game()
        {
            Console.CursorVisible = false;
            Console.WindowWidth = 80;
            _levelCount = 0;
            _roundCount = 45;
            IsRunning = true;
            _score = 0;
            _level = 1;
            InitGameRoundsBetweenSpawn();
            _paddle = new Paddle();
        }
        void InitGameRoundsBetweenSpawn()
        {
            _gameRoundsBetweenSpawn = 50 / _level;
        }

        public void RunOneGameLoop()
        {
            DrawGame();
            _paddle.Move();
            MoveParticles();
            var hasLostParticle = CheckLostParticle();
            if (hasLostParticle)
            {
                IsRunning = false;
                return;
            }
            if (_roundCount >= _gameRoundsBetweenSpawn)
            {
                SpawnParticles();
                InitGameRoundsBetweenSpawn();
                _roundCount = 0;
            }
            _roundCount++;
            _levelCount++;
            if (_levelCount == 100)
            {
                _levelCount = 0;
                _level++;
            }
            Thread.Sleep(100);
        }

        public void ShowGameOver()
        {
            GameConsole.WriteCentered(5, "Game Over! Press ENTER to restart");
            Console.ReadLine();
        }

        void DrawGame()
        {
            Console.Clear();
            GameConsole.WriteRightAligned(79, 0, $"Score: {_score}");
            GameConsole.WriteRightAligned(69, 0, $"Level: {_level}");
            _paddle.Show();
            foreach (var particle in _particles)
            {
                particle.Show();
            }
        }

        void MoveParticles()
        {
            for (var index = _particles.Count - 1; index >= 0; index--)
            {
                var particle = _particles[index];
                var remove = particle.MoveAndCheckForExit();
                if (remove)
                {
                    _score++;
                    _particles.Remove(particle);
                }
            }
        }

        bool CheckLostParticle()
        {
            foreach (var particle in _particles)
            {
                if (particle.DidMiss(_paddle)) return true;
            }
            return false;
        }

        void SpawnParticles()
        {
            var newParticle = new Particle();
            _particles.Add(newParticle);
        }
    }
}
