using System;
using System.Reflection;
using System.Runtime.InteropServices;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug
{
	internal static class Utils
	{
		private const Int64 FileSize = 1024;
		private static String[] FileSizeType = new String[] { "bytes", "Kb", "Mb", "Gb", };
		/// <summary>Convert file size in bytes to string with dimention</summary>
		/// <param name="length">size in bytes</param>
		/// <returns>Size with dimention</returns>
		public static String FileSizeToString(UInt64 length)
		{
			UInt64 constSize = 1;
			Int32 sizePosition = 0;
			while(length > constSize * Utils.FileSize && sizePosition + 1 < Utils.FileSizeType.Length)
			{
				constSize *= Utils.FileSize;
				sizePosition++;
			}
			return $"{(length / constSize):n0} {Utils.FileSizeType[sizePosition]}";
		}

		/// <summary>Clearing fields that were taken as surplus after the end of the structure</summary>
		/// <remarks>Due to the fact that the header describes the size of the structure, then in the structure itself, some values may be less than the structure itself</remarks>
		/// <typeparam name="T">Structure type</typeparam>
		/// <param name="header">Header with structure size</param>
		/// <param name="src">Structure where data will clange</param>
		public static void ClearOverflowFields<T>(SmBios.Header header, ref T src) where T : struct
		{
			FieldInfo[] fields = src.GetType().GetFields(BindingFlags.Instance | BindingFlags.GetField | BindingFlags.Public | BindingFlags.NonPublic);
			foreach(FieldInfo field in fields)
			{
				Byte offset = (Byte)Marshal.OffsetOf(src.GetType(), field.Name);
				if(offset > header.Length)
				{
					Object defaultValue = field.FieldType.IsValueType
						? Activator.CreateInstance(field.FieldType)
						: null;

					TypedReference typeRef = __makeref(src);
					field.SetValueDirect(typeRef, defaultValue);
				}
			}
		}

		/// <summary>Convert structure to bytes</summary>
		/// <typeparam name="T">Structure type</typeparam>
		/// <param name="data">Struncture data</param>
		/// <returns>bytes</returns>
		public static Byte[] ConvertToBytes<T>(T data) where T:struct
		{
			Int32 sizeOfStruct = Marshal.SizeOf(data);
			IntPtr ptr = Marshal.AllocHGlobal(sizeOfStruct);
			try
			{
				Marshal.StructureToPtr(data, ptr, true);

				Byte[] result = new Byte[sizeOfStruct];
				Marshal.Copy(ptr, result, 0, sizeOfStruct);
				return result;
			} finally
			{
				Marshal.FreeHGlobal(ptr);
			}
		}
	}
}