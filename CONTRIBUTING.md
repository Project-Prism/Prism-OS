### In order to contribute code to the main repository, it must follow these guidelines:

<hr/>

All functions, Local and Global Vriables, and NameSpaces Must be Capitalized With The 'PasCal' Naming Convention, And Spelled Properly. They Must Contain Profesional and Easy to Read Names, Usualy They Should Describe what the Function Does if Possible.


Variables and Usings Should be Clustered Like This, With Descending Length:
![image](https://user-images.githubusercontent.com/76945439/167557913-1df4379a-0abc-4628-9e61-2a720ec292bf.png)

<hr/>

If a class has a ton of functions, try to caterogize them in between ``#region NAME`` and ``#endregion`` tags.

<hr/>

#### Do Not Create Stupid Hacky Methods to Get Around Something Unless You Know You Will Change it Before you Commit.
###### (Trust me, You will regret making something very unreliable then having to re-make it for several hours in a more stable manner)

<hr/>

Always use IF statments "properly" and clearly. and example is if you have many nested IFs, invert the condition and move it to the top, and return it if the condition is met.