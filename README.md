# MidTerm Window Programming

A Windows desktop application project developed using C# and Windows Forms as part of the MidTerm Window Programming course.

The project consists of two independent applications: a desktop Paint application and a Slide Piano rhythm game. The project focuses on Windows Forms GUI development, event-driven programming, graphical rendering, keyboard and mouse interaction, timer-based processing, and modular application design.

## Project Overview

This project demonstrates how C# Windows Forms can be used to build interactive desktop applications with custom user interfaces and real-time user interaction.

The project contains two main applications:

1. **Paint Application** — A lightweight drawing application that supports shape creation, object manipulation, grouping, and visual property customization.
2. **Slide Piano Game** — A rhythm-based game in which players interact with falling notes using keyboard input and are evaluated based on their timing and accuracy.

The applications are structured into separate components to keep user interface code, application logic, and assets organized and maintainable.

## Project Structure

```text
MidTerm-WindowProgramming/
│
├── PaintApplication/
│   ├── MainForm.cs
│   ├── SettingsForm.cs
│   ├── ShapeManager.cs
│   ├── PenProperties.cs
│   └── BrushProperties.cs
│
├── SlidePiano/
│   ├── ChooseMap.cs
│   ├── Form1.cs
│   └── ResultForm.cs
│
├── Assets/
│   ├── Audio/
│   └── NoteData/
│
└── README.md
```

## Application 1: Paint Application

The Paint Application provides a graphical canvas for creating and manipulating basic geometric shapes. It is designed around mouse-driven interaction and separates shape management from the user interface.

### Main Form

`MainForm.cs` is the primary interface of the drawing application.

The main canvas provides an interactive area where users can create and manipulate graphical objects.

The application handles mouse events including:

* `MouseDown`
* `MouseMove`
* `MouseUp`

These events are used to implement drawing and object manipulation operations.

### Drawing Tools

The application supports several basic drawing tools:

* Rectangle
* Ellipse
* Line
* Arc
* Mouse / Selection mode

Users can select a drawing tool and drag the mouse across the canvas to create a corresponding shape.

### Shape Management

`ShapeManager.cs` is responsible for managing the graphical objects created on the canvas.

The application supports:

* Creating shapes
* Selecting objects
* Moving objects
* Grouping multiple objects
* Ungrouping grouped objects

The separation of shape management from the main form helps keep drawing logic independent from interface-related code.

### Object Manipulation

In selection mode, users can interact with existing shapes directly on the canvas.

The application supports moving objects by clicking and dragging them to a new position.

### Group and Ungroup

Multiple shapes can be grouped into a single logical object.

Grouping allows users to manipulate several shapes together, while ungrouping restores the individual shapes so they can be edited separately.

### Drawing Properties

The application provides dedicated components for controlling visual properties.

#### Pen Properties

`PenProperties.cs` manages outline properties such as:

* Color
* Width
* Dash style

#### Brush Properties

`BrushProperties.cs` manages the fill properties of shapes, including:

* Fill color

### Settings

`SettingsForm.cs` provides customization options for the application's user interface.

Users can modify:

* Top panel color
* Bottom panel color
* Text color

This separates application appearance settings from the main drawing interface.

## Application 2: Slide Piano Game

The Slide Piano application is a keyboard-controlled rhythm game based on falling notes.

The game uses a four-lane layout. Notes are loaded from external data files and move toward a designated hit line. Players must press the corresponding keyboard key when a note reaches the appropriate position.

The game combines Windows Forms controls, keyboard events, timers, file-based note data, and real-time game state management.

### Map Selection

`ChooseMap.cs` provides the main menu for selecting a song or game map.

The interface contains:

* `ComboBox` (`cbMusic`) for selecting available music
* `Start` button (`btnStart`) for launching the selected map
* `Exit` button (`btnExit`) for closing the application

### Game Form

`Form1.cs` contains the main gameplay logic.

The primary gameplay components include:

* `Panel` (`plGame`) for the game area
* `PictureBox` (`pbHitLine`) for displaying the hit line
* `Timer` (`gameTimer`) for controlling the game loop

The game panel is divided into four lanes corresponding to the available keyboard inputs.

### Game Loop

The game timer controls the main gameplay process.

During each timer update, the application handles tasks such as:

1. Loading and spawning notes according to the selected map.
2. Updating note positions.
3. Moving notes vertically toward the hit line.
4. Detecting player input.
5. Determining whether notes are successfully hit or missed.
6. Updating score and combo information.
7. Calculating gameplay accuracy.

### Note System

