namespace WatchAll.Api.Enums
{
    /// <summary>
    /// Describe status of serial for user
    /// </summary>
    public enum WatchingStatusEnum
    {
        /// <summary>
        /// User is watching serial now 
        /// </summary>
        Watching,

        /// <summary>
        /// User will be watching serial in future
        /// </summary>
        WillWatch,

        /// <summary>
        /// User isn't watching serial now
        /// </summary>
        Holding,

        /// <summary>
        /// User has watched the show
        /// </summary>
        Watched,

        /// <summary>
        /// User doesn't tracking serial
        /// </summary>
        Untracked
    }
}
