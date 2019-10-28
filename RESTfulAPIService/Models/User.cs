using System;

namespace RESTfulAPIService.Models
{
    /// <summary>
    ///     User model for database
    /// </summary>
    public class User
    {
        /// <summary>
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// </summary>
        public string Name { get; set; }
    }
}