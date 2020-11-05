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
- Unity’s default lighting shaders are applied across most objects to provide realistic lighting. 
- Custom fragment shaders are used to add interesting graphical effects, for example by creating a swirl effect for portals and providing colouring for fire particles.
- After the initial render is complete, a custom fragment shader is applied to the initial render texture to provide post-processing effects and generate the final render texture.

### Camera Motion
#### Third Person Static Camera
The game is primarily played with a static camera. The camera is placed high above the level, similar to a bird's eye view, allowing the player to see everything as the play the game. This camera position was chosen as it made the entire level viewable for the player while not issues of traditional cameras, such as occlusion. (image needed)

#### Action Replay Camera
When a level finishes an "action replay" occurs, with the camera moving down towards the player to produce the end game screen. The camera's movements will always to the opposite quarter of the level that the player is on in order to avoid occlusion from the level's walls. (image needed)


## Shaders

### Potion Liquid Shader
The potion liquid shader, produces a swirling liquid, with the liquid slowly falling towards the center. This shader was used for the cauldron liquid, the portal center, as well as the backgrounds for the menus. The shader was produced with help from an online tutorial found [here](http://enemyhideout.com/2016/08/creating-a-whirlpool-shader/). 
<p align="center">
  <img src="Images/CauldronLiquid.gif"  width="300" >
</p>

The first part of the shader is the function rotate, which rotates a point around the center by `rotationAmount` radians. This was done by coverting the initial cartesian point into a polar co-ordinates, increasing the angle by `rotationAmount` radians, and returning the point converted back into a cartesian point.
```Shaderlab
// rotates a point
float2 rotate( float rotationAmount, float2 p)
{
	float a = atan2(p.y, p.x);
	float r = length(p);

	a += rotationAmount;

	return float2(cos(a) * r, sin(a) * r);
}
```
The input uv point is first converted to be relative to the center of the uv texture, then rotated twice with the rotation function. The first rotation rotates all points by a fixed amount, while the latter rotates the points based off a mask called `motion`. This mask increases the rotation speed based of the alpha value of the mask, making the further out points rotate faster.
```Shaderlab
// find point position relative to middle of uv
float2 p = i.uv - float2(0.5, 0.5);

// if point is outside the circle, render it transparent
if (length(p) > 0.5)
{
	return fixed4(0, 0, 0, 0);
}

// For rotation

// _Swirl is general swirl amount, motion.r increases swirl amount the further out it is
p = rotate(_Rotation * _Time * _Speed, p);
p = rotate(_Swirl * (motion.a * _Time), p);
```
Finally, the rotated point is used to calculate the uv point, which is multiplied by the colour to find the final colour output. The x value of the uv is based of time offset by the inverse of the radius of the rotated point. This produces the effect the liquid falling into the center. The y value of the uv is based of the angle of the rotated point. This gives the shader the circlular texture from the initial straight vertical texture.
```Shaderlab
// For texture moving towards center
float2 uv;
        
uv.x = (_Time[0] * _Speed) - (1 / (length(p) + _Swirliness));
// angle of new point
float a = atan2(p.y, p.x);
// divide angle by two pi to get angle in radians scaled to between 0-1
uv.y = a/(3.1416 * 2);

// Now we can get our color.
fixed4 fragColor = tex2D(_MainTex, uv) * _Color;

return fragColor;
```

## Screen Distortion Shader

This shader has two main aspects, a screen shake and distortion effect and a colouring effect. 

The core of the code used to create the screen motion effects is presented below:

```Shaderlab
X = (0.05 * sin(v.vertex.x + 1.5 * sin(_Time.z)) + v.vertex.x
Y = (0.05 * sin(v.vertex.y + 2.5 * cos(_Time.z)) + v.vertex.y
```

The internal trig functions ```sin(_Time.z)``` and ```cos(_Time.z)``` are used to create a screen shake effect. The choice of using different trig functions on each axis was to ensure the screen shake didn't simply slide along a line (which would occur if the same trig functions were used on both axes). The outermost sin function was used to create a stretching / warping effect across the entire image, producing a mild sense of disorientation. Finally, to ensure the final image largely remains centred and intelligible, the distortion effect is scaled down significantly before being added to the original vertex information, causing the effect to only have a moderate effect on the final image.  

The key code used to create colouring effects is as follows:

```Shaderlab
fixed4 textureCol = tex2D(_MainTex, i.uv);

//gain a hue as it swells
fixed4 greenCol = green * abs(sin(_Time.z));
fixed4 finalCol = 0.6 * textureCol + 0.2 * greenCol + 0.2 * green;

```
A greenish colouration is applied to the screen by retrieving the texture colour of the final image and then mixing it with a static green colour and a green colour whose strength varies with sin of the current time, resulting in a green hued image with an intensity that varies over time.


### Fire particle system

The fire of the cauldrons and burning stations were created using Unity’s Particle System API. Each fire consisted of three particle systems of different sizes and colours. Using multiple particle systems helped to create the different “layers” of the fire (red, orange, and yellow).

The texture used in the particle system was created by Evgeny Starostin:
https://80.lv/articles/breakdown-magic-fire-effect-in-unity/


## Evaluation Methods

### Querying Method

#### Methodology
We used the interview querying method. Participants were sent an early version of the game, and instructed to play through a tutorial level and a game level on their own, without observation or input from the developer team. Players were invited to repeat either level as many times as they liked before taking part in an interview conducted over Zoom. 

The interview consisted of open-ended questions. Participants were encouraged to form their own opinions of the game and discuss their feedback with the interviewer in a conversational style. The interviewer took typed notes during the interviews, and reviewed audio recordings of the interviews when necessary. 

#### Participant demographic information
| Age | Gender    | Occupation                     | Self-estimate of hours of video games played per week |
|-----|--------   |--------------------------------|-------------------------------------------------------|
| 20   | Male     | 3rd year undergraduate student | xx                                                    |
| 20   | Female   | 3rd year undergraduate student | xx                                                    |
| 20   | Female   | 2nd year undergraduate student | xx                                                    |
| X   | Male   | 1st year undergraduate student | 40                                                       |
| X   | Male   | Unemployed                     | 30                                                     |

### Strengths and weaknesses of methodology
dot dot dot

### Observational Method
## Methodology
We used the “Think Aloud” observational method. Participants were invited to individually live-stream their playthrough of the tutorial and first stage to the examiners through a discord channel. Examiners remained muted during the playthrough and did not communicate with the participants. Each playthroughs was observed in real time by the examiners and was recorded for future reference and evaluation. 

## Participant demographic information
| Age | Gender | Occupation                     | Self-estimate of hours of video games played per week |
|-----|--------|--------------------------------|-------------------------------------------------------|
| X   | Male   | Doctor of Optometry student    | 16                                                    |
| X   | Male   | 3rd year undergraduate student | 10                                                    |
| X   | Male   | 2nd year undergraduate student | ?                                                     |
| X   | Male   | 1st year undergraduate student | 40                                                    |
| X   | Male   | Unemployed                     | 30                                                    |


## Strengths and weaknesses of methodology
The breadth of this demographic research was quite limited – all participants were either university students or planning on undertaking tertiary studies. Furthermore, all participants were male and sat within a similar age range. However, as our game is quite simple and lacks a story, we expect these factors to have little bearing on how the game is played or perceived.

Additionally, all participants had some degree of familiarity with video games. This familiarity could cause our evaluation of our game to be biased; for example, users may have played similar games before and could use their prior knowledge to supplement sections of the game where goals or mechanics were not communicated clearly. On the other hand, this familiarly meant that we could easily identify where our game failed to meet the expectations of a typical “gamer”.
Another area of weakness of our querying method was that the lack of a dialogue between the participant and examiners meant that feedback was mainly focused on the initial stumbling blocks users faced –in a more open method (TODO GET NAME OR SOMETHING), users could be moved forward, allowing for feedback to be provided across the entire game. However, as our users were given less assistance, their feedback couldn’t extend beyond their points of blockage. As our tutorial was quite lacklustre when we performed our observations, we received limited feedback on later stages of the game. 


## Feedback gathered
- Weak tutorial – did not require users to show competency before they could continue
- Bad recipe effects were confusing to users (did not know what was causing them)
- Easy to accidentally skip messages in tutorial
- Levels were far too difficult, with most users scoring between 10 – 30 out of a possible score of 130. 
- Mistakenly thought togglable stations were meant to be held
- Many users attempted to “break the game” by repeatedly activating item generators
- Throwing was under-utilised, with many users seeming to not be aware of the feature
- TODO etc. 

### Gameplay
* Overall, participants stated that they found the game enjoyable and relatively bug-free, with instructions clearly stating the objective of the game, and controls working as expected
* While this was mentioned in the tutorial, the majority of participants were not aware that ingredients had to be added to the cauldron in a particular order to be considered “correct”. Additionally, they were unaware that certain special effects (such as ingredients exploding) were the result of adding “incorrect” ingredients to the cauldron
	* The tutorial was reworked so that tips were less dense, and the instructions were more clear
	* An additional stage was added to the tutorial, in which players are instructed to make an “incorrect” potion to observe the special effects
* Some participants felt confused by the different cauldron colours, as one of the potions was very similar in colour to the colour used to represent an “incorrect” potion
	* Potion colours were changed so that a black potion represented an “incorrect” potion
	* Grey colours were used to represent different stages of the potion in the cauldron, and bright colours were used to represent the final potions
* In general, participants felt there was a lack of feedback when playing the main level, and were unsure if they had submitted a “correct” potion. Participants suggested using sound effects or visual cues to indicate to indicate if players made a mistake or submitted a “correct” potion
	* Additional sound effects were implemented, such as when an ingredient is finished processing at a station, when an incorrect ingredient is added to a cauldron, and when a potion is delivered into the portal.
* Players were required to hold down a key in order to carry an item, however many participants stated that this felt awkward
	* The mechanism for carrying an item was changed so that pressing the key would pick up or put down an item
* Some players found it difficult to align the character with certain stations or ingredients
	* To reduce the chance of this happening, we spaced out stations and ingredients
	* We also realigned some objects that were not properly aligned with the benches
* Some participants found that the game ran at a very low frame rate (20 to 30 fps)
	* We found that certain models had a very high number of vertices. These were switched to alternate models with less vertices
* The evaluation brought attention to several minor bugs, such as players glitching through certain benches and walls, player movement being disabled after restarting the game, and the ability to generate excessive amounts of ingredients by picking items from the crate in succession
	* These bugs were fixed for the final build of the game

### Graphics
* Overall, participants felt positively about the graphics employed in the game. The graphics were often described as “cute”, and the objects and entities were relatively easy to distinguish
* Some non-interactable decorative objects were placed in the scene, however some participants felt that they might distract inexperienced players
	* Non-interactable objects were placed more thoughtfully in the scene, and used sparingly 

### Other comments
* Participants reported mixed feelings about the tutorial level. 
	* While the tutorial clearly explained the basic concepts of the game, participants felt that progression in the tutorial should be based on task completion, both to provide a sense of achievement for learning controls, and confirm that users were completing the task properly
		* The tutorial was reworked to progress based on task completion
	* Participants felt hindered by the need to always press the spacebar to continue to the next tip, and the inability to navigate back to a previous tip
		* We were unable to remove these challenges with the time given, but would like to have implemented these changes
		* Players can restart the tutorial at any time by using the pause menu
* Many participants also mentioned that they felt the game would be more enjoyable with more music and sound effects, both in game, and for menus and buttons
	* Additonal sound effects were implemented for different stations and actions
	* Music was also added to the main menu and level select scenes, and sound effects were added to UI buttons
* The majority of participants felt that the main level was difficult to play, and stated that they needed more time to complete each order
	* The timing of orders were adjusted, so that more time was allowed to complete each order
* One participant felt the game would be more enjoyable if orders were created dynamically, and in response to the performance of the player (e.g. a new order could be generated if the player had completed all current orders, as opposed based on a fixed time)
	* Due to time constraints, these were not implemented, but we feel they could be implemented in a future build
* Some participants also suggested extra features that might suit the game such as
	* Introducing different characters with different abilities, e.g. being able to hold multiple items at a time, or dash to move quicker
	* Have potion bottles break when dropped to increase the difficulty of the game	
		* Due to time constraints, these were not implemented, but we feel they could be implemented in a future build


## Resource References

Texture for fire particle system:
https://80.lv/articles/breakdown-magic-fire-effect-in-unity/ 


Models and textures: https://assetstore.unity.com/packages/3d/environments/fantasy/mega-fantasy-props-pack-87811


Models: 
https://poly.google.com/user/4aEd8rQgKu2
https://poly.google.com/user/6WNlPxHAo7o
https://poly.google.com/user/a4-Oxy9dNsF


Icons for scroll and ingredients:
https://clipartmax.com/ 
https://clipart-library.com/ 
https://www.clipartkey.com/


Icons for potions:
Created for this project by a friend, Ben Czapla

Logic for storing highscores locally was retrieved from:
https://answers.unity.com/questions/644911/how-do-i-store-highscore-locally-c-simple.html
    
Other useful resources:
https://www.sitepoint.com/adding-pause-main-menu-and-game-over-screens-in-unity/
https://www.youtube.com/watch?v=CJ8FKjYtrT4 (buttons)


## Individual Contributions

### Arriken Worsley
dot dot dot

### Joel Kenna
dot dot dot

### Simon Tran
dot dot dot

### Iris Li
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




