//detect player in room
if distance_to_object(obj_dummy)<=300{
	//look at grid for player
	if(mp_grid_path(global.grid, path, x, y, obj_dummy.x - 16, obj_dummy.y - 16, 1)){
		//point and go towards player
		direction=point_direction(x,y,obj_dummy.x,obj_dummy.y);
		path_start(path, 1, path_action_stop, false);
	}
}