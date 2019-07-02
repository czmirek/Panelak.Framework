namespace Panelak.Database.Oracle
{
    using global::Oracle.ManagedDataAccess.Client;
    using System;

    /// <summary>
    /// Attribute for configuring the database type of Oracle parameters
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class OracleDbTypeAttribute : Attribute
    {
        /// <summary>
        /// Oracle database type
        /// </summary>
        public OracleDbType OracleDbType { get; }

        /// <summary>
        /// Size of oracle type
        /// </summary>
        public int? Size { get; }

        /// <summary>
        /// Creates a new instance of <see cref="OracleDbTypeAttribute"/>.
        /// </summary>
        /// <param name="oracleDbType">Oracle database type</param>
        public OracleDbTypeAttribute(OracleDbType oracleDbType)
        {
            OracleDbType = oracleDbType;
            Size = null;
        }

        /// <summary>
        /// Creates a new instance of <see cref="OracleDbTypeAttribute"/>.
        /// </summary>
        /// <param name="oracleDbType">Oracle database type</param>
        /// <param name="size">Size of the type</param>
        public OracleDbTypeAttribute(OracleDbType oracleDbType, int size)
        {
            OracleDbType = oracleDbType;
            Size = size;
        }
    }
}
