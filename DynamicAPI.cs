/*
 * User: TMZ
 * Date: April 15 2015
 * Time: 12:47PM
 * 
 * Released under GPLv2
 */

using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace APICaller
{
	
	/*CharSet Codes :

None - 1
ANSI - 2
Unicode - 3
Auto - 4

Calling Conventions Codes (System.Reflection) :

Standard - 1
VarArgs - 2
Any - 3
HasThis - 32
ExplicitThis - 64

Native Calling Convention Codes (System.Runtime.InteropServices) :

Winapi - 1
Cdecl - 2
StdCall - 3
ThisCall - 4
FastCall - 5
	 */
	
	public class DynamicAPIs
	{
		
		private readonly string WinLib;
		private readonly string MethodName;
		private readonly string AssemblyName;
		private readonly string ModuleName;
		private readonly string ClassName;
		private readonly Type ReturnType;

		private readonly object[] Parameters;

		public DynamicAPIs(string wLib, string mName, string asmName, string modName, string cName, Type rType, object[] Params)
		{
			WinLib = wLib;
			MethodName = mName;
			AssemblyName = asmName;
			ModuleName = modName;
			ClassName = cName;
			ReturnType = rType;
			Parameters = Params;

			CreateDynamicAPI();
		}

		private object CreateDynamicAPI()
		{

			AssemblyBuilder ASMB = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName(AssemblyName), AssemblyBuilderAccess.RunAndSave);
			ModuleBuilder MODB = ASMB.DefineDynamicModule(ModuleName);
			TypeBuilder TB = MODB.DefineType(ClassName, TypeAttributes.Public);

			Type[] ParameterTypes = new Type[Parameters.Length];

			for (int i = 0; i <= Parameters.Length - 1; i++) {
				ParameterTypes[i] = Parameters[i].GetType();
			}
  
    //You could also enum the Native and Managed CallingConventions and CharSet here and calling them as you wish but for this example, this will do
			MethodBuilder MB = TB.DefinePInvokeMethod(MethodName, WindowsLibrary, MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.PinvokeImpl,
			                                          System.Reflection.CallingConventions.Standard ,
			                                          ReturnType, ParameterTypes,
			                                          CallingConvention.Winapi,
			                                          CharSet.Ansi);

			MB.SetImplementationFlags(MB.GetMethodImplementationFlags() | MethodImplAttributes.PreserveSig);

			return TB.CreateType().GetMethod(MethodName).Invoke(null, Parameters);

		}
		
	}
}
