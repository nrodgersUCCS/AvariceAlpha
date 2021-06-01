/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 4044A450
/// @DnDArgument : "code" "if(distance_to_object(obj_dummy) <= maxDistance){$(13_10)	if(!isOpen)$(13_10)	{$(13_10)		draw_text(x-40,y-20,"Press E to Interact");$(13_10)	}$(13_10)	else{$(13_10)		//Makes UI for StashOverlay$(13_10)		draw_text(x+40,y+8,"Supplies in your stash");$(13_10)		draw_text(x+140,y+44, global.armorStashCount);$(13_10)		draw_text(x+140,y+84, global.helmStashCount);$(13_10)		draw_text(x+140,y+124, global.swordStashCount);$(13_10)		draw_text(x+140,y+164, global.greatswordStashCount);$(13_10)	}$(13_10)}"
if(distance_to_object(obj_dummy) <= maxDistance){
	if(!isOpen)
	{
		draw_text(x-40,y-20,"Press E to Interact");
	}
	else{
		//Makes UI for StashOverlay
		draw_text(x+40,y+8,"Supplies in your stash");
		draw_text(x+140,y+44, global.armorStashCount);
		draw_text(x+140,y+84, global.helmStashCount);
		draw_text(x+140,y+124, global.swordStashCount);
		draw_text(x+140,y+164, global.greatswordStashCount);
	}
}