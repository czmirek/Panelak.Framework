namespace Panelak.Utils
{
    /// <summary>
    /// Structure for providing filtering information for data repositories.
    /// </summary>
    /// <typeparam name="T">Type of the value</typeparam>
    public class ValueFilter<T>
    {
        /// <summary>
        /// Gets or sets the value indicating whether the value filter is set.
        /// </summary>
        public bool IsSet { get; set; } = false;

        /// <summary>
        /// Private value.
        /// </summary>
        private T value = default;

        /// <summary>
        /// Gets or sets the value of the filter.
        /// </summary>
        public T Value
        {
            get => value;
            set
            {
                IsSet = true;
                this.value = value;
            }
        }
    }
}
