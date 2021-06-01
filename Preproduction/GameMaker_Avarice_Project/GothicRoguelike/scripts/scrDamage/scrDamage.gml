/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 7B75C6D3
/// @DnDArgument : "code" "/// Damage()$(13_10)/// @description Damage()$(13_10)if(!global.invincible){ $(13_10)	// Creates a miss chance based on the helmet level$(13_10)	missChance = global.hasHelmet*.40;$(13_10)$(13_10)	// If the attack misses, the helmet degreades by 1 level$(13_10)	// If the player is wearing armor, the armor is reduced by 1 level$(13_10)	// Otherwise, the player dies$(13_10)	if (missChance > random(1))$(13_10)	{$(13_10)		global.invincible = true;$(13_10)		obj_dummy.alarm[1] = 45;$(13_10)		global.hasHelmet = false;$(13_10)		audio_play_sound(snd_Helmet_Destroy,2,0);$(13_10)	}$(13_10)	else$(13_10)	{$(13_10)		if (global.isArmored)$(13_10)		{$(13_10)			// Damage taken with Light Armor$(13_10)			if(global.ArmorLight == true){$(13_10)				global.invincible = true;$(13_10)				obj_dummy.alarm[1] = 45;$(13_10)				global.isArmored = false;$(13_10)				audio_play_sound(snd_Armor_Destroy,2,0);$(13_10)				$(13_10)			}$(13_10)			$(13_10)			// Damage taken with Heavy Armor$(13_10)			if(global.ArmorHeavy == true && global.ArmorHeavyHealth != 0){$(13_10)				global.invincible = true;$(13_10)				obj_dummy.alarm[1] = 45;$(13_10)				audio_play_sound(snd_Armor_Destroy,2,0);$(13_10)				global.ArmorHeavyHealth -=1;$(13_10)				$(13_10)			}$(13_10)			$(13_10)			// Destroys Heavy Armor if it is on last health point$(13_10)			if(global.ArmorHeavy == true && global.ArmorHeavyHealth == 0){$(13_10)				global.invincible = true;$(13_10)				obj_dummy.alarm[1] = 45;$(13_10)				global.ArmorHeavy = false;$(13_10)				global.isArmored = false;$(13_10)				audio_play_sound(snd_Armor_Destroy,2,0);$(13_10)				global.ArmorHeavyHealth = 20;$(13_10)				$(13_10)			}$(13_10)			$(13_10)			// Damage Taken with Demonic Armor$(13_10)			if(global.ArmorDemonic == true && global.ArmorDemonicHealth != 0){$(13_10)				global.invincible = true;$(13_10)				obj_dummy.alarm[1] = 45;$(13_10)				audio_play_sound(snd_Armor_Destroy,2,0);$(13_10)				global.ArmorDemoicHealth -=1;$(13_10)			}$(13_10)			$(13_10)			// Destroys Demonic Armor if it is on last health point$(13_10)			if(global.ArmorDemonic == true && global.ArmorDemonicHealth == 0){$(13_10)				global.invincible = true;$(13_10)				obj_dummy.alarm[1] = 45;$(13_10)				global.ArmorDemonic = false;$(13_10)				global.isArmored = false;$(13_10)				audio_play_sound(snd_Armor_Destroy,2,0);$(13_10)				global.ArmorDemoicHealth = 10;$(13_10)			}$(13_10)			$(13_10)		}$(13_10)		else$(13_10)		{$(13_10)			instance_destroy(obj_dummy);$(13_10)			ds_list_clear(global.inventoryList);$(13_10)			switch (irandom(2))$(13_10)			{$(13_10)				case 1:$(13_10)				audio_play_sound(snd_Death_1,2,0);$(13_10)				break;$(13_10)				case 2:$(13_10)				audio_play_sound(snd_Death_2,2,0);$(13_10)				break;$(13_10)				default:$(13_10)				audio_play_sound(snd_Death_3,2,0);$(13_10)				break;$(13_10)			}$(13_10)			obj_Controller.alarm[1] = room_speed*4;$(13_10)		}$(13_10)	}$(13_10)}"
/// Damage()
/// @description Damage()
if(!global.invincible){ 
	// Creates a miss chance based on the helmet level
	missChance = global.hasHelmet*.40;

	// If the attack misses, the helmet degreades by 1 level
	// If the player is wearing armor, the armor is reduced by 1 level
	// Otherwise, the player dies
	if (missChance > random(1))
	{
		global.invincible = true;
		obj_dummy.alarm[1] = 45;
		global.hasHelmet = false;
		audio_play_sound(snd_Helmet_Destroy,2,0);
	}
	else
	{
		if (global.isArmored)
		{
			// Damage taken with Light Armor
			if(global.ArmorLight == true){
				global.invincible = true;
				obj_dummy.alarm[1] = 45;
				global.isArmored = false;
				audio_play_sound(snd_Armor_Destroy,2,0);
				
			}
			
			// Damage taken with Heavy Armor
			if(global.ArmorHeavy == true && global.ArmorHeavyHealth != 0){
				global.invincible = true;
				obj_dummy.alarm[1] = 45;
				audio_play_sound(snd_Armor_Destroy,2,0);
				global.ArmorHeavyHealth -=1;
				
			}
			
			// Destroys Heavy Armor if it is on last health point
			if(global.ArmorHeavy == true && global.ArmorHeavyHealth == 0){
				global.invincible = true;
				obj_dummy.alarm[1] = 45;
				global.ArmorHeavy = false;
				global.isArmored = false;
				audio_play_sound(snd_Armor_Destroy,2,0);
				global.ArmorHeavyHealth = 20;
				
			}
			
			// Damage Taken with Demonic Armor
			if(global.ArmorDemonic == true && global.ArmorDemonicHealth != 0){
				global.invincible = true;
				obj_dummy.alarm[1] = 45;
				audio_play_sound(snd_Armor_Destroy,2,0);
				global.ArmorDemoicHealth -=1;
			}
			
			// Destroys Demonic Armor if it is on last health point
			if(global.ArmorDemonic == true && global.ArmorDemonicHealth == 0){
				global.invincible = true;
				obj_dummy.alarm[1] = 45;
				global.ArmorDemonic = false;
				global.isArmored = false;
				audio_play_sound(snd_Armor_Destroy,2,0);
				global.ArmorDemoicHealth = 10;
			}
			
		}
		else
		{
			instance_destroy(obj_dummy);
			ds_list_clear(global.inventoryList);
			switch (irandom(2))
			{
				case 1:
				audio_play_sound(snd_Death_1,2,0);
				break;
				case 2:
				audio_play_sound(snd_Death_2,2,0);
				break;
				default:
				audio_play_sound(snd_Death_3,2,0);
				break;
			}
			obj_Controller.alarm[1] = room_speed*4;
		}
	}
}