using System;

namespace SamGame01
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new CoinQuest())
                game.Run();
        }
    }
}
