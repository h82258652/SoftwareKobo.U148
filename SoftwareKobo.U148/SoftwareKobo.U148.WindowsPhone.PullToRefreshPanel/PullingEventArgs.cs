using System;

namespace SoftwareKobo.U148.Controls
{
    public class PullingEventArgs : EventArgs
    {
        public PullingEventArgs(double offset, PullingState state)
        {
            Offset = offset;
            State = state;
        }

        /// <summary>
        /// 已下拉的距离。
        /// </summary>
        /// <remarks>在 PrepareRefresh 和 Refreshing 状态下该值为下拉部分的高度。</remarks>
        public double Offset
        {
            get;
            private set;
        }

        public PullingState State
        {
            get;
            private set;
        }
    }
}