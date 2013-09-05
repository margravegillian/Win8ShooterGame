using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Win8ShooterGame
{
    class ParalaxingBackground
    {

        #region Fields
        Texture2D texture;
        Vector2[] positions;
        int speed;
        int bgHeight;
        int bgWidth;
       

        #endregion

        #region methods

        public void Initialize(ContentManager content,string texturePath,int screenWidth,int screenHeight,int speed)
        {
           

            texture = content.Load<Texture2D>(texturePath);
            this.speed = speed;
            bgHeight = screenHeight;

            bgWidth = screenWidth;

            int numOfTiles = (int)(Math.Ceiling(bgWidth / (float)texture.Width) + 1);
            positions = new Vector2[numOfTiles];
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = new Vector2(i * texture.Width,0);


            }
        }

        public void Update(GameTime gameTime)
        {

            // Update the positions of the background

            for (int i = 0; i < positions.Length; i++)
            {

                // Update the position of the screen by adding the speed

                positions[i].X += speed;

                // If the speed has the background moving to the left

               // if (speed <= 0)
             //   {
//
                    // Check the texture is out of view then put that texture at the end of the screen

                 //   if (positions[i].X <= -texture.Width)
                  //  {

                  //      positions[i].X = texture.Width * (positions.Length - 1);

                   // }

               // }

                // If the speed has the background moving to the right

              //  else
              //  {

                    // Check if the texture is out of view then position it to the start of the screen

               //     if (positions[i].X >= texture.Width * (positions.Length - 1))
                //    {

                //        positions[i].X = -texture.Width;
//
            //        }

               // }

            } 

            for (int i=0;i<positions.Length;i++)
            {
                if (speed <= 0)
                {
                    //check if the texutre is out of view and the put that texture at the end of screen
                    if (positions[i].X <= -texture.Width)
                    {
                        WrapTextureToLeft(i);

                    }
                }
                else
                {
                    if (positions[i].X >= texture.Width * (positions.Length -1))
                    {
                        WrapTextureToRight(i);
                    }
                }
            }

        }

        private void WrapTextureToLeft(int index)
        {
            int prevTexture = index - 1;
            if (prevTexture <0)
                prevTexture = positions.Length -1;
            positions[index].X = positions[prevTexture].X + texture.Width;
        }


        private void WrapTextureToRight(int index)
        {
            int nextTexture = index + 1;
            if (nextTexture == positions.Length)
                nextTexture = 0;
            positions[index].X = positions[nextTexture].X - texture.Width;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < positions.Length; i++)
            {


                Rectangle rectBg = new Rectangle((int)positions[i].X, (int)positions[i].Y, bgWidth, bgHeight);
                spriteBatch.Draw(texture, rectBg, Color.White);

            } 

        }
        #endregion
    }
}
