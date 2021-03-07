A small thematic mod to make it more fun to sail around with friends. 

## Features

- Each additional passenger increases the speed that ships move when in paddle mode (forward or back 1 arrow speed)
- Speed increase is configurable, and defaults to a geometric series sum with a = 1/2, r = 7/8, and n = passenger count


## Known issues

- All users on a server should have the mod installed to minimize issues
- Logging out or disconnecting while on the deck of a ship may cause the passenger count to be incorrectly increased until all players leave the area  


## Pre-Requisites

This mod requires BepInEx. Follow the instructions here if you don't have it installed already: https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/


## Installation

- Extract the archive into a folder. Do not extract into the game folder.
- Move the contents of "plugins" folder into "<GameDirectory>\Bepinex\plugins"
- Start the game


## Configuration

Configuration file is located at "<GameDirectory>\Bepinex\config\ohmg.mods.paddlepower.cfg"

"enabled": enables or disables this plugin in its entirety
"debug_logging": enables or disables printing debug log messages. Kind of spammy and not necessary unless you're trying to figure out a particular issue

"counting_method": What players to count as contributing to ship speed
- ALL: All players on the deck of the ship
- ATTACHED: (default) Only count players on the ship that are seated or holding onto the mast or bow
- SEATED: Only count players that are seated (includes pilot)

"scaling_method" selects which formula to use for calculating player speed contributions
- GEOMETRIC: (default) Each additional player contributes a diminishing amount of extra speed
- LINEAR: Each additional player contributes the same amount of extra speed

GEOMETRIC specific configuration properties:
- Check out https://en.wikipedia.org/wiki/Geometric_series for a primer on geometric series
- The "coefficient" and "ratio" properties correspond to "a" and "r" in the article
- The "ratio" configuration property is constrained to between 0 and 1

LINEAR specific configuration properties:
- "base_amount": Paddle force contributed by the ship pilot
- "additional_amount": Paddle force contributed by each additional player
- "maximum_bonus": Maximum paddling force, regardless of player count
