the hexi compiler works on a line by line basis, each line has an instruction, such as Print=>Hello, World!
on an instruction, it gets split into two, by the '=>', it adds the coresponding byte to a byte list for that function.
It then takes the length of the data after the '=>' and adds that to the byte array, and then adds the actual data to the array.
the length is added so that the function knows how far ahead it will read.

and for running things, it goes byte by byte, if a byte equals an opcode and it is not reading data, it will begin reading that data until the length of the data is reached.
it will then do whatever it needs to do with that data, and then resume by reading the next byte, which will be an opcode.