/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 2A6B462E
/// @DnDArgument : "code" "// If 'E' is pressed while the player is near the armor,$(13_10)// the armor gets equiped. If they already have armor equipped,$(13_10)// put into inventory.$(13_10)if(layer != 2){$(13_10)	if (distance_to_object(obj_dummy) <= 50)$(13_10)	{$(13_10)		if(!global.isArmored){$(13_10)			global.isArmored = true;$(13_10)		}$(13_10)		else if(ds_list_size(global.inventoryList) < 56)$(13_10)		{$(13_10)			global.armorCarried +=1;$(13_10)			$(13_10)			ds_list_add(global.inventoryList,amorSprite);$(13_10)			global.arrayCount ++;$(13_10)		}$(13_10)		audio_play_sound(snd_Armor_Equip, 2, 0);$(13_10)		instance_destroy(self);$(13_10)	}$(13_10)}"
// If 'E' is pressed while the player is near the armor,
// the armor gets equiped. If they already have armor equipped,
// put into inventory.
if(layer != 2){
	if (distance_to_object(obj_dummy) <= 50)
	{
		if(!global.isArmored){
			global.isArmored = true;
		}
		else if(ds_list_size(global.inventoryList) < 56)
		{
			global.armorCarried +=1;
			
			ds_list_add(global.inventoryList,amorSprite);
			global.arrayCount ++;
		}
		audio_play_sound(snd_Armor_Equip, 2, 0);
		instance_destroy(self);
	}
}