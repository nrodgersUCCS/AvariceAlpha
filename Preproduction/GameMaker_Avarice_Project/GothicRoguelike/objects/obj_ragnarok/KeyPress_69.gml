/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 2A6B462E
/// @DnDArgument : "code" "// If 'E' is pressed while the player is near ragnarok,$(13_10)// the ragnarok gets equiped.$(13_10)if(layer != 2){$(13_10)	if (distance_to_object(obj_dummy) <= 50)$(13_10)	{$(13_10)		if(!global.hasRagnarok) and (ds_list_size(global.inventoryList) < 56)$(13_10)		{$(13_10)			global.hasRagnarok = true;$(13_10)			$(13_10)			ds_list_add(global.inventoryList,spr_Ragnarok)$(13_10)			global.arrayCount += 1;$(13_10)		}$(13_10)		audio_play_sound(snd_Pickup_Sound, 2, 0);$(13_10)		instance_destroy(self);$(13_10)	}$(13_10)}"
// If 'E' is pressed while the player is near ragnarok,
// the ragnarok gets equiped.
if(layer != 2){
	if (distance_to_object(obj_dummy) <= 50)
	{
		if(!global.hasRagnarok) and (ds_list_size(global.inventoryList) < 56)
		{
			global.hasRagnarok = true;
			
			ds_list_add(global.inventoryList,spr_Ragnarok)
			global.arrayCount += 1;
		}
		audio_play_sound(snd_Pickup_Sound, 2, 0);
		instance_destroy(self);
	}
}