A small thematic mod to make it more fun to sail around with friends. 

## Features

- Each additional passenger increases the speed that ships move when in paddle mode (forward or back 1 arrow speed)
- Speed increase is configurable, and defaults to a geometric series sum with a = 1/2, r = 3/4, and n = passenger count


## Configuration

- Configuration file is located at "<GameDirectory>\Bepinex\config\ohmg.mods.paddlepower.cfg"
- The enabled property enables or disables this plugin
- Check out https://en.wikipedia.org/wiki/Geometric_series for a primer on geometric series
- The coefficient and ratio properties correspond to "a" and "r" in the article
- The ratio configuration property is constrained to between 0 and 1.


## Known issues

- All users on a server should have the mod installed to minimize issues
- Logging out or disconnecting while on the deck of a ship may cause the passenger count to be incorrectly increased until all players leave the area  

## Pre-Requisites

This mod requires BepInEx. Follow the instructions here if you don't have it installed already: https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/


## Installation

- Extract the archive into a folder. Do not extract into the game folder.
- Move the contents of "plugins" folder into "<GameDirectory>\Bepinex\plugins"
- Start the game