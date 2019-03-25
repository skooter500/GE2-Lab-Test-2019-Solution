# Game Engines 2 Lab Test 2019

## Rules

- You can access the Unity documentation if you need to look something up, but apart from that no use of any internet resources or notes permitted. 
- No use of any prewritten code apart from what is contained in the starter project.
- Fork and clone this git repository to get the starter project for today's test. This has a starter scene and code to implement a variety of steering behaviours and a state machine implementation. There are also prefabs for the fighters, bases and bullets

This is the scenario you will have to program today:

[![YouTube](http://img.youtube.com/vi/PEg3ZzsyzKA/0.jpg)](https://www.youtube.com/watch?v=PEg3ZzsyzKA)

What is happening:

Bases are depicted by the coloured blocks in the scene. Bases start with 5 units of tiberium and accululate tiberium at a rate of one unit each second. The amount of tiberium is shown as a number above the base. When a base has at least 10 tiberium accumulated, it can spawn a fighter ship (the capsules with the trails in the video) at a cost of 10 units of tiberium. Fighters should take the colour of the base. Fighter ships carry 7 units of tiberium. Fighter ships will choose one of the other bases in the scene and go to an attack position close to the other base. When the figher arrives at an appropriate attack position, it will shoot bullets at the base at a cost of 1 unit of tiberium per bullet. Bullets should be same colour as the ship. When the ship runs out of tiberium, it will return to it's base to refuel. Refueling costs 7 units of tiberium and the ship can only refuel when the base has at least 7 units of tiberium. 

If a bullet hits a base, the base will loose .5 units of tiberium.

Marking Scheme:

| Description | Marks |
|-------------|-------|
| Fighter spawning & base accumulating tiberium | 20 marks |
| Fighter picking a target | 20 marks |
| Fighter shooting | 20 marks |
| Fighter refueling |20 marks |
| Base taking dammage | 10 marks |
| Use of git | 10 marks |

- [Submit the URL to your git repo here](https://docs.google.com/forms/d/e/1FAIpQLSc3LiK98GG_KJL0OtqPJqPlUQX5aGSOZCWD6u7vzpLrD3M_8w/viewform)