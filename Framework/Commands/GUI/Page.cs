using System;
using System.Collections.Generic;

namespace PrintHub.Framework.Commands.GUI
{
    public class Page
    {
        public PageLayout Layout;
        public PageElement Header;
        public List<PageElement> Footer;
        public PageElement[,] Elements;
        public int SelectedX;
        public int SelectedY;
        public bool ExitPage;
        public bool WaitOnInfo;
        public bool IncludeBranding;
        private string _branding = @"  _                     " + "\n" + @" /_/ _ . _ _/_ /_/    /_" + "\n" + @"/   / / / //  / / /_//_/ by Jacob DeJean";

        public Page(PageLayout Layout, string ContentFormatting, string[,,] Contents)
        {
            this.Layout = Layout;

            this.Elements = new PageElement[Contents.GetLength(0), Contents.GetLength(1)];

            CreateElements(ContentFormatting, Contents);

            ExitPage = false;
        }

        public void SetHeader(string pageName, string pageDescription)
        {
            Header = new PageElement("    {0,-40}    ", new string[] { string.Format("[{0}] {1}", pageName, pageDescription) }, true, true);
        }

        public void SetFooter(string escMessage, string enterMessage)
        {
            Footer = new List<PageElement>();
            Footer.Add(new PageElement("{0}", new string[] { "[esc]" }, true));
            Footer.Add(new PageElement(" {0} ", new string[] { escMessage }, false, false, ConsoleColor.DarkGray));
            Footer.Add(new PageElement("{0}", new string[] { "[enter]" }, true));
            Footer.Add(new PageElement(" {0}", new string[] { enterMessage }, false, true, ConsoleColor.DarkGray));
        }

        public Coordinate Execute()
        {
            Coordinate Choice = Coordinate.Empty;

            while (!ExitPage)
            {
                Console.ResetColor();
                Console.Clear();

                if (IncludeBranding)
                    Console.WriteLine(_branding);

                Header.Print();

                Print();

                if (Layout == PageLayout.Info && !WaitOnInfo)
                    return Choice;

                Footer.ForEach((p) => p.Print());

                ConsoleKey key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        SelectedX -= SelectedX > 0 ? 1 : 0;
                        break;
                    case ConsoleKey.DownArrow:
                        SelectedX += SelectedX < Elements.GetLength(0) - 1 ? 1 : 0;
                        break;
                    case ConsoleKey.LeftArrow:
                        SelectedY -= SelectedY > 0 ? 1 : 0;
                        break;
                    case ConsoleKey.RightArrow:
                        SelectedY += SelectedY < Elements.GetLength(0) - 1 ? 1 : 0;
                        break;
                    case ConsoleKey.Enter:
                        {
                            Choice = new Coordinate(SelectedX, SelectedY);
                            ExitPage = true;
                            break;
                        }
                    case ConsoleKey.Escape:
                        {
                            ExitPage = true;
                            break;
                        }
                    default:
                        break;
                }
            }

            return Choice;
        }

        public void CreateElements(string formatting, string[,,] contents)
        {
            for (int x = 0; x < Elements.GetLength(0); x++)
            {
                for (int y = 0; y < Elements.GetLength(1); y++)
                {
                    Elements[x, y] = new PageElement(formatting, ExtractZ(x, y, contents));
                }
            }
        }

        public string[] ExtractZ(int x, int y, string[,,] content)
        {
            string[] buffer = new string[content.GetLength(2)];

            for (int i = 0; i < content.GetLength(2); i++)
            {
                buffer[i] = content[x, y, i];
            }

            return buffer;
        }

        public void Print()
        {
            switch (Layout)
            {
                case PageLayout.Grid:
                    Grid();
                    break;
                case PageLayout.List:
                    List();
                    break;
                case PageLayout.Info:
                    Info();
                    break;
            }
        }

        public void Grid()
        {
            for (int x = 0; x < Elements.GetLength(0); x++)
            {
                for (int y = 0; y < Elements.GetLength(1); y++)
                {
                    PageElement element = Elements[x, y];

                    element.Highlight = SelectedX == x && SelectedY == y;

                    element.Print();

                }
                Console.Write("\n");
            }
        }

        public void List()
        {
            for (int x = 0; x < Elements.GetLength(0); x++)
            {
                PageElement element = Elements[x, 0];

                element.Highlight = SelectedX == x;

                element.Print();

                Console.Write("\n");
            }
        }

        public void Info()
        {
            PageElement element = Elements[0, 0];

            element.Print();
        }
    }
}