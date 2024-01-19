extends Sprite2D

func _process(delta):
	if Input.is_mouse_button_pressed(MOUSE_BUTTON_LEFT):
		get_parent().ShineOn(100*delta)
