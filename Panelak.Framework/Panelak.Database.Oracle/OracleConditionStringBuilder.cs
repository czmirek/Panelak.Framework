namespace Panelak.Database.Oracle
{
    using global::Oracle.ManagedDataAccess.Client;

    /// <summary>
    /// Oracle condition string builder
    /// </summary>
    public class OracleConditionStringBuilder : SqlConditionStringBuilder
    {
        /// <summary>
        /// Managed Oracle command builder
        /// </summary>
        private readonly OracleCommandBuilder builder = new OracleCommandBuilder();

        /// <summary>
        /// Returns Oracle prefixed parameter name
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <returns>Oracle prefixed parameter name</returns>
        protected override string GetParamName(string name) => $":{name}";

        /// <summary>
        /// Returns Oracle quoted column identifier
        /// </summary>
        /// <param name="identifier">Column identifier</param>
        /// <returns>Oracle quoted column identifier</returns>
        protected override string GetQuotedIdentifier(string identifier) => builder.QuoteIdentifier(identifier);
    }
}
