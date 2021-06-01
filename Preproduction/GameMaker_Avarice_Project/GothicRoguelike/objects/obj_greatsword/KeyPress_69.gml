/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 617E39AE
/// @DnDArgument : "code" "// If 'E' is pressed while the player is near the sword,$(13_10)// the sword gets equiped. If they already have sword equipped,$(13_10)// put into inventory.$(13_10)if(layer != 2){$(13_10)	if (distance_to_object(obj_dummy) <= 100)$(13_10)	{$(13_10)		if(!global.hasGreatSword){$(13_10)			global.hasGreatSword = true;$(13_10)		}$(13_10)		else if(ds_list_size(global.inventoryList) < 56)$(13_10)		{$(13_10)			global.greatswordsCarried +=1;$(13_10)			$(13_10)			ds_list_add(global.inventoryList,attackingSword);$(13_10)			global.arrayCount += 1;$(13_10)		}$(13_10)		audio_play_sound(snd_Pickup_Sound, 2, 0);$(13_10)		instance_destroy(self);$(13_10)	}$(13_10)}"
// If 'E' is pressed while the player is near the sword,
// the sword gets equiped. If they already have sword equipped,
// put into inventory.
if(layer != 2){
	if (distance_to_object(obj_dummy) <= 100)
	{
		if(!global.hasGreatSword){
			global.hasGreatSword = true;
		}
		else if(ds_list_size(global.inventoryList) < 56)
		{
			global.greatswordsCarried +=1;
			
			ds_list_add(global.inventoryList,attackingSword);
			global.arrayCount += 1;
		}
		audio_play_sound(snd_Pickup_Sound, 2, 0);
		instance_destroy(self);
	}
}