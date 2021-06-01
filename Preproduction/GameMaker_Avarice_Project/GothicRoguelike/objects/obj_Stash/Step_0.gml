/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 57A018B6
/// @DnDArgument : "code" "//Closes the stash if the player is too far away$(13_10)if(distance_to_object(obj_dummy) > maxDistance){$(13_10)	isOpen = false;$(13_10)}$(13_10)//Makes the StashOverlay visible if isOpen is true.$(13_10)if(isOpen){$(13_10)	layer_set_visible(layer_id,true);$(13_10)}$(13_10)//Makes the StashOverlay invisible if isOpen is false.$(13_10)else{$(13_10)	layer_set_visible(layer_id,false);$(13_10)}"
//Closes the stash if the player is too far away
if(distance_to_object(obj_dummy) > maxDistance){
	isOpen = false;
}
//Makes the StashOverlay visible if isOpen is true.
if(isOpen){
	layer_set_visible(layer_id,true);
}
//Makes the StashOverlay invisible if isOpen is false.
else{
	layer_set_visible(layer_id,false);
}