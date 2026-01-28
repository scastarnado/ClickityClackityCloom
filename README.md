<img width="549" height="451" alt="image" src="https://github.com/user-attachments/assets/1e7ac67c-0bde-4d93-83d5-87d750b9c7e5" />

# ClickityClackityCloom

A feature-rich autoclicker for Windows with macro recording and playback capabilities.

## Overview

ClickityClackityCloom is a Windows Forms-based autoclicker application designed to automate repetitive clicking tasks. Built with .NET Framework 4.8, it provides both simple clicking automation and advanced macro recording/playback features for complex click sequences.

### Key Capabilities

- **Automated Mouse Clicking**: Configurable click automation with precise timing control
- **Macro Recording & Playback**: Record complex click sequences and replay them
- **Macro Management**: Save, load, import, and organize multiple macro files
- **Flexible Click Patterns**: Support for left, middle, and right mouse buttons with single or double click modes
- **Customizable Intervals**: Set click intervals from milliseconds to hours
- **Location Options**: Click at current cursor position or fixed coordinates
- **Hotkey Support**: Global hotkeys (F6/F7) to start and stop automation

## Features

### Click Automation
- **Mouse Button Selection**: Left, Middle, or Right click
- **Click Type**: Single click or Double click
- **Repeat Modes**:
  - Repeat indefinitely until stopped
  - Repeat a specific number of times
- **Location Modes**:
  - Current cursor position
  - Fixed position with coordinate picker
- **Timing Control**: Precise interval settings (hours, minutes, seconds, milliseconds)

### Macro System
- **Recording**: Capture mouse clicks with exact positions and timing
- **Playback**: Replay recorded macros with original timing
- **Management**: 
  - Save macros with custom names
  - Load and edit existing macros
  - Import/export macro files
  - Delete unwanted macros
- **Validation**: Built-in validation for macro file structure and integrity

### User Interface
- Clean, intuitive Windows Forms interface
- Real-time status display (Playing/Stopped)
- Visual feedback for all operations
- Localization support (English and Spanish)

## Getting Started

### Download
1. Navigate to the [Releases](https://github.com/scastarnado/ClickityClackityCloom/releases) page
2. Download the latest release
3. Extract the files to a folder of your choice

### Installation
No installation required - simply run `ClickityClakityCloom.exe`

> **Note**: Windows SmartScreen may display a warning because the application is not code-signed by a recognized publisher. This is normal for open-source software. You can safely run the application by clicking "More info" and then "Run anyway".

### Basic Usage

1. **Simple Clicking**:
   - Set your desired interval (hours, minutes, seconds, milliseconds)
   - Choose mouse button (Left/Middle/Right)
   - Select click type (Single/Double)
   - Choose location mode (Current Position/Fixed Position)
   - Press F6 to start, F7 to stop

2. **Recording a Macro**:
   - Click the "Record" button
   - Perform your click sequence
   - Click "Stop Recording"
   - Save the macro with a custom name

3. **Playing a Macro**:
   - Select a macro from the dropdown list
   - Click "Play Macro" or use the hotkey
   - The macro will replay your recorded clicks with original timing

## Hotkeys

- **F6**: Start autoclicker/macro playback
- **F7**: Stop autoclicker/macro playback

## Technical Details

- **Framework**: .NET Framework 4.8
- **Language**: C# 7.3
- **UI Framework**: Windows Forms
- **Dependencies**: Newtonsoft.Json (for macro serialization)
- **Macro Format**: JSON files with click action data

## Macro File Format

Macros are stored as JSON files with the following structure:
```json
{
  "Name": "MacroName",
  "CreatedDate": "2025-01-28T00:00:00",
  "Actions": [
    {
      "X": 100,
      "Y": 200,
      "Interval": 500,
      "MouseButton": "Left",
      "ClickType": "Single",
      "TimeStamp": "2025-01-28T00:00:01"
    }
  ]
}
```

## Use Cases

- Repetitive clicking tasks in games
- Automated testing of UI elements
- Data entry automation
- Productivity enhancement for repetitive workflows
- Accessibility assistance

## Contributing

Contributions are welcome! Please feel free to submit pull requests or open issues for bugs and feature requests.

### Development Setup
1. Clone the repository
2. Open the solution in Visual Studio 2017 or later
3. Restore NuGet packages
4. Build and run

## Security Note

This application uses low-level Windows hooks for mouse automation. It does not collect, transmit, or store any personal data. All macro files are stored locally on your machine. The source code is fully available for review.

## Disclaimer

This software is provided "as is" without warranty of any kind. Use responsibly and in accordance with the terms of service of any applications or games you automate. The developers are not responsible for any consequences resulting from the use of this software, including but not limited to violations of terms of service or automated behavior policies.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.

## Support

For issues, questions, or feature requests, please open an issue on the [GitHub repository](https://github.com/scastarnado/ClickityClackityCloom/issues).
