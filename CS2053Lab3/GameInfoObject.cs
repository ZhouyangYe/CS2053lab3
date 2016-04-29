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
    class GameInfoObject
    {
        int screenWidth;
        int screenHeight;

        Vector2 location;
        SpriteFont font;
        String gameInfo;
        double gameSecond;
        double gameMinute;
        double gamecurrent = 10000;
        int totalTime = 60;
        int health = 100;
        int ammo = 20;
        Boolean WIN = false;
        Boolean LOSE = false;
        int level = 1;
        int torpedo = 10;
        int enemydown = 0;// in order to count your hits;
        int sink = 0;
        int lifes = 5;
        Boolean losefighter = false;//if lose life, this will be set to true;
        Boolean startGame = false;//game has not started;

        public GameInfoObject(int w, int h) { screenWidth = w; screenHeight = h; location = new Vector2(0, screenHeight - 70); }

        public void passLevel() { level++; }

        public void uselife() 
        { 
            lifes--;
            if (lifes <= 0)
            {
                lifes = 0;
            }
        }

        public void addSink() { sink++; }

        public void start() { startGame = true; }

        public void addTorpedo()
        {
            torpedo++;
            if (torpedo >= 10)
            {
                torpedo = 10;
            }
        }

        public void useTorpedo() { torpedo--; if (torpedo < 0) torpedo = 0; }

        public void setFont(SpriteFont t) { font = t; }

        public void addhit() { enemydown++; }

        public int getHit() { return enemydown; }

        public void setGameInfo(String info) { gameInfo = info; }

        public Boolean getWIN() { return WIN; }

        public Boolean getLOSE() { return LOSE; }

        public void addHP(int HP) 
        { 
            health += HP;
            if (health >= 100)
            {
                health = 100;
            }
        }

        public void setWIN(Boolean input) { WIN = input; }

        public int getCountLeft() { return totalTime; }
        public void setLOSE(Boolean input) { LOSE = input; }

        public int getLifes() { return lifes; }

        public void setLifes(int lifesIn) { lifes = lifesIn; }

        public void reduceAmmo() 
        {
      /*      ammo--;
            if (ammo <= 0)
            {
                ammo = 0;
            }*/
        }

        public int getTorpedo() { return torpedo; }

        public int getHealth() { return health; }

        public void setHealth(int healthIn) { health = healthIn; }

        public void reduceHealth(int reducehealth) 
        { 
            health = health - reducehealth;
            if (health <= 0)
            {
                losefighter = true;
            }
            if (lifes <= 0)
            {
                lifes = 0;
            }
        }

        public Boolean loselife() { return losefighter; }

        public void setLoseLife(Boolean loseIn) { losefighter = loseIn; }

        public double getTimeLeft() { return (60 - gameSecond); }

        public void update(GameTime gameTime)
        {
            gameSecond = gameTime.TotalGameTime.Seconds;
            gameMinute = gameTime.TotalGameTime.Minutes;
            if (level == 1)
            {
                if (startGame)
                {
                    gamecurrent = gameSecond;
                    startGame = false;
                }
                if (gameMinute*60 + gameSecond - gamecurrent >= 1)
                {
                    gamecurrent = gameSecond;
                    totalTime--;
                }
                setGameInfo("Time left: " + totalTime + "\nShoot down: " + enemydown + "\nHealth: " + getHealth() + "\nFighters left: " + lifes);
            }
            else if (level == 2)
            {
                setGameInfo("Aircraft Carrier Left: " + (4 - sink) + "\nTorpedo: " + torpedo + "\nHealth: " + getHealth() + "\nFighters left: " + lifes);
            }
            else
            {
                setGameInfo("Torpedo: " + torpedo + "\nHealth: " + getHealth() + "\nFighters left: " + lifes);
            }
        }

        public void increaseHealth() 
        { 
            health = health + 20; 
            ammo = ammo + 1;
            if (ammo > 20) 
            {
                ammo = 20;
            }
            if (health > 100)
            {
                health = 100;
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, gameInfo, location, Color.Orange);
        }
    }
}
