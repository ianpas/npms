using System.Collections.Generic;
using System.IO;

namespace npms
{
    /// <summary>
    /// The source class stores file content as a list of lines.
    /// </summary>
    public class Source
    {
        /// <summary>
        /// Append default EOL symbol \n to each line read in.
        /// </summary>
        /// <param name="file_path"></param>
        public Source(string file_path)
        {
            //
            m_SourceFile = new List<string>();
            using (var reader = new StreamReader(file_path))
            {
                for (; ; )
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    else
                    {
                        m_SourceFile.Add(line + EOL.ToString());
                    }
                }
            }

            //
            m_LineNumber = 0;
            m_CurrentPosition = 0;
        }

        /// <summary>
        /// Return the source character at the current position, possiblely EOF or EOL.
        /// </summary>
        /// <returns></returns>
        public char CurrentChar()
        {
            if (m_LineNumber >= m_SourceFile.Count)
            {
                return EOF;
            }
            else if (m_CurrentPosition > m_SourceFile[m_LineNumber].Length)// need to read one more line
            {
                ReadLine();
                return CurrentChar();
            }

            return m_SourceFile[m_LineNumber][m_CurrentPosition];
        }

        /// <summary>
        /// Move current position to next char, and return it.
        /// </summary>
        /// <returns></returns>
        public char NextChar()
        {
            ConsumeCurrent();
            return CurrentChar();
        }

        /// <summary>
        /// Return the source character following the current character without consuming the current character. 
        /// </summary>
        /// <returns></returns>
        public char PeekChar()
        {
            if (m_SourceFile.Count == 0)
                return EOF;

            int next_position = m_CurrentPosition + 1;

            if (next_position == m_SourceFile[m_LineNumber].Length)
            {
                int next_line = m_LineNumber + 1;
                next_position = 0;

                if (next_line >= m_SourceFile.Count)
                {
                    return EOF;
                }
                else
                {
                    return m_SourceFile[next_line][0];
                }

            }

            return m_SourceFile[m_LineNumber][next_position];
        }


        /// <summary>
        /// Advance one more line and reset current position.
        /// </summary>
        public void ReadLine()
        {
            ++m_LineNumber;
            m_CurrentPosition = 0;
        }

        /// <summary>
        /// Consume current character by advancing one position and do nothing.
        /// </summary>
        public void ConsumeCurrent()
        {
            if (m_SourceFile.Count == 0)
                return;

            ++m_CurrentPosition;
            if (m_CurrentPosition == m_SourceFile[m_LineNumber].Length)
            {
                ReadLine();
            }

        }


        /// <summary>
        /// end-of-line character
        /// </summary>
        public static readonly char EOL = '\n';

        /// <summary>
        /// end-of-file character
        /// </summary>
        public static readonly char EOF = '\0';

        private List<string> m_SourceFile { get; set; }


        /// <summary>
        /// It's the line number of current line.
        /// </summary>
        public int m_LineNumber { get; set; }

        /// <summary>
        /// It's relative to current line.
        /// </summary>
        public int m_CurrentPosition { get; set; }

    }

}