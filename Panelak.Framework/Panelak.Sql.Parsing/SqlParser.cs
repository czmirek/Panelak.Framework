namespace Panelak.Sql.Parsing
{
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Parses the SQL query independently of specific RDBMS by conventions (expressed in Regexes).
    /// </summary>
    public class SqlParser : ISqlParser
    {
        /// <summary>
        /// Parses the query into the SELECT columns, FROM table and ORDER BY columns.
        /// </summary>
        private static readonly Regex QueryParser = new Regex(@"SELECT\s+(?<columns>[\w\|\+_""'\p{L},\s]+)\s+FROM\s+(?<table>[\w\._@]+)(\s+WHERE\s+(?<where>.+))?(\s+ORDER\s+BY\s+(?<orderby>.+))?", RegexOptions.IgnoreCase);

        /// <summary>
        /// Parses the full column expression into an identifier and alias (including ' or "))
        /// </summary>
        private static readonly Regex ColumnParser = new Regex(@"(?<identifier>[\w'\|]+)(?:(?:\s+AS\s+)?(?<alias>[\w\p{L}\s""']+))?", RegexOptions.IgnoreCase);

        /// <summary>
        /// Parses the ORDER BY column identifier with the sort direction
        /// </summary>
        private static readonly Regex OrderbyParser = new Regex(@"(?<identifier>\w+)(?:\s+(?<sortDirection>ASC|DESC))?", RegexOptions.IgnoreCase);

        /// <summary>
        /// Parses table identifier in the FROM clause of query
        /// </summary>
        private static readonly Regex TableIdentifierParser = new Regex(@"FROM\s+(?<table>[\w\._@]+)", RegexOptions.IgnoreCase);

        /// <summary>
        /// Used to check whether the query string contains sub queries (which is not supported)
        /// </summary>
        private static readonly Regex SubqueryCheck = new Regex(@"FROM\s+\(\s*SELECT", RegexOptions.IgnoreCase);

        /// <summary>
        /// Logger instance
        /// </summary>
        private readonly ILogger log;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlParser"/> class.
        /// </summary>
        /// <param name="log">Logger instance</param>
        public SqlParser(ILogger log) => this.log = log ?? throw new ArgumentNullException(nameof(log));

        /// <summary>
        /// Converts the SQL query text into a parsed representation of the query which can be worked on.
        /// </summary>
        /// <param name="sqlText">SELECT SQL query text</param>
        /// <returns>SQL SELECT query model</returns>
        public ISqlSelectQuery ParseSqlQuery(string sqlText)
        {
            log.LogDebug($"Parsing query: {sqlText}");

            if (SubqueryCheck.IsMatch(sqlText))
                throw new SubqueriesNotAllowedException();

            Match matches = QueryParser.Match(sqlText);
            string columns = matches.Groups["columns"].Value;
            string table = matches.Groups["table"].Value;
            string orderBy = matches.Groups["orderby"].Value;
            string where = matches.Groups["where"].Value;

            log.LogTrace($"Columns: {columns}");
            log.LogTrace($"Table: {table}");
            log.LogTrace($"Order by: {orderBy}");
            log.LogTrace($"Where: {where}");

            IEnumerable<SqlColumn> parsedColumns = ColumnParser.Matches(columns)
                                                                .Cast<Match>()
                                                                .Select(m => new SqlColumn(m.Groups["identifier"].Value, m.Groups["alias"].Value));

            log.LogTrace($"Parsed columns: {parsedColumns.Count()}");

            IEnumerable<Match> orderByMatches = OrderbyParser.Matches(orderBy).Cast<Match>();

            log.LogTrace($"Order by columns: {orderByMatches.Count()}");

            if (orderByMatches.Any())
            {
                // change sorting of columns that are present in both the select and orderby clause
                foreach (SqlColumn column in parsedColumns)
                {
                    Match foundMatch = orderByMatches.FirstOrDefault(m => m.Groups["identifier"]?.Value?.Equals(column.Expression, StringComparison.InvariantCultureIgnoreCase) ?? false);

                    if (foundMatch == null)
                    {
                        log.LogTrace($"Default sort for {column.Expression}");
                        continue;
                    }
                    else
                        log.LogTrace($"Applying custom sort for {column.Expression}");

                    string sortFromMatch = foundMatch.Groups["sortDirection"].Value;
                    if (sortFromMatch.Equals("ASC", StringComparison.InvariantCultureIgnoreCase))
                        column.SortOrder = SortOrder.Ascending;
                    else if (sortFromMatch.Equals("DESC", StringComparison.InvariantCultureIgnoreCase))
                        column.SortOrder = SortOrder.Descending;
                }
            }

            var tableIdentifier = new SqlTableIdentifier(table);
            ISqlConditionExpression sqlConditionExpression = null;

            if (matches.Groups["where"].Success)
            {
                log.LogTrace($"Building a binary tree from WHERE condition: {where}");

                var sqlConditionBuilder = new SqlConditionBuilder(log);
                sqlConditionExpression = sqlConditionBuilder.Build(where);
            }

            return new SqlSelectQuery(parsedColumns, tableIdentifier, sqlConditionExpression, log);
        }

        /// <summary>
        /// Converts the table identifier by convention into a parsed object.
        /// </summary>
        /// <param name="tableIdentifier">Table identifier string</param>
        /// <returns>Table identifier model</returns>
        public ISqlTableIdentifier ParseTableIdentifier(string tableIdentifier) => new SqlTableIdentifier(tableIdentifier);

        /// <summary>
        /// Finds a first table in the first FROM expression and parses that to <see cref="ISqlTableIdentifier"/>.
        /// </summary>
        /// <param name="sqlQuery">SQL query string</param>
        /// <returns>Table identifier model</returns>
        public ISqlTableIdentifier ParseTableIdentifierInSqlText(string sqlQuery)
        {
            Match match = TableIdentifierParser.Match(sqlQuery);

            if (match.Success)
                return new SqlTableIdentifier(match.Groups["table"].Value);

            return null;
        }
    }
}
