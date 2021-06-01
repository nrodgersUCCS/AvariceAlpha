/// @DnDAction : YoYo Games.Common.Execute_Code
/// @DnDVersion : 1
/// @DnDHash : 4D713DF0
/// @DnDArgument : "code" "//accuracy variable$(13_10)accuracyOffset = random_range(-20,20);$(13_10)$(13_10)//despawn variables$(13_10)despawnTimer = random_range(120 ,180);$(13_10)$(13_10)// sets arrow to point and move towards where the mouse was  plus offset$(13_10)image_angle = point_direction(x,y,mouse_x,mouse_y) + accuracyOffset$(13_10)direction = point_direction(x,y,mouse_x,mouse_y) + accuracyOffset$(13_10)speed = 8;"
//accuracy variable
accuracyOffset = random_range(-20,20);

//despawn variables
despawnTimer = random_range(120 ,180);

// sets arrow to point and move towards where the mouse was  plus offset
image_angle = point_direction(x,y,mouse_x,mouse_y) + accuracyOffset
direction = point_direction(x,y,mouse_x,mouse_y) + accuracyOffset
speed = 8;