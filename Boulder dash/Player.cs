using System;

namespace Boulderdash
{
    public class Player : AnimateObject
    {
        public int Score { set; get; }


        public Player(int X = 0, int Y = 0, gameElements Description = gameElements.Player, byte health = 5, direction direction = direction.Stop, int score = 0) : base(X, Y, Description, health, direction)
        {
            Description = gameElements.Player;
            Direction = direction.Stop;
            base.health = health;
            Score = 0;
        } 

        public override gameElements CellOnThePreviousPosition(Map map, Point PreviousCell)
        {
            if (CellThatWasHere == gameElements.Coin)
                Score++;

            if (CellThatWasHere == gameElements.Trap)
                health--;
            if (CellThatWasHere == gameElements.Corpse)
                return gameElements.Corpse;     
            else
                return gameElements.Empty;            
        }

        public override char CharDependingOnDirection(direction direction, Map map)
        {
            
            switch (direction)
            {
                case direction.Stop:
                    return 'I';                    
                case direction.Right:
                    return '>';
                case direction.Left:
                    return '<';
                case direction.Up:
                    return '^';
                case direction.Down:
                    return 'v';                    
                default:
                    return 'I';                    
            }            
        }
        
        public void ChooseDirection(ConsoleKey consoleKey)
        {            
            switch (consoleKey)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    Direction = direction.Up;
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    Direction = direction.Down;
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    Direction = direction.Right;
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    Direction = direction.Left;
                    break;
                default:
                    Direction = direction.Stop;
                    break;
            }                        
        }

        public override void Action(Map map)
        {
            if (Console.KeyAvailable)
                ChooseDirection(Console.ReadKey(true).Key);
            
            Move(map);
        }
    }
}