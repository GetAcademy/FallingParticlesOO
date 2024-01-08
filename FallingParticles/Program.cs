using FallingParticles;

while (true)
{
    var game = new Game();
    while (game.IsRunning)
    {
        game.RunOneGameLoop();
    }
    game.ShowGameOver();
}