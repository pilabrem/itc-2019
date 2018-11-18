﻿using Timetabling.Common.SolutionModel;
using Timetabling.Common.Utils;

namespace Timetabling.Common.ProblemModel.Constraints
{
    public class MaxDays : ConstraintBase
    {
        public MaxDays(int d, bool required, int penalty, int[] classes)
            : base(required, penalty, classes)
        {
            D = d;
        }

        public readonly int D;

        public override ConstraintType Type => ConstraintType.Time;

        public override (double hardPenalty, int softPenalty) Evaluate(ISolution s)
        {
            var acc = 0u;
            for (var i = 0; i < Classes.Length - 1; i++)
            {
                acc |= s.GetTime(Classes[i]).Days;
            }

            var count = Utilities.BitCount(acc);
            if (count <= D)
            {
                return (0d, 0);
            }

            count -= D;
            return Required
                ? (count, count * Penalty)
                : (0d, count * Penalty);
        }
    }
}