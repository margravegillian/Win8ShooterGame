using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Win8ShooterGame;
namespace Shooter
    
    
{
    class Player
    {
        #region Properties
        public Animation PlayerAnimation;
        //public Texture2D PlayerTexture;
        // Position of the Player relative to the upper left side of the screen

        public Vector2 Position;
        // State of the player

        public bool Active;

         // Amount of hit points that player has

        public int Health;

         // Get the width of the player ship

        public int Width
        {

            get { return PlayerAnimation.FrameWidth; }

        }



        // Get the height of the player ship

        public int Height
        {

            get { return PlayerAnimation.FrameHeight; }

        } 
        #endregion


        #region xna region
        public void Initialize(Animation animation, Vector2 position)
        {
            //PlayerTexture = texture;
            PlayerAnimation = animation;
            Position = position;
            Active = true;
            Health = 100;
        }

        public void Update(GameTime gameTime)
        {
            PlayerAnimation.Position = Position;
            PlayerAnimation.Update(gameTime);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            PlayerAnimation.Draw(spriteBatch);
        }
        #endregion
    }
}
