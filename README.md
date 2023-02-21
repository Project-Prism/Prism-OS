
<h4>Created on May 11th, 2021, 1:26 AM UTC-8</h4>

# About
Prism OS is a [Cosmos](https://github.com/CosmosOS/Cosmos) Based Operating System Developed With C#.

This Project is Made Possible by Cosmos and the Community Around it, We Give a Huge Thanks to All of the Developers that Contribute to and Support it, Our Work Would Not be Possible Without it.

>Have any problems? Found a bug? Just want to chat? [Join the discord](https://discord.gg/DdERgtGmF6)!

<hr/>

# Media
[!Video1](https://cdn.discordapp.com/attachments/893391662942933013/1077402167100592198/2023-02-20_17-25-07.mp4)
> Demo of lerp animations in  Prism OS.
[!Video2](https://cdn.discordapp.com/attachments/910378077836689428/1077127455371968614/2023-02-19_19-30-18.mkv)
> Demo of the 3D rasterizer in Prism OS.

<hr/>

<ul>
<h4>Prism OS's Feature List:</h4>
    <li>A Basic 3D Rasteriser</li>
    <li>Parsing for Many File Formats (TAR, Bitmap, and Targa)</li>
    <li>LZW Compression and Decompression</li>
    <li>Good Graphics API</li>
    <li>Animation & color fade support</li>
    <li>Custom language lexer, compiler, and runtime</li>
    <li>And more...</li>
    <h6>Note: Some Of These Items Might Not Be Implemented All The Way</h6>
</ul>

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

<hr/>

## Contributing

Want to Add Something New to Prism OS? Simply Create a Pull Request and We'll Review it. (Your Code Should Preferably Follow [These](https://github.com/Project-Prism/Prism-OS/blob/main/CONTRIBUTING.md) Guidelines)

<hr/>

## See also:
[SSharp documentation](https://github.com/Project-Prism/Prism-OS/tree/main/PrismRuntime/SSharp/README.md)

<hr/>

## Licensing

> This project is licensed Under the [GPL-2.0](https://github.com/Project-Prism/Prism-OS/blob/main/LICENSE) licence.

> This project's logo, 'Daimond', was created by Nicole Hammonds from The Noun Project.
