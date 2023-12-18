using FallingParticles;

var game = new Game();
while (game.IsRunning)
{
    game.RunOneGameLoop();
}
game.ShowGameOver();