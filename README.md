# Xamarin.Forms.CustomControls
Different custom controls for Xamarin.Forms. 

[![Build Status](https://dev.azure.com/flokri/Xamarin.Forms.CustomControls/_apis/build/status/CD%20CustomControls%20YAML?branchName=release&stageName=Publish%20Release%20NuGet%20Package)](https://dev.azure.com/flokri/Xamarin.Forms.CustomControls/_build/latest?definitionId=2&branchName=release) [![](https://img.shields.io/nuget/vpre/Xamarin.Forms.CustomControls.svg)](https://nuget.org/packages/Xamarin.Forms.CustomControls)

## What controls are available?
Currently available controls are:
  - Entries:
    - Animated Border Entry
    - Floating Entry
    - Material Entry
    - Confirmation Entry
    - Borderless Entry
    
## How to use?
The project is available on nuget.org (https://www.nuget.org/packages/Xamarin.Forms.CustomControls) or you just simly clone the repo and use reference to the project inside the src folder.

To use a custom control inside your xaml code you just need to add a reference to the CustomControl package/project. 

```
<ContentPage 
  xmlns="http://xamarin.com/schemas/2014/forms" 
  xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"  
  xmlns:cc="clr-namespace:Xamarin.Forms.CustomControls;assembly=Xamarin.Forms.CustomControls">
   ...
</ContentPage>
```

Next, simply take a custom control and customize it to your liking!

```
<cc:AnimatedBorderEntry
    EndColor="Purple"
    GradientColor="True"
    Placeholder="Animated"
    StartColor="Violet"
    TitleColor="Gray" />
```

## Entries
The package include a series of custom entries. All entries does support a bunch of bindable properties so you can customize e.g. the color of any part of the control.

The following custom entries are available.

### Borderless Entry
The borderless entry is exactly what the name implies, a entry eithout any border ;)

It has the same bindable properties as a default entry in Xamarin.Forms.

### Floating Label Entry
The floating label entry is a simple version of a material entry. It features a placeholder label that translate to the top of the entry and works as title when the user focused the entry. It will stay as title as long the focus will be on the entry or the entry text is not string.Empty.

The floating label entry has the following bindable properties (additional to the default properties):

| Property | What it does | Extra info |
| ------ | ------ | ------ |
| `PlaceholderColor` | The color of the placeholder. | |
| `TitleColor` | The color of the title. | |

### Material Entry
The material entry works like the floating label entry only that this entry has a bottom border. 

The material entry has the following bindable properties (additional to the default properties):

| Property | What it does | Extra info |
| ------ | ------ | ------ |
| `PlaceholderColor` | The color of the placeholder. | |
| `TitleColor` | The color of the title. | |
| `DefaultBorderColor` | The default color of the bottom border. | Will be applied when the entry is not focused and the text is empty. |
| `ActiveBorderColor` | The active color of the bottom border. | Will be applied when the entry is focused or the text is not empty. |


### Animation Border Entry
The animation border entry will draw an border, as progress animation, around the entry when the entry is be focused or the text is not empty. Be sure to use some extra margin around the entry when using it.

The animation entry has the following bindable properties (additional to the default properties):

| Property | What it does | Extra info |
| ------ | ------ | ------ |
| `PlaceholderColor` | The color of the placeholder. | |
| `TitleColor` | The color of the title. | |
| `GradientColor` | Specify if the border color should use a gradient. | The gradient is a linear gradient from after the title around the entry to the start of the title.  |
| `StartColor` | The start color of the border. | If gradient is false, the border color will be the start color.  |
| `EndColor` | The end color of the border. |  |

### Confirmation Entry
The confirmation entry serves as one time entry. It is usefull for e.g. newsletter registration. The validation if the value is correct is made via a behavior.

The confirmation entry has the following bindable properties (additional to the default properties):

| Property | What it does | Extra info |
| ------ | ------ | ------ |
| `PlaceholderColor` | The color of the placeholder. | |
| `TitleColor` | The color of the title. | |
| `ConfirmText` | the text of the lable that will be shown when the user taps the confirmation button. | |
| `CornerRadius` | The corner radius of the form containing the entry. | |
| `ViewBackgroundColor` | The background color of the confirmation form. | |
| `ButtonBackgroundColor` | The color of the confirmation button. | |

To verify if the entred text is valid the confirmation dialog needs a validaton behavior. In the example of an newsletter registration entry the behavior checks if the entered string is a valid email (the email validation behavior pre exists in the package).
To use it just add the behavior to the entry:

```
<cc:ConfirmationEntry
    ButtonBackgroundColor="#5c94cd"
    ButtonText="OK"
    ConfirmText="Thank's for registration!"
    CornerRadius="5"
    Keyboard="Email"
    Placeholder="Email Address"
    PlaceholderColor="#5a97d6"
    TextColor="White"
    TitleColor="#5a97d6"
    ViewBackgroundColor="#044f9e">
    <cc:ConfirmFloatingLabelEntry.Behaviors>
        <cc:EmailValidationBehavior />
    </cc:ConfirmFloatingLabelEntry.Behaviors>
</cc:ConfirmationEntry>
```
