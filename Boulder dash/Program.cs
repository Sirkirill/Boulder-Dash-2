using System;
using System.Threading;

namespace Boulderdash
{ 
    class Program
    {
        public static Cell WallFather = new Cell(gameElements.Wall);
        public static Cell CoinFather = new Cell(gameElements.Coin);
        public static Cell TrapFather = new Cell(gameElements.Trap);
        public static Cell StoneFather = new Cell(gameElements.Stone);
        public static Cell SandFather = new Cell(gameElements.Sand);
        public static Cell EmptyFather = new Cell(gameElements.Empty);
        public static Cell PlayerFather = new Cell(gameElements.Player);

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            CharacterOutput("Hello, enter your name: ");
            string PlayerName = Console.ReadLine();
            Player player = new Player();
            Map map = new Map();            
            map.DrawMap();      
            while (map.player.health > 0)
            {
                map.player.Action(map);
                map.UpdateStones();
                map.DrawMap();
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("{1}, your score: {0}, your lives: {2}", map.player.Score, PlayerName,map.player.health);
            }

            Console.WriteLine("Game Over, your score: {0}", map.player.Score);

            Console.ReadKey();
        }
        public static void CharacterOutput(string str)
        {
            bool skip = false;            
            foreach (char ch in str)
            {
                skip = Console.KeyAvailable;
                Console.Write(ch);                
                if (!skip)
                    Thread.Sleep(50);
            }
        }        
    }
}
