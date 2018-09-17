using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insanity
{
    class Player
    {
        GraphicsDeviceManager graphics;//Used to created plain coloured texture

        Texture2D texture;//Player texture
        Texture2D hbG;//Health bar texture (green)
        Texture2D hbR;//Health bar texture (red)
        Texture2D arG;//Armour bar texture (light grey)
        Texture2D arR;//Armour bar texture (dark grey)

        Vector2 loc;//Location
        float rot;//Rotation
        float speed;//Movement speed
        int size = 50;//Player size
        int health = 100;//Starting health
        int armour = 0;//Starting armour
        Boolean alive;//If player is alive
        Boolean lvlFin;//If player has finished the level
        int barW=200;//Width of the health/armour bar
        int barH = 45;//Hieght of the health/armour bar

        //Constructor
        //Sets default values and creates textures for the health and armour bars
        public Player(Texture2D newTexture,float newSpeed,Vector2 location)
        {
            texture = newTexture;
            speed = newSpeed;
            SetLoc(location);
            alive = true;
            lvlFin = false;

            graphics = Insanity.Game.GetGraphics();
            hbG = new Texture2D(graphics.GraphicsDevice, barW, barH);
            hbR = new Texture2D(graphics.GraphicsDevice, barW, barH);
            arG = new Texture2D(graphics.GraphicsDevice, barW, barH);
            arR = new Texture2D(graphics.GraphicsDevice, barW, barH);
            Color[] dataG = new Color[barW * barH];
            Color[] dataR = new Color[barW * barH];
            Color[] dataAG = new Color[barW * barH];
            Color[] dataAR = new Color[barW * barH];
            for (int i = 0; i < dataG.Length; i++)
            {
                dataG[i] = Color.Green;
                dataR[i] = Color.Red;
                dataAG[i] = Color.Gray;
                dataAR[i] = Color.DimGray;
            }
            hbG.SetData(dataG);
            hbR.SetData(dataR);
            arG.SetData(dataAG);
            arR.SetData(dataAR);
        }

        //Draws the player, health bar and armour bar
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)loc.X, (int)loc.Y, size, size), null, Color.White, (float)rot + 1.5f, new Vector2(texture.Width / 2, texture.Height / 2), 0, 0);

            //draw health
            spriteBatch.Draw(hbR, new Rectangle(300, 10, barW, barH), null, Color.White, 0, new Vector2(0, 0), 0, 0);
            float newSize = (float)barW * ((float)health / 100);
            spriteBatch.Draw(hbG, new Rectangle(300, 10, (int)newSize, barH), null, Color.White, 0, new Vector2(0, 0), 0, 0);

            //draw armour
            spriteBatch.Draw(arR, new Rectangle(700, 10, barW, barH), null, Color.White, 0, new Vector2(0, 0), 0, 0);
            newSize = (float)barW * ((float)armour / 100);
            spriteBatch.Draw(arG, new Rectangle(700, 10, (int)newSize, barH), null, Color.White, 0, new Vector2(0, 0), 0, 0);
        }
        
        //Returns location
        public Vector2 GetLoc()
        {
            return loc; 
        }

        //Sets new location to newLoc
        public void SetLoc(Vector2 newLoc)
        {
            loc.X = newLoc.X * 60 + 30;
            loc.Y = newLoc.Y * 60 + 30;
        }

        //Moves the player along the X axis if nothing is blocking
        public void MoveX(float mvAmt,List<Obstacle> objs)
        {
            if (!CheckCollisionWalls('h', mvAmt, objs))
            {
                loc.X += mvAmt;
            }
        }

        //Moves the player along the Y axis if nothing is blocking
        public void MoveY(float mvAmt, List<Obstacle> objs)
        {
            if (!CheckCollisionWalls('v', mvAmt, objs))
            {
                loc.Y += mvAmt;
            }
        }

        //Returns rotation
        public float GetRot()
        {
            return rot;
        }

        //Sets rotation to nRot
        public void SetRot(float nRot)
        {
            rot = nRot;
        }

        //Returns player size
        public int GetSize()
        {
            return size;
        }

        //Returns player movement speed
        public float GetSpeed()
        {
            return speed;
        }

        //Adds amt to the the players health with validation
        public void AddHealth(int amt)
        {
            health += amt;
            if (health > 100)
            {
                health = 100;
            }
        }

        //Adds amt to the the players armour with validation
        public void AddArmour(int amt)
        {
            armour += amt;
            if (armour > 100)
            {
                armour = 100;
            }
        }

        //Reduces armour first then health once armour is depleted by amt
        public void ReduceLife(int amt)
        {
            if (armour <= 0)
            {
                health -= amt;
            }
            else if (armour - amt >= 0)
            {
                armour -= amt;
            }
            else if (armour - amt < 0)
            {
                int newAmt = amt + armour;
                armour = 0;
                health -= newAmt;
            }
            if(health<=0)
            {
                alive = false;
            }
        }

        //Changes the value of alive to t
        public void ChangeAlive(Boolean t)
        {
            alive = t;
        }

        //Checks if player is alive
        public Boolean IsAlive()
        {
            return alive;
        }

        //Chagnes the value of lvlFin to t
        public void ChangeLvlFin(Boolean t)
        {
            lvlFin = t;
        }

        //Checks if level is finished
        public Boolean FinishLvl()
        {
            return lvlFin;
        }

        //Collision detection for the player again all of the obastacles in the level
        public bool CheckCollisionWalls(char ori,float dir, List<Obstacle> objs)
        {
            Rectangle pRect = new Rectangle((int)loc.X, (int)loc.Y , size, size);
            if (ori == 'h')
            {
                pRect.X += ((int)dir);
            }
        
            if (ori == 'v')
            {
                pRect.Y += ((int)dir);
            }
        
            foreach (Obstacle w in objs)
            {
                for (int i = 0; i < w.GetWid(); i++)
                {
                    for (int j = 0; j < w.GetHei(); j++)
                    {
                        Rectangle wRect = new Rectangle((int)(w.GetLoc().X* w.GetSize()) + (i * w.GetSize())+(w.GetSize()/2), (int)(w.GetLoc().Y*w.GetSize()) + (j * w.GetSize()) + (w.GetSize() / 2), w.GetSize(), w.GetSize());
                        if (pRect.Intersects(wRect))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
