﻿using System;
using System.Threading;
using Windows.Management.Deployment;
using System.Runtime.InteropServices;
using System.IO;


namespace ContextReg
{
	class Program
	{
		static void Main(string[] args)
		{
			StartUp.Run(args);

			//Console.WriteLine("Press any key to exit...");
			//Console.ReadKey();
		}
	}

	public class StartUp
	{
		
		public static void Run(string[] cmdArgs)
		{
			//if app isn't running with identity, register its sparse package
			if (!ExecutionMode.IsRunningWithIdentity())
			{
				
				string exePath = AppDomain.CurrentDomain.BaseDirectory;
				string externalLocation = Path.Combine(exePath, @"");
				string sparsePkgPath = Path.Combine(exePath, @"*.msix");

				//Attempt registration
				if (registerSparsePackage(externalLocation, sparsePkgPath))
				{
					//Registration succeded, restart the app to run with identity
					Console.WriteLine("Package Registration succeded!");
				}
				else //Registration failed, run without identity
				{
					Console.WriteLine("Package Registation failed, running WITHOUT Identity");
				}

			}
			else //App is registered and running with identity, handle launch and activation
			{
				Console.WriteLine("Package Registation didn't happen");
			}

		}

		[DllImport("Shell32.dll")]
		public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);

		private const uint SHCNE_ASSOCCHANGED = 0x8000000;
		private const uint SHCNF_IDLIST = 0x0;

		private static bool registerSparsePackage(string externalLocation, string sparsePkgPath)
		{
			bool registration = false;
			try
			{
				Uri externalUri = new Uri(externalLocation);
				Uri packageUri = new Uri(sparsePkgPath);

				Console.WriteLine("exe Location {0}", externalLocation);
				Console.WriteLine();
				Console.WriteLine("msix Address {0}", sparsePkgPath);
				Console.WriteLine();

				Console.WriteLine("  exe Uri {0}", externalUri);
				Console.WriteLine();
				Console.WriteLine("  msix Uri {0}", packageUri);
				Console.WriteLine();

				PackageManager packageManager = new PackageManager();

				//Declare use of an external location
				var options = new AddPackageOptions();
				options.ExternalLocationUri = externalUri;

				Windows.Foundation.IAsyncOperationWithProgress<DeploymentResult, DeploymentProgress> deploymentOperation = packageManager.AddPackageByUriAsync(packageUri, options);

				ManualResetEvent opCompletedEvent = new ManualResetEvent(false); // this event will be signaled when the deployment operation has completed.

				deploymentOperation.Completed = (depProgress, status) => { opCompletedEvent.Set(); };

				Console.WriteLine("Installing package {0}", sparsePkgPath);
				Console.WriteLine();

				Console.WriteLine("Waiting for package registration to complete...");
				Console.WriteLine();

				opCompletedEvent.WaitOne();

				if (deploymentOperation.Status == Windows.Foundation.AsyncStatus.Error)
				{
					Windows.Management.Deployment.DeploymentResult deploymentResult = deploymentOperation.GetResults();
					Console.WriteLine("Installation Error: {0}", deploymentOperation.ErrorCode);
					Console.WriteLine();
					Console.WriteLine("Detailed Error Text: {0}", deploymentResult.ErrorText);
					Console.WriteLine();

				}
				else if (deploymentOperation.Status == Windows.Foundation.AsyncStatus.Canceled)
				{
					Console.WriteLine("Package Registration Canceled");
					Console.WriteLine();
				}
				else if (deploymentOperation.Status == Windows.Foundation.AsyncStatus.Completed)
				{
					registration = true;
					Console.WriteLine("Package Registration succeeded!");
					Console.WriteLine();

					// Notify the shell about the change
					SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_IDLIST, IntPtr.Zero, IntPtr.Zero);

				}
				else
				{
					Console.WriteLine("Installation status unknown");
					Console.WriteLine();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("AddPackageSample failed, error message: {0}", ex.Message);
				Console.WriteLine();
				Console.WriteLine("Full Stacktrace: {0}", ex.ToString());
				Console.WriteLine();

				return registration;
			}

			return registration;
		}

	}
}