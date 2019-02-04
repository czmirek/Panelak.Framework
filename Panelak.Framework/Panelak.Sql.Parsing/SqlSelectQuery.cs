namespace Panelak.Sql.Parsing
{
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Parsed SQL SELECT query model.
    /// </summary>
    internal class SqlSelectQuery : ISqlSelectQuery
    {
        /// <summary>
        /// Logger instance
        /// </summary>
        private readonly ILogger log;

        /// <summary>
        /// List of SQL parameters
        /// </summary>
        private List<ISqlParameter> parameters = new List<ISqlParameter>();

        /// <summary>
        /// List of columns in the SELECT query
        /// </summary>
        private List<SqlColumn> columns = new List<SqlColumn>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlSelectQuery"/> class.
        /// </summary>
        /// <param name="columns">List of columns in the SELECT clause</param>
        /// <param name="tableIdentifier">Table identifier after the FROM clause</param>
        /// <param name="sqlConditionExpression">Condition expression binary tree</param>
        /// <param name="log">Logger instance</param>
        public SqlSelectQuery(
            IEnumerable<SqlColumn> columns,
            ISqlTableIdentifier tableIdentifier,
            ISqlConditionExpression sqlConditionExpression,
            ILogger log)
        {
            this.columns = columns.ToList();
            TableIdentifier = tableIdentifier;
            Condition = sqlConditionExpression;
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <summary>
        /// Gets or sets the identifier of the table in the FROM clause
        /// </summary>
        public ISqlTableIdentifier TableIdentifier { get; set; }

        /// <summary>
        /// Gets the SQL columns in the SELECT clause. Sub queries are not supported.
        /// </summary>
        public IEnumerable<ISqlColumn> Columns => columns.AsReadOnly();

        /// <summary>
        /// Gets or sets a value indicating whether column aliases are to be included in the query
        /// </summary>
        public bool ExcludeAliases { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether pagination is not considered when building this SQL query
        /// </summary>
        public bool NoPagination { get; set; }

        /// <summary>
        /// Gets or sets the Page
        /// Page for pagination purposes. Starts at 1.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the size of the page for pagination purposes.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets the SQL parameters entering into the RDBMS with this query.
        /// </summary>
        public IEnumerable<ISqlParameter> Parameters => parameters.AsReadOnly();

        /// <summary>
        /// Gets the binary tree representation of an SQL condition
        /// </summary>
        public ISqlConditionExpression Condition { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the query contains a geometry filter.
        /// </summary>
        public bool HasGeometryFilter { get; private set; }

        /// <summary>
        /// Gets the geometry filter column
        /// </summary>
        public string GeometryFilterColumn { get; private set; }

        /// <summary>
        /// Gets the geomtery filter bounding box bottom left X coordinate.
        /// </summary>
        public double GeomertryFilterBboxX1 { get; private set; }

        /// <summary>
        /// Gets the geomtery filter bounding box bottom left Y coordinate.
        /// </summary>
        public double GeomertryFilterBboxY1 { get; private set; }

        /// <summary>
        /// Gets the geomtery filter bounding box top right X coordinate.
        /// </summary>
        public double GeomertryFilterBboxX2 { get; private set; }

        /// <summary>
        /// Gets the geomtery filter bounding box top right Y coordinate.
        /// </summary>
        public double GeomertryFilterBboxY2 { get; private set; }

        /// <summary>
        /// Appends new expression in AND node to the root with existing expression
        /// or directly as the root.
        /// </summary>
        /// <param name="expression">New expression</param>
        public void And(ISqlConditionExpression expression)
        {
            if (Condition == null)
                Condition = expression;
            else
                Condition = new And(Condition, expression);
        }

        /// <summary>
        /// Configures the query to append a geometry bounding-box filter for given <paramref name="geometryColumn"/>
        /// with given coordinates.
        /// </summary>
        /// <param name="geometryColumn">Geometry column identifier</param>
        /// <param name="x1">Bounding box left bottom X</param>
        /// <param name="y1">Bounding box left bottom Y</param>
        /// <param name="x2">Bounding box top right X</param>
        /// <param name="y2">Bounding box top right Y</param>
        public void AndGeomInBbox(string geometryColumn, double x1, double y1, double x2, double y2)
        {
            HasGeometryFilter = true;
            GeometryFilterColumn = geometryColumn;
            GeomertryFilterBboxX1 = x1;
            GeomertryFilterBboxY1 = y1;
            GeomertryFilterBboxX2 = x2;
            GeomertryFilterBboxY2 = y2;
        }

        /// <summary>
        /// Appends an IN expression in AND node with existing expression
        /// or directly as the root.
        /// </summary>
        /// <param name="column">Column identifier</param>
        /// <param name="values">Values of the IN operator</param>
        public void AndIn(string column, IEnumerable<string> values)
        {
            var expression = new PropertyIsIn(column, values);

            if (Condition == null)
                Condition = expression;
            else
                Condition = new And(Condition, expression);
        }

        /// <summary>
        /// Parses the expression string and appends it with AND node to the root with the existing expression
        /// or directly as root if no expression is set.
        /// </summary>
        /// <param name="expressionString">Unparsed SQL expression string</param>
        public void AndString(string expressionString)
        {
            var parser = new SqlConditionBuilder(log);
            ISqlConditionExpression expression = parser.Build(expressionString);

            if (Condition == null)
                Condition = expression;
            else
                Condition = new And(Condition, expression);
        }

        /// <summary>
        /// Applies sorting in fallback manner. If no sorting is specified in the query, default
        /// column sorting is used.
        /// </summary>
        /// <param name="defaultColumnSort">Column used for default sorting</param>
        /// <param name="defaultColumnSortOrder">Sort order of default column</param>
        /// <param name="inputSortByColumns">Columns with sorting from the input</param>
        public void ApplySorting(string defaultColumnSort, SortOrder defaultColumnSortOrder, IEnumerable<ISqlSortColumn> inputSortByColumns)
        {
            foreach (ISqlSortColumn sortByColumn in inputSortByColumns)
            {
                // if the sorting column is not present in the list of columns, add it at the beginning
                ISqlColumn existingColumn = Columns.FirstOrDefault(x => x.Expression == sortByColumn.Expression);
                if (existingColumn != null)
                    existingColumn.SortOrder = sortByColumn.SortOrder;
            }

            // if no sorting has been set, use sorting by the default column
            if (!Columns.Any(x => x.SortOrder != SortOrder.Unspecified))
            {
                // if the column exists, just change the order
                ISqlColumn existingDefaultColumn = Columns.FirstOrDefault(x => x.Expression == defaultColumnSort);
                if (existingDefaultColumn != null)
                {
                    existingDefaultColumn.SortOrder = defaultColumnSortOrder;
                    return;
                }

                // if the column is specified in the input, keep the sort order and insert it in the list of columns
                ISqlSortColumn existingInputColumn = inputSortByColumns.FirstOrDefault(col => col.Expression == defaultColumnSort);
                if (existingInputColumn != null)
                {
                    InsertColumn(existingInputColumn.Expression, true, existingInputColumn.SortOrder);
                    return;
                }

                // if the default column does not exist in the query columns or in the input columns
                // create it and insert it
                var defaultSortingColumn = new SqlColumn(defaultColumnSort)
                {
                    SortOrder = defaultColumnSortOrder,
                    Include = true
                };

                columns.Insert(0, defaultSortingColumn);
            }
        }

        /// <summary>
        /// Inserts a new column into the list of SELECT query columns
        /// </summary>
        /// <param name="name">Name of the column</param>
        /// <param name="include">Whether the column has to be included in the SELECT clause</param>
        /// <param name="sortOrder">Sorting of the column</param>
        /// <param name="alias">Optional column alias</param>
        public void InsertColumn(string name, bool include, SortOrder sortOrder, string alias = null) => columns.Insert(0, new SqlColumn(name, alias)
        {
            Include = include,
            SortOrder = sortOrder
        });

        /// <summary>
        /// Removes a column by name
        /// </summary>
        /// <param name="name">Name of the column to remove</param>
        public void RemoveColumn(string name)
        {
            SqlColumn col = columns.FirstOrDefault(c => c.Expression == name);
            if (col != null)
                columns.Remove(col);
        }

        /// <summary>
        /// Finds and sets the given column as primary key.
        /// </summary>
        /// <param name="primaryKeyColumnName">Name of the primary key column</param>
        public void SetPrimaryKey(string primaryKeyColumnName)
        {
            SqlColumn primaryKeyColumn = columns.FirstOrDefault(c => c.Expression == primaryKeyColumnName);
            if (primaryKeyColumn == null)
                throw new InvalidOperationException($"Unable to set {primaryKeyColumnName} as primary key");

            columns.ForEach(col => col.IsPrimaryKey = false);
            primaryKeyColumn.IsPrimaryKey = true;
        }
    }
}
