using System;

namespace Boulderdash
{
    public class GameObject : ICanInteract
    {        
        public gameElements Description { set; get; }        
        public int X { set; get; }
        public int Y { set; get; }

        public GameObject(int x, int y, gameElements description)
        {            
            X = x;
            Y = y;
            Description = description;
        }

        public virtual void Action(Map map) { }

        public virtual void Hit(Map map) { }
    }

    abstract public class InanimateObjects : GameObject
    {
        public InanimateObjects(int X, int Y, gameElements Description) : base(X, Y, Description) { }
    }

    abstract public class AnimateObject : GameObject
    {
        public direction Direction { set; get; }
        public gameElements CellThatWasHere { set; get; }
        public byte health { set; get; }
        public AnimateObject(int X, int Y, gameElements Description, byte health, direction direction) : base(X, Y, Description)
        {
            CellThatWasHere = gameElements.Empty;
            this.health = health;
            Direction = direction;
        }

        public void Move(Map map) 
        {
            Point nextCell = new Point(X, Y);

            if (true) 
            {
                switch (Direction)
                {
                    case direction.Stop:
                        break;
                    case direction.Right:
                        if (canMoveToTheNextCell(new Point(X, Y + 1)))
                           nextCell.Y++;                         
                        break;
                    case direction.Left:
                        if (canMoveToTheNextCell(new Point(X, Y - 1)))
                           nextCell.Y--;                         
                        break;
                    case direction.Up:
                        if (canMoveToTheNextCell(new Point(X - 1, Y)))                        
                            nextCell.X--;                         
                        break;
                    case direction.Down:
                        if (canMoveToTheNextCell(new Point(X + 1, Y)))                                                    
                            nextCell.X++;                            
                        break;
                    default:
                        break;
                }
            }
            
            if (X != nextCell.X || Y != nextCell.Y)
            {
                map[X, Y] = new Cell(CellOnThePreviousPosition(map, new Point(X, Y)));                                    
                CellThatWasHere = map[nextCell.X, nextCell.Y].description; 
                X = nextCell.X;
                Y = nextCell.Y;
                map[X, Y] = new Cell(Description)
                {
                    symbol = CharDependingOnDirection(Direction, map)
                };
            }
            
            Direction = direction.Stop;

            bool canMoveToTheNextCell(Point NextCell)
            {
                if (!inMap(NextCell))
                    return false;

                if (map[NextCell.X, NextCell.Y].description != gameElements.Wall &&
                   map[NextCell.X, NextCell.Y].description != gameElements.Stone) 
                    return true;
                else
                    return false;
            }

            bool inMap(Point ChekedCell)
            {
                if (ChekedCell.X >= map.Height || ChekedCell.X < 0 || ChekedCell.Y >= map.Width || ChekedCell.Y < 0)
                    return false;
                else
                    return true;
            }
        }

        public virtual gameElements CellOnThePreviousPosition(Map map, Point PreviousCell)
        {
            return CellThatWasHere;
        }

        public virtual char CharDependingOnDirection(direction direction, Map map)
        {
            return 'I';
        }

        public override void Hit(Map map)
        {
            health--;
           
        }
    }
}
