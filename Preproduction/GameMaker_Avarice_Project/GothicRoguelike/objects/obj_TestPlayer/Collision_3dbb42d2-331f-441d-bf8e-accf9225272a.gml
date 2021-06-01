/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 1C5732D0
/// @DnDArgument : "code" "//Checks if the player is invincible and if not, causes damage and begins I-frames.$(13_10)if(!global.invincible){$(13_10)	scrDamage();$(13_10)	self.solid = false;$(13_10)}"
//Checks if the player is invincible and if not, causes damage and begins I-frames.
if(!global.invincible){
	scrDamage();
	self.solid = false;
}

/// @DnDAction : YoYo Games.Instances.Destroy_Instance
/// @DnDVersion : 1
/// @DnDHash : 57EE505A
/// @DnDApplyTo : other
with(other) instance_destroy();