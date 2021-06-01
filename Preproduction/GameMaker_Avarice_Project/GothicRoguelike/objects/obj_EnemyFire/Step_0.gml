//when enemy spots player start following
if distance_to_object(obj_dummy)<=100
{
	aggro = true;	
}
if(aggro = true)
{
	if(instance_exists(obj_dummy))
	{
		if(attack == false){
			alarm[0] = room_speed * 2;
			attack = true;
		}
		if(mp_grid_path(global.grid, path, x, y, obj_dummy.x - 16, obj_dummy.y - 16, 1))
		{
			direction = point_direction(x,y,obj_dummy.x,obj_dummy.y);
			path_start(path, 1.5, path_action_stop, false);
		}
	}
}
else
{
	if(movingLeft = false)
	{
		direction = 0
		speed = 1
		if(place_meeting(x + speed ,y ,obj_Wall ))
		{
			movingLeft = true;
			facing = 1;
		}
	}
	else
	{
		direction = 180
		speed = 1
		if(place_meeting(x-speed ,y ,obj_Wall))
		{
			movingLeft = false
			facing = -1;
		}
	}
}

//enemy line of sight
if(collision_line(x,y,x + 200 * facing,y,obj_dummy,true,true))
{
	aggro = true;
}