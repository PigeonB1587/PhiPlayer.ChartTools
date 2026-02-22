using Newtonsoft.Json;
using System;

namespace Nuodami4.PhiPlayer.ChartTools.Core.Phigros
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
        public class JudgeLine
        {
            public float bpm { get; set; }
            public Note[] notesAbove { get; set; } = Array.Empty<Note>();
            public Note[] notesBelow { get; set; } = Array.Empty<Note>();
            public SpeedEvent[] speedEvents { get; set; } = Array.Empty<SpeedEvent>();
            public JudgeLineEvent[] judgeLineDisappearEvents { get; set; } = Array.Empty<JudgeLineEvent>();
            public JudgeLineEvent[] judgeLineMoveEvents { get; set; } = Array.Empty<JudgeLineEvent>();
            public JudgeLineEvent[] judgeLineRotateEvents { get; set; } = Array.Empty<JudgeLineEvent>();
            public override string ToString() => JsonConvert.SerializeObject(this);
        }

        [Serializable]
        public class Note
        {
            public int type { get; set; }
            [JsonConverter(typeof(FloatToIntConverter))] public int time { get; set; }
            public float positionX { get; set; }
            [JsonConverter(typeof(FloatToIntConverter))] public int holdTime { get; set; }
            public float speed { get; set; }
            public override string ToString() => JsonConvert.SerializeObject(this);
        }

        [Serializable]
        public class SpeedEvent
        {
            [JsonConverter(typeof(FloatToIntConverter))] public int startTime { get; set; }
            [JsonConverter(typeof(FloatToIntConverter))] public int endTime { get; set; }
            public float value { get; set; }
            public override string ToString() => JsonConvert.SerializeObject(this);
        }

        [Serializable]
        public class JudgeLineEvent
        {
            [JsonConverter(typeof(FloatToIntConverter))] public int startTime { get; set; }
            [JsonConverter(typeof(FloatToIntConverter))] public int endTime { get; set; }
            public float start { get; set; }
            public float end { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)] public float? start2 { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)] public float? end2 { get; set; }
            public override string ToString() => JsonConvert.SerializeObject(this);
        }
    }
    public class FloatToIntConverter : JsonConverter<int>
    {
        public override int ReadJson(JsonReader reader, Type objectType, int existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value is double d)
                return (int)Math.Round(d);
            if (reader.Value is float f)
                return (int)Math.Round(f);
            if (reader.Value is long l)
                return (int)l;
            return 0;
        }

        public override void WriteJson(JsonWriter writer, int value, JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }
    }
}
