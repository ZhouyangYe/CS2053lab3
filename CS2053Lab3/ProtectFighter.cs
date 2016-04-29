using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CS2053Project
{
    class ProtectFighter
    {
        int screenWidth;
        int screenHeight;

       

        Texture2D texture;
        int width = 50;
        int height = 50;
        Rectangle box;
        Vector2 velocity;


        public ProtectFighter(int w, int h) { screenWidth = w; screenHeight = h; box = new Rectangle(0, 0, width, height); }

        public void setTexture(Texture2D t) { texture = t; }

        public void setLocation(Vector2 l) { box.X = (int) l.X; box.Y = (int) l.Y; }
 
        public void setVelocity(Vector2 v) { velocity = v; }

        public int getWidth() { return width; }
        public int getHeight() { return height; }

        public Rectangle getBox() { return box; }
       // Random random = new Random();/*different place statement may cause bomb dropped synchronized*/
        public void update()
        {
            
            box.X = box.X + (int) velocity.X;
            box.Y = box.Y + (int)velocity.Y;
        }

        public void draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, box, Color.White);
        }
    }
}
