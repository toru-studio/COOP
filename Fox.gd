extends Node2D

var speed : float = .2
var path_end : float
var path_follow : PathFollow2D

# Called when the node enters the scene tree for the first time.
func _ready():
	path_follow = $Path2D/PathFollow2D
	path_follow.progress_ratio = 0.0
	path_end = .985
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	var direction = -1 if path_follow.progress_ratio > 0.99 else 1
	path_follow.progress_ratio += direction * speed * delta
	
	if path_follow.progress_ratio > path_end:
		path_follow.progress_ratio = 0.0
	pass
