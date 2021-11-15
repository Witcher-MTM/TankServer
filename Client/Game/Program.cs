using System;
using WinFormsApp1;
namespace Client_Graphic
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {   
            Login.StartLogin();
            if (Login.IsLogin)
            {
                using (var game = new Game1())
                    game.Run();
            }

        }
    }
}
