using System;

namespace Kana
{
    class Program
    {
        static void Main(string[] args)
        {
            KanaUI kanaUI = new KanaUI(new KanaGame());
            kanaUI.StartGame();
        }
    }
}
