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
    class GameController
    {
        int screenWidth;
        int screenHeight;
        int hit = 0;//the number of onjects hitten by player;
        int level = 1;//totally three levels;
        int framecount = 0;//in order to count the time, little tricky here;
        int framebetween = 0;//count 120 frames between two aircraft carriers come out;
        int frame = 0;
        int hitbomberframe = 0;
        float vy = 1;//control the aircraft direction;
        float vx = 1;//control the aircraft direction;
        float vyy = 2;//control the protectors direction;
        float vxx = 2;//control the protectors direction;
        int NumOfP = 0;//number of protection fighters;
        int sink = 0;//identify how many aircraft carriers have been sank;
        Boolean carriershowup = false;//change to true when aircraft carrier shows up;
        Boolean takeofffighter = false;//aircraft fighter will take off when they see you;
        Boolean battleshipshowup = false;//change to true when battleship shows up;
        Boolean battlesink = false;//change to true if the player has sink the battleship;
        SpriteBatch spriteBatch;
        Random rand = new Random();

        PlayerObject playerObject;
        List<BattleShip> battleship;
        List<GameObject> gameObjectList;
        List<BombObject> bombObjectList;
        List<BomberObject> bomberObjectList;
        GameInfoObject gameInfoObject;
        GameInfoObject2 gameInfoObject2;
        GameInfoBetween gameInfoBetween;
        Bonus bonusInfo;
        List<DestroyerObject1> destroyerobjcet1List;
        List<AircraftCarrier> aircraftcarrierList;
        List<ProtectFighter> protectObjectList;
        List<GameObject> gameObjectRemoveList = new List<GameObject>();

        List<GameObject> gameObjectHitList = new List<GameObject>();
        List<GameObject> removegameObjectHitList = new List<GameObject>();
        List<GameObject> addgameObjectHitList = new List<GameObject>();

        List<BomberObject> bomberObjectHitList = new List<BomberObject>();
        List<AircraftCarrier> AircraftHitList = new List<AircraftCarrier>();
        List<BomberObject> bomberObjectRemoveList = new List<BomberObject>();

        List<DestroyerObject1> removeDestroyerList = new List<DestroyerObject1>();
        List<AircraftCarrier> removeCarrierList = new List<AircraftCarrier>();
        List<BattleShip> removeBattleList = new List<BattleShip>();

        List<BulletObject> bulletObjectRemoveList = new List<BulletObject>();
        List<BombObject> bombObjectRemoveList = new List<BombObject>();
        List<Torpedo> removeTorpedoList = new List<Torpedo>();

        Boolean outOfAmmo = false;//if out of ammo, the player cannot fire anymore;

        Boolean fireKeyDown = false;
        Boolean fireTorpedoDown = false;
       
        int testdrop = 0;
        Texture2D gameObjectTexture;
        Texture2D gameObjectTexture2;
        Texture2D gameObjectTexture3;

        Texture2D protectTexture;//the fighters which will protect the aircraft carriers;
        Texture2D bomberObjectTexture;//texture for enemybombers;
        Texture2D bombObjectTexture;
        Texture2D destroyer1Texture;
        Texture2D aircraftcarrierTexture;
        Texture2D battleshiptexture;

        Texture2D bomberObjectHitTexture;//texture for enemybombers;
        Texture2D destroyer1HitTexture;
        Texture2D aircraftcarrierHitTexture;
        Texture2D battleshipHittexture;

        Random random = new Random();

        
  
        Rectangle box;

        public GameController(int sw, int sh, PlayerObject p, List<GameObject> g, List<BomberObject> blist, GameInfoObject i, List<BombObject> l, GameInfoObject2 ii, GameInfoBetween ib, List<DestroyerObject1> d1l, List<AircraftCarrier> ac, List<ProtectFighter> pl, List<BattleShip> bs, Bonus bonus)
        {
            screenWidth = sw; screenHeight = sh; playerObject = p; gameObjectList = g; bomberObjectList = blist; gameInfoObject = i; bombObjectList = l; gameInfoObject2 = ii; gameInfoBetween = ib; destroyerobjcet1List = d1l;
            aircraftcarrierList = ac; protectObjectList = pl; battleship = bs; bonusInfo = bonus;
        }

        public void setTexture(Texture2D t) {gameObjectTexture = t;}//set the texture of enemy fighters;
        
        public void setTexture2(Texture2D t) { gameObjectTexture2 = t; }

        public void setTexture3(Texture2D t) { gameObjectTexture3 = t; }

        public void setProtectTexture(Texture2D t) { protectTexture = t; }

        public void setBomberTexture(Texture2D bomber) { bomberObjectTexture = bomber; }//set the texture of enemy bombers;

        public void setBombTexture(Texture2D b) { bombObjectTexture = b; }//set the texture for enemy bullets;

        public void setDestroyer1Texture(Texture2D d1) { destroyer1Texture = d1; }

        public void setAircraftCarrierTexture(Texture2D ac) { aircraftcarrierTexture = ac; }

        public void setBattleShipTexture(Texture2D bs) { battleshiptexture = bs; }

        public void setBomberHitTexture(Texture2D sht) { bomberObjectHitTexture = sht; }
        public void setDestroyer1HitTexture(Texture2D sd1ht) { destroyer1HitTexture = sd1ht; }
        public void setAircraftCarrierHitTexture(Texture2D acht) { aircraftcarrierHitTexture = acht; }
        public void setBattleShipHitTexture(Texture2D bsht) { battleshipHittexture = bsht; }

        public Boolean getCarriershowup() { return carriershowup; }

        public void setCarriershowup(Boolean showup) { carriershowup = showup; }

        public List<GameObject> getGameObjectList() { return gameObjectList; }
        public List<BombObject> getBombObjectList() { return bombObjectList; }
        public List<BomberObject> getBomberObjectList() { return bomberObjectList; }
        public List<DestroyerObject1> getDestroyer1ObjectList() { return destroyerobjcet1List; }



        public void control()
        {
       /*     if (gameInfoObject.getWIN() || gameInfoObject.getLOSE())
            {
            }*/
            if (!gameInfoBetween.getFinish()) 
            {
                passInfo();//press to pass the information page between levels;
            }
            else
            {
                if (!battlesink)
                {
                   
                    generateGameObject();//add the enemy fighters;

                    controlObjectDirection();//enemy will follow you from level2;

                    generateBomberObject();//add the enemy bombers;

                    generateDestroyerObject1();//add the enemy destroyers;

                    generateAircraftCarrier();//add the enemy aircraft carrier;

                    generateBattleShip();//add the battleship;

                    controlBattleShip();//control the battleship;

                    controlCarrierDirection();//aircraft carriers will move on the screen;

                    collisionControl();

                    if (level == 1)
                    {
                        checklevel1();
                    }
                    if (level == 2)
                    {
                        checklevel2();
                    }
                }
                if (level == 3)
                {
                    collisionControl();
                    checklevel3();
                }
            }
        }
        void passInfo() 
        {
            KeyboardState kstate = Keyboard.GetState();
            if (gameInfoBetween.getLevel() == 1)//info in front of mission 1;
                gameInfoBetween.setGameInfo("--------------------------PACIFIC EAGLE level 1------------------------------------" + "\n1941, Dec 7, Pearl Harbor" + "\n" + "Pearl Harbor is under attacked! Your squad is ordered to provide backup!" + "\nMain task: " + "\n---" + "You have to shot down 60 jap figters or bombers in one minute!" + "\n" + "Instruction: " + "\n---arrows to control the direction\n---'F' for firing bullets" + "\nBonus:\n---Each bomber will give you 30HP"+ "\n\nPress 'S' to start mission.");
            if (gameInfoBetween.getLevel() == 2)//info in front of mission 2;
                gameInfoBetween.setGameInfo("--------------------------PACIFIC EAGLE level 2------------------------------------" + "\n1942, Jun 4, MidWay" + "\n" + "Japanese Navy is heading for Midway! Destroy their 4 aircraft carriers and survive!" + "\n" + "Main task: " + "\n---" + "You have to sink the four aircraft carriers." + "\nInstruction---'D' for firing torpedos which is limited" + "\nBonus:\n---destroy one enemy destroyer would give you 30HP or 1 torpedo\n---sink one aircraft carrier will gain 50HP" + "\n\nPress 'S' to start mission.");
            if (gameInfoBetween.getLevel() == 3)
                gameInfoBetween.setGameInfo("--------------------------PACIFIC EAGLE level 3------------------------------------" + "\n1945, April 1, Okinawa" + "\n" + "The war is not ended, 'Yamato' battleship is still in Jap Navy." + "\n" + "You need to sink the Yamato Battleship from Japan. It's their last hope." + "\n" + "Main task: " + "\n---" + "You have to sink the Yamato Battleship." + "\nBonus:\n---destroy one enemy destroyer would give you 30HP or 1 torpedo" + "\n\nPress 'S' to start mission.");
            if (kstate.IsKeyDown(Keys.S))
            {
                gameInfoBetween.setFinish(true);
                gameInfoObject.start();
            }
        }

        void checklevel1()//check if you can pass level1;
        {
            if (gameInfoObject.getHit() >= 60 && gameInfoObject.getCountLeft() == 0)//60,0;
            {
                gameInfoBetween.setLevel(2);
                gameInfoBetween.setFinish(false);
                level = 2;
                framecount = 0;
                gameInfoObject.passLevel();
                playerObject.setBox();
                playerObject.passlevel();
                gameInfoObject.setHealth(100);
                gameObjectList.Clear();
                bombObjectList.Clear();
                bomberObjectList.Clear();
            }
            if (gameInfoObject.getHit() < 60 && gameInfoObject.getCountLeft() == 0)
            {
                gameInfoObject.setLOSE(true);
            }
        }

        void checklevel2()//check if you can pass level2;
        {
            if (sink == 4)
            {
                if (framebetween >= 120)
                {
                        gameInfoBetween.setLevel(3);
                        gameInfoBetween.setFinish(false);
                        level = 3;
                        playerObject.setBox();
                        playerObject.passlevel();
                        gameInfoObject.passLevel();
                        gameInfoObject.setHealth(100);
                        gameObjectList.Clear();
                        destroyerobjcet1List.Clear();
                        bombObjectList.Clear();
                        framecount = 0;
                }
            }
        }

        void checklevel3()//check if you can pass level3;
        {
            if (battlesink)
            {
                framebetween++;
                if (framebetween >= 120)
                    gameInfoObject.setWIN(true);       
            }
        }

        void generateBattleShip()
        {
            if (level == 3)
            {
                if (!battleshipshowup && framecount > 2200)
                {
                    BattleShip bs = new BattleShip(screenWidth, screenHeight);
                    bs.setTexture(battleshiptexture);
                    int x = (int)random.Next(screenWidth / 4, screenWidth * 3 / 4 - 300);
                    bs.setLocation(new Vector2(x, -100));
                    vy = 1;
                    vx = -1;
                    bs.setVelocity(new Vector2(vx, vy));
                    battleship.Add(bs);
                    battleshipshowup = true;
                    framecount = 0;
                }
                foreach (BattleShip bs in battleship)
                {
                    Boolean drop = false;
                    box = bs.getBox();
                    testdrop = (int)random.Next(0, 10000);
                    if (testdrop <= 600)
                    {
                        drop = true;
                    }
                    else
                    {
                        drop = false;
                    }
                    if (drop)
                    {
                        //each destroyer will shoot 6 bullets at one time
                        BombObject bomb1 = new BombObject();
                        bomb1.setTexture(bombObjectTexture);
                        // Vector2 origin1 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                        bomb1.setLocation(new Vector2(box.X, box.Y + bs.getHeight() / 2));
                        Vector2 bombVelocity1 = new Vector2(0.0f, 5f);
                        bomb1.setVelocity(bombVelocity1);

                        BombObject bomb2 = new BombObject();
                        bomb2.setTexture(bombObjectTexture);
                        // Vector2 origin2 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                        bomb2.setLocation(new Vector2(box.X, box.Y + bs.getHeight() / 2));
                        Vector2 bombVelocity2 = new Vector2(5f, 5f);
                        bomb2.setVelocity(bombVelocity2);

                        BombObject bomb3 = new BombObject();
                        bomb3.setTexture(bombObjectTexture);
                        // Vector2 origin3 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                        bomb3.setLocation(new Vector2(box.X, box.Y + bs.getHeight() / 2));
                        Vector2 bombVelocity3 = new Vector2(-5f, 5f);
                        bomb3.setVelocity(bombVelocity3);

                        BombObject bomb4 = new BombObject();
                        bomb4.setTexture(bombObjectTexture);
                        // Vector2 origin1 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                        bomb4.setLocation(new Vector2(box.X + bs.getWidth(), box.Y + bs.getHeight() / 2));
                        Vector2 bombVelocity4 = new Vector2(0.0f, 5f);
                        bomb4.setVelocity(bombVelocity4);

                        BombObject bomb5 = new BombObject();
                        bomb5.setTexture(bombObjectTexture);
                        // Vector2 origin2 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                        bomb5.setLocation(new Vector2(box.X + bs.getWidth(), box.Y + bs.getHeight() / 2));
                        Vector2 bombVelocity5 = new Vector2(5f, 5f);
                        bomb5.setVelocity(bombVelocity5);

                        BombObject bomb6 = new BombObject();
                        bomb6.setTexture(bombObjectTexture);
                        // Vector2 origin3 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                        bomb6.setLocation(new Vector2(box.X + bs.getWidth(), box.Y + bs.getHeight() / 2));
                        Vector2 bombVelocity6 = new Vector2(-5f, 5f);
                        bomb6.setVelocity(bombVelocity6);

                        BombObject bomb7 = new BombObject();
                        bomb7.setTexture(bombObjectTexture);
                        // Vector2 origin3 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                        bomb7.setLocation(new Vector2(box.X + bs.getWidth() / 3, box.Y + bs.getHeight() / 2));
                        Vector2 bombVelocity7 = new Vector2(-5f, 5f);
                        bomb7.setVelocity(bombVelocity7);

                        BombObject bomb8 = new BombObject();
                        bomb8.setTexture(bombObjectTexture);
                        // Vector2 origin3 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                        bomb8.setLocation(new Vector2(box.X + bs.getWidth() / 3, box.Y + bs.getHeight() / 2));
                        Vector2 bombVelocity8 = new Vector2(-5f, 5f);
                        bomb8.setVelocity(bombVelocity8);

                        BombObject bomb9 = new BombObject();
                        bomb9.setTexture(bombObjectTexture);
                        // Vector2 origin3 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                        bomb9.setLocation(new Vector2(box.X + bs.getWidth() / 3, box.Y + bs.getHeight() / 2));
                        Vector2 bombVelocity9 = new Vector2(-5f, 5f);
                        bomb9.setVelocity(bombVelocity9);

                        BombObject bomb11 = new BombObject();
                        bomb11.setTexture(bombObjectTexture);
                        // Vector2 origin3 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                        bomb11.setLocation(new Vector2(box.X + bs.getWidth()*2 / 3, box.Y + bs.getHeight() / 2));
                        Vector2 bombVelocity11 = new Vector2(-5f, 5f);
                        bomb11.setVelocity(bombVelocity11);

                        BombObject bomb12 = new BombObject();
                        bomb12.setTexture(bombObjectTexture);
                        // Vector2 origin3 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                        bomb12.setLocation(new Vector2(box.X + bs.getWidth()*2 / 3, box.Y + bs.getHeight() / 2));
                        Vector2 bombVelocity12 = new Vector2(-5f, 5f);
                        bomb12.setVelocity(bombVelocity12);

                        BombObject bomb10 = new BombObject();
                        bomb10.setTexture(bombObjectTexture);
                        // Vector2 origin3 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                        bomb10.setLocation(new Vector2(box.X + bs.getWidth()*2 / 3, box.Y + bs.getHeight() / 2));
                        Vector2 bombVelocity10 = new Vector2(-5f, 5f);
                        bomb10.setVelocity(bombVelocity10);

                        bombObjectList.Add(bomb1);
                        bombObjectList.Add(bomb2);
                        bombObjectList.Add(bomb3);
                        bombObjectList.Add(bomb4);
                        bombObjectList.Add(bomb5);
                        bombObjectList.Add(bomb6);
                        bombObjectList.Add(bomb7);
                        bombObjectList.Add(bomb8);
                        bombObjectList.Add(bomb9);
                        bombObjectList.Add(bomb10);
                        bombObjectList.Add(bomb11);
                        bombObjectList.Add(bomb12);

                        drop = false;
                    }
                    List<BombObject> removeList = new List<BombObject>();
                    foreach (BombObject b in bombObjectList)
                    {
                        Rectangle bombBox = b.getBox();
                        if (bombBox.Y > screenHeight)
                        { removeList.Add(b); }
                        else
                        { }
                    }
                    foreach (BombObject b in removeList)
                    { bombObjectList.Remove(b); }
                }               
            }
        }

        void generateAircraftCarrier()
        {
            if (level == 2)
            {
                if (!carriershowup && framecount>3000)
                {
                    if (framebetween == 500)
                    {
                        AircraftCarrier ac = new AircraftCarrier(screenWidth, screenHeight);
                        ac.setTexture(aircraftcarrierTexture);
                        ac.setTexture2(gameObjectTexture2);
                        ac.setFrameCounter(0);
                        int x = (int)random.Next(screenWidth / 4, screenWidth * 3 / 4 - 300);
                        ac.setLocation(new Vector2(x, -100));
                        vy = 1;
                        vx = 1;
                        ac.setVelocity(new Vector2(vx, vy));
                        aircraftcarrierList.Add(ac);
                        takeofffighter = true;
                        carriershowup = true;
                    }
                    framebetween++;
                }
                    foreach (AircraftCarrier ac in aircraftcarrierList)
                    {
                        Boolean drop = false;
                        box = ac.getBox();
                        testdrop = (int)random.Next(0, 10000);
                        if (testdrop <= 300)
                        {
                            drop = true;
                        }
                        else
                        {
                            drop = false;
                        }
                        if (drop)
                        {
                            //each destroyer will shoot 6 bullets at one time
                            BombObject bomb1 = new BombObject();
                            bomb1.setTexture(bombObjectTexture);
                            // Vector2 origin1 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                            bomb1.setLocation(new Vector2(box.X, box.Y + ac.getHeight() / 2));
                            Vector2 bombVelocity1 = new Vector2(0.0f, 5f);
                            bomb1.setVelocity(bombVelocity1);

                            BombObject bomb2 = new BombObject();
                            bomb2.setTexture(bombObjectTexture);
                            // Vector2 origin2 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                            bomb2.setLocation(new Vector2(box.X, box.Y + ac.getHeight() / 2));
                            Vector2 bombVelocity2 = new Vector2(5f, 5f);
                            bomb2.setVelocity(bombVelocity2);

                            BombObject bomb3 = new BombObject();
                            bomb3.setTexture(bombObjectTexture);
                            // Vector2 origin3 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                            bomb3.setLocation(new Vector2(box.X, box.Y + ac.getHeight() / 2));
                            Vector2 bombVelocity3 = new Vector2(-5f, 5f);
                            bomb3.setVelocity(bombVelocity3);

                            BombObject bomb4 = new BombObject();
                            bomb4.setTexture(bombObjectTexture);
                            // Vector2 origin1 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                            bomb4.setLocation(new Vector2(box.X + ac.getWidth(), box.Y + ac.getHeight() / 2));
                            Vector2 bombVelocity4 = new Vector2(0.0f, 5f);
                            bomb4.setVelocity(bombVelocity4);

                            BombObject bomb5 = new BombObject();
                            bomb5.setTexture(bombObjectTexture);
                            // Vector2 origin2 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                            bomb5.setLocation(new Vector2(box.X + ac.getWidth(), box.Y + ac.getHeight() / 2));
                            Vector2 bombVelocity5 = new Vector2(5f, 5f);
                            bomb5.setVelocity(bombVelocity5);

                            BombObject bomb6 = new BombObject();
                            bomb6.setTexture(bombObjectTexture);
                            // Vector2 origin3 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                            bomb6.setLocation(new Vector2(box.X + ac.getWidth(), box.Y + ac.getHeight() / 2));
                            Vector2 bombVelocity6 = new Vector2(-5f, 5f);
                            bomb6.setVelocity(bombVelocity6);

                            BombObject bomb7 = new BombObject();
                            bomb7.setTexture(bombObjectTexture);
                            // Vector2 origin3 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                            bomb7.setLocation(new Vector2(box.X + ac.getWidth() / 2, box.Y + ac.getHeight() / 2));
                            Vector2 bombVelocity7 = new Vector2(-5f, 5f);
                            bomb7.setVelocity(bombVelocity7);

                            BombObject bomb8 = new BombObject();
                            bomb8.setTexture(bombObjectTexture);
                            // Vector2 origin3 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                            bomb8.setLocation(new Vector2(box.X + ac.getWidth() / 2, box.Y + ac.getHeight() / 2));
                            Vector2 bombVelocity8 = new Vector2(-5f, 5f);
                            bomb8.setVelocity(bombVelocity8);

                            BombObject bomb9 = new BombObject();
                            bomb9.setTexture(bombObjectTexture);
                            // Vector2 origin3 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                            bomb9.setLocation(new Vector2(box.X + ac.getWidth() / 2, box.Y + ac.getHeight() / 2));
                            Vector2 bombVelocity9 = new Vector2(-5f, 5f);
                            bomb9.setVelocity(bombVelocity9);

                            bombObjectList.Add(bomb1);
                            bombObjectList.Add(bomb2);
                            bombObjectList.Add(bomb3);
                            bombObjectList.Add(bomb4);
                            bombObjectList.Add(bomb5);
                            bombObjectList.Add(bomb6);
                            bombObjectList.Add(bomb7);
                            bombObjectList.Add(bomb8);
                            bombObjectList.Add(bomb9);

                            drop = false;
                        }
                        List<BombObject> removeList = new List<BombObject>();
                        foreach (BombObject b in bombObjectList)
                        {
                            Rectangle bombBox = b.getBox();
                            if (bombBox.Y > screenHeight)
                            { removeList.Add(b); }
                            else
                            { }
                        }
                        foreach (BombObject b in removeList)
                        { bombObjectList.Remove(b); }
                    }               
                        // i++;
            }
            
        }

        void controlCarrierDirection()
        {
            foreach (AircraftCarrier ac in aircraftcarrierList)
            {               
                if (ac.getBox().X == 0)
                {
                    vx = 1;
                }
                if (ac.getBox().X == screenWidth - 300)
                {
                    vx = -1;
                }
                if (ac.getBox().Y == 0)
                {
                    vy = 1;
                }
                if (ac.getBox().Y == screenHeight / 2 - 100)
                {
                    vy = -1;
                }
                ac.setVelocity(new Vector2(vx, vy));
            }
        }

        void controlBattleShip()
        {
            foreach (BattleShip bs in battleship)
            {
                if (bs.getBox().X == 0)
                {
                    vx = 1;
                }
                if (bs.getBox().X == screenWidth - 400)
                {
                    vx = -1;
                }
                if (bs.getBox().Y == 0)
                {
                    vy = 1;
                }
                if (bs.getBox().Y == screenHeight / 2 - 100)
                {
                    vy = -1;
                }
                bs.setVelocity(new Vector2(vx, vy));
            }
        }

        void controlObjectDirection()
        {
            foreach (GameObject g in gameObjectList)
            {
                float vy = 2;
                float vx;
                if(level == 1)
                { vx = 0; }
                else if (level == 2)
                {
                    if (g.getBox().X < playerObject.getBox().X)
                    {
                        vx = 1;
                    }
                    else if (g.getBox().X > playerObject.getBox().X)
                    {
                        vx = -1;
                    }
                    else
                    {
                        vx = 0;
                    }
                }
                else
                {
                    if (g.getBox().X < playerObject.getBox().X - 50)
                    {
                        vx = 4;
                    }
                    else if (g.getBox().X > playerObject.getBox().X + 50)
                    {
                        vx = -4;
                    }
                    else
                    {
                        vx = 0;
                        vy = 10;
                    }
                }
                g.setVelocity(new Vector2(vx, vy));
            }
        }

        void generateDestroyerObject1()
        {
            if (level == 1)
            { 
            }
            if (level == 2)
            {
                if (framecount >= 2000 && framecount <= 3000)
                {
                    int i = (int)random.Next(0, 1000);
                    if (i < 10)
                    {
                        DestroyerObject1 d1 = new DestroyerObject1(screenWidth, screenHeight);
                        d1.setTexture(destroyer1Texture);
                        d1.setTexture2(gameObjectTexture2);
                        d1.setFrameCounter(0);
                        int x = (int)random.Next(0, screenWidth - 40);
                        d1.setLocation(new Vector2(x, -100));
                        float vy = 1;
                        float vx = 0;
                        d1.setVelocity(new Vector2(vx, vy));
                        destroyerobjcet1List.Add(d1);

                        // i++;
                    }
                    List<DestroyerObject1> removedestroyer1List = new List<DestroyerObject1>();
                    foreach (DestroyerObject1 d1 in destroyerobjcet1List)
                    {
                        Boolean drop = false;
                        box = d1.getBox();
                        testdrop = (int)random.Next(0, 10000);

                        if (box.Y > screenHeight)
                        {
                            removedestroyer1List.Add(d1);
                        }
                        if (testdrop <= 100)
                        {
                            drop = true;
                        }
                        else
                        {
                            drop = false;
                        }
                        if (drop)
                        {
                            //each destroyer will shoot 6 bullets at one time
                            BombObject bomb1 = new BombObject();
                            bomb1.setTexture(bombObjectTexture);
                            // Vector2 origin1 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                            bomb1.setLocation(new Vector2(box.X + d1.getWidth()/4, box.Y + d1.getHeight() / 2));
                            Vector2 bombVelocity1 = new Vector2(0.0f, 5f);
                            bomb1.setVelocity(bombVelocity1);

                            BombObject bomb2 = new BombObject();
                            bomb2.setTexture(bombObjectTexture);
                            // Vector2 origin2 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                            bomb2.setLocation(new Vector2(box.X + d1.getWidth() / 4, box.Y + d1.getHeight() / 2));
                            Vector2 bombVelocity2 = new Vector2(5f, 5f);
                            bomb2.setVelocity(bombVelocity2);

                            BombObject bomb3 = new BombObject();
                            bomb3.setTexture(bombObjectTexture);
                            // Vector2 origin3 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                            bomb3.setLocation(new Vector2(box.X + d1.getWidth() / 4, box.Y + d1.getHeight() / 2));
                            Vector2 bombVelocity3 = new Vector2(-5f, 5f);
                            bomb3.setVelocity(bombVelocity3);

                            BombObject bomb4 = new BombObject();
                            bomb4.setTexture(bombObjectTexture);
                            // Vector2 origin1 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                            bomb4.setLocation(new Vector2(box.X + d1.getWidth()*3/4, box.Y + d1.getHeight()/2));
                            Vector2 bombVelocity4 = new Vector2(0.0f, 5f);
                            bomb4.setVelocity(bombVelocity4);

                            BombObject bomb5 = new BombObject();
                            bomb5.setTexture(bombObjectTexture);
                            // Vector2 origin2 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                            bomb5.setLocation(new Vector2(box.X + d1.getWidth()*3/4, box.Y + d1.getHeight() / 2));
                            Vector2 bombVelocity5 = new Vector2(5f, 5f);
                            bomb5.setVelocity(bombVelocity5);

                            BombObject bomb6 = new BombObject();
                            bomb6.setTexture(bombObjectTexture);
                            // Vector2 origin3 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                            bomb6.setLocation(new Vector2(box.X + d1.getWidth()*3/4, box.Y + d1.getHeight() / 2));
                            Vector2 bombVelocity6 = new Vector2(-5f, 5f);
                            bomb6.setVelocity(bombVelocity6);

                            bombObjectList.Add(bomb1);
                            bombObjectList.Add(bomb2);
                            bombObjectList.Add(bomb3);
                            bombObjectList.Add(bomb4);
                            bombObjectList.Add(bomb5);
                            bombObjectList.Add(bomb6);

                            drop = false;
                        }
                        List<BombObject> removeList = new List<BombObject>();
                        foreach (BombObject b in bombObjectList)
                        {
                            Rectangle bombBox = b.getBox();
                            if (bombBox.Y > screenHeight)
                            { removeList.Add(b); }
                            else
                            { }
                        }
                        foreach (BombObject b in removeList)
                        { bombObjectList.Remove(b); }
                    }
                    foreach (DestroyerObject1 d1 in removedestroyer1List)
                    {
                        destroyerobjcet1List.Remove(d1);
                    }
                }
            }
            if (level == 3)
            {
                if (framecount <=2000)
                {
                    int i = (int)random.Next(0, 1000);
                    if (i < 10)
                    {
                        DestroyerObject1 d1 = new DestroyerObject1(screenWidth, screenHeight);
                        d1.setTexture(destroyer1Texture);
                        int x = (int)random.Next(0, screenWidth - 40);
                        d1.setLocation(new Vector2(x, -100));
                        float vy = 1;
                        float vx = 0;
                        d1.setVelocity(new Vector2(vx, vy));
                        destroyerobjcet1List.Add(d1);

                        // i++;
                    }
                    List<DestroyerObject1> removedestroyer1List = new List<DestroyerObject1>();
                    foreach (DestroyerObject1 d1 in destroyerobjcet1List)
                    {
                        Boolean drop = false;
                        box = d1.getBox();
                        testdrop = (int)random.Next(0, 10000);

                        if (box.Y > screenHeight)
                        {
                            removedestroyer1List.Add(d1);
                        }
                        if (testdrop <= 100)
                        {
                            drop = true;
                        }
                        else
                        {
                            drop = false;
                        }
                        if (drop)
                        {
                            //each destroyer will shoot 6 bullets at one time
                            BombObject bomb1 = new BombObject();
                            bomb1.setTexture(bombObjectTexture);
                            // Vector2 origin1 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                            bomb1.setLocation(new Vector2(box.X + d1.getWidth() / 4, box.Y + d1.getHeight() / 2));
                            Vector2 bombVelocity1 = new Vector2(0.0f, 5f);
                            bomb1.setVelocity(bombVelocity1);

                            BombObject bomb2 = new BombObject();
                            bomb2.setTexture(bombObjectTexture);
                            // Vector2 origin2 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                            bomb2.setLocation(new Vector2(box.X + d1.getWidth() / 4, box.Y + d1.getHeight() / 2));
                            Vector2 bombVelocity2 = new Vector2(5f, 5f);
                            bomb2.setVelocity(bombVelocity2);

                            BombObject bomb3 = new BombObject();
                            bomb3.setTexture(bombObjectTexture);
                            // Vector2 origin3 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                            bomb3.setLocation(new Vector2(box.X + d1.getWidth() / 4, box.Y + d1.getHeight() / 2));
                            Vector2 bombVelocity3 = new Vector2(-5f, 5f);
                            bomb3.setVelocity(bombVelocity3);

                            BombObject bomb4 = new BombObject();
                            bomb4.setTexture(bombObjectTexture);
                            // Vector2 origin1 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                            bomb4.setLocation(new Vector2(box.X + d1.getWidth() * 3 / 4, box.Y + d1.getHeight() / 2));
                            Vector2 bombVelocity4 = new Vector2(0.0f, 5f);
                            bomb4.setVelocity(bombVelocity4);

                            BombObject bomb5 = new BombObject();
                            bomb5.setTexture(bombObjectTexture);
                            // Vector2 origin2 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                            bomb5.setLocation(new Vector2(box.X + d1.getWidth() * 3 / 4, box.Y + d1.getHeight() / 2));
                            Vector2 bombVelocity5 = new Vector2(5f, 5f);
                            bomb5.setVelocity(bombVelocity5);

                            BombObject bomb6 = new BombObject();
                            bomb6.setTexture(bombObjectTexture);
                            // Vector2 origin3 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                            bomb6.setLocation(new Vector2(box.X + d1.getWidth() * 3 / 4, box.Y + d1.getHeight() / 2));
                            Vector2 bombVelocity6 = new Vector2(-5f, 5f);
                            bomb6.setVelocity(bombVelocity6);

                            bombObjectList.Add(bomb1);
                            bombObjectList.Add(bomb2);
                            bombObjectList.Add(bomb3);
                            bombObjectList.Add(bomb4);
                            bombObjectList.Add(bomb5);
                            bombObjectList.Add(bomb6);

                            drop = false;
                        }
                        List<BombObject> removeList = new List<BombObject>();
                        foreach (BombObject b in bombObjectList)
                        {
                            Rectangle bombBox = b.getBox();
                            if (bombBox.Y > screenHeight)
                            { removeList.Add(b); }
                            else
                            { }
                        }
                        foreach (BombObject b in removeList)
                        { bombObjectList.Remove(b); }
                    }
                    foreach (DestroyerObject1 d1 in removedestroyer1List)
                    {
                        destroyerobjcet1List.Remove(d1);
                    }
                }
            }
        }

        void generateGameObject() 
        {
            if (level == 1)
            {
                int i = (int)random.Next(0, 1000);
                if (i < 30)
                {
                    GameObject g = new GameObject(screenWidth, screenHeight);
                    g.setTexture(gameObjectTexture);
                    g.setTexture2(gameObjectTexture2);
                    g.setFrameCounter(0);
                    int x = (int)random.Next(0, screenWidth - 50);
                    int y = (int)random.Next(0, screenHeight / 2 - 50);
                    g.setLocation(new Vector2(x, -100));
                    float vy = 2;
                    float vx = 0;
                    g.setVelocity(new Vector2(vx, vy));
                    gameObjectList.Add(g);

                    // i++;
                }
                List<GameObject> removegameList = new List<GameObject>();
                foreach (GameObject g in gameObjectList)
                {
                    Boolean drop = false;
                    box = g.getBox();
                    testdrop = (int)random.Next(0, 10000);

                    if (box.Y > screenHeight)
                    {
                        removegameList.Add(g);
                    }
                    if (testdrop <= 30)
                    {
                        drop = true;
                    }
                    else
                    {
                        drop = false;
                    }
                    if (drop)
                    {

                        BombObject bomb = new BombObject();
                        bomb.setTexture(bombObjectTexture);
                        //Vector2 origin = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                        bomb.setLocation(new Vector2(box.X + g.getWidth() / 2, box.Y + g.getHeight() / 2));
                        Vector2 bombVelocity = new Vector2(0.0f, 5f);
                        bomb.setVelocity(bombVelocity);
                        bombObjectList.Add(bomb);
                        drop = false;
                    }
                    List<BombObject> removeList = new List<BombObject>();
                    foreach (BombObject b in bombObjectList)
                    {
                        Rectangle bombBox = b.getBox();
                        if (bombBox.Y > screenHeight)
                        { removeList.Add(b); }
                        else
                        { }
                    }
                    foreach (BombObject b in removeList)
                    { bombObjectList.Remove(b); }
                }
                foreach (GameObject g in removegameList)
                {
                    gameObjectList.Remove(g);
                }

                if (gameInfoObject.getTimeLeft() == 60)//remove all the object left when 1 level is done;
                {
                    removegameList = new List<GameObject>();
                    List<BombObject> removeList = new List<BombObject>();
                    foreach (GameObject g in gameObjectList)
                    {
                        removegameList.Add(g);
                    }
                    foreach (GameObject g in removegameList)
                    {
                        gameObjectList.Remove(g);
                    }
                    foreach (BombObject b in bombObjectList)
                    {
                        removeList.Add(b);
                    }
                    foreach (BombObject b in removeList)
                    {
                        bombObjectList.Remove(b);
                    }
                }
            }
            if (level == 2)
            {
                if (framecount < 2400)// in the first 40 second;
                {
                    int i = (int)random.Next(0, 1000);
                    if (i < 30)
                    {
                        GameObject g = new GameObject(screenWidth, screenHeight);
                        g.setTexture(gameObjectTexture);
                        int x = (int)random.Next(0, screenWidth - 50);
                        int y = (int)random.Next(0, screenHeight / 2 - 50);
                        g.setLocation(new Vector2(x, -100));
                        float vy = 2;
                        float vx = 0;
                        g.setVelocity(new Vector2(vx, vy));
                        gameObjectList.Add(g);

                        // i++;
                    }
                    List<GameObject> removegameList = new List<GameObject>();
                    foreach (GameObject g in gameObjectList)
                    {
                        Boolean drop = false;
                        box = g.getBox();
                        testdrop = (int)random.Next(0, 10000);

                        if (box.Y > screenHeight)
                        {
                            removegameList.Add(g);
                        }
                        if (testdrop <= 30)
                        {
                            drop = true;
                        }
                        else
                        {
                            drop = false;
                        }
                        if (drop)
                        {

                            BombObject bomb = new BombObject();
                            bomb.setTexture(bombObjectTexture);
                            //Vector2 origin = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                            bomb.setLocation(new Vector2(box.X + g.getWidth() / 2, box.Y + g.getHeight() / 2));
                            Vector2 bombVelocity = new Vector2(0.0f, 5f);
                            bomb.setVelocity(bombVelocity);
                            bombObjectList.Add(bomb);
                            drop = false;
                        }
                        List<BombObject> removeList = new List<BombObject>();
                        foreach (BombObject b in bombObjectList)
                        {
                            Rectangle bombBox = b.getBox();
                            if (bombBox.Y > screenHeight)
                            { removeList.Add(b); }
                            else
                            { }
                        }
                        foreach (BombObject b in removeList)
                        { bombObjectList.Remove(b); }
                    }
                    foreach (GameObject g in removegameList)
                    {
                        gameObjectList.Remove(g);
                    }
                }
                else if(framecount < 3000)
                {
                    int i = (int)random.Next(0, 1000);
                    if (i < 10)
                    {
                        GameObject g = new GameObject(screenWidth, screenHeight);
                        g.setTexture(gameObjectTexture);
                        int x = (int)random.Next(0, screenWidth - 50);
                        int y = (int)random.Next(0, screenHeight / 2 - 50);
                        g.setLocation(new Vector2(x, -100));
                        float vy = 2;
                        float vx = 0;
                        g.setVelocity(new Vector2(vx, vy));
                        gameObjectList.Add(g);

                        // i++;
                    }
                    List<GameObject> removegameList = new List<GameObject>();
                    foreach (GameObject g in gameObjectList)
                    {
                        Boolean drop = false;
                        box = g.getBox();
                        testdrop = (int)random.Next(0, 10000);

                        if (box.Y > screenHeight)
                        {
                            removegameList.Add(g);
                        }
                        if (testdrop <= 30)
                        {
                            drop = true;
                        }
                        else
                        {
                            drop = false;
                        }
                        if (drop)
                        {

                            BombObject bomb = new BombObject();
                            bomb.setTexture(bombObjectTexture);
                            //Vector2 origin = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                            bomb.setLocation(new Vector2(box.X + g.getWidth() / 2, box.Y + g.getHeight() / 2));
                            Vector2 bombVelocity = new Vector2(0.0f, 5f);
                            bomb.setVelocity(bombVelocity);
                            bombObjectList.Add(bomb);
                            drop = false;
                        }
                        List<BombObject> removeList = new List<BombObject>();
                        foreach (BombObject b in bombObjectList)
                        {
                            Rectangle bombBox = b.getBox();
                            if (bombBox.Y > screenHeight)
                            { removeList.Add(b); }
                            else
                            { }
                        }
                        foreach (BombObject b in removeList)
                        { bombObjectList.Remove(b); }
                    }
                    foreach (GameObject g in removegameList)
                    {
                        gameObjectList.Remove(g);
                    }
                }
                if (takeofffighter)
                {
                    int i = (int)random.Next(0, 1000);
                    if (i < 50)
                    {
                        GameObject g = new GameObject(screenWidth, screenHeight);
                        g.setTexture(gameObjectTexture);
                        int x = (int)random.Next(0, screenWidth - 50);
                        int y = (int)random.Next(0, screenHeight / 2 - 50);
                        g.setLocation(new Vector2(x, -50));
                        float vy = 2;
                        float vx = 0;
                        g.setVelocity(new Vector2(vx, vy));
                        gameObjectList.Add(g);

                        // i++;
                    }
                    List<GameObject> removegameList = new List<GameObject>();
                    foreach (GameObject g in gameObjectList)
                    {
                        Boolean drop = false;
                        box = g.getBox();
                        testdrop = (int)random.Next(0, 10000);

                        if (box.Y > screenHeight)
                        {
                            removegameList.Add(g);
                        }
                        if (testdrop <= 30)
                        {
                            drop = true;
                        }
                        else
                        {
                            drop = false;
                        }
                        if (drop)
                        {

                            BombObject bomb = new BombObject();
                            bomb.setTexture(bombObjectTexture);
                            //Vector2 origin = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                            bomb.setLocation(new Vector2(box.X + g.getWidth() / 2, box.Y + g.getHeight() / 2));
                            Vector2 bombVelocity = new Vector2(0.0f, 5f);
                            bomb.setVelocity(bombVelocity);
                            bombObjectList.Add(bomb);
                            drop = false;
                        }
                        List<BombObject> removeList = new List<BombObject>();
                        foreach (BombObject b in bombObjectList)
                        {
                            Rectangle bombBox = b.getBox();
                            if (bombBox.Y > screenHeight)
                            { removeList.Add(b); }
                            else
                            { }
                        }
                        foreach (BombObject b in removeList)
                        { bombObjectList.Remove(b); }
                    }
                    foreach (GameObject g in removegameList)
                    {
                        gameObjectList.Remove(g);
                    }
                }               
            }
            
            if (level == 3)
            {
                int i = (int)random.Next(0, 1000);
                if (i < 20)
                {
                    GameObject g = new GameObject(screenWidth, screenHeight);
                    g.setTexture(gameObjectTexture);
                    int x = (int)random.Next(0, screenWidth - 50);
                    int y = (int)random.Next(0, screenHeight / 2 - 50);
                    g.setLocation(new Vector2(x, -50));
                    float vy = 2;
                    float vx = 0;
                    g.setVelocity(new Vector2(vx, vy));
                    gameObjectList.Add(g);
                }
                List<GameObject> removegameList = new List<GameObject>();
                foreach (GameObject g in gameObjectList)
                {
                   if (box.Y > screenHeight)
                    {
                        removegameList.Add(g);
                    }
                }
                foreach (GameObject g in removegameList)
                {
                    gameObjectList.Remove(g);
                }
            }
            framecount++;
        }

        void generateBomberObject() 
        {
            if (level == 1)
            {
                int i = (int)random.Next(0, 1000);
                if (i < 5)
                {
                    BomberObject g = new BomberObject(screenWidth, screenHeight);
                    g.setTexture(bomberObjectTexture);
                    g.setTexture2(gameObjectTexture2);
                    g.setFrameCounter(0);
                    int x = (int)random.Next(0, screenWidth - 70);
                    g.setLocation(new Vector2(x, -100));
                    float vy = 1;
                    float vx = 0;
                    g.setVelocity(new Vector2(vx, vy));
                    bomberObjectList.Add(g);

                    // i++;
                }
                List<BomberObject> removebomberList = new List<BomberObject>();
                foreach (BomberObject g in bomberObjectList)
                {
                    Boolean drop = false;
                    box = g.getBox();
                    testdrop = (int)random.Next(0, 10000);

                    if (box.Y > screenHeight)
                    {
                        removebomberList.Add(g);
                    }
                    if (testdrop <= 100)
                    {
                        drop = true;
                    }
                    else
                    {
                        drop = false;
                    }
                    if (drop)
                    {
                        //each bomber will shoot 3 bullets at one time
                        BombObject bomb1 = new BombObject();
                        bomb1.setTexture(bombObjectTexture);
                        // Vector2 origin1 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                        bomb1.setLocation(new Vector2(box.X + g.getWidth() / 2, box.Y + g.getHeight() / 2));
                        Vector2 bombVelocity1 = new Vector2(0.0f, 5f);
                        bomb1.setVelocity(bombVelocity1);

                        BombObject bomb2 = new BombObject();
                        bomb2.setTexture(bombObjectTexture);
                        // Vector2 origin2 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                        bomb2.setLocation(new Vector2(box.X + g.getWidth() / 2, box.Y + g.getHeight() / 2));
                        Vector2 bombVelocity2 = new Vector2(5f, 5f);
                        bomb2.setVelocity(bombVelocity2);

                        BombObject bomb3 = new BombObject();
                        bomb3.setTexture(bombObjectTexture);
                        // Vector2 origin3 = new Vector2(gameObjectTexture.Width / 2, gameObjectTexture.Height / 2);
                        bomb3.setLocation(new Vector2(box.X + g.getWidth() / 2, box.Y + g.getHeight() / 2));
                        Vector2 bombVelocity3 = new Vector2(-5f, 5f);
                        bomb3.setVelocity(bombVelocity3);

                        bombObjectList.Add(bomb1);
                        bombObjectList.Add(bomb2);
                        bombObjectList.Add(bomb3);

                        drop = false;
                    }
                    List<BombObject> removeList = new List<BombObject>();
                    foreach (BombObject b in bombObjectList)
                    {
                        Rectangle bombBox = b.getBox();
                        if (bombBox.Y > screenHeight)
                        { removeList.Add(b); }
                        else
                        { }
                    }
                    foreach (BombObject b in removeList)
                    { bombObjectList.Remove(b); }
                }
                foreach (BomberObject g in removebomberList)
                {
                    bomberObjectList.Remove(g);
                }
                if (gameInfoObject.getTimeLeft() == 60)//remove all the object left when 1 level is done;
                {
                    removebomberList = new List<BomberObject>();
                    List<BombObject> removeList = new List<BombObject>();
                    foreach (BomberObject g in bomberObjectList)
                    {
                        removebomberList.Add(g);
                    }
                    foreach (BomberObject g in removebomberList)
                    {
                        bomberObjectList.Remove(g);
                    }
                    foreach (BombObject b in bombObjectList)
                    {
                        removeList.Add(b);
                    }
                    foreach (BombObject b in removeList)
                    {
                        bombObjectList.Remove(b);
                    }
                }
            }
            if (level == 2)
            { 
            }
        }

        //collision controlled by different type of weapons and different enemy objects;
        void collisionControl()
        {
            KeyboardState kstate = Keyboard.GetState();
            List<BulletObject> bulletObjectList = playerObject.getBulletObjectList();           
            List<Torpedo> torpedoList = playerObject.getTorpedoList();
            foreach (AircraftCarrier ac in aircraftcarrierList)
            {
                 foreach (BulletObject b in bulletObjectList)
                 {               
                    Rectangle carrierBox = ac.getBox();
                    Rectangle bulletBox = b.getBox();
                    if (bulletBox.Intersects(carrierBox) && !ac.getDestroy())
                    {
                        if (ac.getHealth() > 0)
                        {
                            ac.reduceHealth(2);
                        }
                        if (ac.getHealth() > 0 && !ac.getHit())
                        {
                            hitbomberframe = framecount;
                            ac.setTexture(aircraftcarrierHitTexture);
                            ac.setHit(true);
                        }
                        if (ac.getHealth() == 0)
                        {
                            ac.setDestroy(true);
                            if (ac.getSink())
                            {
                                ac.update();
                                ac.setCount(true);
                                ac.setTexture2(gameObjectTexture2);
                                ac.setTexture3(gameObjectTexture3);
                                sink++;
                                gameInfoObject.addSink();
                                carriershowup = false;
                                takeofffighter = false;
                                framebetween = 0;
                                removeCarrierList.Add(ac);
                                ac.setSink(false);
                                bonusInfo.setInfo("+ 50 HP");
                                gameInfoObject.addHP(50);
                                bonusInfo.setLocation(b.getBox().X, b.getBox().Y);
                                bonusInfo.setShow(true);
                            }
                            else
                            { }
                        } 
                       bulletObjectRemoveList.Add(b);
                    }
                }
                 if (framecount == hitbomberframe + 3)
                 {
                     ac.setTexture(aircraftcarrierTexture);
                     ac.setHit(false);
                 }
            }
            foreach (AircraftCarrier ac in aircraftcarrierList)
            {
                foreach (Torpedo t in torpedoList)
                {              
                    Rectangle carrierBox = ac.getBox();
                    Rectangle torpedoBox = t.getBox();
                    if (torpedoBox.Intersects(carrierBox) && !ac.getDestroy())
                    {
                        if (ac.getHealth()>0)
                        {
                            ac.reduceHealth(70);
                        }
                        if (ac.getHealth() > 0 && !ac.getHit())
                        {
                            hitbomberframe = framecount;
                            ac.setTexture(aircraftcarrierHitTexture);
                            ac.setHit(true);
                        }
                        if (ac.getHealth() == 0)
                        {
                            ac.setDestroy(true);
                            if (ac.getSink())
                            {
                                ac.update();
                                ac.setCount(true);
                                ac.setTexture2(gameObjectTexture2);
                                ac.setTexture3(gameObjectTexture3);
                                sink++;
                                gameInfoObject.addSink();
                                carriershowup = false;
                                takeofffighter = false;
                                framebetween = 0;
                                removeCarrierList.Add(ac);
                                ac.setSink(false);
                                bonusInfo.setInfo("+ 50 HP");
                                gameInfoObject.addHP(50);
                                bonusInfo.setLocation(t.getBox().X, t.getBox().Y);
                                bonusInfo.setShow(true);
                            }
                        }
                        removeTorpedoList.Add(t);
                    }                  
                }
                if (framecount == hitbomberframe + 3)
                {
                    ac.setTexture(aircraftcarrierTexture);
                    ac.setHit(false);
                }
            }

            foreach (BattleShip bs in battleship)
            {
                foreach (BulletObject b in bulletObjectList)
                {               
                    Rectangle battleBox = bs.getBox();
                    Rectangle bulletBox = b.getBox();
                    if (bulletBox.Intersects(battleBox) && !bs.getDestroy())
                    {
                        if (bs.getHealth() > 0)
                        {
                            bs.reduceHealth(1);
                        }
                        if (bs.getHealth() > 0 && !bs.getHit())
                        {
                            hitbomberframe = framecount;
                            bs.setTexture(battleshipHittexture);
                            bs.setHit(true);
                        }
                        if (bs.getHealth() == 0)
                        {
                            bs.setDestroy(true);
                                bs.update();
                                bs.setCount(true);
                                bs.setTexture2(gameObjectTexture2);
                                bs.setTexture3(gameObjectTexture3);
                                battlesink = true;
                                removeBattleList.Add(bs);
                                framebetween = 0;
                        }
                        bulletObjectRemoveList.Add(b);
                    }                  
                }
                if (framecount == hitbomberframe + 3)
                {
                    bs.setTexture(battleshiptexture);
                    bs.setHit(false);
                }
            }

            foreach (BattleShip bs in battleship)
            {
                foreach (Torpedo t in torpedoList)
                {                
                    Rectangle battleBox = bs.getBox();
                    Rectangle torpedoBox = t.getBox();
                    if (torpedoBox.Intersects(battleBox) && !bs.getDestroy())
                    {
                        if (bs.getHealth() > 0)
                        {
                            bs.reduceHealth(40);
                        }
                        if (bs.getHealth() > 0 && !bs.getHit())
                        {
                            hitbomberframe = framecount;
                            bs.setTexture(battleshipHittexture);
                            bs.setHit(true);
                        }
                        if (bs.getHealth() == 0)
                        {
                            bs.setDestroy(true);
                                bs.update();
                                bs.setCount(true);
                                bs.setTexture2(gameObjectTexture2);
                                bs.setTexture3(gameObjectTexture3);
                                battlesink = true;
                                removeBattleList.Add(bs);
                                framebetween = 0;
                        }
                        removeTorpedoList.Add(t);
                    }                   
                }
                if (framecount == hitbomberframe + 3)
                {
                    bs.setTexture(battleshiptexture);
                    bs.setHit(false);
                }
            }

            foreach (GameObject g in gameObjectList)
                    {
                        foreach (BulletObject b in bulletObjectList)
                        {
                            Rectangle gameObjectBox = g.getBox();
                            Rectangle bulletObjectBox = b.getBox();
                            if (bulletObjectBox.Intersects(gameObjectBox) && !g.getCount())
                            {
                                g.update();
                                g.setCount(true);
                                g.setTexture2(gameObjectTexture2);
                                g.setTexture3(gameObjectTexture3);
                                gameObjectRemoveList.Add(g);

                                bulletObjectRemoveList.Add(b);
                                    gameInfoObject.addhit();
                            }
                        }
                    }

                    foreach (DestroyerObject1 d1 in destroyerobjcet1List)
                    {
                        foreach (BulletObject b in bulletObjectList)
                        {
                            Rectangle destroyerBox = d1.getBox();
                            Rectangle bulletObjectBox = b.getBox();
                            if (bulletObjectBox.Intersects(destroyerBox) && !d1.getDestroy())
                            {
                                d1.reduceHealth(10);
                                if (d1.getHealth() > 0 && !d1.getHit())
                                {
                                    hitbomberframe = framecount;
                                    d1.setTexture(destroyer1HitTexture);
                                    d1.setHit(true);
                                }
                                if (d1.getHealth() <= 0)
                                {
                                    d1.setDestroy(true);
                                    d1.update();
                                    d1.setCount(true);
                                    d1.setTexture2(gameObjectTexture2);
                                    d1.setTexture3(gameObjectTexture3);
                                    removeDestroyerList.Add(d1);

                                    int random = rand.Next(0, 10);
                                    if (random < 5)
                                    {
                                        bonusInfo.setInfo("+ 30 HP");
                                        gameInfoObject.addHP(30);
                                        bonusInfo.setLocation(b.getBox().X, b.getBox().Y);
                                    }
                                    else
                                    {
                                        bonusInfo.setInfo("+ 1 Torpedo");
                                        gameInfoObject.addTorpedo();
                                        playerObject.addTorpedo();
                                        bonusInfo.setLocation(b.getBox().X, b.getBox().Y);
                                    }
                                    bonusInfo.setShow(true);
                                }
                                bulletObjectRemoveList.Add(b);
                                // hit++;
                            }
                        }
                        if (framecount == hitbomberframe + 3)
                        {
                            d1.setTexture(destroyer1Texture);
                            d1.setHit(false);
                        }
                    }

                    foreach (DestroyerObject1 d1 in destroyerobjcet1List)
                    {
                        foreach (Torpedo t in torpedoList)
                        {
                            Rectangle destroyerBox = d1.getBox();
                            Rectangle torpedoBox = t.getBox();
                            if (torpedoBox.Intersects(destroyerBox) && !d1.getDestroy())
                            {
                                d1.reduceHealth(200);
                                if (d1.getHealth() <= 0)
                                {
                                    d1.setDestroy(true);
                                    d1.update();
                                    d1.setCount(true);
                                    d1.setTexture2(gameObjectTexture2);
                                    d1.setTexture3(gameObjectTexture3);
                                    removeDestroyerList.Add(d1);
                                }
                                removeTorpedoList.Add(t);

                                int random = rand.Next(0, 10);
                                if (random < 5)
                                {
                                    bonusInfo.setInfo("+ 30 HP");
                                    gameInfoObject.addHP(30);
                                    bonusInfo.setLocation(t.getBox().X, t.getBox().Y);
                                }
                                else
                                {
                                    bonusInfo.setInfo("+ 1 Torpedo");
                                    gameInfoObject.addTorpedo();
                                    playerObject.addTorpedo();
                                    bonusInfo.setLocation(t.getBox().X, t.getBox().Y);
                                }
                                bonusInfo.setShow(true);
                                // hit++;
                            }
                        }
                    }

                    foreach (BomberObject g in bomberObjectList)
                    {
                        foreach (BulletObject b in bulletObjectList)
                        {
                            Rectangle bomberObjectBox = g.getBox();
                            Rectangle bulletObjectBox = b.getBox();
                            if (bulletObjectBox.Intersects(bomberObjectBox) && !g.getDestroy())
                            {
                                
                                g.reducehealth();
                                if (g.getHealth() > 0 && !g.getHit())
                                {
                                    hitbomberframe = framecount;
                                    g.setTexture(bomberObjectHitTexture);
                                    g.setHit(true);
                                }
                                if (g.getHealth() == 0)
                                {
                                    g.setDestroy(true);
                                    g.update();
                                    g.setCount(true);
                                    g.setTexture2(gameObjectTexture2);
                                    g.setTexture3(gameObjectTexture3);
                                    bomberObjectRemoveList.Add(g);

                                        gameInfoObject.addhit();
                                        bonusInfo.setInfo("+ 30 HP");
                                        gameInfoObject.addHP(30);
                                        bonusInfo.setLocation(b.getBox().X, b.getBox().Y);
                                        bonusInfo.setShow(true);
                                }
                                bulletObjectRemoveList.Add(b);                               
                            }                          
                        }
                        if (framecount == hitbomberframe + 3)
                        {
                            g.setTexture(bomberObjectTexture);
                            g.setHit(false);
                        }
                    }
                    
                    //player will crash if he hits an enemy object;

                    foreach (GameObject g in gameObjectList)
                    {
                            if (playerObject.getBox().Intersects(g.getBox()))
                            {
                                
                                if (playerObject.getBox().Intersects(g.getBox()) && !g.getCount())
                                {
                                    gameInfoObject.uselife();
                                    playerObject.setBox();
                                    gameInfoObject.setHealth(100);
                                    g.update();
                                    g.setCount(true);
                                    g.setTexture2(gameObjectTexture2);
                                    g.setTexture3(gameObjectTexture3);
                                    gameObjectRemoveList.Add(g);
                                    gameInfoObject.addhit();
                                }                            
                            }
                        
                    }

                    foreach (BomberObject g in bomberObjectList)
                    {
                        if (playerObject.getBox().Intersects(g.getBox()))
                        {

                            if (playerObject.getBox().Intersects(g.getBox()) && !g.getCount())
                            {
                                gameInfoObject.uselife();
                                playerObject.setBox();
                                gameInfoObject.setHealth(100);
                                g.update();
                                g.setCount(true);
                                g.setTexture2(gameObjectTexture2);
                                g.setTexture3(gameObjectTexture3);
                                bomberObjectRemoveList.Add(g);
                                gameInfoObject.addhit();
                            }
                        }
                    }

                    foreach (BombObject b in bombObjectList)
                    {
                        
                            Rectangle bombObjectBox = b.getBox();
                            Rectangle gameObjectBox = playerObject.getBox();
                            if (gameObjectBox.Intersects(bombObjectBox))
                            {
                                bombObjectRemoveList.Add(b);
                                gameInfoObject.reduceHealth(20);
                                if (gameInfoObject.loselife())
                                {
                                    gameInfoObject.uselife();
                                    playerObject.setBox();
                                    gameInfoObject.setHealth(100);
                                    gameInfoObject.setLoseLife(false);
                                }
                            }
                        
                    }
                    if (kstate.IsKeyUp(Keys.F) && fireKeyDown)
                    {
                        gameInfoObject.reduceAmmo();
                        //gameInfoObject.reduceHealthfromShooting();
                        fireKeyDown = false;
                    }
                    if (kstate.IsKeyDown(Keys.F))
                    {
                        fireKeyDown = true;
                    }
                    if (level > 1)
                    {
                        if (kstate.IsKeyUp(Keys.D) && fireTorpedoDown)
                        {
                            playerObject.reduceTorpedo();
                            gameInfoObject.useTorpedo();
                            fireTorpedoDown = false;
                        }
                        if (kstate.IsKeyDown(Keys.D))
                        {
                            fireTorpedoDown = true;
                        }
                    }
                    foreach (GameObject g in gameObjectRemoveList)
                    {
                        if (g.getFrameCounter() > 9)
                        {
                            //  gameObjectRemoveList.Remove(g);
                            gameObjectList.Remove(g);
                            //collisionCount--;
                            //  break;
                        }
                        //gameInfoObject.setGameInfo("" + g.getFrameCounter());
                    }
                    foreach (BomberObject g in bomberObjectRemoveList)
                    {
                        if (g.getFrameCounter() > 9)
                        {
                            //  gameObjectRemoveList.Remove(g);
                            bomberObjectList.Remove(g);
                            //collisionCount--;
                            //  break;
                        }
                        //gameInfoObject.setGameInfo("" + g.getFrameCounter());
                    }
                    foreach (DestroyerObject1 d1 in removeDestroyerList)
                    {
                        if (d1.getFrameCounter() > 9)
                        {
                            //  gameObjectRemoveList.Remove(g);
                            destroyerobjcet1List.Remove(d1);
                            //collisionCount--;
                            //  break;
                        }
                        //gameInfoObject.setGameInfo("" + g.getFrameCounter());
                    }
                    foreach (AircraftCarrier ac in removeCarrierList)
                    {
                        if (ac.getFrameCounter() > 9)
                        {
                            //  gameObjectRemoveList.Remove(g);
                            aircraftcarrierList.Remove(ac);
                            //collisionCount--;
                            //  break;
                        }
                        //gameInfoObject.setGameInfo("" + g.getFrameCounter());
                    } 
           // foreach (GameObject g in gameObjectRemoveList) { gameObjectList.Remove(g); }
                    foreach (BattleShip bs in removeBattleList)
                    {
                        if (bs.getFrameCounter() > 9)
                        {
                            //  gameObjectRemoveList.Remove(g);
                            battleship.Remove(bs);
                            //collisionCount--;
                            //  break;
                        }
                    }
            foreach (BulletObject g in bulletObjectRemoveList) { bulletObjectList.Remove(g); }
            foreach (BombObject g in bombObjectRemoveList) { bombObjectList.Remove(g); }
          //  foreach (BomberObject g in bomberObjectRemoveList) { bomberObjectList.Remove(g); }
          //  foreach (DestroyerObject1 d1 in removeDestroyerList) { destroyerobjcet1List.Remove(d1); }
            foreach (Torpedo t in removeTorpedoList) { torpedoList.Remove(t); }
          //  foreach (AircraftCarrier ac in removeCarrierList) { aircraftcarrierList.Remove(ac); }

          //  foreach (BattleShip bs in removeBattleList) { battleship.Remove(bs); }
            if (gameInfoObject.getLifes() == 0)
            {
                gameInfoObject.setLOSE(true);
            }
        }
    }
}
