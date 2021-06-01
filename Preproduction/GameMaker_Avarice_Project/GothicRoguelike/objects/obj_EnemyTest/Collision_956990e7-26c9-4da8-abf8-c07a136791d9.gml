/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 53D76266
/// @DnDArgument : "code" "//damages enemy when it gets hit by explosion $(13_10)if(canBeHit)$(13_10){$(13_10)	monsterHealth -= 5;$(13_10)	audio_play_sound(snd_Skeleton_Damage,-100 , 0)$(13_10)	if(monsterHealth <= 0)$(13_10)	{$(13_10)		instance_destroy();	$(13_10)	}$(13_10)	canBeHit = false;$(13_10)	alarm_set(1,20);$(13_10)}"
//damages enemy when it gets hit by explosion 
if(canBeHit)
{
	monsterHealth -= 5;
	audio_play_sound(snd_Skeleton_Damage,-100 , 0)
	if(monsterHealth <= 0)
	{
		instance_destroy();	
	}
	canBeHit = false;
	alarm_set(1,20);
}