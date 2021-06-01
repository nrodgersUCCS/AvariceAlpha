var check = false;
check = instance_exists(obj_dummy);
if(check)
{
	
	direction = point_direction(x, y, mouse_x, mouse_y);
	speed += 1;
}

else
{
	instance_destroy();
}