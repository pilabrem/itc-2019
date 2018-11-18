﻿using Timetabling.Common.SolutionModel;

namespace Timetabling.Common.ProblemModel.Constraints
{
    public class DifferentRoom : ConstraintBase
    {
        public DifferentRoom(bool required, int penalty, int[] classes)
            : base(required, penalty, classes)
        {
        }

        public override ConstraintType Type => ConstraintType.Room;

        public override (double hardPenalty, int softPenalty) Evaluate(ISolution s)
        {
            var hardPenalty = 0;
            for (var i = 0; i < Classes.Length - 1; i++)
            {
                var ci = s.GetRoom(Classes[i]);
                for (var j = i + 1; j < Classes.Length; j++)
                {
                    var cj = s.GetRoom(Classes[i]);
                    if (ci != cj)
                    {
                        continue;
                    }

                    if (!Required)
                    {
                        return (0d, Penalty);
                    }

                    hardPenalty++;
                }
            }

            return hardPenalty != 0 ? (hardPenalty, Penalty) : (0d, 0);
        }
    }
}