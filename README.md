# EasyDoor
This software technically acts as a backdoor, but all I use it for is to annoy my brother. :) It uses C#'s socket tools in order to listen and connect to a TCP connection, and when connected you can send a variety of commands.

## Features:

 **All built in C# with no NuGet packages**

 1. **Shutdown** - *Shutdown the computer remotely*
 2. **Minecraft** - *Closes all instances of Java and Lunar Client.*
 3. **Monkey** - *Opens 100 windows of Chromium with assorted monkey pics. That's a lot of memory.
 4. **Custom Downloads** - *If these features don't suit your needs, code your own custom Windows executable and execute it by providing a URL. *

## Coming soon:

 - [ ] MacOS TCP connections w/ Java (Haven't learned socket yet)
 - [ ] Better Windows UI (C# is a yucky language for UI)
 - [ ] Port fowarding, allowing connections to not just be LAN. (Have to provide a lot of security practices such as passkeys)
 - [ ] More features

## Instructions for use:

 1. Either download a Release from the Releases tab, or manually build it with Visual Studio via cloning the source code.
 2. For the target computer to be controlled, download the server, (WinAvrFramework) on to to the computer, and just double click it and run!
 
**Make sure you have administrator priveleges on the machine to avoid running into permission problems. If not possible, build from the source code and change the** *requestedExecutionLevel* **in the** *app.manifest* **file.**

**The program will automatically copy it's self into the user directory and add it to the registry.**

 3. On the computer you want to control the target machine, download the release of the client, (Server Control) or build it yourself.
 4. Make sure you're on the same WiFi connection as the target computer, record the IP address of the target computer, and when ready just input that IP into the designated area, and click connect.
 
 **Done!** Have fun using this tool, and don't get into too much trouble. ;)
 ---
>  Feel free to submit a bug report, or contact me at my Discord:

**lucaa#4892**
