# Prism OS Runtime

## System calls

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