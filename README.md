<p align="center">
  <h1 align="center">Tower Defense 3D</h1>
  <p align="center">
    3D Tower Defense with Procedurally Generated Levels
    <br />
    <a href="https://minhd-vu.github.io/tower-defense-3d/">Play Online</a>
    .
    <a href="https://github.com/minhd-vu/tower-defense-3d/releases">Releases</a>
    ·
    <a href="https://github.com/minhd-vu/tower-defense-3d/blob/master/report.pdf">Technical Report</a>
  </p>
</p>

https://user-images.githubusercontent.com/18576915/145604368-ccfb6707-08f0-457f-8230-ac9beb1c768b.mp4

https://user-images.githubusercontent.com/18576915/145604387-f9129cd8-0d23-4c5f-aa3f-595e7cb6f7b0.mp4

## Project Proposal
Create a 3d tower defense game that has procedurally generated levels.

### Requirements
#### Procedurally Generated Level
The tower defense level will be procedurally generated. There should be at least one path from enemy spawn to the tower. The tower will be placed randomly. There should be tiles that cannot have weapons placed on them (like trees), and tiles that can. Weapons should not be able to be placed on the path.
#### User Interface
There should be a tower health indicator. There should be a menu for selecting weapons to build. Once selected, the player should be able to select a tile, and the weapon should build if funds are sufficient. There should be a currency indicator. There should be buttons for starting and stopping the movement of enemies (like a pause).
#### Weapons
There should be mutiple types of weapons that vary in damage, rate of fire, and range. Weapons will fire projectiles at enemies in their range. The player should be able to build these weapons on tiles in exchange for currency.
#### Enemies
Enemies will have pathfinding towards to tower. When enemies reach the tower, a corresponding tower health deduction should take place. Enemies will have varying health, speed, and currency drops. Enemies should detect collisions with projectiles and take damage from them.

### Reach Requirements
- Upgradable Weapons
- Weapon Selling

## Technologies

- Unity

## Contributors

- Tori Broadnax
- Jeffrey Do
- Richard Roberts
- Minh Vu

## References
- [3D Tilemap in Unity](https://youtu.be/ulFc6p3hQzQ)
- [Brackeys Unity Tower Defense](https://www.youtube.com/playlist?list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0)
- [Manual Tilemap Unity](https://forum.unity.com/threads/how-can-i-place-a-tile-in-a-tilemap-by-script.508338/)
- [Procedural Tilemap](https://blog.unity.com/technology/procedural-patterns-you-can-use-with-tilemaps-part-i)
- [Kenney Tower Defense](https://www.kenney.nl/assets/tower-defense-kit)
- [Unity GitHub Actions](https://isaacbroyles.com/gamedev/2020/07/04/unity-github-actions.html)

## CI/CD

To create a release for the project, push it with a `tag` which would be the `version number` (e.g. `v0.1`).
```
git push origin <tag>
```
