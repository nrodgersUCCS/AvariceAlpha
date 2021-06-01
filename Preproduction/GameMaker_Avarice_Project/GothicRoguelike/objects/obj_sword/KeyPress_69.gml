/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 2A6B462E
/// @DnDArgument : "code" "// If 'E' is pressed while the player is near the sword,$(13_10)// the sword gets equiped. If they already have sword equipped,$(13_10)// put into inventory.$(13_10)if(layer != 2){$(13_10)	if (distance_to_object(obj_dummy) <= 50)$(13_10)	{$(13_10)		if(!global.hasSword){$(13_10)			global.hasSword = true;$(13_10)		}$(13_10)		else if(ds_list_size(global.inventoryList) < 56)$(13_10)		{$(13_10)			global.swordCarried +=1;$(13_10)			$(13_10)			ds_list_add(global.inventoryList,SwordPickupSprite);$(13_10)			global.arrayCount += 1;$(13_10)		}$(13_10)		audio_play_sound(snd_Pickup_Sound, 2, 0);$(13_10)		instance_destroy(self);$(13_10)	}$(13_10)}"
// If 'E' is pressed while the player is near the sword,
// the sword gets equiped. If they already have sword equipped,
// put into inventory.
if(layer != 2){
	if (distance_to_object(obj_dummy) <= 50)
	{
		if(!global.hasSword){
			global.hasSword = true;
		}
		else if(ds_list_size(global.inventoryList) < 56)
		{
			global.swordCarried +=1;
			
			ds_list_add(global.inventoryList,SwordPickupSprite);
			global.arrayCount += 1;
		}
		audio_play_sound(snd_Pickup_Sound, 2, 0);
		instance_destroy(self);
	}
}