/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 525FEFB7
/// @DnDArgument : "code" "//Makes sure it's the name same seed every game$(13_10)randomize();$(13_10)$(13_10)// Create global variables related to the player$(13_10)//These are how much items are in the stash$(13_10)global.armorStashCount= 1;$(13_10)global.swordStashCount = 1;$(13_10)global.helmStashCount = 1;$(13_10)global.greatswordStashCount = 1;$(13_10)$(13_10)//If the player has this item currently equipped$(13_10)global.hasHelmet = false;$(13_10)global.hasSword = false;$(13_10)global.isArmored = false;$(13_10)global.hasGreatSword = false;$(13_10)global.ArmorLight = false;$(13_10)global.ArmorHeavy = false;$(13_10)global.ArmorDemonic = false;$(13_10)$(13_10)//If the player is carrying this item outside of what they have equipped$(13_10)global.helmetsCarried = 0;$(13_10)global.swordCarried = 0;$(13_10)global.armorCarried = 0;$(13_10)global.greatswordsCarried = 0;$(13_10)$(13_10)//Gives the player I-frames so they can escape from enemies after losing their armor.$(13_10)global.invincible = false;$(13_10)$(13_10)//Tracks armors Health$(13_10)global.ArmorHeavyHealth = 20;$(13_10)global.ArmorDemonicHealth = 10;$(13_10)$(13_10)castleCompletion = 0;$(13_10)infestedCompletion = 0;$(13_10)$(13_10)noHelmet = false;$(13_10)noArmor = false;$(13_10)$(13_10)draw_set_font(fontDefault);$(13_10)switch (irandom(2))$(13_10){$(13_10)	case 1:$(13_10)	audio_play_sound(snd_Hub_2,1,true);$(13_10)	break;$(13_10)	$(13_10)	case 2:$(13_10)	audio_play_sound(snd_Hub_3,1,true);$(13_10)	break;$(13_10)	$(13_10)	default:$(13_10)	audio_play_sound(snd_Hub_1,1,true);$(13_10)	break;$(13_10)}$(13_10)$(13_10)//sets games grid for pathing$(13_10)var cell_width = 32;$(13_10)var cell_heigth = 32;$(13_10)var wcells = room_width / 2;$(13_10)var hcells = room_height / 2;$(13_10)global.grid = mp_grid_create(0, 0, wcells, hcells, cell_width, cell_heigth);$(13_10)mp_grid_add_instances(global.grid, obj_dummy, false);$(13_10)mp_grid_add_instances(global.grid, obj_Wall, true);$(13_10)$(13_10)// player inventory vatiables$(13_10)inventoryOpen = false;$(13_10)//global.inventoryList = array_create(56);$(13_10)global.inventoryList = ds_list_create();$(13_10)global.arrayCount = 0;"
//Makes sure it's the name same seed every game
randomize();

// Create global variables related to the player
//These are how much items are in the stash
global.armorStashCount= 1;
global.swordStashCount = 1;
global.helmStashCount = 1;
global.greatswordStashCount = 1;

//If the player has this item currently equipped
global.hasHelmet = false;
global.hasSword = false;
global.isArmored = false;
global.hasGreatSword = false;
global.ArmorLight = false;
global.ArmorHeavy = false;
global.ArmorDemonic = false;

//If the player is carrying this item outside of what they have equipped
global.helmetsCarried = 0;
global.swordCarried = 0;
global.armorCarried = 0;
global.greatswordsCarried = 0;

//Gives the player I-frames so they can escape from enemies after losing their armor.
global.invincible = false;

//Tracks armors Health
global.ArmorHeavyHealth = 20;
global.ArmorDemonicHealth = 10;

castleCompletion = 0;
infestedCompletion = 0;

noHelmet = false;
noArmor = false;

draw_set_font(fontDefault);
switch (irandom(2))
{
	case 1:
	audio_play_sound(snd_Hub_2,1,true);
	break;
	
	case 2:
	audio_play_sound(snd_Hub_3,1,true);
	break;
	
	default:
	audio_play_sound(snd_Hub_1,1,true);
	break;
}

//sets games grid for pathing
var cell_width = 32;
var cell_heigth = 32;
var wcells = room_width / 2;
var hcells = room_height / 2;
global.grid = mp_grid_create(0, 0, wcells, hcells, cell_width, cell_heigth);
mp_grid_add_instances(global.grid, obj_dummy, false);
mp_grid_add_instances(global.grid, obj_Wall, true);

// player inventory vatiables
inventoryOpen = false;
//global.inventoryList = array_create(56);
global.inventoryList = ds_list_create();
global.arrayCount = 0;