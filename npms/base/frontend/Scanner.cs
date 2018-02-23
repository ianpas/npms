namespace npms
{
    public abstract class Scanner
    {
        public Scanner(Source source) => this.m_Source = source;

        protected abstract Token ExtractToken();

        public Token NextToken()
        {
            m_CurrentToken = ExtractToken();
            return m_CurrentToken;
        }

        public char CurrentChar() => m_Source.CurrentChar();

        public char NextChar() => m_Source.NextChar();

        public void ConsumeCurrent() => m_Source.ConsumeCurrent();

        protected Source m_Source { get; set; }
        public Token m_CurrentToken { get; set; }
    }

}
