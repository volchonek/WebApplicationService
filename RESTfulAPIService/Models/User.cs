using System;

namespace RESTfulAPIService.Models
{
    /// <summary>
    ///     User model for database.
    /// </summary>
    public class User
    {
        /// <summary>
        ///     Guid - unique identifier number.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     User name.
        /// </summary>
        public string Name { get; set; }
    }
}