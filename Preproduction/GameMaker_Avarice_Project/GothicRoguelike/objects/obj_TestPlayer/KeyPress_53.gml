/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 07F8F8BC
/// @DnDArgument : "code" "//Alternate attack way$(13_10)if (canAttack == true)$(13_10){$(13_10)$(13_10)	if (moveSpeedx >= 0 && moveSpeedy == 0)$(13_10)	{$(13_10)		direction = 0;$(13_10)		xdistance = 20;$(13_10)		ydistance = 0;$(13_10)	}$(13_10)	else if (moveSpeedx > 0 && moveSpeedy > 0)$(13_10)	{$(13_10)		direction = 315;$(13_10)		xdistance = 16;$(13_10)		ydistance = 16;$(13_10)	}$(13_10)	else if (moveSpeedx == 0 && moveSpeedy > 0)$(13_10)	{$(13_10)		direction = 270$(13_10)		xdistance = 0;$(13_10)		ydistance = 20;$(13_10)	}$(13_10)	else if (moveSpeedx < 0 && moveSpeedy > 0)$(13_10)	{$(13_10)		direction = 225;$(13_10)		xdistance = -16;$(13_10)		ydistance = 16;$(13_10)	}$(13_10)	else if (moveSpeedx < 0 && moveSpeedy == 0)$(13_10)	{$(13_10)		direction = 180;$(13_10)		xdistance = -20;$(13_10)		ydistance = 0;$(13_10)	}$(13_10)	else if (moveSpeedx < 0 && moveSpeedy < 0)$(13_10)	{$(13_10)		direction = 135;$(13_10)		xdistance = -16;$(13_10)		ydistance = -16;$(13_10)	}$(13_10)	else if (moveSpeedx == 0 && moveSpeedy < 0)$(13_10)	{$(13_10)		direction = 90;$(13_10)		xdistance = 0;$(13_10)		ydistance = -20;$(13_10)	}$(13_10)	else if (moveSpeedx > 0 && moveSpeedy < 0)$(13_10)	{$(13_10)		direction = 45;$(13_10)		xdistance = 16;$(13_10)		ydistance = -16;$(13_10)	}$(13_10)	$(13_10)	sword = instance_create_layer(x+xdistance,y+ydistance,layer,obj_WindTest);$(13_10)	sword.image_angle = direction$(13_10)	if(global.hasGreatSword){$(13_10)		sword.image_yscale *= 1.5;$(13_10)	}$(13_10)	$(13_10)	if(global.hasSword){$(13_10)		sword.image_xscale *= 1.5;$(13_10)	}$(13_10)	alarm_set(0,45);$(13_10)	attacking = true;$(13_10)	canAttack = false;$(13_10)	sword.image_angle += 45$(13_10)	audio_play_sound(snd_Sword_Swipe,-100,0)$(13_10)}"
//Alternate attack way
if (canAttack == true)
{

	if (moveSpeedx >= 0 && moveSpeedy == 0)
	{
		direction = 0;
		xdistance = 20;
		ydistance = 0;
	}
	else if (moveSpeedx > 0 && moveSpeedy > 0)
	{
		direction = 315;
		xdistance = 16;
		ydistance = 16;
	}
	else if (moveSpeedx == 0 && moveSpeedy > 0)
	{
		direction = 270
		xdistance = 0;
		ydistance = 20;
	}
	else if (moveSpeedx < 0 && moveSpeedy > 0)
	{
		direction = 225;
		xdistance = -16;
		ydistance = 16;
	}
	else if (moveSpeedx < 0 && moveSpeedy == 0)
	{
		direction = 180;
		xdistance = -20;
		ydistance = 0;
	}
	else if (moveSpeedx < 0 && moveSpeedy < 0)
	{
		direction = 135;
		xdistance = -16;
		ydistance = -16;
	}
	else if (moveSpeedx == 0 && moveSpeedy < 0)
	{
		direction = 90;
		xdistance = 0;
		ydistance = -20;
	}
	else if (moveSpeedx > 0 && moveSpeedy < 0)
	{
		direction = 45;
		xdistance = 16;
		ydistance = -16;
	}
	
	sword = instance_create_layer(x+xdistance,y+ydistance,layer,obj_WindTest);
	sword.image_angle = direction
	if(global.hasGreatSword){
		sword.image_yscale *= 1.5;
	}
	
	if(global.hasSword){
		sword.image_xscale *= 1.5;
	}
	alarm_set(0,45);
	attacking = true;
	canAttack = false;
	sword.image_angle += 45
	audio_play_sound(snd_Sword_Swipe,-100,0)
}