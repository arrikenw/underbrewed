**The University of Melbourne**
# COMP30019 – Graphics and Interaction


## Table of contents
* [Team Members](#team-members)
* [Introduction](#introduction)
* [How to Play](#how-to-play)
* [Graphics and Camera Motion](#using-images)
* [Shaders](#shaders)
* [Evaluation Methods](#evaluation-methods)
* [Feedback Changes](#feedback-changes)
* [Resource References](#resource-references)
* [Individual Contributions](#individual-contributions)


## Team Members

| Name | Task | State |
| :---         |     :---:      |          ---: |
| Arriken Worsley  | Scenes/Models     |  Done |
| Joel Kenna    | Game Logic      |  Done |
| Simon Tran  | Game Logic      |  Done |
| Iris Li  | UI      |  Done |


## Introduction
Undercooked is a third person casual potion brewing simulator(?). During a level, the player must run around the game, preparing different potions in order to deliver orders on time. Points are given for every order delivered on time with the aim being to get the highest score possible. When an incorrect potion is brewed bad effects can occur, changing the gameplay. The entire game is played with a static camera from a third person perspective high above the level. (bad writing please change entirely) (images needed)


## How to Play
dot dot dot


## Graphics and Camera Motion

### Graphics

### Camera Motion
#### Third Person Static Camera
The game is primarily played with a static camera. The camera is placed high above the level, similar to a bird's eye view, allowing the player to see everything as the play the game. This camera position was chosen as it made the entire level viewable for the player while not issues of traditional cameras, such as occlusion. (image needed)

#### Action Replay Camera
When a level finishes an "action replay" occurs, with the camera moving down towards the player to produce the end game screen. The camera's movements will always to the opposite quarter of the level that the player is on in order to avoid occlusion from the level's walls. (image needed)


## Shaders

### Fire Shader
dot dot dot

## Potion Liquid Shader
dot dot dot

## Screen Distortion Shader
dot dot dot

## Evaluation Methods

### Querying Method
dot dot dot

### Observational Method
dot dot dot

## Feedback Changes
dot dot dot

## Resource References
dot dot dot

## Individual Contributions
dot dot dot
	
## Technologies
Project is created with:
* Unity 2019.4.3f1
* Ipsum version: 2.33
* Ament library version: 999

## Using Images

You can use images/gif by adding them to a folder in your repo:

<p align="center">
  <img src="Gifs/Q1-1.gif"  width="300" >
</p>

To create a gif from a video you can follow this [link](https://ezgif.com/video-to-gif/ezgif-6-55f4b3b086d4.mov).

## Code Snippets 

You can include a code snippet here, but make sure to explain it! 
Do not just copy all your code, only explain the important parts.

```c#
public class firstPersonController : MonoBehaviour
{
    //This function run once when Unity is in Play
     void Start ()
    {
      standMotion();
    }
}
```




