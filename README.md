#Pongutile

Pongutile is Pong implemented using Unity + [Futile](https://github.com/MattRix/Futile). I've made it as an exercise in learning Futile and Unity. As this is my first project using these technologies expect a fair amount of antipatterns, bad ways of doing things and all sorts of kludges.

The game has no victory condition. You can play for hours, the score gets bigger and nothing else happens. From within the game screen you can go back to the title screen pressing Escape.


The code is based on Matt Rix's BananaDemo.


##How to try the demo project##

####How to open the project

- Grab the project from github and put it somewhere - [For the lazy, here's a zip of the whole repo](https://github.com/luisparravicini/Pongutile/zipball/master)
- Make sure you have Unity 4 installed
- Go into PongutileDemoProject/Assets/Scenes and open PongutileDemoScene.unity

####How to make sure you're running it at the right resolution
- Go to File -> Build Settings -> Click "PC and Mac Standalone" -> Click "Switch Platform" (if it's already greyed out, you're good)
- On the Build Settings page, choose Player Settings
- Under Resolution and Presentation, set the size to 960x640
- Go to the "Game" tab 
- In the top left dropdown, choose your resolution (instead of "Free aspect")
- In the top right, make sure "Maximize on Play" is enabled.

Notes: 
- If you choose a specific resolution, but the game window isn't large enough to contain that resolution, it'll open in some random scaled resolution, and everything will be wonky, which is annoying. 

####The font used is [Imagine](http://www.dafont.com/en/imagine-font.font)

##MIT License##

Source code for Pongutile is Copyright © 2013 Luis Parravicini.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the “Software”), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS,” WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

