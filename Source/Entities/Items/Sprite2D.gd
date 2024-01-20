extends Sprite2D


func _process(delta):
	# Mouse button requirement should be removed in final version
	if Input.is_mouse_button_pressed(MOUSE_BUTTON_RIGHT) and _is_mouse_over():
		get_tree().root.get_child(0).AddCurrency(1)
		self.free();

func _is_mouse_over() -> bool:
	var mouse_position = get_global_mouse_position()
	return get_rect().has_point(to_local(mouse_position))