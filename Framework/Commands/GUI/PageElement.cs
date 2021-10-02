using System;

namespace PrintHub.Framework.Commands.GUI
{
    public class PageElement
    {
        public string Formatting;
        public string[] Content;
        public bool Highlight;
        public bool NewLine;
        public ConsoleColor? Color;

        public PageElement(string Formatting, string[] Content, bool Highlight = false, bool NewLine = false, ConsoleColor? Color = null)
        {
            this.Formatting = Formatting;
            this.Content = Content;
            this.Highlight = Highlight;
            this.Color = Color;
            this.NewLine = NewLine;
        }

        public void Print()
        {
            
            if (Highlight)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
            }
            else
                Console.ResetColor();

            //if (Color.HasValue)
             //   Console.ForegroundColor = Color.Value;

            Console.Write(Formatting + (NewLine ? "\n" : ""), Content);
        }

        public static PageElement StringConverter(string text)
        {
            return new PageElement("{0}", new string[] { text });
        }
    }
}