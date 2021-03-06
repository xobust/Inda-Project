﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Project_Broban
{
    class UIController : Controller
    {
        GameManager gameManager; 
        Player player;
        private Texture2D heartTexture;
        private int heartWidth;
        private int heartHeight;
        private int leftOffsetX = 10; // Adds extra space to the left of the hearts
        private TimeSpan playTime;
        private SpriteFont font;      // The font to display the time with

        public UIController(GameManager gameManager)
        {
            this.gameManager = gameManager;
            font = gameManager.font;
            player = gameManager.player;
        }

        /// <summary>
        /// Updates the player reference and gets the playtime from GameManager.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            playTime = gameManager.playTime;
        }

        /// <summary>
        /// Draws the player health in the upper left corner and
        /// the playtime in the upper right corner.
        /// </summary>
        /// <param name="sb">The spriteBatch to draw with.</param>
        public void Draw(SpriteBatch sb)
        {
            font = gameManager.font;
            for (int i = 0; i < player.hp; i++)
            {
                Rectangle destRectangle = new Rectangle(heartWidth * i + leftOffsetX, 0, heartWidth, heartHeight);
                
                sb.Draw(heartTexture, destRectangle, null, Color.White, 0, Vector2.Zero,
                    SpriteEffects.None, 1);
            }

            DrawTimer(sb);
        }

        /// <summary>
        /// Formats the playtime from milliseconds to hh:mm:ss and draws
        /// it on the screen in the upper right corner.
        /// </summary>
        /// <param name="sb">The SpriteBatch to draw with.</param>
        private void DrawTimer(SpriteBatch sb)
        {
            // Format the time
            string outputTime = playTime.ToString(@"hh\:mm\:ss");
            Vector2 textSize = font.MeasureString(outputTime);
            
            // Display formatted time
            Vector2 timerOrigin = new Vector2(textSize.X, 0);
            sb.DrawString(font, outputTime, new Vector2(GameManager.screenWidth - 50, 30),
                Color.White, 0, timerOrigin, 1, SpriteEffects.None, 1);
        }

        /// <summary>
        /// Loads necessary content of the gameObject.
        /// </summary>
        /// <param name="gd">The GraphicsDevice from GameManager.</param>
        /// <param name="cm">The ContentManager from GameManager.</param>
        public void LoadContent(GraphicsDevice gd, ContentManager cm)
        {
            heartTexture = cm.Load<Texture2D>("heart");
            heartWidth = heartTexture.Width;
            heartHeight = heartTexture.Height;
        }
    }
}
