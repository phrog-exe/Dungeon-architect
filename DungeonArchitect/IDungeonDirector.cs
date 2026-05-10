using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonArchitect
{
    /// <summary>
    /// Interfejs dla dyrektora budowy lochów, definiujący proces konstrukcji.
    /// </summary>
    public interface IDungeonDirector
    {
        void Construct(int width, int height, int complexity);
    }
}
