using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Level
    {

        MouseState mouseState;
        KeyboardState newState;
        MouseState oldMState;

        List<Obstacle> walls = new List<Obstacle>();
        int OWT = 30;

        List<Bullet> bullets = new List<Bullet>();
        List<Enemy> enemies = new List<Enemy>();
        int enemyCount = 1;
        int enemyLvl = 1;


        Player player;
        Texture2D playerTex;

        Level()
        {

        }

        void Update()
        {
            mouseState = Mouse.GetState();
            newState = Keyboard.GetState();
            player.Update(gameTime, newState, mouseState, walls);
            UpdateEnemies(gameTime, newState, mouseState, walls, player.GetLoc());
        }

        void Draw()
        {

        }
    }
}
