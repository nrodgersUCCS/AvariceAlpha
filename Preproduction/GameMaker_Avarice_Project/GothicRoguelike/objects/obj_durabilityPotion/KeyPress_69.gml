/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 2A6B462E
/// @DnDArgument : "code" "// If 'E' is pressed while the player is near potion,$(13_10)// the potion gets used.$(13_10)if(layer != 2){$(13_10)	if (distance_to_object(obj_dummy) <= 50)$(13_10)	{$(13_10)		if(!global.hasDurabilityPotionEffect)$(13_10)		{$(13_10)			global.hasDurabilityPotionEffect = true;$(13_10)			instance_create_depth(obj_dummy.x,obj_dummy.y,-1,obj_durabilityPotionAura);$(13_10)			// sets duration of potion$(13_10)			obj_Controller.alarm[4] = 600;$(13_10)		}$(13_10)		audio_play_sound(snd_Pickup_Sound, 2, 0);$(13_10)		instance_destroy(self);$(13_10)	}$(13_10)}"
// If 'E' is pressed while the player is near potion,
// the potion gets used.
if(layer != 2){
	if (distance_to_object(obj_dummy) <= 50)
	{
		if(!global.hasDurabilityPotionEffect)
		{
			global.hasDurabilityPotionEffect = true;
			instance_create_depth(obj_dummy.x,obj_dummy.y,-1,obj_durabilityPotionAura);
			// sets duration of potion
			obj_Controller.alarm[4] = 600;
		}
		audio_play_sound(snd_Pickup_Sound, 2, 0);
		instance_destroy(self);
	}
}