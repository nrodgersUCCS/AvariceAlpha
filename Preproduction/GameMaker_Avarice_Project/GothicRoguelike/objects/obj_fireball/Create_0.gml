/// @DnDAction : YoYo Games.Instances.If_Instance_Exists
/// @DnDVersion : 1
/// @DnDHash : 23D863EE
/// @DnDArgument : "obj" "obj_dummy"
/// @DnDSaveInfo : "obj" "f63c3944-c6a7-424b-9a89-5b4b8f7c7960"
var l23D863EE_0 = false;
l23D863EE_0 = instance_exists(obj_dummy);
if(l23D863EE_0)
{
	/// @DnDAction : YoYo Games.Movement.Set_Direction_Point
	/// @DnDVersion : 1
	/// @DnDHash : 30A650CA
	/// @DnDParent : 23D863EE
	/// @DnDArgument : "x" "obj_dummy.x"
	/// @DnDArgument : "y" "obj_dummy.y"
	direction = point_direction(x, y, obj_dummy.x, obj_dummy.y);

	/// @DnDAction : YoYo Games.Audio.Play_Audio
	/// @DnDVersion : 1
	/// @DnDHash : 14D5D749
	/// @DnDParent : 23D863EE
	/// @DnDArgument : "soundid" "snd_Fireball_Shot"
	/// @DnDSaveInfo : "soundid" "250ae929-c96e-45ee-8f74-484759a8216a"
	audio_play_sound(snd_Fireball_Shot, 0, 0);

	/// @DnDAction : YoYo Games.Movement.Set_Speed
	/// @DnDVersion : 1
	/// @DnDHash : 1233B331
	/// @DnDParent : 23D863EE
	/// @DnDArgument : "speed" "1"
	speed = 1;
}

/// @DnDAction : YoYo Games.Common.Else
/// @DnDVersion : 1
/// @DnDHash : 5B3A8310
else
{
	/// @DnDAction : YoYo Games.Instances.Destroy_Instance
	/// @DnDVersion : 1
	/// @DnDHash : 11BF8891
	/// @DnDParent : 5B3A8310
	instance_destroy();
}