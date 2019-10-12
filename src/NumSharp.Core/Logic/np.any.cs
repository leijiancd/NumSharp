﻿using System;
using NumSharp.Generic;

namespace NumSharp
{
    public static partial class np
    {
        /// <summary>
        ///     Test whether any array element along a given axis evaluates to True.
        /// </summary>
        /// <param name="a">Input array or object that can be converted to an array.</param>
        /// <returns>A new boolean or ndarray is returned unless out is specified, in which case a reference to out is returned.</returns>
        /// <remarks>https://docs.scipy.org/doc/numpy/reference/generated/numpy.any.html</remarks>
        public static bool any(NDArray a)
        {
#if _REGEN
            #region Compute
		    switch (a.typecode)
		    {
			    %foreach supported_dtypes,supported_dtypes_lowercase%
			    case NPTypeCode.#1: return _any_linear<#2>(a.MakeGeneric<#2>());
			    %
			    default:
				    throw new NotSupportedException();
		    }
            #endregion
#else

            #region Compute
            switch (a.typecode)
            {
                case NPTypeCode.Boolean: return _any_linear<bool>(a.MakeGeneric<bool>());
                case NPTypeCode.Byte: return _any_linear<byte>(a.MakeGeneric<byte>());
                case NPTypeCode.Int16: return _any_linear<short>(a.MakeGeneric<short>());
                case NPTypeCode.UInt16: return _any_linear<ushort>(a.MakeGeneric<ushort>());
                case NPTypeCode.Int32: return _any_linear<int>(a.MakeGeneric<int>());
                case NPTypeCode.UInt32: return _any_linear<uint>(a.MakeGeneric<uint>());
                case NPTypeCode.Int64: return _any_linear<long>(a.MakeGeneric<long>());
                case NPTypeCode.UInt64: return _any_linear<ulong>(a.MakeGeneric<ulong>());
                case NPTypeCode.Char: return _any_linear<char>(a.MakeGeneric<char>());
                case NPTypeCode.Double: return _any_linear<double>(a.MakeGeneric<double>());
                case NPTypeCode.Single: return _any_linear<float>(a.MakeGeneric<float>());
                case NPTypeCode.Decimal: return _any_linear<decimal>(a.MakeGeneric<decimal>());
                default:
                    throw new NotSupportedException();
            }
            #endregion
#endif
        }

        /// <summary>
        ///     Test whether any array element along a given axis evaluates to True.
        /// </summary>
        /// <param name="a">Input array or object that can be converted to an array.</param>
        /// <param name="axis">Axis or axes along which a logical OR reduction is performed. The default (axis = None) is to perform a logical OR over all the dimensions of the input array. axis may be negative, in which case it counts from the last to the first axis.</param>
        /// <returns>A new boolean or ndarray is returned unless out is specified, in which case a reference to out is returned.</returns>
        /// <remarks>https://docs.scipy.org/doc/numpy/reference/generated/numpy.any.html</remarks>
        public static NDArray<bool> any(NDArray nd, int axis)
        {
            throw new NotImplementedException(); //TODO
        }

        private static bool _any_linear<T>(NDArray<T> nd) where T : unmanaged
        {
            if (nd.Shape.IsContiguous)
            {
                unsafe
                {
                    var addr = nd.Address;
                    var len = nd.size;
                    for (int i = 0; i < len; i++)
                    {
                        if (!addr[i].Equals(default(T))) //if (lhs != 0/false/0f)
                            return true;
                    }

                    return false;
                }
            } else
            {
                using (var incr = new NDIterator<T>(nd))
                {
                    var next = incr.MoveNext;
                    var hasnext = incr.HasNext;

                    while (hasnext())
                    {
                        if (!next().Equals(default(T))) //if (lhs != 0/false/0f)
                            return true;
                    }

                    return false;
                }
            }
        }
    }
}
