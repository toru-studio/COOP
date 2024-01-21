extends Sprite2D

var is_mouse_over = false

func _process(delta):
	if Input.is_mouse_button_pressed(MOUSE_BUTTON_LEFT) and _is_mouse_over():
		get_parent().ShineOn(100 * delta)

func _is_mouse_over() -> bool:
	var mouse_position = get_global_mouse_position()
	return get_rect().has_point(to_local(mouse_position))
