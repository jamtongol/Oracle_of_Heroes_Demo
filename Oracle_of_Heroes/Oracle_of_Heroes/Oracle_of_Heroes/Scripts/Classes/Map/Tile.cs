using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Oracle_of_Heroes.Scripts.Classes.Map
{
    /// <summary>
    /// A tile in a map is a type of Sprite
    /// </summary>
    class Tile : Oracle_of_Heroes.Scripts.Classes.Sprite.Sprite
    {
        // The tileId
        private int _tileId;
        public int tileId
        {
            get
            {
                return this._tileId;
            }
            set
            {
                this._tileId = value;
            }
        }
        public Tile(string name, bool isCollideable, int id)
            : base(name, isCollideable)
        {
            this._tileId = id;
        }
    }
}
