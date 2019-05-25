using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boulderdash
{
    public class Map
    {
        public int Height { get; }
        public int Width { get; }
        public int FrameNumber { set; get; }
        public int NumberOfCoins { set; get; }
        public int NumberOfTrap { set; get; }
        public int NumberOfStones { set; get; }
        //public Pair<int, int> PlayerPos { set; get; } // нужно // НЕ НУЖНО, В ГЕНЕРАЦИИ СОЗДАЕМ PLAYER и Enemies
        // не надо рисовать [ ] когда появятся экземпляры их классов они сами себя нарисуют, в генерации лишь создаем и задаем им координаты        
        
        //public ListOfDestroyedWalls listOfDestroyedWalls = new ListOfDestroyedWalls( new DestroyedWall[] { new DestroyedWall(false), new DestroyedWall(false), new DestroyedWall(false), new DestroyedWall(false), new DestroyedWall(false)} );
        public Player player = new Player();        
        public Cell[,] map = new Cell[80, 50];

        public Map(int numberOfTrap = 0, int numberOfCoins = 0, int numberOfStones = 0, int height = 30, int width = 30)
        {
            FrameNumber = 0;
            
            NumberOfCoins = numberOfCoins;
            NumberOfTrap = numberOfTrap;
            NumberOfStones = numberOfStones;
           Height = height;
            if (height % 2 == 0)
                Height += 1;

            Width = width;
            if (width % 2 == 0)
                Width += 1;

            GenerateNewMap();
        }

        public Cell this[int i, int j]
        {
            set { map[i, j] = value; }
            get { return map[i, j]; }
        }

        private void GenerateNewMap()
        {                        
            Random rnd = new Random();
            
            Point EmptyCell;
            int square = Height * Width;
           
             
            int NumberOfCoinsIter = NumberOfCoins;
            int NumberOfTrapIter = NumberOfTrap;
            int NumberOfStonesIter = NumberOfStones;
           

            if (this.NumberOfCoins == 0)
                NumberOfCoinsIter = rnd.Next(square / 10) + 1;
            if (this.NumberOfStones == 0)
                NumberOfStonesIter = rnd.Next(square / 10) + 1;
            if (this.NumberOfTrap == 0)
                NumberOfTrapIter = rnd.Next(square / 30) + 1;
            if (NumberOfTrapIter > 20)
                NumberOfTrapIter = 20;

            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    map[row, col] = new Cell(gameElements.Empty);
                }
            }
            
                for (int row = 0; row < Height; row++)
                {
                    for (int col = 0; col < Width; col++)
                    {
                        map[row, col].CopyCell(Program.SandFather);
                    }
                }


            for (int row = 0; row < Height; row++)
            {
                map[row, 0].CopyCell(Program.WallFather);
            }
            for (int row = 0; row < Height; row++)
            {
                map[row, Width - 1].CopyCell(Program.WallFather);
            } // генерация правой и левой стенки            
            for (int col = 0; col < Height; col++)
            {
                map[0, col].CopyCell(Program.WallFather);
            }
            for (int col = 0; col < Height; col++)
            {
                map[Height-1, col].CopyCell(Program.WallFather);
            }
            // генерация верхних и нижних стенок


            while (NumberOfStonesIter-- != 0)
            {
                EmptyCell = GetRandomEmptyCell();
                map[EmptyCell.X, EmptyCell.Y].CopyCell(Program.StoneFather);
            } // генерация монет
            while (NumberOfCoinsIter-- != 0)
            {
                EmptyCell = GetRandomEmptyCell();
                map[EmptyCell.X, EmptyCell.Y].CopyCell(Program.CoinFather);
            } // генерация монет

            while (NumberOfTrapIter-- > 0)
            {
                EmptyCell = GetRandomEmptyCell();
                //map[EmptyCell.X, EmptyCell.Y].CopyCell(Program.EmptyFather);
                map[EmptyCell.X, EmptyCell.Y] = new Cell(gameElements.Trap);
            } // генерация врагов

            EmptyCell = GetRandomEmptyCell();
            map[EmptyCell.X, EmptyCell.Y].CopyCell(Program.PlayerFather);
            // спавн игрока 

            


            UpdateInfo();
        }
        
        public void DrawMap(int x = 0, int y = 0)
        {
            //Console.Clear();
            Console.SetCursorPosition(x, y);
            //Console.SetCursorPosition(x, y);
                      
            for (int row = 0; row <Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    map[row, col].DrawCell();
                }                
                Console.WriteLine();
            }
            FrameNumber++;
        }

        public List<GameObject> GetStones()
        {
            List<GameObject> stones = new List<GameObject>();
            for (int i = 1; i < Height - 1; i++)
            {
                for (int j = 1; j < Width - 1; j++)
                {
                    if (map[i, j].description == gameElements.Stone)
                        stones.Add(new GameObject(i, j,map[i,j].description));
                }
            }
            return stones;
        }
        public void UpdateStone(object stone)
        {
            var s = (GameObject)stone;
            if (map[player.X - 1, player.Y].description == gameElements.Stone)
            {
                player.health = 0;
            }
            else if (map[s.X + 1, s.Y].description == gameElements.Empty)
            {
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        if (i == s.X+1 && j == s.Y )
                        {
                            map[i, j].CopyCell(Program.EmptyFather);
                            map[i,j].description = gameElements.Stone;
                            map[i - 1, j].CopyCell(Program.EmptyFather);
                        }
                    }
                }
            }
        }
        public Task<int> UpdateStones()
        {
            return Task.Run(() =>
            {
                foreach (var stone in GetStones())
                {
                    UpdateStone(stone);
                }
                return 1;
            });
            

        }
        public async void UpdateAllStones()
        {
            Task taskupdate = UpdateStones();
            int value = await UpdateStones();
            DrawMap();
        }
        public Point GetRandomEmptyCell()
        {
            int x = 0;
            int y = 0;
            Random rnd = new Random();
            while (map[x, y].description != gameElements.Empty && map[x, y].description != gameElements.Sand)
            {
                x = rnd.Next(this.Height);
                y = rnd.Next(this.Width);
            }
            return new Point(x, y);
        }

        
        
        public void UpdateInfo()
        {
            NumberOfCoins = 0;
            NumberOfTrap = 0;

            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {                    
                    switch (map[row, col].description)
                    {                        
                        case gameElements.Player:
                            player.X = row;
                            player.Y = col;
                            break;
                        case gameElements.Trap:
                            NumberOfTrap++;
                            break;                        
                        case gameElements.Coin:
                            NumberOfCoins++;
                            break;                        
                        default:
                            break;
                    }
                }
            }
        }
    }
}