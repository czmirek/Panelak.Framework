namespace Panelak.Sql
{
    /// <summary>
    /// Implements conversion from SQL models to parameterized models.
    /// </summary>
    public interface ISqlBuilder
    {
        /// <summary>
        /// Builds a <see cref="IParameterizedQuery"/> out of given <see cref="ISqlSelectQuery"/>.
        /// </summary>
        /// <param name="sqlQuery">Configured query</param>
        /// <returns>Query with given SQL text and parameters.</returns>
        IParameterizedQuery BuildQuery(ISqlSelectQuery sqlQuery);

        /// <summary>
        /// Builds a "SELECT COUNT(*) FROM {<paramref name="table"/>} [WHERE {<paramref name="filter"/>}] query.
        /// </summary>
        /// <param name="table">Table identifier of the query</param>
        /// <param name="filter">Optional filter expression in WHERE clause</param>
        /// <returns>Constructed SQL query model with parameters</returns>
        IParameterizedQuery BuildCountQuery(ISqlTableIdentifier table, ISqlConditionExpression filter);
    }
}
