using System.Collections.Generic;

namespace Nuodami4.PhiPlayer.ChartTools.Utils
{
    public static partial class Converter
    {
        public static Core.PhiPlayer.Chart.Root ToPhiPlayer(this Core.RePhiedit.Chart.Root chart)
        {
            var bpmList = new List<Core.PhiPlayer.Chart.Bpm>();
            foreach (var rawBpmItem in chart.BPMList)
            {
                bpmList.Add(new Core.PhiPlayer.Chart.Bpm()
                {
                    startTime = rawBpmItem.startTime,
                    bpm = rawBpmItem.bpm
                });
            }

            var judgeLineList = new List<Core.PhiPlayer.Chart.JudgeLine>();
            foreach (var rawLine in chart.judgeLineList)
            {
                var speedEventList = new List<Core.PhiPlayer.Chart.JudgeLineEvent>();
                var judgeLineTransformLayerList = new List<Core.PhiPlayer.Chart.JudgeLineTransformLayer>();
                var noteList = new List<Core.PhiPlayer.Chart.Note>();

                foreach (var rawSpeedEventItem in rawLine.eventLayers[0].speedEvents)
                {
                    speedEventList.Add(new Core.PhiPlayer.Chart.JudgeLineEvent()
                    {
                        start = rawSpeedEventItem.start * (float)RePhiedit.speedScale,
                        end = rawSpeedEventItem.end * (float)RePhiedit.speedScale,
                        startTime = rawSpeedEventItem.startTime,
                        endTime = rawSpeedEventItem.endTime,
                        bezierPoints = rawSpeedEventItem.bezierPoints,
                        easingLeftX = rawSpeedEventItem.easingLeft,
                        easingRightX = rawSpeedEventItem.easingRight,
                        easingType = RePhiedit.ConvertEasingType(rawSpeedEventItem.easingType)
                    });
                }

                foreach (var rawNote in rawLine.notes)
                {
                    noteList.Add(new Core.PhiPlayer.Chart.Note()
                    {
                        speed = rawNote.speed,
                        startTime = rawNote.startTime,
                        endTime = rawNote.endTime,
                        hitFxTint = rawNote.tintHitEffects,
                        key = 0,
                        noteTint = new int[] { rawNote.color[0], rawNote.color[1], rawNote.color[2], rawNote.alpha },
                        positionX = rawNote.positionX * (float)RePhiedit.xScale,
                        type = RePhiedit.ConvertNoteType(rawNote.type)
                    });
                }

                foreach (var rawEventLayer in rawLine.eventLayers)
                {
                    var moveXEventList = new List<Core.PhiPlayer.Chart.JudgeLineEvent>();
                    var moveYEventList = new List<Core.PhiPlayer.Chart.JudgeLineEvent>();
                    var rotateEventList = new List<Core.PhiPlayer.Chart.JudgeLineEvent>();
                    var disappearEventList = new List<Core.PhiPlayer.Chart.JudgeLineEvent>();

                    foreach (var rawEventItem in rawEventLayer.moveXEvents)
                    {
                        moveXEventList.Add(new Core.PhiPlayer.Chart.JudgeLineEvent()
                        {
                            start = rawEventItem.start * (float)RePhiedit.xScale,
                            end = rawEventItem.end * (float)RePhiedit.xScale,
                            startTime = rawEventItem.startTime,
                            endTime = rawEventItem.endTime,
                            bezierPoints = rawEventItem.bezierPoints,
                            easingLeftX = rawEventItem.easingLeft,
                            easingRightX = rawEventItem.easingRight,
                            easingType = RePhiedit.ConvertEasingType(rawEventItem.easingType)
                        });
                    }

                    foreach (var rawEventItem in rawEventLayer.moveYEvents)
                    {
                        moveYEventList.Add(new Core.PhiPlayer.Chart.JudgeLineEvent()
                        {
                            start = rawEventItem.start * (float)RePhiedit.yScale,
                            end = rawEventItem.end * (float)RePhiedit.yScale,
                            startTime = rawEventItem.startTime,
                            endTime = rawEventItem.endTime,
                            bezierPoints = rawEventItem.bezierPoints,
                            easingLeftX = rawEventItem.easingLeft,
                            easingRightX = rawEventItem.easingRight,
                            easingType = RePhiedit.ConvertEasingType(rawEventItem.easingType)
                        });
                    }

                    foreach (var rawEventItem in rawEventLayer.rotateEvents)
                    {
                        rotateEventList.Add(new Core.PhiPlayer.Chart.JudgeLineEvent()
                        {
                            start = -rawEventItem.start,
                            end = -rawEventItem.end,
                            startTime = rawEventItem.startTime,
                            endTime = rawEventItem.endTime,
                            bezierPoints = rawEventItem.bezierPoints,
                            easingLeftX = rawEventItem.easingLeft,
                            easingRightX = rawEventItem.easingRight,
                            easingType = RePhiedit.ConvertEasingType(rawEventItem.easingType)
                        });
                    }

                    foreach (var rawEventItem in rawEventLayer.alphaEvents)
                    {
                        disappearEventList.Add(new Core.PhiPlayer.Chart.JudgeLineEvent()
                        {
                            start = rawEventItem.start,
                            end = rawEventItem.end,
                            startTime = rawEventItem.startTime,
                            endTime = rawEventItem.endTime,
                            bezierPoints = rawEventItem.bezierPoints,
                            easingLeftX = rawEventItem.easingLeft,
                            easingRightX = rawEventItem.easingRight,
                            easingType = RePhiedit.ConvertEasingType(rawEventItem.easingType)
                        });
                    }
                }

                judgeLineList.Add(new Core.PhiPlayer.Chart.JudgeLine()
                {
                    bpms = bpmList.ToArray(),
                    speedEvents = speedEventList.ToArray(),
                    judgeLineTransformLayers = judgeLineTransformLayerList.ToArray(),
                    notes = noteList.ToArray(),
                    fatherLineIndex = rawLine.father,
                    isLocalEulerAngle = rawLine.rotateWithFather,
                    isLocalPosition = rawLine.father >= 0 ? true : false,
                    judgeLineTint = new int[] { 255, 255, 255 }
                });
            }

            var obj = new Core.PhiPlayer.Chart.Root()
            {
                formatVersion = 1,
                offset = -chart.META.offset / 1000f,
                judgeLineList = judgeLineList.ToArray()
            };

            return obj;
        }
    }

