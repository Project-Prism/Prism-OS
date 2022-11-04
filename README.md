
<h4>Created on May 11th, 2021, 1:26 AM UTC-8</h4>

# About
#### Prism OS is a [Cosmos](https://github.com/CosmosOS/Cosmos) Based Operating System Developed With C#.
#### This Project is Made Possible by Cosmos and the Community Around it, We Give a Huge Thanks to All of the Developers that Contribute to and Support it, Our Work Would Not be Possible Without it.
###### Have any problems? Found a bug? Just want to chat? [Join the discord](https://discord.gg/DdERgtGmF6)!
###### Visit our website [here](https://prism-project.net/)!

<hr/>

# Donations
#### Donations aren't needed at the moment but they would show me support and give me motivation to continue the project. You can donate to our monero wallet:
###### 44WnT2itBkUWzNEHE2xcAEJcSR8jpBmuaPujVNFZvh1PHjPkH9pi2QnJXEd1vU8J4vUZAKpVJTtuAA1CzA7XqFGoMzmuomy

<hr/>

# Screenshots
![image](https://user-images.githubusercontent.com/76945439/197369522-348d341a-20ba-4209-80d1-10a5cb489f7a.png)

<hr/>

<ul>
<h4>Prism OS's Feature List:</h4>
    <li>A Basic 3D Rasteriser</li>
    <li>Parsing for Many File Formats (TAR, Bitmap, and Targa)</li>
    <li>A UI library</li>
    <li>LZW Compression and Decompression</li>
    <li>Good Graphics API</li>
    <li>System Event Sounds</li>
    <li>Much More.
    <h6>Note: Some Of These Items Might Not Be Implemented All The Way</h6>
</ul>

<hr/>

- ## Build Status
- [x] PrismAudio
- [x] PrismBinary
- [x] PrismGL2D
- [x] PrismGL3D
- [x] PrismNetwork
- [X] Kernel
- [X] PrismTools
- [X] PrismUI

<hr/>

- ## Documentation TO-DO
- [ ] PrismAudio
- [ ] PrismBinary
- [x] PrismGL2D
- [ ] PrismGL3D
- [x] PrismNetwork
- [x] PrismTools
- [ ] PrismUI


<hr/>

## Notable contributors
| Name                                                  | Contribution(s)              |
|-------------------------------------------------------|------------------------------|
| [Cosmos team](https://github.com/CosmosOS/Cosmos)     | The entirety of Cosmos       |
| [Terminal.cs](https://github.com/terminal-cs)         | Founder, Main developer      |
| [theopcoder](https://github.com/theopcoder)           | Website developer            |

<hr/>

## Syscalls

These are the avalable syscalls Prism OS offers, the EAX value will always become the return value after calling.

| Name    | EAX  | EBX         | ECX      | EDX  | ESI | EDI |
|---------|------|-------------|----------|------|-----|-----|
| Realloc | 0x0  | Old Pointer | New Size |  -   |  -  |  -  |
| Alloc   | 0x1  | Size        |     -    |  -   |  -  |  -  |
| Free    | 0x2  | Pointer     |     -    |  -   |  -  |  -  |
| Copy64  | 0x3  | Destination |  Source  | Size |  -  |  -  |
| Copy32  | 0x4  | Destination |  Source  | Size |  -  |  -  |
| Copy16  | 0x5  | Destination |  Source  | Size |  -  |  -  |
| Copy8   | 0x6  | Destination |  Source  | Size |  -  |  -  |
| Fill64  | 0x7  | Destination |  Value   | Size |  -  |  -  |
| Fill32  | 0x8  | Destination |  Value   | Size |  -  |  -  |
| Fill16  | 0x9  | Destination |  Value   | Size |  -  |  -  |
| Fill8   | 0x10 | Destination |  Value   | Size |  -  |  -  |
| Prompt  | 0x11 | Title       |  Message |  -   |  -  |  -  |
| Print   | 0x12 | Text        |     -    |  -   |  -  |  -  |

<hr/>

## Contributing

#### Want to Add Something New to Prism OS? Simply Create a Pull Request and We'll Review it. (Your Code Should Preferably Follow [These](https://github.com/Project-Prism/Prism-OS/blob/main/CONTRIBUTING.md) Guidelines)

<hr/>

## Licensing

> This project is licensed Under the [GPL-2.0](https://github.com/Project-Prism/Prism-OS/blob/main/LICENSE) licence.

> This project's logo, 'Daimond', was created by Nicole Hammonds from The Noun Project.
