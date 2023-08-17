# ContextMenu for ILSpy - New Windows 11 Context Menu Including Windows 10 now

## Update 
Change: ContextMenuImplementation.dll now needs to be placed in the same directory as ILSpy.exe.
Rationale: This simplifies path handling and ensures that the correct paths are automatically constructed without additional calculations.

Action:

1. Place ContextMenuImplementation.dll in the same directory as ILSpy.exe.
2. Utilize the provided code, including the LaunchILSpy function and the Invoke method, without modification.
3. Register the context menu handler with the updated location of ContextMenuImplementation.dll.
4. Confirm that COM registration and shell extension registration steps are performed accurately.

Result: The context menu will work seamlessly, launching ILSpy.exe for the selected file, and no additional path information is required.



## Description

ContextMenu for ILSpy is a helpful extension that enhances the user experience of ILSpy, a popular .NET assembly browser and decompiler. This extension brings the new Windows 11 context menu style to ILSpy, making it visually appealing and consistent with the latest Windows design guidelines.

## Features

- Modern Windows 11 context menu style.
- Improved visual aesthetics.
- Enhanced user experience.

## Credits

ContextMenu for ILSpy is built upon the foundation of the outstanding ILSpy project, a product of dedicated open-source contributors. We extend our sincere gratitude to the ILSpy team for their remarkable work and contribution to the .NET community.


## Installation

To use ContextMenu for ILSpy, follow these steps:

1. Download the latest release from the [GitHub repository](https://github.com/modz2014/ILSpyContextMenu/releases).
2. Extract the downloaded ZIP file to a location of your choice.

**Note: Before proceeding with the installation, you need to modify a line of code in the project to specify the location of the ILSpy executable.**

3. Open the project solution in your preferred IDE (e.g., Visual Studio).
4. Locate the file responsible for invoking the ILSpy executable. This may be within the source code for the context menu functionality.
5. Look for a line of code similar to this: `sei.lpFile = L"ILSpy.exe";`
6. Modify the path `L"ILSpy.exe"` to point to the location where ILSpy is installed on your machine. For example: `sei.lpFile = L"C:\\Program Files\\ILSpy\\ILSpy.exe";`

## Building the MSIX Package

After modifying the code to point to the correct ILSpy installation location, you can proceed with building the MSIX package to use the ContextMenu for ILSpy extension.

1. Ensure that you have the necessary MSIX packaging tools installed on your system. If you don't have them, download and install the [Windows Application Packaging Project](https://docs.microsoft.com/en-us/windows/msix/packaging-tool/get-packaging-tool) from the Microsoft Store.
2. In your IDE, right-click on the project solution and select "Add" -> "New Project."
3. Choose "Windows Application Packaging Project" from the list of project templates and give it an appropriate name (e.g., "ContextMenuILSpyMSIX").
4. Right-click on the new packaging project in the solution explorer and select "Add Reference."
5. Add a reference to the project containing the ContextMenu for ILSpy extension.
6. Open the `Package.appxmanifest` file in the packaging project, and under the "Applications" tab, select the main executable of your project as the entry point.
7. Build the packaging project to generate the MSIX package.

## Installation via MSIX Package

Now that you have the MSIX package, you can install the ContextMenu for ILSpy extension on your Windows 11 machine:

1. Locate the generated MSIX package file (usually with a `.msix` extension).
2. Double-click the MSIX package file to start the installation.
3. Follow the on-screen instructions to install the extension.
4. Once the installation is complete, open ILSpy on your Windows 11 machine.
5. Right-click on various elements within ILSpy, such as assemblies, classes, methods, or fields, and the new Windows 11 context menu will appear.

## Feedback and Contributions

Feedback, bug reports, and suggestions are welcome! If you encounter any issues or have ideas for improving the extension, please [open an issue](https://github.com/modz2014/ILSpyContextMenu/issues) on the GitHub repository.

If you'd like to contribute to the development of ContextMenu for ILSpy, you can fork the repository, make changes, and submit a pull request. Your contributions are highly appreciated.

## License

ContextMenu for ILSpy is licensed under the [MIT License](https://opensource.org/licenses/MIT), which allows you to use, modify, and distribute the extension freely.

## Disclaimer

This extension is not officially affiliated with or endorsed by ILSpy, Windows 11, or Microsoft. It is an independent project developed by the community to enhance the ILSpy user experience.

Please use this extension responsibly and always back up your important data before installing any software.

## Contact



Happy decompiling with the new Windows 11 context menu in ILSpy!

![ContextMenu for ILSpy](https://github.com/modz2014/ILSpyContextMenu/blob/main/Contextmenu.png)
