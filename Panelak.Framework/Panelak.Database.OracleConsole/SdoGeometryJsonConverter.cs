namespace Panelak.Database.OracleConsole
{
    using Panelak.Geometry;
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// JSON to SDO_GEOMETRY model converter for Newtonsoft.Json converter.
    /// </summary>
    public class SdoGeometryJsonConverter : JsonConverter
    {
        /// <summary>
        /// Determines whether this instance can convert the specified <see cref="Geometry"/>.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>true if this instance can convert the specified object type; otherwise, false.</returns>
        public override bool CanConvert(Type objectType) => objectType.Equals(typeof(Geometry));

        /// <summary>
        /// Gets a value indicating whether this Newtonsoft.Json.JsonConverter can write
        /// </summary>
        public override bool CanWrite => false;

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The Newtonsoft.Json.JsonReader to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            SdoGeometry sdoGeometry = serializer.Deserialize<SdoGeometry>(reader);

            var converter = new SdoGeometryConverter();
            return converter.GeometryFromSdo(sdoGeometry);
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The Newtonsoft.Json.JsonWriter to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotImplementedException();
    }
}
