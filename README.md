# Panelak's source codes

Current projects.

1) ORM for *Sql Server* and *Oracle*
    - Reflection based
    - Normalized geometry models (`SqlGeometry` and `SDO_GEOMETRY`) with `Panelak.Geometry`
    - [Panelak.Database](https://www.nuget.org/packages/Panelak.Database/)
    - [Panelak.Database.SqlServer](https://www.nuget.org/packages/Panelak.Database.SqlServer/)
    - [Panelak.Database.Oracle](https://www.nuget.org/packages/Panelak.Database.Oracle/)

2) SQL parsing
   - [Panelak.Sql.Parsing](https://www.nuget.org/packages/Panelak.Sql.Parsing/)
 
3) Simple geometry and drawing
   - [Panelak.Geometry](https://www.nuget.org/packages/Panelak.Geometry/)
   - [Panelak.Drawing](https://www.nuget.org/packages/Panelak.Drawing/)
  

## Database ORM with SQL parsing

``Panelak.Database[.Oracle|.SqlServer]`` is a simple ORM for Sql Server and Oracle libraries able to accept parsed SQL queries from unsafe sources (using ``Panelak.Sql.Parsing``).

``Panelak.Sql.Parsing`` contains a tokenizer and parser of simple SQL queries and can convert an SQL condition expression (assuming some constraints) to a binary tree according to operator precedence and accounting for parentheses. This is useful for executing SQL queries from unsafe sources. The SQL queries must conform to a specific set of rules (e.g. no subqueries, no function expressions etc.)

## SELECT SQL parsing

Build a SQL query model with a [`SqlParser `](https://github.com/Panelak/Panelak.Framework/blob/master/Panelak.Framework/Panelak.Sql.Parsing/SqlParser.cs) like this:

```csharp
var parser = new SqlParser();
ISqlSelectQuery sqlQueryModel = parser.Parse("SELECT column1, column2 FROM table WHERE colum3 > column4");
```

The parser supports basic SQL queries with these constraints.
- the SQL must include 
  - SELECT and FROM
  - optionally WHERE and ORDER BY
- does not support subqueries
- does not support expressions, inline functions etc., only column identifiers with aliases
- the condition must contain only logical and comparison expressions of columns. Parentheses are supported.

### SQL condition binary tree parser

SQL conditions can be converted to a binary tree individually. This may be useful for verifying SQL condition strings from unsafe sources.

The condition is first converted into tokens and the tokens are then converted to the binary tree.

The binary tree is formed with respect to operator precedence (which is basically same in all popular RDBMS) and parentheses.

Only basic logic operators (`AND, OR, NOT`) and basic comparison operators (`<, >, >=, <=, !=, =, LIKE, NOT LIKE, IS NULL, IS NOT NULL`) are supported.

Usage:

```csharp
var cBuilder = new SqlConditionBuilder(logger);
ISqlConditionExpression binaryTree = cBuilder.Build("col1 > 1 AND (col2 LIKE 'someString' OR col3 != 123)");
```

The condition `col1 > 1 AND (col2 LIKE 'someString' OR col3 != 123)` forms the following binary tree.

<pre>
    col > 1
   / 
AND    col2 LIKE 'someString'
   \  /
    OR
      \
       col3 != 123
</pre>

See more in [tests](https://github.com/czmirek/Panelak.Framework/tree/master/Panelak.Framework/Panelak.Sql.Parsing.Test).