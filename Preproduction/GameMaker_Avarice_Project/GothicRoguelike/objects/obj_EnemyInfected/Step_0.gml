//when enemy spots player start following
if distance_to_object(obj_dummy)<=100
{
	if(mp_grid_path(global.grid, path, x, y, obj_dummy.x - 16, obj_dummy.y - 16, 1))
	{
		direction = point_direction(x,y,obj_dummy.x,obj_dummy.y);
		path_start(path, 1.4, path_action_stop, false);
	}
}
