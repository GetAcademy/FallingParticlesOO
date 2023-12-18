namespace FallingParticles
{
    internal class Game
    {
        private int paddlePosition;
        private int paddleMoveDistance = 6;
        private string paddle = "========";
        private List<Particle> particles = new List<Particle>();
        private int level;
        private int score;
        private int gameRoundsBetweenSpawn;
        private readonly Random random = new Random();
        private int _levelCount;
        private int _roundCount;
        public bool IsRunning { get; private set; }

        public Game()
        {
            Console.CursorVisible = false;
            Console.WindowWidth = 80;
            _levelCount = 0;
            _roundCount = 45;
            var centerX = Console.WindowWidth / 2;
            paddlePosition = centerX - (centerX % paddleMoveDistance);
            particles.Clear();
            IsRunning = true;
            score = 0;
            level = 1;
            InitGameRoundsBetweenSpawn();
        }
        void InitGameRoundsBetweenSpawn()
        {
            gameRoundsBetweenSpawn = 50 / level;
        }

        public void RunOneGameLoop()
        {
            DrawGame();
            MovePaddle();
            MoveParticles();
            var hasLostParticle = CheckLostParticle();
            if (hasLostParticle)
            {
                IsRunning = false;
                return;
            }
            if (_roundCount >= gameRoundsBetweenSpawn)
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
                level++;
            }
            Thread.Sleep(100);
        }

        public void ShowGameOver()
        {
            var text = "Game Over! Press ENTER to restart";
            Console.SetCursorPosition(40 - text.Length / 2, 5);
            Console.WriteLine(text);
            Console.ReadLine();
        }

        void DrawGame()
        {
            Console.Clear();
            Console.SetCursorPosition(60, 0);
            Console.Write($"Score: {score}");
            Console.SetCursorPosition(71, 0);
            Console.Write($"Level: {level}");
            Console.SetCursorPosition(paddlePosition, Console.WindowHeight - 1);
            Console.Write(paddle);

            foreach (var particle in particles)
            {
                var particleX = (int)Math.Floor(particle.X);
                var particleY = (int)Math.Floor(particle.Y);
                Console.SetCursorPosition(particleX, particleY);
                Console.Write("O");
            }
        }

        void MovePaddle()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                var moveLeft = key.Key == ConsoleKey.LeftArrow && paddlePosition >= paddleMoveDistance;
                var moveRight = key.Key == ConsoleKey.RightArrow && paddlePosition < Console.WindowWidth - paddle.Length;
                if (moveLeft || moveRight)
                {
                    var direction = moveLeft ? -1 : 1;
                    paddlePosition += direction * 3 * paddle.Length / 4;
                }
            }
        }

        void MoveParticles()
        {
            for (var index = particles.Count - 1; index >= 0; index--)
            {
                var particle = particles[index];
                particle.Y += 0.5f;
                if (particle.Y > Console.WindowHeight - 1)
                {
                    score++;
                    particles.Remove(particle);
                }
            }
        }

        bool CheckLostParticle()
        {
            foreach (var particle in particles)
            {
                if ((particle.X < paddlePosition || particle.X > paddlePosition + paddle.Length)
                    && particle.Y == Console.WindowHeight - 1)
                {
                    return true;
                }
            }

            return false;
        }

        void SpawnParticles()
        {
            var newParticle = new Particle
            {
                X = random.Next(0, Console.WindowWidth),
                Y = 0
            };
            particles.Add(newParticle);
        }

    }
}
