/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 30FBF58B
/// @DnDArgument : "code" "// Call parent event$(13_10)event_inherited()$(13_10)$(13_10)path_start(bosspatrol, 2, path_action_continue, true);$(13_10)$(13_10)alarm[2] = room_speed * 3.5;$(13_10)$(13_10)wall = layer_get_id("Tiles_1");$(13_10)gate = layer_get_id("Instances_1");"
// Call parent event
event_inherited()

path_start(bosspatrol, 2, path_action_continue, true);

alarm[2] = room_speed * 3.5;

wall = layer_get_id("Tiles_1");
gate = layer_get_id("Instances_1");