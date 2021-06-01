/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 704CE410
/// @DnDArgument : "code" "//Objects visible on quest being active$(13_10)if(global.questActive == true)$(13_10){$(13_10)	mask_index = spr_tempCrossbow;$(13_10)}$(13_10)else$(13_10){$(13_10)	mask_index = spr_Empty;$(13_10)}"
//Objects visible on quest being active
if(global.questActive == true)
{
	mask_index = spr_tempCrossbow;
}
else
{
	mask_index = spr_Empty;
}