using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Windows.Foundation;
using Windows.Management.Deployment;

namespace ContextReg;

public class StartUp
{
	private const uint SHCNE_ASSOCCHANGED = 134217728u;

	private const uint SHCNF_IDLIST = 0u;

	public static void Run(string[] cmdArgs)
	{
		string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
		string text = FindPackageFile(baseDirectory, "*.msix");
		if (text != null && !ExecutionMode.IsRunningWithIdentity())
		{
			string sparsePkgPath = Path.Combine(baseDirectory, text);
			if (!registerSparsePackage(baseDirectory, sparsePkgPath))
			{
			}
		}
	}

	[DllImport("Shell32.dll")]
	public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);

	private static bool registerSparsePackage(string externalLocation, string sparsePkgPath)
	{
		bool result = false;
		try
		{
			Uri value = new Uri(externalLocation);
			Uri packageUri = new Uri(sparsePkgPath);
			PackageManager packageManager = new PackageManager();
			AddPackageOptions addPackageOptions = new AddPackageOptions();
			addPackageOptions.ExternalLocationUri = value;
			IAsyncOperationWithProgress<DeploymentResult, DeploymentProgress> asyncOperationWithProgress = packageManager.AddPackageByUriAsync(packageUri, addPackageOptions);
			ManualResetEvent opCompletedEvent = new ManualResetEvent(initialState: false);
			asyncOperationWithProgress.Completed = delegate
			{
				opCompletedEvent.Set();
			};
			opCompletedEvent.WaitOne();
			if (asyncOperationWithProgress.Status == AsyncStatus.Error)
			{
				DeploymentResult results = asyncOperationWithProgress.GetResults();
			}
			else if (asyncOperationWithProgress.Status != AsyncStatus.Canceled && asyncOperationWithProgress.Status == AsyncStatus.Completed)
			{
				result = true;
				SHChangeNotify(134217728u, 0u, IntPtr.Zero, IntPtr.Zero);
			}
		}
		catch (Exception)
		{
			return result;
		}
		return result;
	}

	private static string FindPackageFile(string directory, string searchPattern)
	{
		string[] files = Directory.GetFiles(directory, searchPattern);
		return (files.Length != 0) ? Path.GetFileName(files[0]) : null;
	}
}
