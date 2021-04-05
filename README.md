# COM-port
Interface for messaging between COM-ports.

# About COM-port
In computing, a serial port is a serial communication interface through which information transfers in or out sequentially one bit at a time. This is in contrast to a parallel port, which communicates multiple bits simultaneously in parallel. Throughout most of the history of personal computers, data has been transferred through serial ports to devices such as modems, terminals, various peripherals, and directly between computers.

Modern consumer PCs have largely replaced serial ports with higher-speed standards, primarily USB. However, serial ports are still frequently used in applications demanding simple, low-speed interfaces, such as industrial automation systems, scientific instruments, point of sale systems and some industrial and consumer products.

Server computers may use a serial port as a control console for diagnostics, while networking hardware (such as routers and switches) commonly use serial console ports for configuration, diagnostics, and emergency maintenance access. To interface with these and other devices, USB-to-serial converters can quickly and easily add a serial port to a modern PC.

# Project description
## Interface


Here you can choose avaliable:
* ports (for example, COM1 or COM2);
* baud rate (9600 bit/s, 19200 bit/s, 38400 bit/s, 57600 bit/s, 115200 bit/s);
* protocol (ASCII or Binary).

**Important!**

For everything to work correctly for COM-ports exchanging messages, the same speed and protocol must be set.
## How to use?
1. If it is possible for your computer, you can use hardware(for example, RS-232) or install virtual serial port driver.
2. Choose all settings (port, baud rate, protocol) and press **Open**.
3. After that, if two COM-ports were opened, you can send and receive messages.
