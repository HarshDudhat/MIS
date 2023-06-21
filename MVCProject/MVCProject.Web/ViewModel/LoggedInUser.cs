// -----------------------------------------------------------------------
// <copyright file="LoggedInUser.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.ViewModel
{
    /// <summary>
    ///  View Model for Logged in users for SignalR UserHub
    /// </summary>
    public class LoggedInUser
    {
        /// <summary>
        /// Gets or sets UserId
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets ConnectionId
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// Gets or sets Token
        /// </summary>
        public string Token { get; set; }
    }
}