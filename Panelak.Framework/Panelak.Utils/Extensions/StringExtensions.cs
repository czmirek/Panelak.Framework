namespace Panelak.Utils
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// String extensions
    /// </summary>
    public static class StringExtensions
    {
        
        /// <summary>
        /// Serializes the object to XML using <see cref="XmlSerializer"/>.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="value">Instance of object</param>
        /// <returns>Serialized XML</returns>
        public static string ToXml<T>(this T value)
        {
            if (value == null)
                return String.Empty;
            
            var xmlserializer = new XmlSerializer(typeof(T));
            var stringWriter = new StringWriter();
            using (var writer = XmlWriter.Create(stringWriter))
            {
                xmlserializer.Serialize(writer, value);
                return stringWriter.ToString();
            }
        }
    }
}
