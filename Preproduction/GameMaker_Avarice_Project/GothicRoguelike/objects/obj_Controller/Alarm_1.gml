/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 6B77E55C
/// @DnDArgument : "code" "//Caused by player's death$(13_10)$(13_10)//Goes to the base$(13_10)room_goto(BaseDemo);$(13_10)$(13_10)audio_stop_all();$(13_10)$(13_10)switch (irandom(2))$(13_10){$(13_10)	case 1:$(13_10)	audio_play_sound(snd_Hub_2,1,true);$(13_10)	break;$(13_10)	$(13_10)	case 2:$(13_10)	audio_play_sound(snd_Hub_3,1,true);$(13_10)	break;$(13_10)	$(13_10)	default:$(13_10)	audio_play_sound(snd_Hub_1,1,true);$(13_10)	break;$(13_10)}$(13_10)$(13_10)//Removes items if they're carrying it$(13_10)global.hasHelmet = false;$(13_10)global.isArmored = false;$(13_10)global.hasSword = false;$(13_10)global.hasGreatSword = false;$(13_10)$(13_10)//Removes items if they have them in the inventory$(13_10)global.armorCarried = 0;$(13_10)global.helmetsCarried = 0;$(13_10)global.swordCarried = 0;$(13_10)global.greatswordsCarried = 0;$(13_10)$(13_10)$(13_10)"
//Caused by player's death

//Goes to the base
room_goto(BaseDemo);

audio_stop_all();

switch (irandom(2))
{
	case 1:
	audio_play_sound(snd_Hub_2,1,true);
	break;
	
	case 2:
	audio_play_sound(snd_Hub_3,1,true);
	break;
	
	default:
	audio_play_sound(snd_Hub_1,1,true);
	break;
}

//Removes items if they're carrying it
global.hasHelmet = false;
global.isArmored = false;
global.hasSword = false;
global.hasGreatSword = false;

//Removes items if they have them in the inventory
global.armorCarried = 0;
global.helmetsCarried = 0;
global.swordCarried = 0;
global.greatswordsCarried = 0;