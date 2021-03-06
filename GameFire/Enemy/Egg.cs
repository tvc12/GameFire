﻿using GameFire.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameFire.Enemy
{
    public class Egg : GameObject
    {
        #region Properties
        private TypeChiken type;
        private bool isBreak;
        private sbyte indexNow;
        private Texture2D textureBreak;
        private Rectangle desRectBreak;
        private Rectangle sourceRectBreak;

        private SoundEffect soundBreak;

        public bool IsBreak { get => isBreak; private set => isBreak = value; }

        #endregion

        #region Constructor
        public Egg(ContentManager content, Vector2 speed, Vector2 index, Rectangle location, TypeChiken type) : base(content, speed, index, location)
        {
            this.type = type;
            IsBreak = false;
            Load();
        }
        #endregion

        #region Load & Unload
        protected override void Load()
        {
            switch ((int)type)
            {
                case 1:
                    _skin = _content.Load<Texture2D>("Enemy/egg");
                    textureBreak = _content.Load<Texture2D>("Enemy/eggBreak");

                    break;
                case 2:
                    _skin = _content.Load<Texture2D>("Enemy/egg_1");
                    textureBreak = _content.Load<Texture2D>("Enemy/eggBreak_1");
                    break;
                default:
                    _skin = _content.Load<Texture2D>("Enemy/egg");
                    textureBreak = _content.Load<Texture2D>("Enemy/eggBreak");
                    break;
            }
            if (_desRectSkin.Size == Point.Zero)
            {
                _desRectSkin.Width = _skin.Width;
                _desRectSkin.Height = _skin.Height;
            }
                switch (_random.Next(0,2))
            {
                case 0:
                    soundBreak = _content.Load<SoundEffect>("Music/chicken/Egg_break");
                    break;
                default:
                    soundBreak = _content.Load<SoundEffect>("Music/chicken/Egg_break1");
                    break;
            }
            desRectBreak = new Rectangle(0, 0, textureBreak.Bounds.Width / 8, textureBreak.Height);
            sourceRectBreak = desRectBreak;
        }
        protected override void Unload()
        {
            textureBreak = null;
            soundBreak = null ;
            base.Unload();
        }
        #endregion

        #region Method
        public override void Update(GameTime gameTime)
        {
            if (!_visible) return;
            else
            {
                if(IsBreak)
                {
                    AnimationBreak(gameTime);
                }
                else
                {
                    if (_desRectSkin.Y < (_index.Y * 97))
                    {
                        _desRectSkin.Y +=(int) _speed.Y;
                    }
                    else
                    {
                        IsBreak = true;
                        soundBreak.Play(0.75f, 0, 0);
                        desRectBreak.Location = _desRectSkin.Location;
                    }
                }
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Color color)
        {
            if (!_visible) return;
            else
            {
                if(IsBreak)
                {                   
                    spriteBatch.Draw(textureBreak, desRectBreak, sourceRectBreak, color);
                }
                else
                {
                    base.Draw(gameTime, spriteBatch, color);
                }
            }
        }
        private void AnimationBreak(GameTime gameTime)
        {
            _totalTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_totalTime >= 70.0f)
            {
                if (indexNow >= 9)
                {
                    this._visible = false;
                    IsBreak = false;
                }
                else
                {
                    _totalTime = 0;
                    sourceRectBreak.X = desRectBreak.Width * ++indexNow;
                }
            }
        }
        #endregion

        #region Destructor
        ~Egg()
        {
            Unload();
        }
        #endregion
    }
}
