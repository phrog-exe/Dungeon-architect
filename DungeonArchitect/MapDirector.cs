using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonArchitect
{
    public class MapDirector : IDungeonDirector
    {
        private readonly IMapBuilder _builder;
        public MapDirector(IMapBuilder builder) => _builder = builder;

        // Metoda do szybkiego generowania (bez kroków)
        public void Construct(int w, int h, int complexity)
        {
            _builder.Reset(w, h);
            // Wywołujemy, ale nie musimy iterować, jeśli nie chcemy kroków
            foreach (var step in _builder.GeneratePathSteps(complexity)) { }
            _builder.PlaceSpecialTiles();
        }
    }
}
