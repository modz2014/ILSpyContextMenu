# ContextMenu for ILSpy - Elevate Your ILSpy Experience with a Modern Context Menu

## Update: Simplified ILSpy Integration
Now, integrating with ILSpy is easier than ever. To use the ContextMenu for ILSpy extension:

1. Place the `ContextMenuImplementation.dll` file in the same directory as your `ILSpy.exe`.
2. Utilize the provided code without any modifications.
3. Register the context menu handler with the updated location of `ContextMenuImplementation.dll`.
4. Ensure precise COM registration and shell extension steps.

Result: Enjoy a seamless context menu experience, launching ILSpy for selected files, without the need for extra path handling.

## Description

Experience ILSpy like never before with the ContextMenu for ILSpy extension. This remarkable addition transforms the appearance of ILSpy's context menu into a modern and visually appealing Windows 11 style. Elevate your .NET assembly browsing and decompiling journey with this integration that aligns perfectly with the latest Windows design guidelines.

## Features

- Revamped Windows 11 context menu style.
- Enhanced visual aesthetics that blend seamlessly with Windows 11.
- An upgraded user experience that aligns with modern design trends.

## Credits

ContextMenu for ILSpy builds upon the foundation of the outstanding ILSpy project, a result of relentless dedication from open-source contributors. Our heartfelt appreciation goes out to the ILSpy team for their incredible work and contributions to the .NET community.

ILSpy: [https://github.com/icsharpcode/ILSpy](https://github.com/icsharpcode/ILSpy)

## Installation

To incorporate ContextMenu for ILSpy into your workflow, follow these straightforward steps:

1. Download the latest release from the [GitHub repository](https://github.com/modz2014/ILSpyContextMenu/releases).
2. Extract the downloaded ZIP file to a location of your preference.

## Building the MSIX Package

Once you've adjusted the code to point to your ILSpy installation, proceed to create the MSIX package for the ContextMenu for ILSpy extension:

1. Ensure that you have the required MSIX packaging tools installed. If not, download and install the [Windows Application Packaging Project](https://docs.microsoft.com/en-us/windows/msix/packaging-tool/get-packaging-tool) from the Microsoft Store.
2. Right-click your project solution in the IDE and select "Add" -> "New Project."
3. Choose "Windows Application Packaging Project" from the project templates list and provide a suitable name (e.g., "ContextMenuILSpyMSIX").
4. Access the project's "Add Reference" menu by right-clicking the new packaging project in the solution explorer.
5. Add a reference to the project containing the ContextMenu for ILSpy extension.
6. Open the `Package.appxmanifest` file within the packaging project. Under the "Applications" tab, designate your project's main executable as the entry point.
7. Build the packaging project to generate the MSIX package.

## Installation via MSIX Package

With your MSIX package ready, install the ContextMenu for ILSpy extension on your Windows 11 machine:

1. Locate the generated MSIX package file (usually with a `.msix` extension).
2. Double-click the MSIX package file to initiate the installation.
3. Follow the on-screen prompts to finalize the installation process.
4. Once installation completes, launch ILSpy on your Windows 11 system.
5. Right-click on various ILSpy elements such as assemblies, classes, methods, or fields to experience the revamped Windows 11 context menu.

## Feedback and Contributions

We warmly welcome feedback, bug reports, and suggestions! Should you encounter any issues or possess ideas for refining the extension, please don't hesitate to [open an issue](https://github.com/modz2014/ILSpyContextMenu/issues) on the GitHub repository.

For those inclined to contribute to ContextMenu for ILSpy, feel free to fork the repository, make your enhancements, and submit a pull request. We greatly value your contributions.

## License

ContextMenu for ILSpy operates under the [MIT License](https://opensource.org/licenses/MIT), granting you the freedom to use, modify, and distribute the extension as needed.

## Disclaimer

This extension maintains an independent status and is not officially endorsed by or affiliated with ILSpy, Windows 11, or Microsoft. It represents a collaborative community project aimed at enhancing the ILSpy user experience.

Please approach extension usage responsibly and take precautionary measures to safeguard your critical data before installing new software.

## Contact

For inquiries or feedback, don't hesitate to reach out.

Wishing you a productive and enjoyable time exploring .NET with the fresh Windows 11 context menu in ILSpy!

![ContextMenu for ILSpy](https://github.com/modz2014/ILSpyContextMenu/blob/main/Contextmenu.png)
