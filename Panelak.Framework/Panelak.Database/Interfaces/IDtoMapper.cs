namespace Panelak.Database
{
    using System.Collections.Generic;
    using System.Data;

    /// <summary>
    /// Mapper for conversion of database result to DTO.
    /// </summary>
    public interface IDtoMapper
    {
        /// <summary>
        /// Maps a database result from the data reader to the enumeration of given DTOs.
        /// </summary>
        /// <typeparam name="T">DTO type</typeparam>
        /// <param name="dataReader">RDBMS driver reader</param>
        /// <param name="databaseType">RDBMS type</param>
        /// <returns>Enumeration of rows</returns>
        IEnumerable<T> MapToDto<T>(IDataReader dataReader, DatabaseType databaseType) where T : new();
    }
}
