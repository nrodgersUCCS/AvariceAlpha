/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 136ED755
/// @DnDArgument : "code" "//Transports the player to the castle level and plays proper music$(13_10)audio_stop_all()$(13_10)room_goto(CastleDemo)$(13_10)audio_play_sound(snd_Ingame_1,100,true)"
//Transports the player to the castle level and plays proper music
audio_stop_all()
room_goto(CastleDemo)
audio_play_sound(snd_Ingame_1,100,true)