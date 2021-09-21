using System;
using System.Reflection;
using System.Text;

namespace Demo
{
	public static class Utils
	{
		public static void ConsoleWriteMembers(Object obj)
		{
			Utils.ConsoleWriteMembers(null, obj);
		}
		public static void ConsoleWriteMembers(String title, Object obj)
		{
			if(!String.IsNullOrEmpty(title))
				Console.Write(title + ": ");
			Console.WriteLine(Utils.GetReflectedMembers(obj));
		}
		public static String GetReflectedMembers(Object obj)
		{
			if(obj == null)
				return "<NULL>";


			StringBuilder result = new StringBuilder();
			Type objType = obj.GetType();
			if(objType.Assembly.GetName().Name == "mscorlib")
				result.Append(obj.ToString() + "\t");
			else
			{
				foreach(PropertyInfo prop in objType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
				{
					Object value = prop.GetValue(obj, null);
					result.AppendFormat("\t{0}: {1}\r\n", prop.Name, value);
				} foreach(FieldInfo field in objType.GetFields(BindingFlags.Instance | BindingFlags.Public))
					result.AppendFormat("\t{0}: {1}\r\n", field.Name, field.GetValue(obj));
			}

			return result.ToString();
		}

		/// <summary>Проверка на установку битового флага в поле</summary>
		/// <param name="flags">Целочисленное хранилище значение битовых флагов</param>
		/// <param name="key">Ключ флага, существование которого необходимо получить</param>
		/// <returns>Флаг установлен</returns>
		public static Boolean IsFlagSet(Int64 flags, Int64 key)
		{
			return (flags & key) > 0;
		}
	}
}