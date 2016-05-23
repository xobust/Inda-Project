﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Project_Broban
{
    
    public enum Direction
    {
        Up, Down, Left, Right
    }

    public class Player : GameObject
    {
        public Direction PlayerDirection;
        private Texture2D PlayerTexture;
        private int TextureHeight;
        private int TextureWidth;
        private int SpriteRow = 0; // Which row of the spritesheet to draw
        private float Size;
        public int hp;
        public Vector2 Position;
        public Vector2 TileStartPos;
        public Vector2 NextPos;

        public Boolean MovingRight;
        public Boolean MovingLeft;
        public Boolean MovingUp;
        public Boolean MovingDown;

        public float MoveSpeed;

        private Vector2 Origin;
        private float Depth;

        public bool Attacking;
        public int AttackRange;
        public TimeSpan AttackTime;
        int AttackDamage;

        public Player(Vector2 tileStartPos)
        {
            TileStartPos = tileStartPos;
            PlayerDirection = Direction.Down;
            Size = 1;
            hp = 5;
            MoveSpeed = 0.1f;
            Attacking = false;
            AttackRange = 150;
            AttackTime = new TimeSpan(0,0,0,0,200);
            AttackDamage = 1;
        }

        public void Attack(Monster monster)
        {
            monster.TakeDamage(AttackDamage);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (!Attacking)
            {
                if (state.IsKeyDown(Keys.W))
                {
                    MovingUp = true;
                }
                if (state.IsKeyDown(Keys.A))
                {
                    MovingLeft = true;
                }
                if (state.IsKeyDown(Keys.S))
                {
                    MovingDown = true;
                }
                if (state.IsKeyDown(Keys.D))
                {
                    MovingRight = true;
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            // The original texture should have the origin in the middle-bottom
            Origin = new Vector2((TextureWidth * Size) / 2,
                                 (TextureHeight/4 * Size));

            Depth = (Position.Y / GameManager.screenHeight);
            Depth = MathHelper.Clamp(Depth, 0.01f, 1);

            switch (PlayerDirection)
            {
                case Direction.Down:
                    SpriteRow = 0;
                    break;
                case Direction.Up:
                    SpriteRow = 1;
                    break;
                case Direction.Right:
                    SpriteRow = 2;
                    break;
                case Direction.Left:
                    SpriteRow = 3;
                    break;
            }
            Rectangle SourceRect = new Rectangle(0, SpriteRow * TextureHeight / 4, TextureWidth, TextureHeight / 4);
            
            sb.Draw(PlayerTexture, Position, SourceRect, Color.White, 0,
                    Origin, Size, SpriteEffects.None, Depth);
        }

        public void LoadContent(GraphicsDevice gd, ContentManager cm)
        {
            PlayerTexture = cm.Load<Texture2D>("player");
            TextureHeight = PlayerTexture.Height;
            TextureWidth = PlayerTexture.Width;
        }

        public void UnloadContent()
        {
        }

    }
}