    public static class RePhiedit
    {
        public const double speedScale = 1.3333333333333333d; // value = 4 / 3
        public const double xScale = 0.0014814814814815d; // value = 1 / 675
        public const double yScale = 0.0022222222222222d; // value = 1 / 450

        private static readonly Dictionary<int, int> NoteTypeMap = new Dictionary<int, int>()
        {
            {1, 1}, {2, 3}, {3, 4}, {4, 2}
        };

        public static int ConvertNoteType(int i)
        {
            if (NoteTypeMap.TryGetValue(i, out int type))
            {
                return type;
            }
            return 1;
        }

        private static readonly Dictionary<int, int> EasingTypeMap = new Dictionary<int, int>()
        {
            {1, 1}, {2, 3}, {3, 2}, {4, 6}, {5, 5}, {6, 4}, {7, 7}, {8, 9}, {9, 8},
            {10, 12}, {11, 11}, {12, 10}, {13, 13}, {14, 15}, {15, 14}, {16, 18},
            {17, 17}, {18, 21}, {19, 20}, {20, 24}, {21, 23}, {22, 22}, {23, 25},
            {24, 27}, {25, 26}, {26, 30}, {27, 29}, {28, 31}, {29, 28}
        };

        public static int ConvertEasingType(int i)
        {
            if (EasingTypeMap.TryGetValue(i, out int type))
            {
                return type;
            }
            return 1;
        }
    }
}
