using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insanity
{
    class Level1
    {
        MouseState mouseState;
        KeyboardState newState;
        MouseState oldMState;
        GamePadState gpState;
        GamePadState OgpState;

        Random rnd = new Random();

        Player player;

        private Texture2D playerTex;
        private Texture2D floor;
        private Texture2D wall;
        private Texture2D bullet;
        private List<Texture2D> people;
        private Texture2D crosshair;
        private Texture2D UIhealth;
        private Texture2D UIarmour;
        private Texture2D PUhealth;
        private Texture2D PUarmour;
        private Texture2D lvlGoal;

        private SoundEffect background;
        private SoundEffect laser;
        private SoundEffect scream;

        SoundEffectInstance bgIns;
        SoundEffectInstance laserIns;
        SoundEffectInstance screamIns;

        int LevelNum = 1;

        Vector2 chLoc = new Vector2(660, 420);

        List<Obstacle> walls = new List<Obstacle>();
        List<Pickup> pickups = new List<Pickup>();
        List<Bullet> bullets = new List<Bullet>();
        List<Enemy> enemies = new List<Enemy>();
        ContentManager Content;

        public Level1(ContentManager acontent)
        {
            Content = acontent;

            playerTex = Content.Load<Texture2D>("person3");
            floor = Content.Load<Texture2D>("grass_close_05_D");
            wall = Content.Load<Texture2D>("sand_close_01_D");
            bullet = Content.Load<Texture2D>("Bullet");

            people = new List<Texture2D>();
            people.Add(Content.Load<Texture2D>("person1"));
            people.Add(Content.Load<Texture2D>("person2"));
            people.Add(Content.Load<Texture2D>("person3"));

            //Load UI images
            crosshair = Content.Load<Texture2D>("crosshair");
            UIhealth = Content.Load<Texture2D>("health");
            UIarmour = Content.Load<Texture2D>("armour");

            //Load pick-up images
            PUhealth = Content.Load<Texture2D>("healthPU");
            PUarmour = Content.Load<Texture2D>("armourPU");
            lvlGoal = Content.Load<Texture2D>("missionMarker");

            background = Content.Load<SoundEffect>("bgSound");
            bgIns = background.CreateInstance();
            bgIns.IsLooped = true;
            bgIns.Volume = 0.3f;
            bgIns.Play();

            laser = Content.Load<SoundEffect>("laser");
            laserIns = laser.CreateInstance();
            laserIns.Volume = 0.01f;

            scream = Content.Load<SoundEffect>("scream");
            screamIns = scream.CreateInstance();
            screamIns.Volume = 0.1f;

            BuildLevel(LevelNum,true);
        }

        public void BuildLevel(int lvlNum,Boolean dead)
        {            
            walls = new List<Obstacle>();
            pickups = new List<Pickup>();
            bullets = new List<Bullet>();
            enemies = new List<Enemy>();
            if(LevelNum==0)
            {

            }
            else if (LevelNum == 1)
            {
                if(dead)
                {
                    player = new Player(playerTex, 5, new Vector2(1.5f, 13));
                }
                else
                {
                    player.SetLoc(new Vector2(1.5f, 13));
                }

                //WALLS - place x=0-22 y=0-12
                //OUTER WALLS
                CreateWall(new Vector2(0, 0), 22, 1);//top
                CreateWall(new Vector2(3, 13), 20, 1);//bottom
                CreateWall(new Vector2(1, 14), 2, 1);//blocker
                CreateWall(new Vector2(0, 0), 1, 14);//left
                CreateWall(new Vector2(21, 0), 1, 11);//right
                CreateWall(new Vector2(22, 11), 1, 2);//right

                //INNER WALLS
                //Horizontal
                CreateWall(new Vector2(1, 4), 4, 1);
                CreateWall(new Vector2(4, 1), 1, 1);
                CreateWall(new Vector2(4, 3), 1, 1);
                CreateWall(new Vector2(1, 7), 7, 1);
                CreateWall(new Vector2(7, 8), 1, 1);
                CreateWall(new Vector2(7, 4), 7, 1);
                CreateWall(new Vector2(13, 1), 1, 1);
                CreateWall(new Vector2(13, 3), 1, 1);
                CreateWall(new Vector2(9, 11), 4, 1);
                CreateWall(new Vector2(9, 12), 1, 1);
                CreateWall(new Vector2(13, 6), 6, 1);
                //Vertical
                CreateWall(new Vector2(7, 10), 1, 3);
                CreateWall(new Vector2(7, 1), 1, 2);
                CreateWall(new Vector2(12, 6), 1, 5);
                CreateWall(new Vector2(16, 9), 1, 4);

                //Enemies
                CreateEnemy(1, 3, rnd.Next(800, 2500));
                CreateEnemy(8, 1, rnd.Next(800, 2500));
                CreateEnemy(12, 3, rnd.Next(800, 2500));
                CreateEnemy(2, 5, rnd.Next(800, 2500));
                CreateEnemy(16, 3, rnd.Next(800, 2500));
                CreateEnemy(18, 3, rnd.Next(800, 2500));
                CreateEnemy(10, 8, rnd.Next(800, 2500));
                CreateEnemy(13, 8, rnd.Next(800, 2500));
                CreateEnemy(14, 11, rnd.Next(800, 2500));
                CreateEnemy(17, 11, rnd.Next(800, 2500));
                CreateEnemy(20, 9, rnd.Next(800, 2500));

                //PICKUPS
                //Health
                pickups.Add(new Pickup(PUhealth, "health", new Vector2(10, 2)));
                //Armour
                pickups.Add(new Pickup(PUarmour, "armour", new Vector2(18, 1)));
                pickups.Add(new Pickup(PUarmour, "armour", new Vector2(12, 12)));
                //End marker
                pickups.Add(new Pickup(lvlGoal, "finish", new Vector2(22, 11.5f)));
            }
            else if (LevelNum == 2)
            {
                if (dead)
                {
                    player = new Player(playerTex, 5, new Vector2(0, 11.5f));
                }
                else
                {
                    player.SetLoc(new Vector2(0, 11.5f));
                }

                //WALLS - place x=0-22 y=0-12
                //OUTER WALLS
                CreateWall(new Vector2(0, 0), 22, 1);//top
                CreateWall(new Vector2(0, 13), 9, 1);//bottom
                CreateWall(new Vector2(11, 13), 11, 1);//bottom
                CreateWall(new Vector2(0, 0), 1, 11);//left
                CreateWall(new Vector2(-1, 11), 1, 2);//blocker
                CreateWall(new Vector2(21, 0), 1, 13);//right

                //INNER WALLS
                //Horizontal
                CreateWall(new Vector2(3, 3), 5, 1);
                CreateWall(new Vector2(9, 3), 7, 1);
                CreateWall(new Vector2(17, 3), 2, 1);
                CreateWall(new Vector2(7, 6), 9, 1);
                CreateWall(new Vector2(3, 7), 5, 1);
                CreateWall(new Vector2(15, 8), 4, 1);
                CreateWall(new Vector2(3,10), 4, 1);
                CreateWall(new Vector2(8,10), 5, 1);
                CreateWall(new Vector2(14,10), 5, 1);
                //Vertical
                CreateWall(new Vector2(3, 3), 1, 3);
                CreateWall(new Vector2(3, 7), 1, 6);
                CreateWall(new Vector2(7, 3), 1, 5);
                CreateWall(new Vector2(11, 6), 1, 5);
                CreateWall(new Vector2(18, 3), 1, 8);

                //Enemies
                CreateEnemy(4, 1, rnd.Next(800, 2500));
                CreateEnemy(5, 5, rnd.Next(800, 2500));
                CreateEnemy(6, 9, rnd.Next(800, 2500));
                CreateEnemy(9, 8, rnd.Next(800, 2500));
                CreateEnemy(11, 5, rnd.Next(800, 2500));
                CreateEnemy(17, 6, rnd.Next(800, 2500));
                CreateEnemy(13, 8, rnd.Next(800, 2500));
                CreateEnemy(16, 9, rnd.Next(800, 2500));
                CreateEnemy(4, 12, rnd.Next(800, 2500));
                CreateEnemy(16, 11, rnd.Next(800, 2500));
                CreateEnemy(20, 8, rnd.Next(800, 2500));

                //PICKUPS
                //Health 
                pickups.Add(new Pickup(PUhealth, "health", new Vector2(4, 8)));
                pickups.Add(new Pickup(PUhealth, "health", new Vector2(12, 4)));
                //Armour
                pickups.Add(new Pickup(PUarmour, "armour", new Vector2(6, 6)));
                pickups.Add(new Pickup(PUarmour, "armour", new Vector2(10, 9)));
                pickups.Add(new Pickup(PUarmour, "armour", new Vector2(17, 9)));
                //End marker
                pickups.Add(new Pickup(lvlGoal, "finish", new Vector2(9.5f, 14)));
            }
            else if (LevelNum == 3)
            {
                if (dead)
                {
                    player = new Player(playerTex, 5, new Vector2(9.5f, 0));
                }
                else
                {
                    player.SetLoc(new Vector2(9.5f, 0));
                }

                //WALLS - place x=0-22 y=0-12
                //OUTER WALLS
                CreateWall(new Vector2(0, 0), 9, 1);//top
                CreateWall(new Vector2(11, 0), 11, 1);//top
                CreateWall(new Vector2(9, -1), 2, 1);//blocker
                CreateWall(new Vector2(0, 13), 22, 1);//bottom
                CreateWall(new Vector2(0, 0), 1, 4);//left
                CreateWall(new Vector2(0, 6), 1, 7);//left
                CreateWall(new Vector2(21, 0), 1, 13);//right

                //INNER WALLS
                //Horizontal
                CreateWall(new Vector2(1, 3), 11, 1);
                CreateWall(new Vector2(14, 3), 5, 1);
                CreateWall(new Vector2(1, 6), 4, 1);
                CreateWall(new Vector2(14, 6), 3, 1);
                CreateWall(new Vector2(16, 7), 3, 1);
                CreateWall(new Vector2(7, 8), 3, 1);
                CreateWall(new Vector2(16, 9), 3, 1);
                CreateWall(new Vector2(3, 10), 2, 1);
                CreateWall(new Vector2(14, 10), 3, 1);
                CreateWall(new Vector2(7, 11), 3, 1);
                //Vertical
                CreateWall(new Vector2(3, 1), 1, 1);
                CreateWall(new Vector2(7, 2), 1, 1);
                CreateWall(new Vector2(4, 6), 1, 5);
                CreateWall(new Vector2(7, 6), 1, 3);
                CreateWall(new Vector2(7, 11), 1, 2);
                CreateWall(new Vector2(9, 8), 1, 4);
                CreateWall(new Vector2(11, 3), 1, 5);
                CreateWall(new Vector2(14, 6), 1, 5);
                CreateWall(new Vector2(18, 3), 1, 5);

                //Enemies
                CreateEnemy(5, 2, rnd.Next(800, 2500));
                CreateEnemy(18, 1, rnd.Next(800, 2500));
                CreateEnemy(9, 5, rnd.Next(800, 2500));
                CreateEnemy(14, 5, rnd.Next(800, 2500));
                CreateEnemy(1, 8, rnd.Next(800, 2500));
                CreateEnemy(15, 8, rnd.Next(800, 2500));
                CreateEnemy(8, 9, rnd.Next(800, 2500));
                CreateEnemy(13, 9, rnd.Next(800, 2500));
                CreateEnemy(8, 10, rnd.Next(800, 2500));
                CreateEnemy(11, 11, rnd.Next(800, 2500));
                CreateEnemy(18, 11, rnd.Next(800, 2500));

                //PICKUPS
                //Health
                pickups.Add(new Pickup(PUhealth, "health", new Vector2(1, 2)));
                pickups.Add(new Pickup(PUhealth, "health", new Vector2(3, 8)));
                pickups.Add(new Pickup(PUhealth, "health", new Vector2(17, 6)));
                //Armour
                pickups.Add(new Pickup(PUarmour, "armour", new Vector2(15, 7)));
                pickups.Add(new Pickup(PUarmour, "armour", new Vector2(15, 9)));
                //End marker
                pickups.Add(new Pickup(lvlGoal, "finish", new Vector2(-1, 4.5f)));
            }
            else if (LevelNum == 4)
            {
                if (dead)
                {
                    player = new Player(playerTex, 5, new Vector2(21, 4.5f));
                }
                else
                {
                    player.SetLoc(new Vector2(21, 4.5f));
                }

                //WALLS - place x=0-22 y=0-12
                //OUTER WALLS
                CreateWall(new Vector2(3, 0), 19, 1);//top
                CreateWall(new Vector2(0, 13), 22, 1);//bottom
                CreateWall(new Vector2(0, 0), 1, 13);//left
                CreateWall(new Vector2(21, 0), 1, 4);//right
                CreateWall(new Vector2(21, 6), 1, 7);//right
                CreateWall(new Vector2(22, 4), 1, 2);//blocker
                //Horizontal
                CreateWall(new Vector2(3, 6), 10, 2);
                CreateWall(new Vector2(15, 5), 4, 1);
                CreateWall(new Vector2(15, 8), 4, 1);
                //Vertical
                CreateWall(new Vector2(6, 2), 1, 10);
                CreateWall(new Vector2(15, 2), 1, 4);
                CreateWall(new Vector2(15, 8), 1, 4);
                
                //Enemies
                CreateEnemy(2, 3, rnd.Next(800, 2500));
                CreateEnemy(2, 10, rnd.Next(800, 2500));
                CreateEnemy(9, 2, rnd.Next(800, 2500));
                CreateEnemy(9, 4, rnd.Next(800, 2500));
                CreateEnemy(9, 9, rnd.Next(800, 2500));
                CreateEnemy(9, 11, rnd.Next(800, 2500));
                CreateEnemy(17, 2, rnd.Next(800, 2500));
                CreateEnemy(17, 11, rnd.Next(800, 2500));

                //PICKUPS
                //Health
                pickups.Add(new Pickup(PUhealth, "health", new Vector2(4, 4)));
                pickups.Add(new Pickup(PUhealth, "health", new Vector2(17, 10)));
                //Armour
                pickups.Add(new Pickup(PUarmour, "armour", new Vector2(4, 9)));
                pickups.Add(new Pickup(PUarmour, "armour", new Vector2(17, 3)));
                //End marker
                pickups.Add(new Pickup(lvlGoal, "finish", new Vector2(1.5f, -1f)));
            }
        }

        public void ResetLevel(Boolean dead)
        {
            walls = null;
            pickups = null;
            bullets = null;
            enemies = null;
            if(!dead)
            {
                if (LevelNum < 4)
                    LevelNum++;
                else
                    LevelNum = 1;
            }
            else
            {
                player = null;
            }

            BuildLevel(LevelNum,dead);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            for (int i = 0; i < 22; i++)
            { 
                for (int j = 0; j < 14; j++)
                {
                    spriteBatch.Draw(floor, new Rectangle(i * 60+30, j * 60+30, 60, 60), null, Color.White, 0, new Vector2(floor.Width / 2, floor.Height / 2), 0, 0);
                }
            }
            foreach (Bullet b in bullets)
            {
                b.Draw(spriteBatch);
            }


            foreach (Obstacle e in walls)
            {
                e.Draw(spriteBatch);
            }

            foreach (Enemy e in enemies)
            {
                e.Draw(spriteBatch);
            }

            foreach (Pickup p in pickups)
            {
                p.Draw(spriteBatch);
            }
            player.Draw(spriteBatch);
            if(!player.IsAlive())
            {
                ResetLevel(true);
            }
            if(player.FinishLvl())
            {
                player.ChangeLvlFin(false);
                ResetLevel(false);
            }
            spriteBatch.Draw(UIhealth, new Rectangle(250,10, 45,45), null, Color.White, 0, new Vector2(0, 0), 0, 0);
            spriteBatch.Draw(UIarmour, new Rectangle(650, 10, 45, 45), null, Color.White, 0, new Vector2(0, 0), 0, 0);

            spriteBatch.Draw(crosshair, chLoc, null, Color.White, 0f, new Vector2(crosshair.Width / 2, crosshair.Height / 2), 0.5f, 0, 0);
            
        }

        public void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            newState = Keyboard.GetState();
            gpState = GamePad.GetState(PlayerIndex.One);
            UpdateEnemies(gameTime, newState, mouseState, walls, player.GetLoc());

            UpdateBullets();

            if(!gpState.IsConnected)
            {

                if (newState.IsKeyDown(Keys.A))
                {
                    player.MoveX(-player.GetSpeed(),walls);
                }
                if (newState.IsKeyDown(Keys.D))
                {
                    player.MoveX(player.GetSpeed(), walls);
                }
                if (newState.IsKeyDown(Keys.W))
                {
                    player.MoveY(-player.GetSpeed(), walls);
                }
                if (newState.IsKeyDown(Keys.S))
                {
                    player.MoveY(player.GetSpeed(), walls);
                }
                player.SetRot((float)(Math.Atan2(mouseState.Y - player.GetLoc().Y, mouseState.X - player.GetLoc().X)));

                chLoc.X = mouseState.X;
                chLoc.Y = mouseState.Y;

                if (mouseState.LeftButton == ButtonState.Pressed && oldMState.LeftButton == ButtonState.Released)
                {
                    Shoot(player.GetLoc(), player.GetRot(), "player");
                }
                oldMState = mouseState;
            }
            else
            {
                player.MoveX(gpState.ThumbSticks.Left.X * player.GetSpeed(),walls);
                player.MoveY(-gpState.ThumbSticks.Left.Y * player.GetSpeed(),walls);

                if(chLoc.X<1320 || chLoc.X > 0)
                {
                    chLoc.X += gpState.ThumbSticks.Right.X * 20;
                }
                if (chLoc.Y < 840 || chLoc.Y > 0)
                {
                    chLoc.Y -= gpState.ThumbSticks.Right.Y *20;
                }

                if(gpState.Triggers.Right > 0 && OgpState.Triggers.Right == 0)
                {
                    Shoot(player.GetLoc(), player.GetRot(), "player");
                }

                player.SetRot((float)(Math.Atan2(chLoc.Y - player.GetLoc().Y, chLoc.X - player.GetLoc().X)));

                OgpState = gpState;
            }

            for(int i=0;i<pickups.Count;i++)
            {
                if(pickups[i].CheckCollision(player))
                {
                    pickups[i].PickedUp(player);
                }
                if(!pickups[i].GetVis())
                {
                    pickups.RemoveAt(i);
                    i--;
                }
            }

            
        }

        public void UpdateBullets()
        {
            foreach (Bullet b in bullets)
            {
                b.UpdatePos();
                CheckCollisionBullets(b);
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].GetVis())
                {
                    bullets.RemoveAt(i);
                    i--;
                }
            }
        }

        void Shoot(Vector2 ori,float rot,string btype)
        {
            Bullet newBullet = new Bullet(Content.Load<Texture2D>("Bullet"));
            newBullet.SetVel(new Vector2((float)Math.Cos(rot), (float)Math.Sin(rot)) * 12f);
            newBullet.SetPos(ori + newBullet.GetVel() * 5);
            newBullet.SetBType(btype);
            newBullet.SetRot(rot);
            laserIns.Play();

            if (bullets.Count < 20)
            {
                bullets.Add(newBullet);
            }
        }

        void UpdateEnemies(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState, List<Obstacle> walls, Vector2 playerLoc)
        {
            foreach (Enemy e in enemies)
            {
                e.UpdatePos(gameTime, newState, mouseState, walls, player.GetLoc());
                if (e.CanShoot())
                {
                    Shoot(e.GetLoc(),e.GetRot(),"enemy");
                    e.SetShoot(false);
                }
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                if (!enemies[i].GetVis())
                {
                    screamIns.Play();
                    enemies.RemoveAt(i);
                    i--;
                }
            }
        }

        void CheckCollisionBullets(Bullet b)
        {
            Rectangle bRect = new Rectangle((int)b.GetPos().X, (int)b.GetPos().Y, b.texture.Width / 5, b.texture.Height / 5);

            foreach (Obstacle w in walls)
            {
                for (int i = 0; i < w.GetWid(); i++)
                {
                    for (int j = 0; j < w.GetHei(); j++)
                    {
                        Rectangle wRect = new Rectangle((int)(w.GetLoc().X * w.GetSize()) + (i * w.GetSize()), (int)(w.GetLoc().Y * w.GetSize()) + (j * w.GetSize()), w.GetSize(), w.GetSize());
                        if (bRect.Intersects(wRect))
                        {
                            b.SetVis(false);
                        }
                    }
                }
            }
            foreach (Enemy e in enemies)
            {
                Rectangle eRect = new Rectangle((int)e.GetLoc().X, (int)e.GetLoc().Y, e.GetSize(), e.GetSize());
                if (bRect.Intersects(eRect))
                {
                    if(b.GetBType()=="player")
                    {
                        e.ReduceHealth(rnd.Next(10,20));
                    }
                    b.SetVis(false);
                }
            }

            Rectangle pRect = new Rectangle((int)player.GetLoc().X, (int)player.GetLoc().Y, player.GetSize(), player.GetSize());
            if (bRect.Intersects(pRect))
            {
                if (b.GetBType() == "enemy")
                {
                    player.ReduceLife(rnd.Next(10, 20));
                }
                b.SetVis(false);
            }
        }

        void CreateEnemy(int x, int y,int shotSpeed)
        {
            Enemy newEnemy = new Enemy(people[rnd.Next(0,people.Count-1)], shotSpeed);
            newEnemy.SetPos(new Vector2(x, y));
            newEnemy.SetVis(true);
            enemies.Add(newEnemy);
        }

        //loc - starting location
        //w - units wide
        //h - units high
        void CreateWall(Vector2 loc, int w, int h)
        {
            walls.Add(new Obstacle(wall, loc, w, h));
        }
    }
}
