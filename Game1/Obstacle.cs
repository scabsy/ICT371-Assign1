using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insanity
{
    class Obstacle
    {
        Texture2D texture;//Obstacle texture
        Vector2 loc;//Location
        int width;//Number of tiles wide
        int height;//Number of tiles high
        int size = 60;//Width and height of individual tile

        //Constructor
        public Obstacle(Texture2D newTexture, Vector2 newloc, int w, int h)
        {
            loc = newloc;
            width = w;
            height = h;
            texture = newTexture;
        }

        //Draw the obstacle as a grid that is width wide and height high using the same texture
        public void Draw(SpriteBatch spriteBatch)
        {
            for(int i=0;i<width;i++)
            {
                for(int j=0;j < height;j++)
                {
                    spriteBatch.Draw(texture, new Rectangle((int)(loc.X*size) + (i * size), (int)(loc.Y*size) + (j * size) , size, size), Color.White);
                }
            }
        }

        //Get starting location
        public Vector2 GetLoc()
        {
            return loc;
        }

        //Get the width
        public int GetWid()
        {
            return width;
        }

        //Get the height
        public int GetHei()
        {
            return height;
        }

        //Get the tile size
        public int GetSize()
        {
            return size;
        }
    }
}
