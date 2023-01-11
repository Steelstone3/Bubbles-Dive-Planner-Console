Bubbles Dive Planner Console
============================

The idea behind Bubbles Dive Planner is to aim for scuba divers to be able to perform the safe planning of scuba diving activities. Currently supports the Bulhmann dive model with more planned and gas management for a single cylinder with multiple cylinders planned.

The project uses Rust with no external crates libraries.

### Running Bubbles Dive Planner

> cd ~/Bubbles-Dive-Planner-Console/BubblesDivePlanner
> 
> dotnet restore
>
> dotnet build
> 
> dotnet run

This application has been tested to run on debain derived Linux, Windows 10 and Mac OS 10 beyond this scope your experiences may vary.

### Tests

> cd ~/Bubbles-Dive-Planner-Console/BubblesDivePlannerTests
> 
> dotnet restore
>
> dotnet build
>
> dotnet test

### Dependencies

Follow the steps for installing dotnet runtime for your given operating system.

> https://dotnet.microsoft.com/en-us/download/dotnet/6.0

Install the following .net tool and use its upgrade feature to keep 3rd party packages updated

> dotnet tool install --global dotnet-outdated-tool
>
> dotnet outdated --upgrade

### Legal

I nor any contributors to this project can guarantee absolute safety when using "Bubbles Dive Planner Console" for recreational or technical scuba diving activities. Please seek proper training from a recognised agency before partaking in any scuba diving activities. In addition please consult published dive tables, use a dive computer and other such guarantee's to "sanity check" "Bubbles Dive Planner Console".