/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 4044A450
/// @DnDArgument : "code" "//Tells the player how to interact with NPC$(13_10)if(distance_to_object(obj_dummy) <= maxDistance){$(13_10)	if(!isOpen)$(13_10)	{$(13_10)		draw_text(x-40,y-20,"Press E to Interact with NPC");$(13_10)	}$(13_10)}"
//Tells the player how to interact with NPC
if(distance_to_object(obj_dummy) <= maxDistance){
	if(!isOpen)
	{
		draw_text(x-40,y-20,"Press E to Interact with NPC");
	}
}