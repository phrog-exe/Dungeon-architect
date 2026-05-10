using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonArchitect
{

    public interface IMapBuilder
    {
        void Reset(int width, int height);
        IEnumerable<Tile> GeneratePathSteps(int steps);
        void PlaceSpecialTiles();
        Tile[,] GetMap();
    }
}

