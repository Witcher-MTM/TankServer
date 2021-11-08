using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Client_Graphic
{
    public class Menu
    {
        public bool IsActive { get; set; }
        public Button BtnPlay { get; set; }
        public Button BtnSettings { get; set; }
        public Button BtnExit { get; set; }
        public SpriteFont text { get; set; }
        public bool Exit { get; set; }
        public Menu(Button btn , SpriteFont text)
        {
            BtnPlay = new Button(new Rectangle(300, 300, 300, 80), btn.Texture);
            BtnSettings = new Button(new Rectangle(600, 300, 300, 80), btn.Texture);
            BtnExit = new Button(new Rectangle(900, 300, 300, 80), btn.Texture);
            this.text = text;
            Exit = false;
            IsActive = false;
        }

        public void Draw(SpriteBatch spritebatch)
        {

            spritebatch.Draw(BtnPlay.Texture, BtnPlay.Rectangle, Color.Black);
            spritebatch.DrawString(text, "Play", new Vector2(420, 320), Color.White);
            spritebatch.Draw(BtnSettings.Texture, BtnSettings.Rectangle, Color.Black);
            spritebatch.DrawString(text, "Settings", new Vector2(690, 320), Color.White);
            spritebatch.Draw(BtnExit.Texture, BtnExit.Rectangle, Color.Black);
            spritebatch.DrawString(text, "Exit", new Vector2(1020, 320), Color.White);

        }
        public void CathClick()
        {
            if(IsActive == true)
            {
                var mouse = Mouse.GetState();
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    if (new Rectangle(mouse.X, mouse.Y, 5, 5).Intersects(BtnPlay.Rectangle))
                    {
                        IsActive = false;
                    }
                    if (new Rectangle(mouse.X, mouse.Y, 5, 5).Intersects(BtnSettings.Rectangle))
                    {
                        
                    }
                    if (new Rectangle(mouse.X, mouse.Y, 5, 5).Intersects(BtnExit.Rectangle))
                    {
                        Exit = true;
                    }
                }
            }
            
        }

    }
}
