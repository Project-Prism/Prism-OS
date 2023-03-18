<p align="center">
  <img src="https://github.com/Project-Prism/Prism-OS/raw/main/PrismOS/Media/Images/Prism.bmp" width="25%" />
</p>

<p align="center">
  <img src="https://img.shields.io/github/downloads/Project-Prism/Prism-OS/total?style=plastic" />
  <img src="https://img.shields.io/github/v/release/Project-Prism/Prism-Os?style=plastic" />
  <img src="https://img.shields.io/github/license/Project-Prism/Prism-OS?style=plastic" />
  <img src="https://img.shields.io/github/issues/Project-Prism/Prism-OS?style=plastic" />
  <img src="https://img.shields.io/discord/893388221424234496?style=plastic" />
</p>

<hr/>

Prism OS is a [Cosmos](https://github.com/CosmosOS/Cosmos) Based Operating System Developed With C#. It has many features which can be used for many different things, ranging from real-time animation, 3D rendering, network downloads, a filesystem, CPU-accelerated floating-point math, compression, basic parsing for some file formats, and much more!

You can find documentation for the entire project here: (Coming soon!)

This Project is Made Possible by Cosmos and the Community Around it, We Give a Huge Thanks to All of the Developers that Contribute to and Support it, Our Work Would Not be Possible Without it.

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

## Drivers

Prism OS supports inclusion/exclusion of drivers at compile time, You can modify what is added when compiling by editing [this](https://github.com/Project-Prism/Prism-OS/blob/main/Directory.Build.props) file.

| Available Constants |
|---------------------|
| IncludeVMWARE       |
| IncludeVBE          |

<hr/>

## Contributing

Want to Add Something New to Prism OS? Simply Create a Pull Request and We'll Review it. (Your Code Should Preferably Follow [These](https://github.com/Project-Prism/Prism-OS/blob/main/CONTRIBUTING.md) Guidelines)

## Licensing

> This project is licensed Under the [GPL-2.0](https://github.com/Project-Prism/Prism-OS/blob/main/LICENSE) licence.

> This project's logo, 'Daimond', was created by Nicole Hammonds from The Noun Project.
