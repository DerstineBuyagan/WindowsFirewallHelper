﻿using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WindowsFirewallHelper.Helpers;

namespace WindowsFirewallHelper.COMInterop
{
    [Guid("39EB36E0-2097-40BD-8AF2-63A13B525362")]
    [ComImport]
    internal interface INetFwProducts : IEnumerable
    {
        [DispId(1)]
        int Count
        {
            [DispId(1)]
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(2)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.IUnknown)]
        object Register(
            [MarshalAs(UnmanagedType.Interface)] [In]
            INetFwProduct product
        );

        [DispId(3)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.Interface)]
        INetFwProduct Item([In] int index);

        [DispId(-4)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(EnumeratorToEnumVariantMarshaler))]
        new IEnumerator GetEnumerator();
    }
}