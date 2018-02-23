namespace npms
{
    public abstract class Token
    {

        public Token(Source source)
        {
            m_Source = source;
            m_LineNumber = source.m_LineNumber;
            m_Position = source.m_CurrentPosition;

            Extract();
        }

        /// <summary>
        /// Modify current position in source file and set type,text and value of token.
        /// </summary>
        protected abstract void Extract();

        protected char CurrentChar() => m_Source.CurrentChar();
        protected char NextChar() => m_Source.NextChar();
        protected void ConsumeCurrent() => m_Source.ConsumeCurrent();
        protected char PeekChar() => m_Source.PeekChar();

        //
        public object m_Type { get; set; }

        /// <summary>
        /// Token text in source file
        /// </summary>
        public string m_Text { get; set; }


        /// <summary>
        /// The value of token
        /// </summary>
        public object m_Value { get; set; }

        protected Source m_Source { get; set; }

        /// <summary>
        /// The line number of token in source file
        /// </summary>
        public int m_LineNumber { get; set; }

        /// <summary>
        /// The position of token(first character) in the line of the source file.
        /// </summary>
        protected int m_Position { get; set; }

    }

}
