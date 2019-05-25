using System;

namespace Boulderdash
{
    public class Cell
    {        
        public gameElements description { set; get; }
        public ConsoleColor frontColor { set; get; }
        public ConsoleColor backColor { set; get; }

        public char symbol { set; get; }
        public Cell(gameElements Description, char Symbol = ' ', ConsoleColor FrontColor = ConsoleColor.White, ConsoleColor BackColor = ConsoleColor.Black)
        {            
            description = Description;
            DefoultCellSettings();
        }
        public void DefoultCellSettings()
        {
            Random rnd = new Random();
            switch (description)
            {
                case gameElements.Empty:                    
                    frontColor = ConsoleColor.White;
                    backColor = ConsoleColor.Black;
                    symbol = ' ';
                    break;
                case gameElements.Sand:
                    frontColor = ConsoleColor.Yellow;
                    backColor = ConsoleColor.Black;
                    symbol = '.';
                    break;
                case gameElements.Stone:
                    frontColor = ConsoleColor.Gray;
                    backColor = ConsoleColor.Black;
                    symbol = '@';
                    break;
                case gameElements.Player:
                    frontColor = ConsoleColor.Cyan;
                    backColor = ConsoleColor.Black;
                    symbol = 'I';
                    break;                
                case gameElements.Trap:
                    frontColor = ConsoleColor.Red;
                    backColor = ConsoleColor.Black;
                    symbol = ',';
                    break;
                case gameElements.Wall:
                    frontColor = ConsoleColor.DarkGray;
                    backColor = ConsoleColor.DarkGray;
                    symbol = '#';
                    break;
                case gameElements.Coin:
                    frontColor = ConsoleColor.DarkYellow;
                    backColor = ConsoleColor.Black;
                    symbol = 'o';
                    break;
                case gameElements.Corpse:
                    frontColor = ConsoleColor.Magenta;
                    backColor = ConsoleColor.Black;
                    symbol = '_';
                    break;
                
                
                default:
                    break;
            }
        }
        public void CopyCell(Cell Copied)
        {            
            symbol = Copied.symbol;
            backColor = Copied.backColor;
            frontColor = Copied.frontColor;
            description = Copied.description;            
        }
        public void DrawCell()
        {
            Console.ForegroundColor = frontColor;
            Console.BackgroundColor = backColor;
            Console.Write("{0} ", symbol);        
        }
    }
}