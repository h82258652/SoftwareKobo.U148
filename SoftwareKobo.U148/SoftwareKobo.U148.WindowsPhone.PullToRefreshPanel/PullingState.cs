namespace SoftwareKobo.U148.Controls
{
    public enum PullingState
    {
        /// <summary>
        /// 正在下拉。
        /// </summary>
        Pull,

        /// <summary>
        /// 已达到下拉最大高度，松开即刷新。
        /// </summary>
        PrepareRefresh,

        /// <summary>
        /// 正在刷新。
        /// </summary>
        Refreshing
    }
}