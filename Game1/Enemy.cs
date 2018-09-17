using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Insanity
{
    class Enemy
    {
        GraphicsDeviceManager graphics;
        Random rnd = new Random();

        Timer timer;//Timer to determine when the enemy can shoot
        
        Vector2 loc;//Location
        float rot;//Rotation
        static int size = 50;//Size of the enemy
        Boolean canShoot;//If enemy can shoot
        int shootSpeed;//Time between shots fired
        int health;//Health

        Texture2D texture;//Enemy texture
        Texture2D hbG;//Health bar texture (green)
        Texture2D hbR;//Health bar texture (red)

        bool isVisible;//Enemy visibility

        //Constructor
        //Sets default values and creates the textures required for health bar
        public Enemy(Texture2D newTexture,int shootInt)
        {
            texture = newTexture;
            isVisible = false;
            canShoot = false;
            shootSpeed = shootInt;
            health = 100;

            timer = new Timer(shootSpeed);
            timer.Elapsed += new ElapsedEventHandler(goToShoot);
            timer.AutoReset = true;
            timer.Start();

            graphics = Insanity.Game.GetGraphics();
            hbG = new Texture2D(graphics.GraphicsDevice, size, size / 5);
            hbR = new Texture2D(graphics.GraphicsDevice, size, size / 5);
            Color[] dataG = new Color[size * (size / 5)];
            Color[] dataR = new Color[size * (size / 5)];
            for (int i=0;i< dataG.Length;i++)
            {
                dataG[i] = Color.Green;
                dataR[i] = Color.Red;
            }
            hbG.SetData(dataG);
            hbR.SetData(dataR);
        }

        //Used with the timer. Changes canShoot so enemy is able to shoot again
        private void goToShoot(object sender,EventArgs e)
        {
            canShoot = true;
        }

        //Returns canShoot
        public Boolean CanShoot()
        {
            return canShoot;
        }

        //Sets canShoot to t
        public void SetShoot(Boolean t)
        {
            canShoot = t;
        }

        //Draws the enemy and their respective health bar
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)loc.X, (int)loc.Y, size, size), null, Color.White, (float)rot + 1.5f, new Vector2(texture.Width / 2, texture.Height / 2), 0, 0);
            //healthbar
            spriteBatch.Draw(hbR, new Rectangle((int)loc.X - size / 2, (int)loc.Y - size, size, size/5), null, Color.White, 0, new Vector2(0, 0), 0, 0);
            float newSize = (float)size * ((float)health / 100);
            spriteBatch.Draw(hbG, new Rectangle((int)loc.X-size/2, (int)loc.Y - size, (int)newSize, size / 5), null, Color.White, 0, new Vector2(0, 0), 0, 0);
        }

        //Updates any aspects of the enemy
        public void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState, List<Obstacle> walls, Vector2 playerLoc)
        {
            UpdatePos(gameTime, keyboardState, mouseState, walls, playerLoc);
        }

        //Updates the enemy to look at the player
        public void UpdatePos(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState, List<Obstacle> walls,Vector2 playerLoc)
        {
            rot = (float)(Math.Atan2(playerLoc.Y - loc.Y, playerLoc.X - loc.X));
        }

        //Returns the texture
        public Texture2D GetTex()
        {
            return texture;
        }

        //Returns the location
        public Vector2 GetLoc()
        {
            return loc;
        }

        //Sets the locations to newPos
        public void SetPos(Vector2 newPos)
        {
            loc.X = newPos.X * 60+30;
            loc.Y = newPos.Y * 60+30;
        }

        //Returns enemy rotation
        public float GetRot()
        {
            return rot;
        }

        //Returns enemy visbility
        public bool GetVis()
        {
            return isVisible;
        }

        //Set the visibility to newVis
        public void SetVis(bool newVis)
        {
            isVisible = newVis;
        }

        //Return size
        public int GetSize()
        {
            return size;
        }

        //Set size to newSize
        public void SetSize(int newSize)
        {
            size = newSize;
        }
        
        //Reduce health by amt
        public void ReduceHealth(int amt)
        {
            health -= amt;
            if(health<=0)
            {
                SetVis(false);
            }
        }

        //Return health
        public int GetHealth()
        {
            return health;
        }
    }
}
