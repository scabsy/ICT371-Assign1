using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insanity
{
    class Pickup
    {
        Texture2D texture;//Texture of pickup
        Vector2 loc;//Location
        string puType;//Type of pick up(health,armour, finish)
        Boolean isVis;//Pickup visibility
        int size = 30;//Size of pickup

        //Constructor
        public Pickup(Texture2D newTex,string npuType,Vector2 newLoc)
        {
            texture = newTex;
            loc.X = newLoc.X * 60+15;
            loc.Y = newLoc.Y * 60+15;
            puType = npuType;
            isVis = true;
            if(puType=="finish")
            {
                size *= 2;
            }
        }

        //Draw pickup
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)loc.X, (int)loc.Y, size, size), null, Color.White, 0f, new Vector2(size / 2, size / 2), 0, 0);
        }

        //Action performed based on pickup type when collected
        public void PickedUp(Player p)
        {
            //Increase player health
            if(puType=="health")
            {
                p.AddHealth(25);
            }
            //Increase player armoud
            else if(puType=="armour")
            {
                p.AddArmour(20);
            }
            //Set level to completed
            else if(puType=="finish")
            {
                p.ChangeLvlFin(true);
            }

            isVis = false;
        }

        //Gets visibility of pickup
        public Boolean GetVis()
        {
            return isVis;
        }

        //Check for the collision of the player and pickup
        public bool CheckCollision(Player p)
        {
            Rectangle pRect = new Rectangle((int)loc.X, (int)loc.Y, size, size);
        
            Rectangle wRect = new Rectangle((int)p.GetLoc().X, (int)p.GetLoc().Y, p.GetSize(), p.GetSize());
            if (pRect.Intersects(wRect))
            {
                return true;
            }
            
            return false;
        }
    }
}
