namespace Panelak.Sql
{
    /// <summary>
    /// Implements parsing functionality for RDBMS independent SQL SELECT query and table identifiers.
    /// </summary>
    public interface ISqlParser
    {
        /// <summary>
        /// Parses the SQL SELECT query.
        /// </summary>
        /// <param name="sqlText">SQL query string</param>
        /// <returns>Parsed SQL query model</returns>
        ISqlSelectQuery ParseSqlQuery(string sqlText);

        /// <summary>
        /// Parses the table identifier by the convention.
        /// </summary>
        /// <param name="tableIdentifier">String representation of the table identifier</param>
        /// <returns>Parsed table identifier model</returns>
        ISqlTableIdentifier ParseTableIdentifier(string tableIdentifier);

        /// <summary>
        /// Parses the first table identifier in the FROM clause of any SQL query
        /// </summary>
        /// <param name="sqlText">SQL query string</param>
        /// <returns>Parsed table identifier model</returns>
        ISqlTableIdentifier ParseTableIdentifierInSqlText(string sqlText);
    }
}