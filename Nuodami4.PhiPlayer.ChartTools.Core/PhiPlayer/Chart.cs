using Newtonsoft.Json;
using System;

namespace Nuodami4.PhiPlayer.ChartTools.Core.PhiPlayer
{
    public static class Chart
    {
        [Serializable]
        public class Root
        {
            public int formatVersion { get; set; }
            public float offset { get; set; }
            public JudgeLine[] judgeLineList { get; set; } = Array.Empty<JudgeLine>();
            public override string ToString() => JsonConvert.SerializeObject(this);
            public static Root FromJson(string jsonString) => JsonConvert.DeserializeObject<Root>(jsonString);
        }

        [Serializable]
        public class Bpm
        {
            public float bpm { get; set; }
            public int[] startTime { get; set; } = Array.Empty<int>();
            public override string ToString() => JsonConvert.SerializeObject(this);
        }

        [Serializable]
        public class JudgeLineEvent
        {
            public int[] startTime { get; set; } = Array.Empty<int>();
            public int[] endTime { get; set; } = Array.Empty<int>();
            public int easingType { get; set; }
            public float easingLeftX { get; set; }
            public float easingRightX { get; set; }
            public float[] bezierPoints { get; set; } = Array.Empty<float>();
            public float start { get; set; }
            public float end { get; set; }
            public override string ToString() => JsonConvert.SerializeObject(this);
        }

        [Serializable]
        public class JudgeLineTransformLayer
        {
            public JudgeLineEvent[] judgeLineMoveXEvents { get; set; } = Array.Empty<JudgeLineEvent>();
            public JudgeLineEvent[] judgeLineMoveYEvents { get; set; } = Array.Empty<JudgeLineEvent>();
            public JudgeLineEvent[] judgeLineRotateEvents { get; set; } = Array.Empty<JudgeLineEvent>();
            public JudgeLineEvent[] judgeLineDisappearEvents { get; set; } = Array.Empty<JudgeLineEvent>();
            public override string ToString() => JsonConvert.SerializeObject(this);
        }

        [Serializable]
        public class Note
        {
            public int type { get; set; }
            public int key { get; set; }
            public int[] startTime { get; set; } = Array.Empty<int>();
            public int[] endTime { get; set; } = Array.Empty<int>();
            public float positionX { get; set; }
            public float speed { get; set; }
            public int[] hitFxTint { get; set; } = Array.Empty<int>();
            public int[] noteTint { get; set; } = Array.Empty<int>();
            public override string ToString() => JsonConvert.SerializeObject(this);
        }

        [Serializable]
        public class JudgeLine
        {
            public Bpm[] bpms { get; set; } = Array.Empty<Bpm>();
            public Note[] notes { get; set; } = Array.Empty<Note>();
            public JudgeLineEvent[] speedEvents { get; set; } = Array.Empty<JudgeLineEvent>();
            public JudgeLineTransformLayer[] judgeLineTransformLayers { get; set; } = Array.Empty<JudgeLineTransformLayer>();
            public int[] judgeLineTint { get; set; } = Array.Empty<int>();
            public int fatherLineIndex { get; set; }
            public bool isLocalEulerAngle { get; set; }
            public bool isLocalPosition { get; set; }
            public override string ToString() => JsonConvert.SerializeObject(this);
        }
    }
}
