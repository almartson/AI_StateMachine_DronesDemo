# AI_StateMachine_DonesDemo
## What is this Project?

It is a simple implementation of an :robot: A.I. behaviour :suspect:, by using the 'State Pattern' and State Machines in Unity3D :video_game: and C#. It is fully based on two great Blog Articles written by the excellent Tutor:
:necktie: Jason Weimann, of unity3d.college.


### 1- Unity3D AI with State Machine (FSM), Drones, and Lasers!:gun:
Link: https://unity3d.college/2019/04/28/unity3d-ai-with-state-machine-drones-and-lasers/


### 2- Unity3D Design Patterns – State – :robot:  Building a Unity3D State Machine
Link:
https://unity3d.college/2017/05/26/unity3d-design-patterns-state-basic-state-machine/


### 3- And the YouTube video (Tutorial): :movie_camera: Unity3D AI with State Machine (FSM), Drones, and Lasers!
Link:
https://www.youtube.com/watch?v=YdERlPfwUb0


## Notes:

:bulb: There are 2 important Scenes in the code:

#### :low_brightness: 1- DroneBadPractice:
An A.I. example using bad coding practices: An Super-Big Switch - Case with too many cases. Each 'case' would correspond to a State of the A.I. (for instance: Walk, Shoot, Dance, etc.); and we would use an 'Enum' variable for the States Name.

#### :low_brightness: 2- Drone:
The recommended A.I. implementation, using a well designed and simple 'State Pattern' in C#. 
The Scripts and code for this Scene can be found inside the  'Assets/scripts/GoodStateMachine/'  folder.


:bulb: I was motivated to rewrite this code because in the Tutorial Jason Weimann did not include the code of the State Machine Pattern, only the 'bad way' of writing A.I. (which is based on a BIIIIIG Switch-Case, with as many States as Enum Values). I think it might be available only to his Patreon Sponsors, which is understandable.


:bulb: This code is not :100: Optimized. It is intended to be a Template for Prototyping and making Game 'Proof-of-Concept' Demos. For making it a final product; it would be necessary to make some changes, for example: replacing the Raycasting implementation in the WanderState.cs class (one of the A.I. States) by one that does less Raycasts (and those Raycasts need to be limited in their extension and size), and we would need to remove the constant execution of GetComponent<>() from the collider. One possible solution would be to cache all game Characters that are to collide (in a Static Dictionary) inside a Component (maybe a Static Class), and compare in each Frame (update() execution) the Collider 'ID' with the one we would have cached (i.e.: a simple Search in the Dictionary, by 'ID'). Using well limited Raycasts is ok, but the command Debug.Raycast is just for 'debugging', so it would need to be commented out too.


:bulb: About the Lighting:
I decided to test an Assets called 'VertexDynamicLightmap' (Vertex Lit Shader: Baked Shadows Realtime Light, made by: Nurface): to allow having Realtime Point Lights :flashlight: (one per Drone) with Baked Lighting (i.e.: a Directional source of light, which would be: :sunny: the Sun), basically Vertex Lit (Rendering Path). It works like a charm and looks fine. 
You can download it in: https://assetstore.unity.com/packages/vfx/shaders/vertex-lit-shader-baked-shadows-realtime-light-75977


:video_game: It was made in Unity 2018.4.0f1.


I hope it to be useful for anybody studying A.I.

AlMartson


********************************

MIT License

Copyright (c) 2020 AlMartson

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
© 2020 GitHub, Inc.

