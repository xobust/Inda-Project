﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Project_Broban
{
    public class Room : GameObject
    {
        int XPosition;
        int YPosition;
        private string[][] map;
        private const int mapSizeX = 12;
        private const int mapSizeY = 29;
        public List<Monster> monsters;
        public List<GameObject> SceneObjects;
        private Random rngGenerator;

        //Todo: calculate this
        private int MonsterAmount = 10;

        public Room(int xPosition, int yPosition)
        {
            monsters = new List<Monster>();
            SceneObjects = new List<GameObject>();
            rngGenerator = new Random();
            
            XPosition = xPosition;
            YPosition = yPosition;
            map = new string[mapSizeX][];

            for (int x = 0; x < mapSizeX; x++)
            {
                map[x] = new string[mapSizeY];
                for (int y = 0; y < mapSizeY; y++)
                {
                    map[x][y] = null;
                }
            }

            SpawnMonsters();
        }

        /// <summary>
        /// Generates the room map
        /// </summary>
        /// <returns></returns>
        public void Generate()
        {
            for (int x = 0; x < mapSizeX; x++)
            {
                for (int y = 0; y < mapSizeY; y++)
                {
                    map[x][y] = "Grass";
                }
            }
        }

        public void SpawnMonsters()
        {
            for (int i = 0; i < MonsterAmount; i++)
            {
                Monster tempmonster = new Monster(rngGenerator.Next(0,700),
                                          rngGenerator.Next(0,400));
                monsters.Add(tempmonster);
                SceneObjects.Add(tempmonster);
            }
            //Test that bothe monster and SceneObjects carry the same reference
            //Console.WriteLine("ReferenceEquals(a, b) = {0}", 
            //      Object.ReferenceEquals(monsters[0], SceneObjects[0]));

        }
        /// <summary>
        /// Updates the state of the gameobject
        /// </summary>
        public void Update()
        {
            foreach (GameObject obj in SceneObjects)
            {
                obj.Update();
            }
        }

        /// <summary>
        /// Renders the gameobject to the screen using the spritebatch
        /// </summary>
        public void Draw(SpriteBatch sb)
        {
            TileRenderer.Instance.Draw(sb, map);
            foreach (GameObject obj in SceneObjects)
            {
                obj.Draw(sb);
            }
        }

        /// <summary>
        /// Loads necessary content of the gameobject
        /// </summary>
        public void LoadContent(GraphicsDevice gd, ContentManager cm)
        {
        }

        /// <summary>
        /// Unloads content from the gameobject
        /// </summary>
        public void UnloadContent()
        {

        }
    }
}