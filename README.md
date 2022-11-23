
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
    <li>Custom language lexer, compiler, and runtime.</li>
    <li>And more...</li>
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

All references to strings will be pointers to null-terminated strings in whatever format was used, typicaly it will be UTF-8.

| Name                | EAX  | EBX          | ECX      | EDX  | ESI | EDI | Returns                         |
|---------------------|------|--------------|----------|------|-----|-----|---------------------------------|
| Console.WriteLine   | 0x00 | Message      | -        | -    | -   | -   | -                               |
| Console.Write       | 0x01 | Message      | -        | -    | -   | -   | -                               |
| Console.Clear       | 0x02 | -            | -        | -    | -   | -   | -                               |
| Console.SetColor    | 0x03 | FG Color     | BG Color | -    | -   | -   | -                               |
| Console.ResetColor  | 0x04 | -            | -        | -    | -   | -   | -                               |
| Memory.Realloc      | 0x05 | Old Address  | New Size | -    | -   | -   | New Address                     |
| Memory.Alloc        | 0x06 | Size         | -        | -    | -   | -   | Address                         |
| Memory.Free         | 0x07 | Address      | -        | -    | -   | -   | -                               |
| Memory.Set          | 0x08 | Address      | Value    | -    | -   | -   | -                               |
| Memory.Get          | 0x09 | Address      | -        | -    | -   | -   | Value                           |
| Memory.Copy64       | 0x10 | Destination  | Source   | Size | -   | -   | -                               |
| Memory.Copy32       | 0x11 | Destination  | Source   | Size | -   | -   | -                               |
| Memory.Copy16       | 0x12 | Destination  | Source   | Size | -   | -   | -                               |
| Memory.Copy8        | 0x13 | Destination  | Source   | Size | -   | -   | -                               |
| Memory.Fill64       | 0x14 | Destination  | Value    | Size | -   | -   | -                               |
| Memory.Fill32       | 0x15 | Destination  | Value    | Size | -   | -   | -                               |
| Memory.Fill16       | 0x16 | Destination  | Value    | Size | -   | -   | -                               |
| Memory.Fill8        | 0x17 | Destination  | Value    | Size | -   | -   | -                               |
| File.Read           | 0x18 | Full Path    | -        | -    | -   | -   | Binary                          |
| File.Write          | 0x19 | Full Path    | Binary   | -    | -   | -   | -                               |
| File.Remove         | 0x20 | Full Path    | Name     | -    | -   | -   | -                               |
| File.Create         | 0x21 | Full Path    | Name     | Type | -   | -   | -                               |
| File.Exists         | 0x22 | Full Path    | -        | -    | -   | -   | 0 = False, 1 = File, 2 = Folder |
| File.GetSize        | 0x23 | Full Path    | -        | -    | -   | -   | Size of the file                |
| GUI.ShowDialogBox   | 0x24 | Title        | Message  | -    | -   | -   | -                               |
| GUI.ShowWindow      | 0x25 | Title        | -        | -    | -   | -   | Index of new window             |
| GUI.AddControl      | 0x26 | Window Index | Type     | Args | -   | -   | -                               |

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
