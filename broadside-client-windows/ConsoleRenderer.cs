using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace broadside_client_windows
{
    public class ConsoleRenderer
    {
        char[] buffer;
        //char[] blankArray;  //An array filled with spaces, used to clear the buffer.
        int bufferWidth;
        int bufferHeight;
        private StreamWriter stdout;    //Used to write to the console using a stream to get the fastest write speed possible.

        public ConsoleRenderer(int bufferWidth, int bufferHeight)
        {
            this.bufferWidth = bufferWidth;
            this.bufferHeight = bufferHeight;
            buffer = new char[bufferWidth * bufferHeight];
            /*blankArray = new char[bufferWidth * bufferHeight];
            for (int i = 0; i < blankArray.Length; i++) {
                blankArray[i] = ' ';
            } */
            stdout = new StreamWriter(Console.OpenStandardOutput());
        }

        /// <summary>
        /// Clears the buffer.
        /// </summary>
        private void ClearBuffer()
        {
            Array.Clear(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Outputs the contents of the buffer to the console.
        /// </summary>
        private void DrawBuffer()
        {
            for (int i = 0; i < buffer.Length; i++) {
                stdout.Write(buffer[i]);
            }
        }

        /// <summary>
        /// Call this to output the buffer to the screen and then clear it.
        /// </summary>
        public void Paint()
        {
            DrawBuffer();
            ClearBuffer();
        }

        /// <summary>
        /// Writes a character to the buffer at the specified location.
        /// </summary>
        /// <param name="x">The x co-ordinate to write to</param>
        /// <param name="y">The y co-ordinate to write to</param>
        /// <param name="c">The character to write</param>
        private int WriteChar(int x, int y, char c)
        {
            try {
                buffer[(y * bufferWidth) + x] = c;
                return 0;
            } catch {
                return 1;
            }
        }

        public void WriteString(int x, int y, string text)
        {
            char[] characters = text.ToCharArray();

            for (int i = 0; i < characters.Length; i++) {
                if (WriteChar(x + i, y, characters[i]) == 1)
                    return;
            }
        }

        /// <summary>
        /// Draws a box at the specified location.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="border"></param>
        public void DrawBox(int x, int y, int width, int height, BorderStyles border)
        {
            switch (border) {
                case BorderStyles.oneLine:
                    //Place corners
                    WriteChar(x, y, '┌');
                    WriteChar(x + (width - 1), y, '┐');
                    WriteChar(x + (width - 1), y + (height - 1), '┘');
                    WriteChar(x, y + (height - 1), '└');
                    //Place edges
                    for (int tempY = y; tempY < (height - 1); tempY++) {
                        WriteChar(x, tempY, '│');
                        WriteChar(x + (width - 1), tempY, '│');
                    }
                    for (int tempX = x; tempX < (width - 1); tempX++) {
                        WriteChar(tempX, y, '─');
                        WriteChar(tempX, y + (height - 1), '─');
                    }
                    break;

                case BorderStyles.twoLine:
                    //Place corners
                    WriteChar(x, y, '╔');
                    WriteChar(x + (width - 1), y, '╗');
                    WriteChar(x + (width - 1), y + (height - 1), '╝');
                    WriteChar(x, y + (height - 1), '╚');
                    //Place edges
                    for (int tempY = y; tempY < (height - 1); tempY++) {
                        WriteChar(x, tempY, '║');
                        WriteChar(x + (width - 1), tempY, '║');
                    }
                    for (int tempX = x; tempX < (width - 1); tempX++) {
                        WriteChar(tempX, y, '═');
                        WriteChar(tempX, y + (height - 1), '═');
                    }
                    break;
            }
        }
    }
}
