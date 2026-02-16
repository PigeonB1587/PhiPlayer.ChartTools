using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nuodami4.PhiPlayer.ChartTools.Core.PhiPlayer
{
    public static class Chart
    {
        [Serializable]
        public class Root
        {
            public int formatVersion { get; set; }
            public float offset { get; set; }
            public JudgeLine[] judgeLineList { get; set; }
        }

        [Serializable]
        public class Bpm
        {
            public float bpm { get; set; }
            public int[] startTime { get; set; }
        }

        [Serializable]
        public class JudgeLineEvent
        {
            public int[] startTime { get; set; }
            public int[] endTime { get; set; }
            public int easingType { get; set; }
            public float easingLeftX { get; set; }
            public float easingRightX { get; set; }
            public float[] bezierPoints { get; set; }
            public float start { get; set; }
            public float end { get; set; }
        }

        [Serializable]
        public class JudgeLineTransformLayer
        {
            public JudgeLineEvent[] judgeLineMoveXEvents { get; set; }
            public JudgeLineEvent[] judgeLineMoveYEvents { get; set; }
            public JudgeLineEvent[] judgeLineRotateEvents { get; set; }
            public JudgeLineEvent[] judgeLineDisappearEvents { get; set; }
        }

        [Serializable]
        public class Note
        {
            public int type { get; set; }
            public int key { get; set; }
            public int[] startTime { get; set; }
            public int[] endTime { get; set; }
            public float positionX { get; set; }
            public float speed { get; set; }
            public int[] hitFxTint { get; set; }
            public int[] noteTint { get; set; }
        }

        [Serializable]
        public class JudgeLine
        {
            public Bpm[] bpms { get; set; }
            public Note[] notes { get; set; }
            public JudgeLineEvent[] speedEvents { get; set; }
            public JudgeLineTransformLayer[] judgeLineTransformLayers { get; set; }
            public int[] judgeLineTint { get; set; }
            public int fatherLineIndex { get; set; }
            public bool isLocalEulerAngle { get; set; }
            public bool isLocalPosition { get; set; }
        }
    }
}
