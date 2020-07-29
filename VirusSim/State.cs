namespace VirusSim
{
    /// <summary>
    /// <c>State</c> Enumerator.
    /// All possible agent and grid states.
    /// </summary>
    public enum State
    {
        /// <summary>Agent doesn't exist.</summary>
        Null,

        /// <summary>Agent is healthy.</summary>
        Healthy,

        /// <summary>Agent is infected.</summary>
        Infected,

        /// <summary>Agent is dead.</summary>
        Dead,
    }
}