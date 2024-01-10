using Stride.Engine;

namespace R3.Stride3d.Sandbox
{
    class R3_Stride3d_SandboxApp
    {
        static void Main(string[] args)
        {
            using (var game = new Game())
            {
                game.Run();
            }
        }
    }
}
