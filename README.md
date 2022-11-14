
<h4>Created on May 11th, 2021, 1:26 AM UTC-8</h4>

# About
Prism OS is a [Cosmos](https://github.com/CosmosOS/Cosmos) Based Operating System Developed With C#.

This Project is Made Possible by Cosmos and the Community Around it, We Give a Huge Thanks to All of the Developers that Contribute to and Support it, Our Work Would Not be Possible Without it.

>Have any problems? Found a bug? Just want to chat? [Join the discord](https://discord.gg/DdERgtGmF6)!

<hr/>

# Donations
Donations aren't needed at the moment but they would show me support and give me motivation to continue the project. You can donate to our monero wallet:
> 44WnT2itBkUWzNEHE2xcAEJcSR8jpBmuaPujVNFZvh1PHjPkH9pi2QnJXEd1vU8J4vUZAKpVJTtuAA1CzA7XqFGoMzmuomy

<hr/>

# Quick note
You may see projects nested in one another in references tab, and it looks confusing. However this is only for that project's sake and will not need to be included in external projects unless using calls or variables that include those library types. For instance, you might want to use PrismUI in your own project, but it references PrismGL2D, which references PrismBinary. As long as you don't using anything withing PrismGL2D that depends on PrismBinary, you will only need to import PrismGL2D.dll.

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

| Name       | EAX  | EBX         | ECX              | EDX    | ESI | EDI | Returns                            |
|------------|------|-------------|------------------|--------|-----|-----|------------------------------------|
| Realloc    | 0x0  | Old Pointer | New Size         | -      | -   | -   | New Pointer                        |
| Alloc      | 0x1  | Size        | -                | -      | -   | -   | Pointer                            |
| Free       | 0x2  | Pointer     | -                | -      | -   | -   | -                                  |
| Copy64     | 0x3  | Destination | Source           | Size   | -   | -   | -                                  |
| Copy32     | 0x4  | Destination | Source           | Size   | -   | -   | -                                  |
| Copy16     | 0x5  | Destination | Source           | Size   | -   | -   | -                                  |
| Copy8      | 0x6  | Destination | Source           | Size   | -   | -   | -                                  |
| Fill64     | 0x7  | Destination | Value            | Size   | -   | -   | -                                  |
| Fill32     | 0x8  | Destination | Value            | Size   | -   | -   | -                                  |
| Fill16     | 0x9  | Destination | Value            | Size   | -   | -   | -                                  |
| Fill8      | 0x10 | Destination | Value            | Size   | -   | -   | -                                  |
| Print      | 0x11 | Text        | -                | -      | -   | -   | -                                  |
| FSRead     | 0x12 | FullPath    | Write Buffer ptr | -      | -   | -   | Length                             |
| FSWrite    | 0x13 | FullPath    | Read Buffer ptr  | Length | -   | -   | -                                  |
| FSDelete   | 0x14 | FullPath    | 0=Folder, 1=File | -      | -   | -   | -                                  |
| FSMake     | 0x15 | FullPath    | 0=Folder, 1=File | -      | -   | -   | 0=Exists,1=NoPath, 1=Success       |
| FSExists   | 0x16 | FullPath    | -                | -      | -   | -   | 0=False, 1=Folder, 2=foldFileer    |
| FSize      | 0x17 | FullPath    | -                | -      | -   | -   | Size of file in bytes              |
| Prompt     | 0x18 | Title       | Message          | -      | -   | -   | -                                  |

<hr/>

## Contributing

Want to Add Something New to Prism OS? Simply Create a Pull Request and We'll Review it. (Your Code Should Preferably Follow [These](https://github.com/Project-Prism/Prism-OS/blob/main/CONTRIBUTING.md) Guidelines)

<hr/>

## See also:
[GLang documentation](https://github.com/Project-Prism/Prism-OS/tree/main/PrismRuntime/GLang/README.md)

<hr/>

## Licensing

> This project is licensed Under the [GPL-2.0](https://github.com/Project-Prism/Prism-OS/blob/main/LICENSE) licence.

> This project's logo, 'Daimond', was created by Nicole Hammonds from The Noun Project.
