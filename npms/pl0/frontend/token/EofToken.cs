namespace npms
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Token" />
    class EofToken : Token
    {
        public EofToken(Source source)
            : base(source)
        {
            m_Type = PascalTokenType.Eof;
        }

        /// <summary>
        /// Do nothing.
        /// </summary>
        protected override void Extract()
        {

        }
    }

}
