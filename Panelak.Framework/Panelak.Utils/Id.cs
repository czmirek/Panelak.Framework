namespace Panelak.Utils
{
    /// <summary>
    /// Struct for strongly typed IDs in a string format
    /// </summary>
    /// <typeparam name="T">Type of the ID</typeparam>
    public struct Id<T>
    {
        private readonly string value;
        public Id(string value) => this.value = value;
        public static explicit operator string(Id<T> id) => id.value;
        public static explicit operator Id<T>(string value) => new Id<T>(value);
    }
}
