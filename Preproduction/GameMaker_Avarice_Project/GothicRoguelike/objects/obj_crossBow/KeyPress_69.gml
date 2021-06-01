/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 2A6B462E
/// @DnDArgument : "code" "// If 'E' is pressed while the player is near the crossbow,$(13_10)// the crossbow gets equiped.$(13_10)if(layer != 2){$(13_10)	if (distance_to_object(obj_dummy) <= 50)$(13_10)	{$(13_10)		if(!global.hasCrossbow){$(13_10)			global.hasCrossbow = true;$(13_10)		}$(13_10)		audio_play_sound(snd_Pickup_Sound, 2, 0);$(13_10)		instance_destroy(self);$(13_10)	}$(13_10)}"
// If 'E' is pressed while the player is near the crossbow,
// the crossbow gets equiped.
if(layer != 2){
	if (distance_to_object(obj_dummy) <= 50)
	{
		if(!global.hasCrossbow){
			global.hasCrossbow = true;
		}
		audio_play_sound(snd_Pickup_Sound, 2, 0);
		instance_destroy(self);
	}
}