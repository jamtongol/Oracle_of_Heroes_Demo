using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Oracle_of_Heroes.Scripts.Interfaces
{
    interface ISprite
    {
        string name
        {
            get;
            set;
        }

        int height
        {
            get;
            set;
        }

        int width
        {
            get;
            set;
        }

        bool isCollideable
        {
            get;
            set;
        }

        Texture2D texture
        {
            get;
            set;
        }

        void load(ContentManager contManager);
        void draw(SpriteBatch spriteBatch, Vector2 position, int scale);
    }
}
