namespace Panelak.Database
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using RegisteredProperties = System.Collections.Generic.Dictionary<string, System.Reflection.PropertyInfo>;
    using RegisteredTypes = System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, System.Reflection.PropertyInfo>>;

    /// <summary>
    /// Default implementation of the SQL results in the <see cref="IDataReader"/> to the DTO in a type parameter.
    /// </summary>
    public class DefaultDtoMapper : IDtoMapper
    {
        /// <summary>
        /// Defines the registeredTypes
        /// </summary>
        private readonly RegisteredTypes registeredTypes = new RegisteredTypes();

        private static readonly object lockObj = new object();

        /// <summary>
        /// Default implementation of TryCustomMap does not apply any custom mapping.
        /// </summary>
        /// <param name="dataValue">Database value</param>
        /// <param name="dataValueType">Database value type</param>
        /// <param name="value">Mapped property value</param>
        /// <returns>True if mapping was successful</returns>
        public virtual bool TryCustomMap(object dataValue, Type dataValueType, out object value)
        {
            value = null;
            return false;
        }
        
        /// <summary>
        /// Reads from the data reader and converts the data to the enumeration of DTOs.
        /// </summary>
        /// <typeparam name="T">DTO type</typeparam>
        /// <param name="dataReader">Data reader</param>
        /// <param name="databaseType">Type of the database</param>
        /// <returns>Enumeration of DTOs</returns>
        public IEnumerable<T> MapToDto<T>(IDataReader dataReader, Database.DatabaseType databaseType)
            where T : new()
        {
            var result = new List<T>();
            Type dtoType = typeof(T);

            lock (lockObj)
            {
                if (!registeredTypes.ContainsKey(dtoType.AssemblyQualifiedName))
                {
                    PropertyInfo[] dtoTypeProperties = dtoType.GetProperties();
                    var dictionary = dtoTypeProperties.ToDictionary(p =>
                    {
                        return p.Name.ToLowerInvariant();
                    });

                    registeredTypes.Add(dtoType.AssemblyQualifiedName, dictionary);
                }
            }
            
            RegisteredProperties properties = registeredTypes[dtoType.AssemblyQualifiedName];

            while (dataReader.Read())
            {
                object row = new T();

                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    string resultColumn = dataReader.GetName(i).ToLowerInvariant();

                    if (!properties.ContainsKey(resultColumn))
                        continue;

                    PropertyInfo propertyInfo = properties[resultColumn];
                    object dataValue = dataReader.GetValue(i);
                    Type dataValueType = dataValue.GetType();

                    if (TryCustomMap(dataValue, dataValueType, out object customMap))
                    {
                        propertyInfo.SetValue(row, customMap);
                        continue;
                    }

                    if (propertyInfo.PropertyType.Equals(dataValueType))
                    {
                        // assign if types match
                        propertyInfo.SetValue(row, dataValue);
                    }
                    else if (dataReader.IsDBNull(i))
                    {
                        // assign null value
                        propertyInfo.SetValue(row, null);
                    }
                    else if (Nullable.GetUnderlyingType(properties[resultColumn].PropertyType) != null)
                    {
                        propertyInfo.SetValue(row, dataValue);
                    }
                }

                result.Add((T)row);
            }

            return result.AsReadOnly();
        }
    }
}
