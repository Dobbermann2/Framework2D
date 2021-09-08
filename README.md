# Framework2D
A 2D game framework using OpenTK inspired by Monogame


## Project layout ##
* **Framework2D**
  - Framework2D is responsible for lower-level rendering and input. It uses OpenTK to batch-render 2D sprites.
  
* **SandboxGame**
  - Debugging/Testing project to test Framework2D
  
* **Engine2D**
  - The Engine2D implements higher-level game engine concepts like an Entity-Component-System and [wip] 2D Physics


* **EngineSandbox**
  - Debugging/Testing project to test engine.
  
  
  
## Features ##

Framework2D:
  - Game Loop
  - Fast batch rendering
  - Texture batching
  - Spritesheet support
  - [Wip] Asset management (Currently only textures)

Engine2D:
  - Entity-Component-System
  - Custom user script components
  - Scene serialization
  - [Wip] Runtime c# code compilation to allow users to script custom components
  - [Wip] 2D Physics using ChipmunkSharp
  - 
