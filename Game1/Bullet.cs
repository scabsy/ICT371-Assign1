using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insanity
{
    class Bullet
    {
        public Texture2D texture;//Texture of the bullet

        Vector2 pos;//Position of the bullet
        Vector2 vel;//Velocity of the bullet
        float rot;//Rotation of the bullet
        string btype;//Type of bullet

        bool isVisible;//Visibility of the bullet

        //Constructor
        public Bullet(Texture2D newTexture)
        {
            texture = newTexture;
            isVisible = true;
        }
        
        //Draw the bullet
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,pos,null,Color.White, rot, new Vector2(0,0),.2f,SpriteEffects.None,0);
        }

        //Update position of the bullet
        public void UpdatePos()
        {
            pos += vel;
        }

        //Get the position of the bullet
        public Vector2 GetPos()
        {
            return pos;
        }

        //Set the position of the bullet to newPos
        public void SetPos(Vector2 newPos)
        {
            pos = newPos;
        }

        //Get the current velocity of the bullet
        public Vector2 GetVel()
        {
            return vel;
        }

        //Set the velocity of the bullet
        public void SetVel(Vector2 newVel)
        {
            vel = newVel;
        }
        
        //Return bullet type
        public string GetBType()
        {
            return btype;
        }

        //Set bullet type
        public void SetBType(string newType)
        {
            btype = newType;
        }

        //Set the rotation of the bullet
        public void SetRot(float newRot)
        {
            rot = newRot+ (float)1.5;
        }

        //Get visibility of the bullet
        public bool GetVis()
        {
            return isVisible;
        }

        //Set visibility of the bullet
        public void SetVis(bool newVis)
        {
            isVisible = newVis;
        }
    }
}