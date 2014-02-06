using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using NetVolveLib.Grid;
using NetVolveLib.Parameters;

namespace NetVolveCGUI
{
    class Program
    {

        #region WinAPI

        [DllImport("Kernel32")]
        public static extern bool SetConsoleCtrlHandler(EventHandler Handler, bool Add);
        public delegate bool EventHandler(CtrlTypes CtrlType);
        public enum CtrlTypes
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT
        }

        #endregion

        private const int WindowHeight = 52;
        private const int WindowWidth = 99;
        private const int Threads = 4;

        public static Dictionary<Color, WarriorColor> Colors = new Dictionary<Color, WarriorColor>();
        public static Parameter Parameter = ParameterLoader.FromFile("settings.cfg");
        public static Grid Grid;

        public static BufferedConsole BufferedConsole = new BufferedConsole(WindowWidth, WindowHeight);

        public static DateTime Started = DateTime.Now;

        public static int SelectedWarrior;
        public static GridWarrior[] CurrentWarriors;

        static EventHandler _handler;

        public static bool Closing = false;

        static void Main(string[] args)
        {

            Console.Title = "NetVolve ConsoleGUI";

            _handler += new EventHandler(ProcessExit);
            SetConsoleCtrlHandler(_handler, true);

            if (File.Exists("save.bin"))
                Grid = GridSerializer.Load("save.bin");
                if(File.Exists("save_b.bin"))
                    LoadColors();
            else
                Grid = new Grid(Parameter);

            Grid.StartAsync(Threads);
            int i = 0;
            while (!Closing)
            {
                CheckInput();
                DrawGrid();
                DrawRanking();
                DrawInfo();
                DrawBest();
                BufferedConsole.Write("|NetVolve 0.2 by BigJk|", 3, 51);
                BufferedConsole.Flush();
                Thread.Sleep(1);
                if(i % 1000 == 0)
                    Save();
                i++;
            }

            SaveColors();
            GridSerializer.Save("save.bin", Grid);

        }

        static void CheckInput()
        {
            if (!Console.KeyAvailable) return;
            ConsoleKey k = Console.ReadKey(false).Key;

            switch (k)
            {
                case ConsoleKey.UpArrow:
                    if (SelectedWarrior != 0) SelectedWarrior--;
                    break;
                case ConsoleKey.DownArrow:
                    if (SelectedWarrior < 24) SelectedWarrior++;
                    break;
                case ConsoleKey.P:
                    Pause();
                    break;
                case ConsoleKey.S:
                    Save();
                    break;
                case ConsoleKey.E:
                    if (!paused)
                        Pause();
                    GridWarrior[] w = Grid.GetWarriors(20);
                    Directory.CreateDirectory("exported");
                    for (int i = 0; i < w.Length; i++)
                    {
                        File.WriteAllText("exported\\" + i + w[i].Warrior.Name, w[i].Warrior.ToString());
                    }
                    Pause();
                    break;
            }
        }

        private static bool paused = false;
        static void Pause()
        {
            if (paused)
            {
                Grid.StartAsync(Threads);
                paused = !paused;
                return;
            }
            Grid.StopAsync();
            paused = !paused;
        }

        static void Save()
        {
            Grid.StopAsync();
            GridSerializer.Save("save.bin", Grid);
            SaveColors();
            Grid.StartAsync(Threads);
        }

        static void SaveColors()
        {
            using (FileStream fileStream = new FileStream("save_b.bin", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, Colors);
            }
        }

