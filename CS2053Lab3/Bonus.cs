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
    class Bonus
    {
        int screenWidth;
        int screenHeight;

        Vector2 location;
        SpriteFont font;
        String gameInfo;
        double gameSecond;
        Boolean showBonus = false;

        public Bonus(int w, int h) { screenWidth = w; screenHeight = h;}

        public void setFont(SpriteFont t) { font = t; }

        public void setLocation(int w, int h) { location = new Vector2(w, h); }

        public void setShow(Boolean showIn) { showBonus = showIn; }

        public Boolean getShow() { return showBonus; }

        public void setInfo(String input) { gameInfo = input; }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, gameInfo, location, Color.Orange);
        }
    }
}
