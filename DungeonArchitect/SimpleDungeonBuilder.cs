using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonArchitect
{
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

        public IEnumerable<Tile> GeneratePathSteps(int steps)
        {
            int curX = _w / 2;
            int curY = _h / 2;

            for (int i = 0; i < steps; i++)
            {
                if (_map[curX, curY].Type == TileType.Wall)
                {
                    _map[curX, curY].Type = TileType.Floor;
                    yield return _map[curX, curY];
                }

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
                        if (!entrancePlaced) { _map[x, y].Type = TileType.Entrance; entrancePlaced = true; }
                        lastFloor = _map[x, y];
                    }
                }
            }
            if (lastFloor != null) lastFloor.Type = TileType.Exit;
        }

        public Tile[,] GetMap() => _map;
    }


}

