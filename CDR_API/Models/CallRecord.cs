namespace CDR_API.Models
{
    /// <summary>
    /// Represents a detailed record of a telephone call.
    /// </summary>
    public class CallRecord
    {
        /// <summary>
        /// Gets or sets the unique identifier for the caller.
        /// This is typically the caller's phone number.
        /// </summary>
        public string CallerId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the recipient of the call.
        /// This is typically the recipient's phone number.
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// Gets or sets the date when the call was initiated.
        /// </summary>
        public DateTime CallDate { get; set; }

        /// <summary>
        /// Gets or sets the time when the call ended.
        /// This, combined with the <see cref="CallDate"/>, indicates the call duration.
        /// </summary>
        public DateTime EndTime { get; set; } // Assuming DateTime captures both date and time for simplicity

        /// <summary>
        /// Gets or sets the duration of the call in seconds.
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets the cost associated with the call.
        /// This value is typically in the currency specified by <see cref="Currency"/>.
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Gets or sets the unique reference identifier for the call.
        /// This could be used for tracking calls in billing or other systems.
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the currency code for the cost of the call, in GBP.
        /// </summary>
        public string Currency { get; set; }
    }

}
