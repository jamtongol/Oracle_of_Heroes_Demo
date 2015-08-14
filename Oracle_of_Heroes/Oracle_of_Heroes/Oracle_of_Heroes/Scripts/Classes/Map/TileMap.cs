using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Oracle_of_Heroes.Scripts.Classes.Map
{
    /// <summary>
    /// The tile map contains the tiles in a map, the size of the map
    /// and its individual tiles, and the orientaton of the map
    /// </summary>
    class TileMap
    {
        // Fields
        private int _width;
        private int _height;
        private int _tileWidth;
        private int _tileHeight;
        private int _scale;
        private int[][] _mapSet;
        private string _mapData;
        private Dictionary<int, Tile> _tileSet;

        // Property Implementation
        public string mapData
        {
            get
            {
                return _mapData;
            }
            set
            {
                _mapData = value;
            }
        }

        // Constructor
        public TileMap(string mapData)
        {
            _mapData = mapData;
            _tileSet = new Dictionary<int,Tile>();
        }

        // Methods
        // load the map
        public void load(ContentManager contManager)
        {
            // We are going to read the data file and we expect it
            // to be formatted at a certain way
            string line;
            string[] fileBlocks, lineBlocks, arrayBlocks;
            char[] fileDelimeters = {';'};
            char[] lineDelimeters = {'='};
            char[] arrayDelimeters = { '[', ',', ']'};

            StreamReader stream = new StreamReader(_mapData);
            line = stream.ReadToEnd();
            stream.Close();
            fileBlocks = line.Split(fileDelimeters);
            
            // We split each of the sections and get the properties
            // of this map
            foreach (string element in fileBlocks)
            {
                lineBlocks = element.Split(lineDelimeters);
                if (String.Equals(lineBlocks[0].Trim(), "width"))
                {
                    _width = Int32.Parse(lineBlocks[1]);
                }
                else if (String.Equals(lineBlocks[0].Trim(), "height"))
                {
                    _height = Int32.Parse(lineBlocks[1]);
                }
                else if (String.Equals(lineBlocks[0].Trim(), "tileWidth"))
                {
                    _tileWidth = Int32.Parse(lineBlocks[1]);
                }
                else if (String.Equals(lineBlocks[0].Trim(), "tileHeight"))
                {
                    _tileHeight = Int32.Parse(lineBlocks[1]);
                }
                else if (String.Equals(lineBlocks[0].Trim(), "scale"))
                {
                    _scale = Int32.Parse(lineBlocks[1]);
                }
                else if (String.Equals(lineBlocks[0].Trim(), "types"))
                {
                    arrayBlocks = lineBlocks[1].Split(arrayDelimeters);
                    arrayBlocks = arrayBlocks.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                    loadTileSet(contManager, arrayBlocks);
                }
                else if (String.Equals(lineBlocks[0].Trim(), "map"))
                {
                    arrayBlocks = lineBlocks[1].Trim('\r', '\n').Split(arrayDelimeters);
                    arrayBlocks = arrayBlocks.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                    loadMapSet(arrayBlocks);
                }
            }
        }

        // load the map set
        public void loadMapSet(string[] blocks)
        {
            // given an array, go through the array and
            // load the orientation of the map
            int[] mapBlocks = Array.ConvertAll(blocks, s => Int32.Parse(s));
            this._mapSet = new int[this._width][];
            for (int i = 0; i < this._width; i++)
            {
                this._mapSet[i] = new Int32[this._height];
                Array.Copy(mapBlocks, i * this._height, this._mapSet[i], 0, this._height);
            }
        }

        // load the tile set
        public void loadTileSet(ContentManager contManager, string[] blocks)
        {
            // given an array, create tiles using the data from the array
            // a data is formatted as such:
            // [tileId, name, isCollideable, number of Frames,
            //      [[frame id, x, y, nextframeId, frameLength],
            //      .... ]
            // ....]
            Tile tile;
            int totalFrames;
            for (int i = 0; i + 4 < blocks.Length; i += 4)
            {
                tile = new Tile("Sprites/Map/" + blocks[i + 1].Trim().Trim('\"'), Convert.ToBoolean(blocks[i + 2]), Int32.Parse(blocks[i]));
                tile.width = this._tileWidth;
                tile.height = this._tileHeight;
                tile.load(contManager);
                totalFrames = Int32.Parse(blocks[i + 3]) + 1;
                for (int j = i + 4; j + 4 < totalFrames * 5 + i; j += 5)
                {
                    tile.addFrame(Int32.Parse(blocks[j + 1]), Int32.Parse(blocks[j + 2]), Int32.Parse(blocks[j]), Int32.Parse(blocks[j + 3]), float.Parse(blocks[j + 4]));
                }
          
                _tileSet.Add(Int32.Parse(blocks[i]), tile);
                i += 5 * (totalFrames - 1);
            }
        }

        // update the map
        public void update(GameTime gameTime)
        {
            foreach (KeyValuePair<int, Tile> tilePair in this._tileSet)
            {
                tilePair.Value.update(gameTime);
            }
        }

        // for each tile in the map, draw the tile
        public void draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < this._width; y++)
            {
                for (int x = 0; x < this._height; x++)
                {
                    this._tileSet[this._mapSet[y][x]].draw(spriteBatch, new Vector2(x * this._tileWidth * this._scale, y * this._tileHeight * this._scale), this._scale);

                }
            }
        }
    }
}
