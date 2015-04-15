/*
 * User: TMZ
 * Date: April 15 2015
 * Time: 12:48PM
 * 
 * Released under GPLv2
 */

using System;
using System.Reflection;

namespace APICaller
{
	class Program
	{
		
		
		public static void Main(string[] args)
		{
			Console.Title = "Dynamic API Caller";
			Console.WriteLine("Press any key to call your API!");
			Console.ReadKey(true);
			
			string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
			string asmName = Assembly.GetExecutingAssembly().FullName;
			string methodName = MethodBase.GetCurrentMethod().Name;

			DynamicAPIs CreateDynamicAPI = new DynamicAPIs("user32.dll", "MessageBoxA", asmName, methodName, className, typeof(int), new object[] {
			                                               	IntPtr.Zero,
			                                               	"Test Message",
			                                               	"Test Title",
			                                               	0
			                                               });
			
			
			Console.Write("Press any key to exit . . . ");
			Console.ReadKey(true);
		}
		
		
	}
}
