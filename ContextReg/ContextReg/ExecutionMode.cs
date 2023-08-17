#define DEBUG
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace ContextReg;

internal class ExecutionMode
{
	[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	private static extern int GetCurrentPackageFullName(ref int packageFullNameLength, ref StringBuilder packageFullName);

	internal static bool IsRunningWithIdentity()
	{
		if (isWindows7OrLower())
		{
			return false;
		}
		StringBuilder packageFullName = new StringBuilder(1024);
		int packageFullNameLength = 0;
		int currentPackageFullName = GetCurrentPackageFullName(ref packageFullNameLength, ref packageFullName);
		return currentPackageFullName != 15700;
	}

	private static bool isWindows7OrLower()
	{
		int major = Environment.OSVersion.Version.Major;
		int minor = Environment.OSVersion.Version.Minor;
		double num = (double)major + (double)minor / 10.0;
		return num <= 6.1;
	}

	internal static string GetSafeAppxLocalFolder()
	{
		try
		{
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex.Message);
		}
		return null;
	}
}
