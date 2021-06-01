/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 730B2813
/// @DnDArgument : "code" "if (canBeHit == true)$(13_10){$(13_10)	audio_play_sound(snd_boss_Damage,-100, false)$(13_10)	health -= 2;$(13_10)	canBeHit = false;$(13_10)	alarm_set(0,20);$(13_10)}$(13_10)$(13_10)if (health <= 0)$(13_10){$(13_10)	instance_destroy(self);$(13_10)	layer_set_visible(wall, false)$(13_10)	layer_destroy(gate);$(13_10)}"
if (canBeHit == true)
{
	audio_play_sound(snd_boss_Damage,-100, false)
	health -= 2;
	canBeHit = false;
	alarm_set(0,20);
}

if (health <= 0)
{
	instance_destroy(self);
	layer_set_visible(wall, false)
	layer_destroy(gate);
}