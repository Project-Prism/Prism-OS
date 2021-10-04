# Prism OS
###### <small>Last updated 9/8/2021  |  Project started sometime in May 2021</small>
Have any problems? Found a bug? Just want to chat? [Join the discord](https://discord.gg/DdERgtGmF6)!

Prism OS is a [Cosmos](https://github.com/CosmosOS/Cosmos) based operating system developed with C#. The operating system is wrapped around an easy to navigate graphical user interface and includes core functionalities such as a filesystem and networking. Prism OS also comes with a [custom scripting language](https://github.com/Project-Prism/Hexi) that allows for easier interaction with the system within the OS itself*.

This project is made possible by Cosmos and the community around it, and we give a huge thanks to all of the developers that contribute to it.

##### *The scripting language is still under heavy development.

## Why?
Here are some reasons provided by our developers:

[terminal-cs](https://github.com/terminal-cs): Our motivation for this project is the fact most current operating systems are slow, insecure, bloated, have spyware, And the fact that some are still based off of their old kernels written 20+ years ago.

[deadlocust](https://github.com/deaddlocust): Idk man I was just bored.

## Screenshots
![](https://github.com/Project-Prism/Prism-OS/blob/main/Screenshots/Prism%20OS%20(21.9.28).png?raw=true)
![](https://github.com/Project-Prism/Prism-OS/blob/main/Screenshots/Prism%20OS%20(21.9.8).png?raw=true)

## Notable contributors
| Name                                             | Contributions                |
|--------------------------------------------------|------------------------------|
| [Terminal.cs](https://github.com/terminal-cs)    | General development, logo    |
| [deadlocust](https://github.com/deaddlocust)     | General development          |
| [Nifanfa](https://github.com/nifanfa)            | Bitfont                      |
| [CrisisSDK](https://github.com/CrisisSDK)        | Graphics, window manager     |
| [GamingFrame](https://github.com/ThomasBeHappy/) | Cosmos canvas fix (soon)     |
| [theopcoder](https://github.com/theopcoder)      | Website                      |

## To-do list

| Topic                | Difficulty | Notes                        |
|----------------------|-----------|------------------------------|
|Implement YASM into Cosmos|Easy|Starting soon|
|Implement Hexi|Hard|Requires threading|
|Drivers|Hard|-|
|Canvas speedup|-| Someone aparently knows how|


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
