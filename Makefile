all: build run
  
build:
	@dotnet build --no-incremental
  
run:
	@qemu-system-x86_64 -enable-kvm -m 4G -device e1000,netdev=net0 -netdev user,id=net0 -device ac97 -cdrom ./PrismOS/bin/Debug/net6.0/PrismOS.iso