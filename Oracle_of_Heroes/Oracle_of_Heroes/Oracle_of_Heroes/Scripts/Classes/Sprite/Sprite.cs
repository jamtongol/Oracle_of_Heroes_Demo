using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Oracle_of_Heroes.Scripts.Classes.Sprite
{
    /// <summary>
    /// A Struct that holds data regrading a Frame such as its:
    ///     x and y positions
    ///     frame Id
    ///     the next frame Id
    ///     time before the next frame is loaded
    /// A frame is used to animate a sprite
    /// </summary>
    public struct Frame
    {
        public int x, y, frameId, nextFrameId;
        public float frameLength;

        public Frame(int x, int y, int frameId, int nextFrameId, float frameLength)
        {
            this.x = x;
            this.y = y;
            this.frameId = frameId;
            this.nextFrameId = nextFrameId;
            this.frameLength = frameLength;
        }
    }

    /// <summary>
    /// A sprite
    /// </summary>
    class Sprite : Oracle_of_Heroes.Scripts.Interfaces.ISprite
    {
        // Fields
        private string _name;
        private int _width;
        private int _height;
        private int _currentFrame;
        private Dictionary<int, Frame> _frames;
        private bool _isCollideable;
        private Texture2D _texture;
        private float frameTimer;

        // Property Implementation
        public string name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        public int width
        {
            get
            {
                return this._width;
            }
            set
            {
                this._width = value;
            }
        }

        public int height
        {
            get
            {
                return this._height;
            }
            set
            {
                this._height = value;
            }
        }

        public bool isCollideable
        {
            get
            {
                return this._isCollideable;
            }
            set
            {
                this._isCollideable = value;
            }
        }

        public Texture2D texture
        {
            get
            {
                return this._texture;
            }
            set
            {
                this._texture = value;
            }
        }

        // Constructors
        public Sprite(string name, bool isCollideable)
        {
            this._name = name;
            this._isCollideable = isCollideable;
            this._frames = new Dictionary<int,Frame>();
            this.frameTimer = 0.0f;
        }

        // Methods
        // Add a frame to the sprite
        // A frame is used for animating the sprite
        public void addFrame(int x, int y, int frameId, int nextFrameId, float frameLength)
        {
            this._frames.Add(frameId, new Frame(x, y, frameId, nextFrameId, frameLength));
        }

        // get the current frame
        public Frame getCurrentFrame()
        {
            return this._frames[_currentFrame];
        }

        // get the next frame
        public Frame getNextFrame()
        {
            return this._frames[(this.getCurrentFrame().nextFrameId)];
        }

        // load the image
        public void load(ContentManager contManager)
        {
            this._texture = contManager.Load<Texture2D>(_name);
        }

        // update the sprite
        public void update(GameTime gametime)
        {
            this.frameTimer += (float)gametime.ElapsedGameTime.TotalSeconds;
            if (this.frameTimer > this.getCurrentFrame().frameLength)
            {
                this._currentFrame = this.getCurrentFrame().nextFrameId;
                this.frameTimer = 0.0f;
            }
        }

        // draw the sprite in the spriteBatch at the position and scale it
        public void draw(SpriteBatch spriteBatch, Vector2 position, int scale)
        {
            spriteBatch.Draw(texture, position, new Rectangle(this.getCurrentFrame().x, this.getCurrentFrame().y, this.width, this.height), Color.White, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 1);
        }
    }
}
