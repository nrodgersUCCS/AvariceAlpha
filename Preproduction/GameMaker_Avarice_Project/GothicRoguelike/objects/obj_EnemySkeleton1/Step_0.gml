//checks to see if player is near
if distance_to_object(obj_dummy)<=1
{
aggro = true
}
//enemy moves towards player if they can see them 
if(aggro = true)
{
	//look at grid for player
	if(instance_exists(obj_dummy))
	{
		if(mp_grid_path(global.grid, path, x, y, obj_dummy.x - 16, obj_dummy.y - 16, 1))
		{
			//point and go towards player
			//direction=point_direction(x,y,obj_dummy.x,obj_dummy.y);
			path_start(path, 1, path_action_stop, false);
		}
	}
}
//get enemy to patrol left to right while passive
else if(distance_to_object(obj_dummy) > 50)
{
	path_end();
	if(patrolingLeft = false)
	{
		direction = 0;
		speed = 1
		if(place_meeting(x + 1,y,obj_Wall))
		{
			patrolingLeft = true;	
		}
	}
	else
	{
		direction = 180;
		speed = 1;
		if(place_meeting(x - 1,y,obj_Wall))
		{
			patrolingLeft = false;	
		}
	}
}

//determines where enemy is facing
if(direction = 0)
{
facing = 1;	
}
else
{
facing = -1;	
}

//checks to see if the player is in line with the enemy without any walls in the way
//if (collision_line_list(x,y,x+ 100 * facing, y, obj_dummy, true, true, inView ,true))
if (collision_line(x,y,x+ 100 * facing, y, obj_dummy, true, true))
{
	//if(ds_list_find_index(inView, 1) = obj_dummy)
	//{
		aggro = true
	//}
}