using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace NetVolveCGUI
{
    class BufferedConsole
    {

        #region WinAPI

        private const int GENERIC_READ = unchecked((int) 0x80000000);
        private const int GENERIC_WRITE = 0x40000000;

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleTextAttribute(
            IntPtr hConsoleOutput,
            uint wAttributes); /* declaring the setconsoletextattribute function*/

        private enum CharacterAttributes
        {
            FOREGROUND_BLUE = 0x0001,
            FOREGROUND_GREEN = 0x0002,
            FOREGROUND_RED = 0x0004,
            FOREGROUND_INTENSITY = 0x0008,
            BACKGROUND_BLUE = 0x0010,
            BACKGROUND_GREEN = 0x0020,
            BACKGROUND_RED = 0x0040,
        }

        [DllImport("Kernel32.dll")]
        private static extern IntPtr CreateConsoleScreenBuffer(
            int dwDesiredAccess, int dwShareMode,
            IntPtr secutiryAttributes,
            UInt32 flags,
            IntPtr screenBufferData);

        [DllImport("kernel32.dll")]
        private static extern IntPtr SetConsoleActiveScreenBuffer(IntPtr hConsoleOutput);

        [DllImport("kernel32.dll")]
        private static extern bool WriteConsole(
            IntPtr hConsoleOutput, string lpBuffer,
            uint nNumberOfCharsToWrite, out uint lpNumberOfCharsWritten,
            IntPtr lpReserved);

        [StructLayout(LayoutKind.Sequential)]
        private struct COORD
        {
            public short X;
            public short Y;

            public COORD(short X, short Y)
            {
                this.X = X;
                this.Y = Y;
            }
        };

        private const int STD_OUTPUT_HANDLE = -11;

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private extern static bool GetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool bMaximumWindow, [In, Out] CONSOLE_FONT_INFOEX lpConsoleCurrentFont);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetCurrentConsoleFontEx(
            IntPtr ConsoleOutput,
            bool MaximumWindow,
            CONSOLE_FONT_INFOEX ConsoleCurrentFontEx
            );

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private class CONSOLE_FONT_INFOEX
        {
            private int cbSize;
            public CONSOLE_FONT_INFOEX()
            {
                cbSize = Marshal.SizeOf(typeof(CONSOLE_FONT_INFOEX));
            }
            public int FontIndex;
            public short FontWidth;
            public short FontHeight;
            public int FontFamily;
            public int FontWeight;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string FaceName;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CONSOLE_CURSOR_INFO
        {
            public uint Size;
            public bool Visible;
        }

        [DllImport("kernel32.dll")]
        static extern bool SetConsoleCursorInfo(IntPtr hConsoleOutput,
           [In] ref CONSOLE_CURSOR_INFO lpConsoleCursorInfo);

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleCursorPosition(IntPtr hConsoleOutput,
            COORD dwCursorPosition);

        #endregion

        private IntPtr _ptrConsole1;
        private IntPtr _ptrConsole2;
        private bool _selectedConsole;
        private CONSOLE_FONT_INFOEX font = new CONSOLE_FONT_INFOEX();

        public BufferedConsole(int width, int height)
        {

            Console.WindowHeight = height;
            Console.WindowWidth = width;
            Console.BufferHeight = height;
            

            CONSOLE_CURSOR_INFO cci = new CONSOLE_CURSOR_INFO();
            cci.Size = 1;
            cci.Visible = false;

            _selectedConsole = true;
            _ptrConsole1 = CreateConsoleScreenBuffer( GENERIC_READ | GENERIC_WRITE, 0x3, IntPtr.Zero, 1, IntPtr.Zero);
            _ptrConsole2 = CreateConsoleScreenBuffer( GENERIC_READ | GENERIC_WRITE, 0x3, IntPtr.Zero, 1, IntPtr.Zero);

            
            font.FaceName = "Terminal";
            font.FontFamily = 48;
            font.FontHeight = 8;
            font.FontIndex = 1;
            font.FontWeight = 700;
            font.FontWidth = 6;

            SetCurrentConsoleFontEx(_ptrConsole1, false, font);
            Switch();
            SetCurrentConsoleFontEx(_ptrConsole2, false, font);
            Switch();
            SetCurrentConsoleFontEx(_ptrConsole1, false, font);

            SetConsoleCursorInfo(_ptrConsole1, ref cci);
            SetConsoleCursorInfo(_ptrConsole2, ref cci);
            SetConsoleActiveScreenBuffer(_ptrConsole1);

        }

        public void SetColor(int color)
        {
            SetConsoleTextAttribute(_selectedConsole ? _ptrConsole2 : _ptrConsole1, (uint)color);
        }

        public void Write(string text)
        {
            uint c = 0;
            WriteConsole(_selectedConsole ? _ptrConsole2 : _ptrConsole1, text, (uint) text.Length, out c, IntPtr.Zero);
        }

        public void Write(string text, int x, int y)
        {
            SetConsoleCursorPosition(_selectedConsole ? _ptrConsole2 : _ptrConsole1, new COORD((short)x, (short)y));
            Write(text);
        }

        public void Write(string text, int x, int y, int color)
        {
            SetConsoleTextAttribute(_selectedConsole ? _ptrConsole2 : _ptrConsole1, (uint)color);
            Write(text, x, y);
        }

        public void Write(string text, int color)
        {
            SetConsoleTextAttribute(_selectedConsole ? _ptrConsole2 : _ptrConsole1, (uint)color);
            Write(text);
        }

        public void Flush()
        {
            Switch();
        }

        private void Switch()
        {
            SetConsoleActiveScreenBuffer(_selectedConsole ? _ptrConsole2 : _ptrConsole1);
            _selectedConsole = !_selectedConsole;
        }
    }
}