Game notes are defined using external text files stored in the project assets.

The note data determines when and where notes should appear during gameplay.

This approach separates gameplay data from the game logic, making it possible to create or modify maps without changing the core gameplay implementation.

### Keyboard Input

Players interact with the game using keyboard keys associated with the four lanes.

When a key is pressed, the game evaluates the corresponding note and determines whether the input occurs within the valid hit window.

### Scoring System

The game tracks several performance metrics:

* Score
* Combo
* Accuracy
* Hit and miss results

These metrics are displayed after the game ends.

## Result Screen

`ResultForm.cs` displays the player's final performance after completing a song.

The result screen includes:

* Final score
* Accuracy
* Maximum combo

The user can then choose between:

* `Replay` (`btnReplay`) to play the selected map again
* `Menu` (`btnMenu`) to return to the map selection screen

## Architecture

The project follows a modular structure that separates the user interface from application-specific logic and external resources.

### GUI Layer

The GUI layer contains the Windows Forms used to interact with the applications.

Examples include:

* `MainForm.cs`
* `SettingsForm.cs`
* `ChooseMap.cs`
* `Form1.cs`
* `ResultForm.cs`

### Core Logic Layer

The core logic contains the components responsible for application behavior.

For the Paint Application:

* Shape creation and management
* Object selection and movement
* Grouping and ungrouping
* Drawing properties

For the Slide Piano Game:

* Note spawning
* Note movement
* Keyboard input processing
* Hit detection
* Score calculation
* Combo tracking
* Accuracy calculation
* Game state management

### Assets and Data

The project stores external resources separately from application logic.

```text
Assets/
├── Audio/
└── NoteData/
```

Audio files provide the music used by the Slide Piano application, while text-based note data defines the timing and lane information required by each map.

## Technologies

| Technology          | Purpose                               |
| ------------------- | ------------------------------------- |
| C#                  | Application programming language      |
| Windows Forms       | Desktop GUI framework                 |
| .NET Framework      | Application runtime and framework     |
| Visual Studio 2022  | Development environment               |
| System.Drawing      | Graphical rendering and shape drawing |
| Windows Forms Timer | Real-time game loop processing        |
| Text files          | Note and map data storage             |

Windows Forms is designed for interactive Windows desktop applications and provides controls, event handling, mouse and keyboard input, and graphical capabilities suitable for this project.

## Key Concepts Demonstrated

This project demonstrates several fundamental concepts of Windows desktop application development:

* Event-driven programming
* Windows Forms GUI design
* Mouse event handling
* Keyboard event handling
* Custom graphical rendering
* Object-oriented programming
* Modular application architecture
* Timer-based processing
* File-based data management
* State management
* User interface customization
* Interactive object manipulation

## How to Run

### Requirements

* Windows operating system
* Visual Studio 2022
* C# development tools
* .NET Framework compatible with the project

### Running the Project

1. Clone or download the repository.
2. Open the project or solution in Visual Studio 2022.
3. Restore or verify the required project dependencies.
4. Select the application you want to run.
5. Build the project.
6. Press `F5` or select `Start` in Visual Studio.

For the Slide Piano application, make sure that the required audio files and note data are available in the expected `Assets` directories before running the game.

## Current Status

### Paint Application

Implemented:

* Basic shape drawing
* Rectangle, ellipse, line, and arc tools
* Shape selection and movement
* Pen customization
* Brush customization
* UI color customization
* Group and ungroup operations

### Slide Piano Game

Implemented:

* Music selection menu
* Four-lane gameplay
* Note spawning
* Note movement
* Keyboard-based interaction
* Hit and miss detection
* Score calculation
* Combo tracking
* Accuracy calculation
* Result screen
* Replay and menu navigation

## Future Improvements

Potential improvements include:

* More advanced drawing tools
* Shape resizing and rotation
* Undo and redo functionality
* Saving and loading drawing projects
* Improved collision and hit detection
* More flexible map generation
* Additional game modes
* Visual and audio feedback
* Improved animations and UI transitions
* More comprehensive game statistics
* Improved responsive layout and interface design

## Authors

**Students**

* Tran Gia Kiet
* Mai Viet Thanh

**Instructor**

* PhD. Le Van Vinh

**Institution**

* Ho Chi Minh City University of Technology and Education (HCMUTE)

## Project Purpose

This project was developed as a practical application of Windows Forms programming concepts. It combines graphical editing and interactive game development to demonstrate the use of GUI controls, event-driven programming, user input processing, graphical rendering, timers, and modular software design.
