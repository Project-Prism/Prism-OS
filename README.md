# Prism OS
###### <small>Last updated 10/6/2021  |  Project started sometime in May 2021</small>
Have any problems? Found a bug? Just want to chat? [Join the discord](https://discord.gg/DdERgtGmF6)!

Prism OS is a [Cosmos](https://github.com/CosmosOS/Cosmos) based operating system developed with C#. The operating system is wrapped around an easy to navigate graphical user interface and includes core functionalities such as a filesystem and networking.

This project is made possible by Cosmos and the community around it, and we give a huge thanks to all of the developers that contribute to it.

## Screenshots
![](https://github.com/Project-Prism/Prism-OS/blob/main/Screenshots/Prism%20OS%20(21.9.28).png?raw=true)
![](https://github.com/Project-Prism/Prism-OS/blob/main/Screenshots/Prism%20OS%20(21.9.8).png?raw=true)

## Notable contributors
| Name                                              | Contributions                |
|---------------------------------------------------|------------------------------|
| [Cosmos team](https://github.com/CosmosOS/Cosmos) | Cosmos, helping out          |
| [Terminal.cs](https://github.com/terminal-cs)     | General development, logo    |
| [deadlocust](https://github.com/deaddlocust)      | General development          |
| [Nifanfa](https://github.com/nifanfa)             | Bitfont                      |
| [CrisisSDK](https://github.com/CrisisSDK)         | Rounded cube, coal v1 & v2    |
| [TheOpCoder](https://github.com/theopcoder)       | Website developer            |

## To-do list

| Topic                      |  Difficulty  | Notes                         |
|----------------------------|--------------|-------------------------------|
| Implement YASM into Cosmos |     Easy     | Starting soon                 |
| Drivers                    |  Very Hard   | -                             |
| Fix vbe circle & image     | Easy/Medium  | (in cosmos) should be ez      |
| Re-Implement mouse         |     Easy     | -                             |
| Implement resource files   | -            | better for managment (cosmos) |
| Implement Hexi             |     -        | Hexi unfinished               |


## Building Prism
Prism OS requires that you are using the latest devkit version of cosmos in order to successfully compile.

###### <small>It is not recommended to distribute modified copies of Prism OS. Instead, you should do a PR to get whatever features you want implemented. It will most likely be accepted.</small>

### Step 1: 
Open visual studio and click "clone repo"

### Step 2:
Copy the URL for this github page and copy it into the "clone repo" text box and click "clone"

### Step 3:
Wait for the clone process to finish and then open the solution file (PrismProject.sln) from the solution explorer

### Step 4:
At the top of the screen, you should see an option called "build", click that and the prism will compile!
