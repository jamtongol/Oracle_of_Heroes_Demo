#######################################################
#Description
#######################################################
Oracle of Heroes is an ongoing project of mine that started when I was in the first year of University.
This game is an rpg game engine that draws inspiration from Nintendo's Legend of Zelda.


In this demo, the engine shows how it renders it's tile engine using only a data file.
(Oracle_of_Heroes\Oracle_of_Heroes\bin\x86\Debug\Content\test.data for debugging mode or
Oracle_of_Heroes\Oracle_of_Heroes\bin\x86\Debug\Content for release mode).

#######################################################
# Requirements
#######################################################
This game requies the XNA game engine, (the XNA Extension for Visual Studio 2012: https://mxa.codeplex.com/releases/view/117564),
in order to run.

#######################################################
# Details
#######################################################
One can modify the script such that the sizes, tiles or orientation of the map changes. Changes must include the following variables:

	width		-the number of tile columns
	height		-the number of tile rows
	tileWidth	-the width of a tile
	tileHeight	-the height of a tile
	scale		-the scale of each tile
	types		-the types of files
	map		-the orientation of the map

The types variable are the different types of tiles in the map and must be formmatted accordingly
	[an idfor the tile (for easy lookup), the name of the tile(must be the same name as the file), isCollideable, the number of frames(for animation purposes)
		[[an id for the frame (for easy lookup), the x position within the image file, the y position within the image file, the next frame to load, the amount of time for this frame to display (in seconds)],
		...]
	...];

The map variable is the list of tiles in the map.

There are currently only three textures loaded in this game (flower.png, wave.png, wall.png). In order to load more open the project in vs2012, right click on
Oracle_of_HeroesContent(Content)/Sprites/Map, select Add->Existing Item, and browse for the texture files

#######################################################
# Examples
#######################################################

Here is an example for the type variable

width = 8;
height = 11;
tileWidth = 16;
tileHeight = 16;
scale = 2;
types = [
[0, flower, false, 4,
	[[0, 0, 0, 1, 0.3],
	[1, 17, 0, 2, 0.3],
	[2, 34, 0, 3, 0.3],
	[3, 51, 0, 0, 0.3]],
[1, wave, true, 4,
	[[0, 0, 0, 1, 0.3],
	[1, 17, 0, 2, 0.3],
	[2, 34, 0, 3, 0.3],
	[3, 51, 0, 0, 0.3]],
[2, wall, true, 1,
	[[0, 0, 0, 0, 100]]
];


Here is an example for the map variable

map = [
[1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0],
[1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0],
[1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0],
[1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0],
[1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0],
[1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0],
[1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0],
[1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0]
];
