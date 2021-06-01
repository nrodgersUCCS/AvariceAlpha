/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 39843475
/// @DnDArgument : "code" "//If the quest is completed when interacting with NPC, draws completion$(13_10)if(global.questCompleted == true)$(13_10){$(13_10)	draw_text(x+40,y+8,questTitle);$(13_10)	draw_text(x+15,y+44, questDescription);$(13_10)	draw_text(x+15,y+84, goldReward);$(13_10)	draw_text(x+15,y+124, expReward);$(13_10)}"
//If the quest is completed when interacting with NPC, draws completion
if(global.questCompleted == true)
{
	draw_text(x+40,y+8,questTitle);
	draw_text(x+15,y+44, questDescription);
	draw_text(x+15,y+84, goldReward);
	draw_text(x+15,y+124, expReward);
}