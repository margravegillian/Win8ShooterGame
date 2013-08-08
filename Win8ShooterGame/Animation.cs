using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Win8ShooterGame
{
    class Animation
    {
        //the image collection
        Texture2D spriteStrip;
        //the scale used to display sprite strip
        public float scale;
        //the time since we last updated the frame
        int elapsedTime;
        //the time we display the frame until next one
        int frameTime;
        // the number of frames the animation conatins
        int frameCount;
        // the index of the current fame we are displaying
        int currentFrame;
        //the color of the frame we will be displaying
        Color color;
        //the area of the image strip we want to display
        Rectangle sourceRect = new Rectangle();
        //the area where we want to display the image strip in the game
        Rectangle destinationRect = new Rectangle();
        //width of a given frame
        public int FrameWidth;
        //height of a given frame;
        public int FrameHeight;
        //the state of the animation
        public bool Active;
        // determins if the animation will keep playing or deactivate after one run
        public bool Looping;
        //position of given frame
        public Vector2 Position;

        public void Initialize(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int frameCount, int frameTime, Color color, float scale, bool looping)
        {
            this.color = color;
            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;
            this.frameCount = frameCount;
            this.frameTime = frameTime;
            this.scale = scale;
            Looping = looping;
            Position = position;
            spriteStrip = texture;
            //set the time to zero
            elapsedTime = 0;
            currentFrame = 0;
            //set the animation to active by default
            Active = true;
        }
        public void Update(GameTime gameTime)
        {
            //do not update teh game if we are not active
            if (Active == false) return;
            //update the elappsed time
            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            //if the elapsed time is larger than the frame time
            //we need to switch frames
            if (elapsedTime > frameTime)
            {
                //move to the next frame
                currentFrame++;

                //if the currentFrame is equal to framecount reset current frame to zero
                if (currentFrame == frameCount)
                {
                    currentFrame = 0;
                    //if we are not looping deactive the animation
                    if (Looping == false)
                        Active = false;
                }
                //reset the elapsed time to zero
                elapsedTime = 0;
            }
            //grab the correct frme in the image strip by multiplying the current frame index by 
            //frame width
            sourceRect = new Rectangle(currentFrame * FrameWidth, 0, FrameWidth, FrameHeight);
            // Grab the correct frame in the image strip by multiplying the currentFrame index by the frame width

          //  destinationRect = new Rectangle((int)Position.X - (int)(FrameWidth * scale) / 2,(int)Position.Y - (int)(FrameHeight * scale) / 2,
         //  (int)(FrameWidth * scale),(int)(FrameHeight * scale));

            destinationRect = new Rectangle((int)Position.X - (int)(FrameWidth * scale) / 2, (int)Position.Y - (int)(FrameHeight * scale) / 2, 
            (int)(FrameWidth * scale), (int)(FrameHeight * scale));
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                spriteBatch.Draw(spriteStrip, destinationRect, sourceRect, color);

            }

        }
    }
}
