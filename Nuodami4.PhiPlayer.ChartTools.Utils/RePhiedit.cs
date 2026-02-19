using System.Collections.Generic;

namespace Nuodami4.PhiPlayer.ChartTools.Utils
{
    public static partial class ChartConverter
    {
        public static Core.PhiPlayer.Chart.Root ToPhiPlayer(Core.RePhiedit.Chart.Root chart)
        {
            var obj = new Core.PhiPlayer.Chart.Root()
            {
                formatVersion = 1,
                offset = -chart.META.offset / 1000f,
            };

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
                        easingType = rawSpeedEventItem.easingType
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
                        noteTint = rawNote.color,
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
                    judgeLineTint = new int[] { 1, 1, 1 }
                });
            }

            return obj;
        }
    }

    public static class RePhiedit
    {
        public const double speedScale = 1.3333333333333333d; // value = 4 / 3
        public const double xScale = 0.0014814814814815d; // value = 1 / 675
        public const double yScale = 0.0022222222222222d; // value = 1 / 450
        public static int ConvertNoteType(int i)
        {
            switch (i)
            {
                case 1:
                    return 1;
                case 2:
                    return 3;
                case 3:
                    return 4;
                case 4:
                    return 2;
                default:
                    return 1;
            }
        }
    }
}
