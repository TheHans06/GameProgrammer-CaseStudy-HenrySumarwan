# Lentera Nusantara - Game Programmer Case Study

A 3D exploration prototype featuring a 2D memory match puzzle UI, developed as a technical assessment for a Game Programmer role.

## Core Features Implemented
* **3D Exploration:** Third-person character movement within a 3D environment.
* **Object Interaction:** Proximity-based interaction with specific 3D objects in the scene.
* **State Management (Freeze State):** Player character movement is completely locked (frozen) while the 2D puzzle is active.
* **2D UI Minigame:** A functional Memory Match Card puzzle requiring players to find matching pairs of hidden icons.
* **Seamless Transitions:** Smooth UI closing and immediate return of 3D character control upon puzzle completion or cancellation.

## Controls
* **W, A, S, D** - Move Character
* **Mouse** - Rotate Camera
* **E** - Interact with objects / Start Puzzle

## Shuffling Algorithm - Fisher-Yates
The system utilizes the Fisher-Yates shuffle algorithm to dynamically randomize card layouts every time the puzzle starts. It works by iterating through the card list in a single pass, selecting a random index from the remaining elements, and swapping positions to ensure perfectly unbiased randomness. This approach is highly efficient with an $O(N)$ time complexity, keeping the game lightweight and performant.

## Credits & Third-Party Assets
This project utilizes the following third-party assets from the Unity Asset Store:
* **[Ultimate Low Poly Dungeon]** - 3D Environment & Level Design
* **[FREE Low Poly Human - RPG Character]** - 3D Player Character Model & Animations
* **[CultistLike Card Game]** - Card Game Mechanics & Packs
* **[2D Cards Game Art Pack]** - 2D Puzzle GUI & Visual Elements

## PROJECT LINKS :
* **DEMO VIDEO** - [https://youtu.be/KOZt2silHNc](https://youtu.be/KOZt2silHNc)
* **BUILD** - [https://drive.google.com/file/d/1F2L9FzBihJVybyHSibzCbTaWuvPgZbLy/view?usp=sharing](https://drive.google.com/file/d/1F2L9FzBihJVybyHSibzCbTaWuvPgZbLy/view?usp=sharing)
