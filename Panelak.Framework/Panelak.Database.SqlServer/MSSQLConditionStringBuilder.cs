namespace Panelak.Database.SqlServer
{
    using System.Data.SqlClient;

    /// <summary>
    /// SQL Server condition string builder
    /// </summary>
    public class MSSQLConditionStringBuilder : SqlConditionStringBuilder
    {
        /// <summary>
        /// System SQL command builder
        /// </summary>
        private readonly SqlCommandBuilder builder = new SqlCommandBuilder();

        /// <summary>
        /// Returns SQL Server prefixed parameter name
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <returns>SQL Server prefixed parameter name</returns>
        protected override string GetParamName(string name) => $"@{name}";

        /// <summary>
        /// Returns SQL Server quoted column identifier
        /// </summary>
        /// <param name="identifier">Column identifier</param>
        /// <returns>SQL Server quoted column identifier</returns>
        protected override string GetQuotedIdentifier(string identifier) => builder.QuoteIdentifier(identifier);
    }
}
