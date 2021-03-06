# Hostname Connect Valheim Plugin

## Features
- Connect to dedicated servers using a hostname or IP address
- The last hostname or IP address used is pre-populated in the connection panel
- The last successful password used is pre-populated in the connection panel

## Notes
Hostname, port, and password are stored in a configuration file (<GameDirectory>\BepInEx\config\ohmg.mods.hostnameconnect.cfg). The last used server password is 
stored in plaintext in this file. If this concerns you, you can disable password storage by setting the save_password configuration property to false. 

## Pre-Requisites
This mod requires BepInEx. Follow the instructions here if you don't have it installed already: https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/

## Installation (Easy)
- Download archive from https://www.nexusmods.com/valheim/mods/160?tab=files or https://valheim.thunderstore.io/package/mgway/HostnameConnect/
- Extract the archive into a folder. Do not extract into the game folder.
- Move the contents of "plugins" folder into "<GameDirectory>\Bepinex\plugins"
- Start the game and use hostnames

## Build and Install
- Copy requisite DLLs from the game install to the Libs directory. See README.md in that directory for details
- Open solution in Visual Studio
- Build
- Copy bin/Release/ValheimHostnameConnect.dll to <GameDirectory>\Bepinex\plugins
	- This folder should already exist. If it doesn't, ensure that you installed BepInEx correctly
