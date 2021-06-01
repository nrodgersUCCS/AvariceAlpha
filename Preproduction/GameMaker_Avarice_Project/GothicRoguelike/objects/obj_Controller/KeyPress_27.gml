/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 1B2C3B3B
/// @DnDArgument : "code" "//If the StashOverlay is open, causes it to close.$(13_10)if(layer_get_visible(layer_get_id("StashOverlay"))){$(13_10)	obj_Stash.isOpen = false;$(13_10)}$(13_10)//If not, closes the game.$(13_10)else{$(13_10)	game_end();$(13_10)}"
//If the StashOverlay is open, causes it to close.
if(layer_get_visible(layer_get_id("StashOverlay"))){
	obj_Stash.isOpen = false;
}
//If not, closes the game.
else{
	game_end();
}