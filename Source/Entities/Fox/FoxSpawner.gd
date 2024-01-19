extends Path2D

var fox
var alapsed : float

# Called when the node enters the scene tree for the first time.
func _ready():
	fox = preload("res://Source/Entities/Fox/fox.tscn")
	alapsed = 0.0
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	alapsed += delta
	if alapsed > 3:
		add_child(fox.instantiate())
		alapsed = 0.0
	pass
