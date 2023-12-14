using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Foundation;
using Windows.Management.Deployment;
namespace ContextReg
{
    public class StartUp
    {
        private const uint SHCNE_ASSOCCHANGED = 134217728u;
        private const uint SHCNF_IDLIST = 0u;

        public static async Task RunAsync(string[] cmdArgs)
        {
            try
            {
                string exeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string baseDirectory = Path.Combine(exeDirectory, "ContextMenu");
                MessageBox.Show("Base Directory: " + baseDirectory); // Show message box

                string msixPackagePath = FindPackageFile(baseDirectory, "*.msix");
                MessageBox.Show("MSIX Package Path: " + msixPackagePath); // Show message box
                if (msixPackagePath != null && !ExecutionMode.IsRunningWithIdentity())
                {
                    // Combine base directory and MSIX package path
                    string sparsePkgPath = Path.Combine(baseDirectory, msixPackagePath);

                    // Extract and install the certificates
                    string certificatePath = "Die.cer";
                    await ExtractAndInstallCertificateAsync(sparsePkgPath, certificatePath);

                    // Register the sparse package
                    await RegisterSparsePackageAsync(baseDirectory, sparsePkgPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                // Log detailed information, including stack trace
                // Handle exception here
            }
        }


        private static async Task ExtractAndInstallCertificateAsync(string msixPackagePath, string certificatePath)
        {
            try
            {
                // Extract the root certificate from the MSIX package
                string rootCertificatePath = "Die.cer";
                string powershellCommand = $"$signature = Get-AuthenticodeSignature -FilePath '{msixPackagePath}'; Export-Certificate -Cert $signature.SignerCertificate -FilePath '{rootCertificatePath}'";
                await RunCommandAsync("powershell", $"-Command \"{powershellCommand}\"");

                // Install the root certificate to Trusted Root Certification Authorities
                await InstallRootCertificateAsync(rootCertificatePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during certificate installation: {ex.Message}");
                // Handle exception here or rethrow it if needed
            }
        }

        private static async Task InstallRootCertificateAsync(string certificatePath)
        {
            try
            {
                // Install the root certificate to Trusted Root Certification Authorities
                await RunCommandAsync("certutil", $"-addstore Root \"{certificatePath}\"");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error installing root certificate: {ex.Message}");
                // Handle exception here
            }
        }


        private static async Task RunCommandAsync(string fileName, string arguments)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using (Process process = new Process { StartInfo = processStartInfo })
            {
                process.Start();
                await Task.Run(() => process.WaitForExit());
            }
        }

        private static async Task RegisterSparsePackageAsync(string externalLocation, string sparsePkgPath)
        {
            try
            {
                Uri value = new Uri(externalLocation);
                Uri packageUri = new Uri(sparsePkgPath);

                PackageManager packageManager = new PackageManager();
                AddPackageOptions addPackageOptions = new AddPackageOptions { ExternalLocationUri = value };

                IAsyncOperationWithProgress<DeploymentResult, DeploymentProgress> asyncOperationWithProgress = packageManager.AddPackageByUriAsync(packageUri, addPackageOptions);
                DeploymentResult results = await asyncOperationWithProgress.AsTask();

                if (asyncOperationWithProgress.Status != AsyncStatus.Canceled && asyncOperationWithProgress.Status == AsyncStatus.Completed)
                {
                    SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_IDLIST, IntPtr.Zero, IntPtr.Zero);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error registering sparse package: {ex.Message}");
                // Handle exception here
            }
        }

        private static string FindPackageFile(string directory, string searchPattern)
        {
            string[] files = Directory.GetFiles(directory, searchPattern);
            return files.Length != 0 ? Path.GetFileName(files[0]) : null;
        }

        [DllImport("Shell32.dll")]
        public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);
    }
}