        static void LoadColors()
        {
            using (FileStream fileStream = new FileStream("save_b.bin", FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                Colors = (Dictionary<Color, WarriorColor>)binaryFormatter.Deserialize(fileStream);
            }
        }

        static bool ProcessExit(CtrlTypes t)
        {
            Grid.StopAsync();
            Closing = true;
            return true;
        }
        
        static void DrawFrame(int x, int y, int width, int height)
        {

            BufferedConsole.SetColor(15);

            string corner = "+" + new string('-', width - 2) + "+";
            string empty = "|" + new string(' ', width - 2) + "|";

            BufferedConsole.Write(corner, x, y);

            for (int i = 1; i < height; i++)
            {
                BufferedConsole.Write(empty, x, y + i);
            }

            BufferedConsole.Write(corner, x, y + height);

        }

        private static Dictionary<Color, List<Point>> GetColorMap()
        {
            Dictionary<Color, List<Point>> colors = new Dictionary<Color, List<Point>>();
            for (int y = 0; y < Grid.Size; y++)
            {
                for (int x = 0; x < Grid.Size; x++)
                {
                    if (colors.ContainsKey(Grid.Cells[y, x].Owner.Color))
                    {
                        colors[Grid.Cells[y, x].Owner.Color].Add(new Point(x, y));
                    }
                    else
                    {
                        colors.Add(Grid.Cells[y, x].Owner.Color, new List<Point>() {new Point(x, y)});
                    }
                }
            }
            return colors;
        }

        static void DrawGrid()
        {
            DrawFrame(0, 0, 52, 51);
            BufferedConsole.Write("|Grid|", 3, 0);

            int Offset = 25 - (Grid.Size / 2);

            Dictionary<Color, List<Point>> colors = GetColorMap();
            foreach (KeyValuePair<Color, List<Point>> color in colors)
            {
                WarriorColor w = GetColor(color.Key);
                BufferedConsole.Write(w.Char.ToString(), color.Value[0].X + 1 + Offset, color.Value[0].Y + 1 + Offset, w.Color);
                for (int i = 1; i < color.Value.Count; i++)
                {
                    BufferedConsole.Write(w.Char.ToString(), color.Value[i].X + 1 + Offset, color.Value[i].Y + 1 + Offset);
                }
            }
            //for (int y = 0; y < Grid.Size; y++)
            //{
            //    for (int x = 0; x < Grid.Size; x++)
            //    {
            //        WarriorColor w = GetColor(Grid.Cells[y, x].Owner.Color);
            //        BufferedConsole.Write(w.Char.ToString(), 1 + x, y + 1, w.Color);
            //    }
            //}
        }

        static void DrawRanking()
        {
            DrawFrame(51, 0, 47, 28);
            BufferedConsole.Write("|Ranking|", 53, 0);
            CurrentWarriors = Grid.GetWarriors(25);

            for (int i = 0; i < CurrentWarriors.Length; i++)
            {
                WarriorColor color = GetColor(CurrentWarriors[i].Color);
                BufferedConsole.Write(color.Char.ToString(), 53, i + 2, color.Color);

                string main = CurrentWarriors[i].OwnedCells.Count + " " + CurrentWarriors[i].Warrior.Name;
                if (CurrentWarriors[i].Warrior.Name.Length > 15)
                    main = CurrentWarriors[i].OwnedCells.Count + " " + CurrentWarriors[i].Warrior.Name.Substring(0, 15);
                string second = "by " + CurrentWarriors[i].Warrior.Author;
                if (CurrentWarriors[i].Warrior.Author.Length > 20)
                    second = "by " + CurrentWarriors[i].Warrior.Author.Substring(0, 20) + "...";

                BufferedConsole.SetColor(15);
                if (CurrentWarriors[i].Warrior.Name.EndsWith("PRE"))
                {
                    BufferedConsole.Write(main, 55, i + 2, 10);
                    BufferedConsole.SetColor(15);
                }
                else
                {
                    BufferedConsole.Write(main, 55, i + 2);
                }

                if (main.Length + second.Length + 6 >= 44)
                {

                    int v = ((main.Length + second.Length + 6) - 44) + 3;

                    second = second.Remove(second.Length - 7) + "...";
                }

                BufferedConsole.Write(second, 56 + main.Length, i + 2, 8);
                BufferedConsole.SetColor(15);
            }
        }

        static void DrawInfo()
        {
            DrawFrame(51, 28, 47, 5);
            BufferedConsole.Write("|Info|", 53, 28);
            BufferedConsole.Write("Warrior Count: " + Grid.Warriors.Count, 53, 30);
            TimeSpan elapsed = DateTime.Now - Started;
            BufferedConsole.Write("Elapsed Time:  " + elapsed.Hours + ":" + elapsed.Minutes + ":" + elapsed.Seconds, 53, 31);
        }

        static void DrawBest()
        {
            DrawFrame(51, 33, 47, 18);
            BufferedConsole.Write("|" + CurrentWarriors[SelectedWarrior].Warrior.Name + "|", 53, 33);
            WarriorColor wColor = GetColor(CurrentWarriors[SelectedWarrior].Color);
            BufferedConsole.Write(wColor.Char.ToString(), 53 + ("|" + CurrentWarriors[SelectedWarrior].Warrior.Name + "|").Length, 33, wColor.Color);
            BufferedConsole.SetColor(15);

            for (int i = 0; i < 15; i++)
            {
                if (i >= CurrentWarriors[SelectedWarrior].Warrior.CodeLines.Length) { break; }
                BufferedConsole.Write(CurrentWarriors[SelectedWarrior].Warrior.CodeLines[i].ToString(), 53, 35 + i);
            }

            for (int i = 0; i < 15; i++)
            {
                if (i + 15 >= CurrentWarriors[SelectedWarrior].Warrior.CodeLines.Length) { break; }
                BufferedConsole.Write(CurrentWarriors[SelectedWarrior].Warrior.CodeLines[i + 15].ToString(), 53 + 22, 35 + i);
            }
        }

        static WarriorColor GetColor(Color w)
        {
            if (Colors.ContainsKey(w))
                return Colors[w];
            Colors.Add(w, new WarriorColor());
            return Colors[w];
        }

    }
}
