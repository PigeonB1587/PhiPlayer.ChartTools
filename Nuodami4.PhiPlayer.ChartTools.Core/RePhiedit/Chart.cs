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
            public int[] startTime { get; set; } = Array.Empty<int>();
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
            public EventLayers[] eventLayers { get; set; } = Array.Empty<EventLayers>();
            public Note[] notes { get; set; } = Array.Empty<Note>();
            public bool rotateWithFather { get; set; } = false;
            public int father { get; set; }
            public override string ToString() => JsonConvert.SerializeObject(this);
        }

        [Serializable]
        public class Note
        {
            public int above { get; set; }
            public int alpha { get; set; }
            [JsonProperty(PropertyName = "tint")] public int[] color { get; set; } = new int[] { 255, 255, 255 };
            public int[] endTime { get; set; } = Array.Empty<int>();
            public float positionX { get; set; }
            public float speed { get; set; }
            public int[] startTime { get; set; } = Array.Empty<int>();
            public int type { get; set; }
            public int[] tintHitEffects { get; set; } = new int[] { 255, 255, 255 };
            public override string ToString() => JsonConvert.SerializeObject(this);
        }

        [Serializable]
        public class EventLayers
        {
            public Event[] alphaEvents { get; set; } = Array.Empty<Event>();
            public Event[] moveXEvents { get; set; } = Array.Empty<Event>();
            public Event[] moveYEvents { get; set; } = Array.Empty<Event>();
            public Event[] rotateEvents { get; set; } = Array.Empty<Event>();
            public Event[] speedEvents { get; set; } = Array.Empty<Event>();
            public override string ToString() => JsonConvert.SerializeObject(this);
        }

        [Serializable]
        public class Event
        {
            public int bezier { get; set; } = 0;
            public float[] bezierPoints { get; set; } = Array.Empty<float>();
            public float easingLeft { get; set; } = 0.0f;
            public float easingRight { get; set; } = 1.0f;
            public int easingType { get; set; } = 1;
            public float end { get; set; }
            public int[] endTime { get; set; } = Array.Empty<int>();
            public float start { get; set; }
            public int[] startTime { get; set; } = Array.Empty<int>();
            public override string ToString() => JsonConvert.SerializeObject(this);
        }

        [Serializable]
        public class Root : IRoot
        {
            public BPMItem[] BPMList { get; set; } = Array.Empty<BPMItem>();
            public META META { get; set; }
            public JudgeLine[] judgeLineList { get; set; } = Array.Empty<JudgeLine>();
            public override string ToString() => JsonConvert.SerializeObject(this);
            public static Root FromJson(string jsonString) => JsonConvert.DeserializeObject<Root>(jsonString);
        }
    }
}
