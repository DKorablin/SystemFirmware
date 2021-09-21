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
			return String.Format("{0:n0} {1}", length / constSize, Utils.FileSizeType[sizePosition]);
		}

		/// <summary>Чистка полей, которые были взяты как излишек после окончания структуры</summary>
		/// <remarks>В связи с тем, что в заголовке описан размер структуры, то в самой структуре некоторые значения могут быть меньше чем сама структура</remarks>
		/// <typeparam name="T">Тип структуры</typeparam>
		/// <param name="header">Заголовок с размером структуры</param>
		/// <param name="src">Структура, где изменяются значения</param>
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