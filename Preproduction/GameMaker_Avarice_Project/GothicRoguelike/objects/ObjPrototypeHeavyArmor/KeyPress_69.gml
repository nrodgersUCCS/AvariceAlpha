/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 3EF39D48
/// @DnDArgument : "code" "if(layer != 2){$(13_10)	if (distance_to_object(obj_dummy) <= 50)$(13_10)	{$(13_10)		if(!global.isArmored){$(13_10)			global.isArmored = true;$(13_10)			global.ArmorHeavy = true;$(13_10)		}$(13_10)		else if(ds_list_size(global.inventoryList) < 56)$(13_10)		{$(13_10)			global.armorCarried +=1;$(13_10)			$(13_10)			ds_list_add(global.inventoryList,sprArmorProtoHeavy)$(13_10)			global.arrayCount += 1;$(13_10)		}$(13_10)		audio_play_sound(snd_Armor_Equip, 2, 0);$(13_10)		instance_destroy(self);$(13_10)	}$(13_10)}"
if(layer != 2){
	if (distance_to_object(obj_dummy) <= 50)
	{
		if(!global.isArmored){
			global.isArmored = true;
			global.ArmorHeavy = true;
		}
		else if(ds_list_size(global.inventoryList) < 56)
		{
			global.armorCarried +=1;
			
			ds_list_add(global.inventoryList,sprArmorProtoHeavy)
			global.arrayCount += 1;
		}
		audio_play_sound(snd_Armor_Equip, 2, 0);
		instance_destroy(self);
	}
}