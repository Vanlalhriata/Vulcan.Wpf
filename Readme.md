# Vulcan.Wpf

Vulcan.Wpf is a WPF framework that uses MEF for Dependency Injection.

### Usage:

###### Infrastructure:
* Add the Core and Utils projects to your solution, or make references to their binaries.
* In your WPF application, Replace `Application` with `Vulcan.Wpf.Core.FrameworkApp` in App.xaml.
* Derive your main window from `Vulcan.Wpf.Core.Shell` instead of `Window`.
* Make sure to include a `Frame` in your main window. This is where the views will be placed.
* Set the `Shell.StartUpView` property to the alias of the first view.

###### Views and ViewModels:
* Views are derived from `Vulcan.Wpf.Core.View`. They can be exported using the `ViewExport` attribute.
* ViewModels are derived from `Vulcan.Wpf.Core.ViewModelBase`. They can be exported using the `ViewModelExport` attribute.
* DataContexts can be set in XAML as follows:

    `DataContext="{Binding MainViewModel, Source={x:Static v:FrameworkApp.ViewModelLocator}}"`

### Notes:
* The namespace for the framework is http://schemas.makkati.in/wpf/framework

### License:

Licensed under the MIT License