namespace Panelak.Sql.Parsing
{
    /// <summary>
    /// States of the internal parsing state machine
    /// </summary>
    internal enum ParseState
    {
        /// <summary>
        /// Initial state, start of expression
        /// </summary>
        ExpressionStart,

        /// <summary>
        /// State signaling identifier presence in the expression
        /// </summary>
        LeftSideIdentifier,

        /// <summary>
        /// State signaling two sided operator in the expression
        /// </summary>
        TwoSidedOperator,        
    }
}