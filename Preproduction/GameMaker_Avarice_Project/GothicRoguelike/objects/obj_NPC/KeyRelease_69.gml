/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 455CA889
/// @DnDArgument : "code" "//Makes the isOpen variable change if the player presses e near it$(13_10)if(distance_to_object(obj_dummy) <= maxDistance){$(13_10)	if(isOpen){$(13_10)		isOpen = false;$(13_10)	}$(13_10)	else{$(13_10)		isOpen = true;		$(13_10)	}$(13_10)}"
//Makes the isOpen variable change if the player presses e near it
if(distance_to_object(obj_dummy) <= maxDistance){
	if(isOpen){
		isOpen = false;
	}
	else{
		isOpen = true;		
	}
}