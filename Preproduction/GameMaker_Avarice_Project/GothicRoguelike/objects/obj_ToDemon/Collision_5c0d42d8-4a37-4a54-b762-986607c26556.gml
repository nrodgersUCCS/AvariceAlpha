/// @DnDAction : YoYo Games.Rooms.Go_To_Room
/// @DnDVersion : 1
/// @DnDHash : 76212CCB
/// @DnDArgument : "room" "DemonDemo"
/// @DnDSaveInfo : "room" "55058e4d-903e-474f-8aa4-0bcbc238b3f1"
room_goto(DemonDemo);

/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 2AD7A2B7
/// @DnDArgument : "code" "//Transports the player to the castle level and plays proper music$(13_10)audio_stop_all();$(13_10)$(13_10)switch (irandom(2))$(13_10){$(13_10)	case 1:$(13_10)	audio_play_sound(snd_Boss_Fight_2,1,true);$(13_10)	break;$(13_10)	$(13_10)	case 2:$(13_10)	audio_play_sound(snd_Boss_Fight_3,1,true);$(13_10)	break;$(13_10)	$(13_10)	default:$(13_10)	audio_play_sound(snd_Boss_Fight_1,1,true);$(13_10)	break;$(13_10)}"
//Transports the player to the castle level and plays proper music
audio_stop_all();

switch (irandom(2))
{
	case 1:
	audio_play_sound(snd_Boss_Fight_2,1,true);
	break;
	
	case 2:
	audio_play_sound(snd_Boss_Fight_3,1,true);
	break;
	
	default:
	audio_play_sound(snd_Boss_Fight_1,1,true);
	break;
}