﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumSharp.Extensions
{
    public static partial class NDArrayExtensions
    {
        public static NDArray_Legacy<double> Min(this NDArray_Legacy<NDArray_Legacy<double>> np)
        {
            var min = new NDArray_Legacy<double>();

            for (int d = 0; d < np.NDim; d++)
            {
                var value = np.Data.Select(x => x.Data[d]).Min();
                min.Data.Add(value);
            }

            return min;
        }
    }
}
