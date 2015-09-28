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
        char[] oldBuffer;   //The buffer rendered previously, used to only draw what is needed.
        int[] colours;  //The colours of the buffer
        int bufferWidth;
        int bufferHeight;
        //private StreamWriter stdout;    //Used to write to the console using a stream to get the fastest write speed possible.

        public ConsoleRenderer(int bufferWidth, int bufferHeight)
        {
            this.bufferWidth = bufferWidth;
            this.bufferHeight = bufferHeight;
            buffer = new char[bufferWidth * bufferHeight];
            oldBuffer = new char[bufferWidth * bufferHeight];
            //stdout = new StreamWriter(Console.OpenStandardOutput(), System.Text.Encoding.Default);
            //stdout.AutoFlush = false;
        }

        /// <summary>
        /// Clears the buffer.
        /// </summary>
        private void ClearBuffer()
        {
            Array.Copy(buffer, oldBuffer, buffer.Length);
            Array.Clear(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Outputs the contents of the buffer to the console.
        /// </summary>
        private void DrawBuffer()
        {
            /*
            //Concatenate the buffer into one continuous string
            string output = "";
            for (int i = 0; i < buffer.Length; i++) {
                output += buffer[i];
                //stdout.Write(buffer[i]);
            }
            stdout.Write(output);
            stdout.Flush();
             * */

            for (int x = 0; x < bufferWidth; x++) {
                for (int y = 0; y < bufferHeight; y++) {
                    if (buffer[y * bufferWidth + x] != oldBuffer[y * bufferWidth + x]) {
                        Console.SetCursorPosition(x, y);
                        Console.Write(buffer[y * bufferWidth + x]);
                    }
                }
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
                    for (int tempY = y + 1; tempY < y + (height - 1); tempY++) {
                        WriteChar(x, tempY, '│');
                        WriteChar(x + (width - 1), tempY, '│');
                    }
                    for (int tempX = x + 1; tempX < x + (width - 1); tempX++) {
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
                    for (int tempY = y + 1; tempY < y + (height - 1); tempY++) {
                        WriteChar(x, tempY, '║');
                        WriteChar(x + (width - 1), tempY, '║');
                    }
                    for (int tempX = x + 1; tempX < x + (width - 1); tempX++) {
                        WriteChar(tempX, y, '═');
                        WriteChar(tempX, y + (height - 1), '═');
                    }
                    break;
            }
        }
    }
}
