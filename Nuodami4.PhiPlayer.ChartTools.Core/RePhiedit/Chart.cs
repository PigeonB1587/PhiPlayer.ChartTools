using Newtonsoft.Json;
using System;

namespace Nuodami4.PhiPlayer.ChartTools.Core.RePhiedit
{
    public static class Chart
    {
        [Serializable]
        public class BPMItem
        {
            public float bpm { get; set; }
            public int[] startTime { get; set; }
            public override string ToString() => JsonConvert.SerializeObject(this);
        }

        [Serializable]
        public class META
        {
            public int RPEVersion { get; set; }
            public int offset { get; set; }
            public override string ToString() => JsonConvert.SerializeObject(this);
        }

        [Serializable]
        public class JudgeLine
        {
            public EventLayers[] eventLayers { get; set; }
            public Note[] notes { get; set; }
            public bool rotateWithFather { get; set; } = false;
            public int father { get; set; }
            public override string ToString() => JsonConvert.SerializeObject(this);
        }

        [Serializable]
        public class Note
        {
            public int above { get; set; }
            public int alpha { get; set; }
            [JsonProperty(PropertyName = "tint")] public int[] color { get; set; }
            public int[] endTime { get; set; }
            public float positionX { get; set; }
            public float speed { get; set; }
            public int[] startTime { get; set; }
            public int type { get; set; }
            public int[] tintHitEffects { get; set; }
            public override string ToString() => JsonConvert.SerializeObject(this);
        }

        [Serializable]
        public class EventLayers
        {
            public Event[] alphaEvents { get; set; }
            public Event[] moveXEvents { get; set; }
            public Event[] moveYEvents { get; set; }
            public Event[] rotateEvents { get; set; }
            public Event[] speedEvents { get; set; }
            public override string ToString() => JsonConvert.SerializeObject(this);
        }

        [Serializable]
        public class Event
        {
            public int bezier { get; set; } = 0;
            public float[] bezierPoints { get; set; }
            public float easingLeft { get; set; } = 0.0f;
            public float easingRight { get; set; } = 1.0f;
            public int easingType { get; set; } = 1;
            public float end { get; set; }
            public int[] endTime { get; set; }
            public float start { get; set; }
            public int[] startTime { get; set; }
            public override string ToString() => JsonConvert.SerializeObject(this);
        }

        [Serializable]
        public class Root
        {
            public BPMItem[] BPMList { get; set; }
            public META META { get; set; }
            public JudgeLine[] judgeLineList { get; set; }
            public override string ToString() => JsonConvert.SerializeObject(this);
        }
    }
}
