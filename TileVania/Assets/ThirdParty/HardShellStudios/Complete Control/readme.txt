Input Manager - Complete Control
Hard Shell Studios - developed by Haydn Comley

Super Simple Setup.
1) Right Click in the Project Assets, goto "Create" then "Input Asset" then "Default Config".
2) Make sure this is inside a folder called "Resources" and called "KeyBindings"
3) Click on the created asset and use the Inspector UI to manage your inputs.
4) Click the "Import" button if needed (will be required when begining)

Video Quick Start Guide
https://youtu.be/4qR3iMaBYDk 

Inputs Available

hInput.GetKey()
hInput.GetKeyDown()
hInput.GetKeyUp()
hInput.GetAxis()
etc... etc...

They work as the standard Unity ones do.

You can set a key by using hInput.SetKey()
Refer to the UI_ButtonRebind.cs script for an example of how to make this work with a GUI
