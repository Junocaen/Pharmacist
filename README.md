# Custom Reactui.Events
The ReactiveUI.Events nuget package is not build for modern uwp application since it is stuc on UAP v10.0.16299. ReactiveUI.Events is nothing more then running the Pharmacist.Console application and serve it in an SKD version.   

## Generate Event wrapper file with Pharmacist.Console
[Pharmacist (Github)](https://github.com/reactiveui/Pharmacist) does not support a TargetFramework, only hardcoded. This version changed the hardcoded version of 10.0.16299 to 10.0.18362.   

To run the Pharmacist.Console tool go to the root of this project and enter this command:

```powershell
PS F:\dev\ReactiveUiEvents4UAP\Pharmacist\Pharmacist\src\Pharmacist.Console> dotnet run generate-platform --platforms=uwp --output-path="F:/dev/ReactiveUiEvents4UAP/Pharmacist/Generated_Events_CS/" --output-prefix="Events_"
```

### Edit Framework version yourself 

Change the version ```src/Pharmacist.Core/Extractors/PlatformExtractors/UWP.cs``` for uwp. When editing this, make sure the version is supported in the NuGetFrameworkHelper ```src/Pharmacist.Core/NuGet/NuGetFrameworkHelper.cs```

## Add the SDKReferences when using ReactiveUI.Events (uwp) or the Pharmacist generated file

ReactiveUI.Events does not add the neccesarry skd reverences (neither the Pharmacist generated files). Pharmacist generate wrappers of everything in the ```C:\Program Files (x86)\Windows Kits\10\UnionMetadata\10.0.18362.0\Windows.winmd```, so also the Extension SDKs in ```C:\Program Files (x86)\Windows Kits\10\Extension SDKs```. To prevent weird errors, add atlease the following Univeral Windows Extentions (according to the .csproj of ReactiveUI.Events):
- WindowsDesktop
- WindowsMobile   

To to this either add this to your .csproj

```xml
  <ItemGroup>
    <SDKReference Include="WindowsDesktop, Version=10.0.18362.0">
      <Name>Windows Desktop Extensions for the UWP</Name>
    </SDKReference>
    <SDKReference Include="WindowsMobile, Version=10.0.18362.0">
      <Name>Windows Mobile Extensions for the UWP</Name>
    </SDKReference>
  </ItemGroup>
```

or go in Visual Studio to your project -> righ click on References -> "Add reference..." -> Univeral Windows -> Extentions -> select WindowsMobile and WindowsDesktop with the correct version.
## Add the Events_uwp.cs file to your uwp project file
Just use it where ever you like

[![Build Status](https://dev.azure.com/dotnet/ReactiveUI/_apis/build/status/Pharmacist-CI)](https://dev.azure.com/dotnet/ReactiveUI/_build/latest?definitionId=82) [![Code Coverage](https://codecov.io/gh/reactiveui/pharmacist/branch/master/graph/badge.svg)](https://codecov.io/gh/reactiveui/pharmacist)
<a href="#backers">
        <img src="https://opencollective.com/reactiveui/backers/badge.svg">
</a>
<a href="#sponsors">
        <img src="https://opencollective.com/reactiveui/sponsors/badge.svg">
</a>
<a href="https://reactiveui.net/slack">
        <img src="https://img.shields.io/badge/chat-slack-blue.svg">
</a>
<br />
<br />
<a href="https://github.com/reactiveui/pharmacist">
        <img width="170" height="170" src="https://github.com/reactiveui/styleguide/blob/master/logo_pharmacist/logo.svg"/>
</a>

# Pharmacist

Builds observables from events.

## NuGet Packages

Install the following packages to start using Pharmacist.

| Name                          | Platform          | NuGet                            |
| ----------------------------- | ----------------- | -------------------------------- |
| [Pharmacist.Core][Core]       | Core - Libary     | [![CoreBadge]][Core]             |
| [Pharmacist][GlobalTool]      | Global Tool       | [![GlobalToolBadge]][GlobalTool] |
| [Pharmacist.MSBuild][MsBuild] | MSBuild Task      | [![MsBuildBadge]][MsBuild]       |
| [Pharmacist.Common][Common]   | Common            | [![CommonBadge]][Common]         |

[Core]: https://www.nuget.org/packages/Pharmacist.Core/
[CoreBadge]: https://img.shields.io/nuget/v/Pharmacist.Core.svg

[GlobalTool]: https://www.nuget.org/packages/Pharmacist/
[GlobalToolBadge]: https://img.shields.io/nuget/v/Pharmacist.svg

[MsBuild]: https://www.nuget.org/packages/Pharmacist.MSBuild/
[MsBuildBadge]: https://img.shields.io/nuget/v/Pharmacist.MSBuild.svg

[Common]: https://www.nuget.org/packages/Pharmacist.Common/
[CommonBadge]: https://img.shields.io/nuget/v/Pharmacist.Common.svg

## What does it do?

Pharmacist will convert events within an assembly and create observable wrappers for them. 

It has been called pharmacist since it will generate Reactive Extensions (Rx) Observables for events, so it's a play on word about "Rx" which is short for prescriptions.

It can generate the observables for the following:
* System Platforms, such as Xamarin (iOS/Android/TVOS), UWP
* NuGet packages
* Assemblies.

There is a MSBuild Task version and a global tool version.

## How do I use?

### MsBuild

Pharmacist can automatically create Observable wrappers for projects for their explicitly included NuGet packages.

In your .csproj file you can add:

```xml
<ItemGroup>
    <PackageReference Include="Pharmacist.MsBuild" Version="1.*" PrivateAssets="all" />
    <PackageReference Include="Pharmacist.Common" Version="1.*" />
</ItemGroup>
```

`Pharmacist.MsBuild` contains the MsBuild target file and appropriate tasks. `Pharmacist.Common` includes common source code that is required to invoke the generated observables.

This will generate wrappers only for other included `PackageReference`'s contained within the project file.

For example if you had a inclusion for `Xamarin.Forms` in the same project that includes Pharmacist it will generate Observable wrappers for the included codes. It will not include any child `PackageReference` from other projects included via a `ProjectReference`. You can override that functionality by including `<PharmacistGlobalPackages>true</PharmacistGlobalPackages>` in a `PropertyGroup` inside your project.

Also it will only generate the specified `ProjectReference` and none of it's dependent packages. For example for a ProjectReference inclusion of `ReactiveUI`, it will not generate dependencies such as `System.Reactive`. If you want to generate observable wrappers for the dependent NuGet packages you must explicitly include them. 

### Command Line

Pharmacist comes with a command line version which is useful for generating for a specified platform.

The following values are supported for platforms: `Android`, `iOS`, `Mac`, `WPF`, `UWP`, `Winforms`, `TVOS`

Install the global tool:

```Batchfile
dotnet tool install -g Pharmacist
```

To generate files:

```Batchfile
pharmacist generate-platform -p <Platform> -o c:/directory/for/output --output-prefix="Events"
```

This would generate a file named `Events_<Platform>.cs`, where Platform would be the platform specified.

In the near future this will be changing to TargetFramework with /wpf and /winforms options.

## Contribute

Pharmacist is developed under an OSI-approved open source license, making it freely usable and distributable, even for commercial use. Because of our Open Collective model for funding and transparency, we are able to funnel support and funds through to our contributors and community. We ❤ the people who are involved in this project, and we’d love to have you on board, especially if you are just getting started or have never contributed to open-source before.

So here's to you, lovely person who wants to join us — this is how you can support us:

* [Responding to questions on StackOverflow](https://stackoverflow.com/questions/tagged/reactiveui)
* [Passing on knowledge and teaching the next generation of developers](http://ericsink.com/entries/dont_use_rxui.html)
* [Donations](https://reactiveui.net/donate) and [Corporate Sponsorships](https://reactiveui.net/sponsorship)
* [Asking your employer to reciprocate and contribute to open-source](https://github.com/github/balanced-employee-ip-agreement)
* Submitting documentation updates where you see fit or lacking.
* Making contributions to the code base.
