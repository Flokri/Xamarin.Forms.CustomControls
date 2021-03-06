﻿using System;

namespace Xamarin.Forms.CustomControls.Animations
{
    /// <summary>
    /// a dashed stroke
    /// </summary>
    class DashedStroke
    {
        public float[] Intervals { get; set; }
        public float Phase { get; set; }

        public DashedStroke(float[] intervals, float phase)
        {
            Intervals = new float[intervals.Length];
            Array.Copy(intervals, Intervals, intervals.Length);
            Phase = phase;
        }

        public DashedStroke(DashedStroke strokeDash)
            : this(strokeDash.Intervals, strokeDash.Phase) { }
    }
}
