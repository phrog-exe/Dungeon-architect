using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonArchitect
{
    class DungeonLogic
    {
        // 1. ENUM - Typy kafelków
        public enum TileType { Wall, Floor, Entrance, Exit }

        // 2.Pojedynczy kafelek
        public class Tile
        {
            public int X { get; set; }
            public int Y { get; set; }
            public TileType Type { get; set; } = TileType.Wall;
        }

        // 3. INTERFEJSY
        public interface IMapBuilder
        {
            void Reset(int width, int height);
            void GeneratePath(int steps);
            void PlaceSpecialTiles();
            Tile[,] GetMap();
        }

        public interface IRenderer
        {
            void Render(Tile[,] map);
        }

        // 4. Budowniczy 
        public class SimpleDungeonBuilder : IMapBuilder
        {
            private Tile[,] _map;
            private int _w, _h;
            private Random _rng = new Random();

            public void Reset(int width, int height)
            {
                _w = width; _h = height;
                _map = new Tile[width, height];
                for (int x = 0; x < width; x++)
                    for (int y = 0; y < height; y++)
                        _map[x, y] = new Tile { X = x, Y = y, Type = TileType.Wall };
            }

            public void GeneratePath(int steps)
            {
                // Zaczynamy na środku
                int curX = _w / 2;
                int curY = _h / 2;

                for (int i = 0; i < steps; i++)
                {
                    // zamiana ściany na podłogę
                    _map[curX, curY].Type = TileType.Floor;

                    // losowanie kierunku 0-góra, 1-dół, 2-lewo, 3-prawo
                    int dir = _rng.Next(4);
                    if (dir == 0 && curY > 1) curY--;
                    else if (dir == 1 && curY < _h - 2) curY++;
                    else if (dir == 2 && curX > 1) curX--;
                    else if (dir == 3 && curX < _w - 2) curX++;
                }
            }

            public void PlaceSpecialTiles()
            {
                
                bool entrancePlaced = false;
                Tile lastFloor = null;

                for (int x = 0; x < _w; x++)
                {
                    for (int y = 0; y < _h; y++)
                    {
                        if (_map[x, y].Type == TileType.Floor)
                        {
                            if (!entrancePlaced)
                            {
                                _map[x, y].Type = TileType.Entrance;
                                entrancePlaced = true;
                            }
                            lastFloor = _map[x, y];
                        }
                    }
                }

                if (lastFloor != null) lastFloor.Type = TileType.Exit;
            }

            public Tile[,] GetMap() => _map;
        }

        // 5. Builder
        public class MapDirector
        {
            private readonly IMapBuilder _builder;
            public MapDirector(IMapBuilder builder) => _builder = builder;

            public void Construct(int w, int h, int complexity)
            {
                _builder.Reset(w, h);
                _builder.GeneratePath(complexity);
                _builder.PlaceSpecialTiles();
            }
        }

        // 6. GUI settings
        public class GeneratorSettings
        {
            public int MapSize { get; set; } = 20;
            public int Complexity { get; set; } = 100;
        }
    }
}

