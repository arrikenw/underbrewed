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
The potion liquid shader, produces a swirling liquid, with the liquid slowly falling towards the center. This shader was used for the cauldron liquid, the portal center, as well as the backgrounds for the menus. The shader was produced with help from an online tutorial found [here](http://enemyhideout.com/2016/08/creating-a-whirlpool-shader/). 

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
dot dot dot

## Evaluation Methods

### Querying Method
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


## Feedback Changes
dot dot dot

## Resource References
dot dot dot

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




