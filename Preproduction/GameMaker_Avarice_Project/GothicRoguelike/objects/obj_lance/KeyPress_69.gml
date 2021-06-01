/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 2A6B462E
/// @DnDArgument : "code" "// If 'E' is pressed while the player is near lance,$(13_10)// the lance gets used.$(13_10)if(layer != 2){$(13_10)	if (distance_to_object(obj_dummy) <= 50)$(13_10)	{$(13_10)		if(!global.hasLance) and (ds_list_size(global.inventoryList) < 56)$(13_10)		{$(13_10)			global.hasLance = true;$(13_10)			ds_list_add(global.inventoryList,spr_lance)$(13_10)			global.arrayCount += 1;$(13_10)		}$(13_10)		audio_play_sound(snd_Pickup_Sound, 2, 0);$(13_10)		instance_destroy(self);$(13_10)	}$(13_10)}"
// If 'E' is pressed while the player is near lance,
// the lance gets used.
if(layer != 2){
	if (distance_to_object(obj_dummy) <= 50)
	{
		if(!global.hasLance) and (ds_list_size(global.inventoryList) < 56)
		{
			global.hasLance = true;
			ds_list_add(global.inventoryList,spr_lance)
			global.arrayCount += 1;
		}
		audio_play_sound(snd_Pickup_Sound, 2, 0);
		instance_destroy(self);
	}
}